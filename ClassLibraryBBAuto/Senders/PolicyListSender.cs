using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class PolicyListSender
    {
        private const int SEND_DAY = 5;

        public void SendNotification()
        {
            if (DateTime.Today.Day != SEND_DAY)
                return;

            PolicyList policyList = PolicyList.getInstance();
            List<Policy> list = policyList.GetPolicyEnds();

            if (!ListEmpty(list))
            {
                Driver employeeAutoDept = GetDriverForSending();
                
                string mailText = CreateMail(list);

                eMail email = new eMail();

                email.SendNotification(employeeAutoDept, mailText);
            }
        }

        private bool ListEmpty(List<Policy> list)
        {
            return list.Count() == 0;
        }

        private Driver GetDriverForSending()
        {
            DriverList driverList = DriverList.getInstance();

            return driverList.GetDriverListByRole(RolesList.Editor).First();
        }

        private string CreateMail(List<Policy> policies)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Policy policy in policies)
            {
                sb.AppendLine(policy.ToMail());
            }

            MailTextList mailTextList = MailTextList.getInstance();
            MailText mailText = mailTextList.getItemByType(MailTextType.Policy);

            return mailText == null ? "Шаблон текста письма не найден" : mailText.Text.Replace("List", sb.ToString());
        }
    }
}
