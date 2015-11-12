using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryBBAuto;

namespace UnitTestProject1.Classes
{
    [TestClass]
    public class DictionaryTest
    {
        public DictionaryTest()
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();
        }

        private void IsEquals(MyDictionary dictionary, int id, string expextedValue)
        {
            string value = dictionary.getItem(id);
            Assert.AreEqual(expextedValue, value);
        }

        private void IsEquals(MyDictionary dictionary, string value, int expextedID)
        {
            int id = dictionary.getItem(value);
            Assert.AreEqual(expextedID, id);
        }

        [TestMethod]
        public void ColorGetItem()
        {
            const int ID = 2;
            const string VALUE = "Черный";

            Colors dictionary = Colors.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void CompGetItem()
        {
            const int ID = 2;
            const string VALUE = "Ингосстрах";

            Comps dictionary = Comps.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void CulpritsGetItem()
        {
            const int ID = 2;
            const string VALUE = "Остекление";

            Culprits dictionary = Culprits.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void DeptsGetItem()
        {
            const int ID = 2;
            const string VALUE = "Представительство г.Краснодар (отдел НС)";

            Depts dictionary = Depts.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void EmployeesNamesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Диспечер-нарядчик";

            EmployeesNames dictionary = EmployeesNames.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void EngineTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Дизель";

            EngineTypes dictionary = EngineTypes.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void MarksTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Lada";

            Marks dictionary = Marks.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void OwnersTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "ООО \"Гематек\"";

            Owners dictionary = Owners.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void PositionsTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Региональный директор";

            Positions dictionary = Positions.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void RepairTypesTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Кузовной";

            RepairTypes dictionary = RepairTypes.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void RolesTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Сотрудник транспортного отдела";

            Roles dictionary = Roles.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void ServiceStantionsTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Тойта Центр Автово";

            ServiceStantions dictionary = ServiceStantions.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void StatusAfterDTPsTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "А/м на ходу";

            StatusAfterDTPs dictionary = StatusAfterDTPs.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }

        [TestMethod]
        public void ViolationTypesTypesGetItem()
        {
            const int ID = 2;
            const string VALUE = "Нарушение правил остановки тс";

            ViolationTypes dictionary = ViolationTypes.getInstance();

            IsEquals(dictionary, ID, VALUE);
            IsEquals(dictionary, VALUE, ID);
        }
    }
}
