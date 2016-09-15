using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain.Senders
{
    public class PolicyListSender
    {
        private const int SEND_DAY = 5;

        public void SendNotification()
        {
            SendToBoss();

            if (DateTime.Today.Day != SEND_DAY)
                return;

            PolicyList policyList = PolicyList.getInstance();
            IEnumerable<Policy> list = policyList.GetPolicyEnds();

            if (!ListEmpty(list))
            {
                Driver driversTo = GetDriverForSending();
                
                string mailText = CreateMail(list);

                EMail email = new EMail();

                email.SendNotification(driversTo, mailText);
            }
        }

        private void SendToBoss()
        {
            PolicyList policyList = PolicyList.getInstance();
            IEnumerable<Policy> list = policyList.GetPolicyAccount();

            if (!ListEmpty(list))
            {
                Driver driversTo = GetDriverForSending(RolesList.Boss);

                string mailText = CreateMailToBoss(list);

                var email = new EMail();

                email.SendNotification(driversTo, mailText);
            }
        }

        private bool ListEmpty(IEnumerable<Policy> list)
        {
            return list.Count() == 0;
        }

        private Driver GetDriverForSending(RolesList role = RolesList.Editor)
        {
            return DriverList.getInstance().GetDriverListByRole(role).First();
        }

        private string CreateMail(IEnumerable<Policy> policies)
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

        private string CreateMailToBoss(IEnumerable<Policy> policies)
        {
            return string.Format("Добрый день!\n\n"
                 + "В программе BBAuto появились новые страховые полисы для согласования. Количество полисов: {0}", policies.Count());
        }
    }
}
