using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryBBAuto;

namespace UnitTestProject1
{
    [TestClass]
    public class ProviderTest
    {
        public ProviderTest()
        {
            DataBase.InitDataBase();
        }

        [TestMethod]
        public void SelectMarkSQL()
        {
            Provider.InitSQLProvider();

            GetFirstMarkID();
        }

        [TestMethod]
        public void SelectMarkWebService()
        {
            Provider.InitWebServiceProvider();

            GetFirstMarkID();
        }
                
        public void GetFirstMarkID()
        {
            IProvider provider = Provider.GetProvider();
            string value = provider.SelectOne("Mark");

            Assert.AreEqual("6", value);
        }
    }
}
