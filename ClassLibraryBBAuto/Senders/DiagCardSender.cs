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
    public class DiagCardSender
    {
        private const int SEND_DAY = 5;
        private const int MAILS_COUNT = 100;

        public void SendNotification()
        {
            if (DateTime.Today.Day != SEND_DAY)
                return;

            DiagCardList diagCardList = DiagCardList.getInstance();
            List<DiagCard> list = diagCardList.GetDiagCardEnds().ToList();

            int begin = 0;
            int end = 0;

            if (!ListEmpty(list))
            {
                STSList stsList = STSList.getInstance();
                
                while (end < list.Count)
                {
                    begin = end;
                    end += ((end + MAILS_COUNT) < list.Count) ? MAILS_COUNT : (list.Count - end);

                    List<DiagCard> listCut = new List<DiagCard>();

                    for (int i = begin; i < end; i++)
                        listCut.Add(list[i]);

                    IEnumerable<Car> carList = diagCardList.GetCarListFromDiagCardList(listCut);
                    List<string> files = new List<string>();

                    foreach (Car car in carList)
                    {
                        STS sts = stsList.getItem(car);
                        if (sts.File != string.Empty)
                            files.Add(sts.File);
                    }

                    string mailText = CreateMail(listCut);

                    EMail email = new EMail();

                    Driver employeeAutoDept = GetDriverForSending();
                    email.SendNotification(employeeAutoDept, mailText, true, files);
                }
            }
        }

        private bool ListEmpty(IEnumerable<DiagCard> list)
        {
            return list.Count() == 0;
        }

        private Driver GetDriverForSending()
        {
            DriverList driverList = DriverList.getInstance();

            return driverList.GetDriverListByRole(RolesList.Editor).First();
        }

        private string CreateMail(IEnumerable<DiagCard> diagCards)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DiagCard diagCard in diagCards)
            {
                sb.AppendLine(diagCard.ToMail());
            }

            MailTextList mailTextList = MailTextList.getInstance();
            MailText mailText = mailTextList.getItemByType(MailTextType.DiagCard);

            return mailText == null ? "Шаблон текста письма не найден" : mailText.Text.Replace("List", sb.ToString());
        }
    }
}
