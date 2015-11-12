using System;
using ClassLibraryBBAuto;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    public class PolicyTest
    {
        public PolicyTest()
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();
        }

        [Test]
        public void GetPolicyEnds()
        {
            PolicyList policyList = PolicyList.getInstance();
            List<Policy> list = policyList.GetPolicyList(new DateTime(2015, 6, 1));
            Assert.AreEqual(98, list.Count);

            List<Car> listCar = policyList.GetCarListByPolicyList(list);
            Assert.AreEqual(66, listCar.Count);
        }
    }
}