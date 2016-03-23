using System;
using ClassLibraryBBAuto;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    public class PassportTest
    {
        public PassportTest()
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();
        }

        [Test]
        public void GetPassportNumber()
        {
            PassportList passportList = PassportList.getInstance();
            Passport passport;

            Random random = new Random();
            do
            {
                int index = random.Next(1, 2924);
                passport = passportList.getPassport(index);

            } while (passport == null);

            var list = passport.Number.Split(' ');
            Assert.AreEqual(2, list.Length);
        }
    }
}
