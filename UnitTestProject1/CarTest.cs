using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBAuto.Domain;

namespace UnitTestProject1
{
    [TestClass]
    public class CarTest
    {
        const int ID_CAR = 1;

        public CarTest()
        {
            DataBase.InitMockDataBase();
            Provider.InitSQLProvider();
        }
                
        [TestMethod]
        public void CarSaleTest()
        {
            CarSaleList carSaleList = CarSaleList.getInstance();
            CarSale carSale = carSaleList.getItem(ID_CAR);

            Assert.IsNull(carSale);

            CarBuy();

            CarList carList = CarList.getInstance();
            Car car = carList.getItem(ID_CAR);

            Assert.IsFalse(car.info.IsSale);

            carSale = new CarSale(ID_CAR);
            carSale.Save();

            car = carList.getItem(ID_CAR);

            Assert.IsTrue(car.info.IsSale);

            carSaleList.Delete(ID_CAR);

            carSale = carSaleList.getItem(ID_CAR);

            Assert.IsNull(carSale);
        }

        [TestMethod]
        public void CarBuy()
        {
            CarList carList = CarList.getInstance();
            carList.Delete(ID_CAR);

            Assert.IsNull(carList.getItem(ID_CAR));

            Car car = new Car();
            car.Save();

            Assert.IsNotNull(carList.getItem(ID_CAR));
        }
    }
}
