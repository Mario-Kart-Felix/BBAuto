using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataLayer;

namespace BBAuto.Domain
{
    public class Account : MainDictionary
    {
        private const int NOT_SAVE_ID = 0;

        private string _number;
        private int _agreed;
        private int _idPolicyType;
        private int _idOwner;
        private int _businessTrip;

        public string Number { get; set; }
        public bool Agreed { get { return Convert.ToBoolean(_agreed); } }
        private PolicyType policyType { get { return (PolicyType)_idPolicyType; } }

        internal string Owner
        {
            get
            {
                Owners owners = Owners.getInstance();
                return owners.getItem(_idOwner);
            }
        }

        public double Sum
        {
            get
            {
                PolicyList policyList = PolicyList.getInstance();
                return policyList.GetPaymentSum(this);
            }
        }

        public int PaymentNumber { get { return PaymentIndex + 1; } }
        public int PaymentIndex { get; set; }

        public string IDOwner
        {
            get { return _idOwner.ToString(); }
            set { int.TryParse(value, out _idOwner); }
        }

        public string IDPolicyType
        {
            get { return _idPolicyType.ToString(); }
            set { int.TryParse(value, out _idPolicyType); }
        }

        public bool BusinessTrip
        {
            get { return Convert.ToBoolean(_businessTrip); }
            set { _businessTrip = Convert.ToInt32(value); }
        }

        public string File { get; set; }

        public int Position { get { return _id; } }

        public Account()
        {
            _id = NOT_SAVE_ID;
            _idPolicyType = 1;
        }

        internal Account(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            _number = row.ItemArray[1].ToString();
            int.TryParse(row.ItemArray[2].ToString(), out _agreed);
            int.TryParse(row.ItemArray[3].ToString(), out _idPolicyType);
            int.TryParse(row.ItemArray[4].ToString(), out _idOwner);
            
            int paymentIndex;
            int.TryParse(row.ItemArray[5].ToString(), out paymentIndex);
            PaymentIndex = paymentIndex;

            int.TryParse(row.ItemArray[6].ToString(), out _businessTrip);
            File = row.ItemArray[7].ToString();
            _fileBegin = File;
        }

        internal override object[] getRow()
        {
            int idCar = GetIDCar();

            string btnName = (CanAgree()) ? "Согласовать" : string.Empty;
            string btnFile = (string.IsNullOrEmpty(File)) ? string.Empty : "Просмотр";
            
            return new object[8] { _id, idCar, _number, policyType, Owner, Sum, btnName, btnFile };
        }

        private int GetIDCar()
        {
            PolicyList policyList = PolicyList.getInstance();
            DataTable dt = policyList.ToDataTable(this);

            int idCar = 1;

            if (dt.Rows.Count > 0)
                int.TryParse(dt.Rows[0].ItemArray[1].ToString(), out idCar);

            return idCar;
        }

        internal Driver GetDriver()
        {
            int idCar = GetIDCar();

            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);

            DriverCarList driverCarList = DriverCarList.getInstance();
            return driverCarList.GetDriver(car);
        }

        public bool CanAgree()
        {
            PolicyList policyList = PolicyList.getInstance();
            DataTable dt = policyList.ToDataTable(this);

            return (_agreed == 0) && (dt.Rows.Count > 0);
        }

        public void Agree()
        {
            if (_agreed == 0)
            {
                eMail mail = new eMail();
                mail.sendMailAccount(this);
                _agreed = 1;
                ExecQuery();
            }
        }

        public void BindWithPolicy(int idPolicy, int payment)
        {
            if (IsNotSaved())
                Save();

            PolicyList policyList = PolicyList.getInstance();
            Policy policy = policyList.getItem(idPolicy);

            if (IsPolicyKaskoAndPayment2())
                policy.SetAccountID(_id, payment);
            else
                policy.SetAccountID(_id, payment);
        }

        private bool IsNotSaved()
        {
            return _id == NOT_SAVE_ID;
        }

        public override void Save()
        {
            if (IsNotSaved())
            {
                AccountList accountList = AccountList.getInstance();

                if (accountList.Exists(_number))
                    throw new Exception("Счёт с таким номером уже существует");

                ExecQuery();                
                accountList.Add(this);
            }

            DeleteFile(File);

            File = WorkWithFiles.fileCopy(File, "Accounts", _id.ToString());

            ExecQuery();
        }

        private void ExecQuery()
        {
            int.TryParse(_provider.Insert("Account", _id, _number, _agreed, _idPolicyType, _idOwner, PaymentIndex, _businessTrip, File), out _id);
        }

        public bool IsPolicyKaskoAndPayment2()
        {
            return policyType == PolicyType.КАСКО && PaymentNumber == 2;
        }
    }
}
