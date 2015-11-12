using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryBBAuto;

namespace UnitTestProject1.Classes
{
    [TestClass]
    public class ListTest
    {
        public ListTest()
        {
            DataBase.InitMockDataBase();
            Provider.InitMockProvider();
        }

        [TestMethod]
        public void GetItem()
        {
            const int ID = 1;

            AccountList list = AccountList.getInstance();

            Account account = list.getItem(ID);

            Assert.AreEqual("1", account.IDPolicyType);

            account = new Account();
            account.IDPolicyType = "2";
            account.Save();

            list.Add(account);

            account = list.getItem(ID);

            Assert.AreEqual("2", account.IDPolicyType);
        }
    }
}
