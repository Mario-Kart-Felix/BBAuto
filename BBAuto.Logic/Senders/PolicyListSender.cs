using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.Senders
{
  public class PolicyListSender
  {
    private const int SEND_DAY = 5;

    public void SendNotification()
    {
      if (DateTime.Today.Day != SEND_DAY)
        return;

      PolicyList policyList = PolicyList.getInstance();
      IEnumerable<Policy> list = policyList.GetPolicyEnds();

      if (list.Any())
      {
        Driver driversTo = GetDriverForSending();

        string mailText = CreateMail(list);

        EMail email = new EMail();

        email.SendNotification(driversTo, mailText);
      }
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
  }
}
