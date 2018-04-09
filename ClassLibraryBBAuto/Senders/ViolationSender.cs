using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders
{
  public class ViolationSender
  {
    public void SendNotification()
    {
      IEnumerable<Violation> list = ViolationList.getInstance().GetViolationForAccount();

      if (list.Count() > 0)
      {
        Driver driversTo = GetDriverForSending();

        string mailText = CreateMail(list);

        EMail email = new EMail();

        email.SendNotification(driversTo, mailText, false);
      }
    }

    private Driver GetDriverForSending()
    {
      DriverList driverList = DriverList.getInstance();

      return driverList.GetDriverListByRole(RolesList.Boss).First();
    }

    private string CreateMail(IEnumerable<Violation> violations)
    {
      return string.Format("Добрый день!\n\n"
                           + "В программе BBAuto появились новые нарушения ПДД на согласование. Количество нарушений: {0}",
        violations.Count());
    }
  }
}
