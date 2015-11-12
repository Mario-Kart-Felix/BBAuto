using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class PolicyList : MainList
    {
        private List<Policy> list;
        private static PolicyList uniqueInstance;

        private PolicyList()
        {
            list = new List<Policy>();

            loadFromSql();
        }

        public static PolicyList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PolicyList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Policy");

            clearList();
                        
            foreach (DataRow row in dt.Rows)
            {
                Policy policy = new Policy(row);

                Add(policy);
            }
        }

        public void Add(Policy policy)
        {
            if (list.Exists(item => item == policy))
                return;

            list.Add(policy);
        }

        private void clearList()
        {
            if (list.Count > 0)
                list.Clear();
        }

        public Policy getItem(int idPolicy)
        {
            var policyList = list.Where(item => item.IsEqualsID(idPolicy));

            return (policyList.Count() > 0) ? policyList.First() : null;
        }

        public Policy getItem(Car car, PolicyType policyType)
        {
            var policyList = from policy in list
                         where policy.isEqualCarID(car) && policy.Type == policyType
                         orderby policy.DateEnd descending
                         select policy;

            return (policyList.Count() > 0) ? policyList.First() : car.CreatePolicy();
        }

        internal DataTable ToDataTable()
        {
            var policies = list.Where(item => !item.IsCarSaleWithDate).OrderByDescending(item => item.DateEnd);

            return createTable(policies.ToList());
        }

        public DataTable ToDataTable(Car car)
        {
            var policies = from policy in list
                           where policy.isEqualCarID(car)
                           orderby policy.DateEnd descending
                           select policy;

            return createTable(policies.ToList());
        }

        public DataTable ToDataTable(Account account)
        {
            List<Policy> policies = GetPolicyByAccount(account);

            return createTable(policies);
        }

        public DataTable ToDataTable(PolicyType policyType, string idOwner, int paymentNumber)
        {
            List<Policy> policies = new List<Policy>();

            policies = (from policy in list
                        where !policy.IsCarSale && policy.Type == policyType
                            && policy.IdOwner == idOwner && !policy.IsHaveAccountID(paymentNumber) && policy.IsActual()
                        orderby policy.DateEnd descending
                        select policy).ToList();

            return createTable(policies.ToList());
        }

        public double GetPaymentSum(Account account)
        {
            List<Policy> policies = GetPolicyByAccount(account);

            double sum = 0;
            foreach (Policy policy in policies)
            {
                sum += GetSum(policy, account);
            }

            return sum;
        }

        private List<Policy> GetPolicyByAccount(Account account)
        {
            var policies = from policy in list
                           where ((policy.EqualsAccountID(account))
                                || (account.IsPolicyKaskoAndPayment2() && (policy.EqualsAccountID2(account))))
                           orderby policy.DateEnd descending
                           select policy;

            return policies.ToList();
        }

        private double GetSum(Policy policy, Account account)
        {
            return (account.IsPolicyKaskoAndPayment2()) ? policy.Pay2ToDouble : policy.PayToDouble;
        }

        private DataTable createTable(List<Policy> policies)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("Тип полиса");
            dt.Columns.Add("Страхователь");
            dt.Columns.Add("Страховщик");
            dt.Columns.Add("Номер полиса");
            dt.Columns.Add("Pay", Type.GetType("System.Double"));
            dt.Columns.Add("Начало действия", Type.GetType("System.DateTime"));
            dt.Columns.Add("Окончание действия", Type.GetType("System.DateTime"));
            dt.Columns.Add("LimitCost", Type.GetType("System.Double"));
            dt.Columns.Add("Pay2", Type.GetType("System.Double"));

            policies.ForEach(item => dt.Rows.Add(item.getRow()));
            
            return dt;
        }

        public void Delete(int idPolicy)
        {
            Policy police = getItem(idPolicy);

            list.Remove(police);

            police.Delete();
        }

        public List<Policy> GetPolicyEnds()
        {
            List<Policy> policyList = GetPolicyList(DateTime.Today.AddMonths(1));

            return policyList.Where(item => !item.IsNotificationSent).ToList();
        }
        
        public List<Policy> GetPolicyList(DateTime date)
        {
            return list.Where(police => (police.DateEnd.Month == date.Month && police.DateEnd.Year == date.Year && !police.IsCarSale)).ToList();
        }

        public List<Car> GetCarListByPolicyList(List<Policy> list)
        {
            return list.OrderBy(policy => policy.GetCar().grz).Select(policy => policy.GetCar()).Distinct().ToList();
        }

        public Policy GetPolicyFromList(Car car, List<Policy> list, PolicyType policyType)
        {
            List<Policy> policyList = list.Where(policy => policy.isEqualCarID(car) && policy.Type == policyType).ToList();

            return policyList.Count == 0 ? new Policy(0) : policyList.First();
        }
    }
}
