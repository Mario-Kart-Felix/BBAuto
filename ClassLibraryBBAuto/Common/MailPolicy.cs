using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Common
{
  public static class MailPolicy
  {
    public static string Send(Car car, PolicyType type)
    {
      EMail mail = new EMail();

      mail.sendMailPolicy(car, type);

      DriverCarList driverCarList = DriverCarList.getInstance();
      Driver driver = driverCarList.GetDriver(car);

      return string.Concat("Полис ", type.ToString(), " отправлен на адрес ", driver.email);
    }
  }
}
