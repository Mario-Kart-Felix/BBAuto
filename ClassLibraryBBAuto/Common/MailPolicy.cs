using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public static class MailPolicy
    {
        public static string Send(Car car, PolicyType type)
        {
            eMail mail = new eMail();

            mail.sendMailPolicy(car, type);

            DriverCarList driverCarList = DriverCarList.getInstance();
            Driver driver = driverCarList.GetDriver(car);

            return string.Concat("Полис ", type.ToString(), " отправлен на адрес ", driver.email);
        }
    }
}
