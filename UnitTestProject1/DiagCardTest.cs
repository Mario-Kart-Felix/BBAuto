using System;
using NUnit.Framework;
using ClassLibraryBBAuto;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestFixture]
    public class DiagCardTest
    {
        public DiagCardTest()
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();
        }

        [TestCase(2015, 6, Result = 10)]
        [TestCase(2015, 7, Result = 1)]
        [TestCase(2015, 8, Result = 0)]
        [TestCase(2015, 9, Result = 12)]
        public int GetDiagCardEnds(int year, int month)
        {
            DateTime date = new DateTime(year, month, 1);
            DiagCardList diagCardList = DiagCardList.getInstance();
            List<DiagCard> list = diagCardList.GetDiagCardList(date);
            return list.Count;
        }
    }
}
