using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Senders
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

                eMail email = new eMail();

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
                 + "В программе BBAuto появились новые нарушения ПДД на согласование. Количество нарушений: {0}", violations.Count());
        }
    }
}
