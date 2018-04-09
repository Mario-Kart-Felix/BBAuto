using System.Collections.Generic;
using System.Linq;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders
{
  public class AccountSender
  {
    public void SendNotification()
    {
      AccountList accountList = AccountList.GetInstance();
      IEnumerable<Account> list = accountList.GetAccountForAgree();

      if (list.Any())
      {
        Driver driversTo = GetDriverForSending(RolesList.Boss);

        string mailText = CreateMailToBoss(list);

        var email = new EMail();

        email.SendNotification(driversTo, mailText);
      }
    }

    private Driver GetDriverForSending(RolesList role = RolesList.Editor)
    {
      return DriverList.getInstance().GetDriverListByRole(role).First();
    }

    private string CreateMailToBoss(IEnumerable<Account> list)
    {
      return string.Format("Добрый день!\n\n"
                           + "В программе BBAuto появились новые счета по страховым полисам для согласования. Количество счетов: {0}",
        list.Count());
    }
  }
}
