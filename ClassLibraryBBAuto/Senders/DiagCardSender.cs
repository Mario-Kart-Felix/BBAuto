using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class DiagCardSender
    {
        private const int SEND_DAY = 5;

        public void SendNotification()
        {
            if (DateTime.Today.Day != SEND_DAY)
                return;

            DiagCardList diagCardList = DiagCardList.getInstance();
            List<DiagCard> list = diagCardList.GetDiagCardEnds();

            if (!ListEmpty(list))
            {
                List<Car> carList = diagCardList.GetCarListFromDiagCardList(list);
                STSList stsList = STSList.getInstance();

                List<string> files = new List<string>();

                foreach(Car car in carList)
                {
                    STS sts = stsList.getItem(car);
                    if (sts.File != string.Empty)
                        files.Add(sts.File);
                }

                string mailText = CreateMail(list);

                eMail email = new eMail();

                Driver employeeAutoDept = GetDriverForSending();
                email.SendNotification(employeeAutoDept, mailText, files);
            }
        }

        private bool ListEmpty(List<DiagCard> list)
        {
            return list.Count() == 0;
        }

        private Driver GetDriverForSending()
        {
            DriverList driverList = DriverList.getInstance();

            return driverList.GetDriverListByRole(RolesList.Editor).First();
        }

        private string CreateMail(List<DiagCard> diagCards)
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
