using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataLayer;

namespace ClassLibraryBBAuto
{
    public sealed class Policy : MainDictionary, IActual
    {
        private string _number;
        private double _pay;
        private int _idOwner;
        private int _idComp;
        private int _idAccount;
        private int _idAccount2;
        private double _limitCost;
        private double _pay2;
        private DateTime _datePay2;        
        private int _idPolicyType;
        private int _idCar;
        private DateTime _dateBegin;
        private DateTime _dateEnd;
        private int _notifacationSent;
        private string _comment;
        private string _file;
        
        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public string Pay
        {
            get { return _pay.ToString().Replace(',', '.'); }
            set { double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _pay); }
        }

        public double PayToDouble { get { return _pay; } }

        public string IdOwner
        {
            get { return _idOwner.ToString(); }
            set { int.TryParse(value, out _idOwner); }
        }

        public string IdComp
        {
            get { return _idComp.ToString(); }
            set { int.TryParse(value, out _idComp); }
        }

        public string Number
        {
            get { return _number == string.Empty ? "нет данных" : _number; }
            set { _number = value; }
        }
        
        public bool IsCarSale
        {
            get
            {
                Car car = GetCar();
                return car.info.IsSale;
            }
        }

        public bool IsCarSaleWithDate
        {
            get
            {
                Car car = GetCar();

                if (car.info.IsSale)
                {
                    CarSaleList carSaleList = CarSaleList.getInstance();
                    return !string.IsNullOrEmpty(carSaleList.getItem(car).Date);
                }
                else
                    return false;
            }
        }
                
        public DateTime DateBegin
        {
            get { return _dateBegin == new DateTime() ? DateTime.Today : _dateBegin; }
            set { _dateBegin = value; }
        }

        public DateTime DateEnd
        {
            get { return _dateEnd == new DateTime() ? DateTime.Today : _dateEnd; }
            set { _dateEnd = value; }
        }

