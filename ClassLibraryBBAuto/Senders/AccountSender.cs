using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBAuto.Domain.Senders
{
    public class AccountSender
    {
        public void SendNotification()
        {
            AccountList accountList = AccountList.getInstance();
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
                 + "В программе BBAuto появились новые счета по страховым полисам для согласования. Количество счетов: {0}", list.Count());
        }
    }
}