        public string LimitCost
        {
            get { return _limitCost.Equals(0) ? string.Empty : _limitCost.ToString().Replace(',', '.'); }
            set { double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _limitCost); }
        }

        public string Pay2
        {
            get { return _pay2.Equals(0) ? string.Empty : _pay2.ToString().Replace(',', '.'); }
            set { double.TryParse(value.Trim().Replace(" ", "").Replace('.', ','), out _pay2); }
        }

        public double Pay2ToDouble { get { return _pay2; } }

        public PolicyType Type
        {
            get { return (PolicyType)_idPolicyType; }
            set { _idPolicyType = (int)value; }
        }

        public DateTime DatePay2
        {
            get { return _datePay2; }
            set { _datePay2 = value; }
        }

        public string DatePay2ToString
        {
            get { return IsEmptyDate(_datePay2) ? string.Empty : _datePay2.ToShortDateString(); }
        }

        public string DatePay2ForSQL
        {
            get { return IsEmptyDate(_datePay2) ? string.Empty : _datePay2.Year.ToString() + "-" + _datePay2.Month.ToString() + "-" + _datePay2.Day.ToString(); }
        }

        internal bool IsNotificationSent
        {
            get { return Convert.ToBoolean(_notifacationSent); }
            private set { _notifacationSent = Convert.ToInt32(value); }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public Policy(int idCar)
        {
            _idCar = idCar;
            _id = 0;
        }

        public Policy(DataRow row)
        {
            fillFields(row);
        }
        
        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            int.TryParse(row.ItemArray[2].ToString(), out _idPolicyType);
            IdOwner = row.ItemArray[3].ToString();
            IdComp = row.ItemArray[4].ToString();
            _number = row.ItemArray[5].ToString();
            DateTime.TryParse(row.ItemArray[6].ToString(), out _dateBegin);
            DateTime.TryParse(row.ItemArray[7].ToString(), out _dateEnd);
            Pay = row.ItemArray[8].ToString();
            _file = row.ItemArray[9].ToString();
            _fileBegin = _file;

            LimitCost = row.ItemArray[10].ToString();
            Pay2 = row.ItemArray[11].ToString();
            DateTime.TryParse(row.ItemArray[12].ToString(), out _datePay2);

            int.TryParse(row.ItemArray[13].ToString(), out _idAccount);
            int.TryParse(row.ItemArray[14].ToString(), out _idAccount2);

            int.TryParse(row.ItemArray[15].ToString(), out _notifacationSent);

            _comment = row.ItemArray[16].ToString();
        }

        public override void Save()
        {
            if (_id == 0)
            {
                PolicyList policyList = PolicyList.getInstance();
                policyList.Add(this);

                execSave();
            }

            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "cars", _idCar, "Policy", _number);
            _fileBegin = _file;

            execSave();
        }

        private void execSave()
        {
            int.TryParse(_provider.Insert("Policy", _id, _idPolicyType, _idCar, IdOwner, IdComp, _number, _dateBegin, _dateEnd, Pay, LimitCost, Pay2, DatePay2ForSQL, _file, _notifacationSent, _comment), out _id);
        }
        
        public bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(_idCar);
        }

        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("Policy", _id);
        }
        
        internal bool EqualsAccountID(Account account)
        {
            return account.IsEqualsID(_idAccount) && _idAccount != 0;
        }

        internal bool EqualsAccountID2(Account account)
        {
            return account.IsEqualsID(_idAccount2);
        }

        public void SetAccountID(int idAccount, int paymentNumber)
        {
            _provider.DoOther("exec Policy_Insert_AccountID @p1, @p2, @p3", _id, idAccount, paymentNumber);
        }

        public bool IsInList(Account account)
        {
            return ((account.IsEqualsID(_idAccount)) || (account.IsEqualsID(_idAccount2)));
        }

        public void ClearAccountID(Account account)
        {
            int sqlPaymentNumber = 1;

            if (account.IsPolicyKaskoAndPayment2())
            {
                _idAccount2 = 0;
                sqlPaymentNumber = 2;
            }
            else
                _idAccount = 0;

            _provider.DoOther("exec Policy_Delete_AccountID @p1, @p2", _id, sqlPaymentNumber);
        }

        internal bool IsHaveAccountID(int paymentNumber)
        {
            return (paymentNumber == 1) ? _idAccount != 0 : _idAccount2 != 0;
        }

        public bool IsAgreed(int paymentNumber)
        {
            if (IsHaveAccountID(paymentNumber))
            {
                AccountList accountList = AccountList.getInstance();

                int idAccount = (paymentNumber == 1) ? _idAccount : _idAccount2;

                Account account = accountList.getItem(idAccount);
                return account.Agreed;
            }
            else
                return false;
        }

        internal override object[] getRow()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            Owners owners = Owners.getInstance();
            Comps comps = Comps.getInstance();

            return new object[] { _id, _idCar, car.BBNumber, car.grz, Type, owners.getItem(Convert.ToInt32(IdOwner)),
                comps.getItem(Convert.ToInt32(IdComp)), Number, _pay, DateBegin, DateEnd,
                _limitCost, _pay2};
        }

        private bool IsEmptyDate(DateTime date)
        {
            return date == new DateTime();
        }

        public Car GetCar()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            return car;
        }

        public string ToMail()
        {
            IsNotificationSent = true;
            execSave();

            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            StringBuilder sb = new StringBuilder();
            sb.Append(car.grz);
            sb.Append(" ");
            sb.Append(Type);
            sb.Append(" ");
            sb.Append(Number);
            sb.Append(" ");
            sb.Append(DateEnd.ToShortDateString());
            return sb.ToString();
        }

        public bool IsDateActual()
        {
            throw new NotImplementedException();
        }

        public bool IsHaveFile()
        {
            throw new NotImplementedException();
        }

        public bool IsActual()
        {
            return _dateEnd >= DateTime.Today;
        }
    }
}
