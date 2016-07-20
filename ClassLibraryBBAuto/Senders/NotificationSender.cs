using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class NotificationSender
    {
        private INotificationList _list;

        public NotificationSender(INotificationList list)
        {
            _list = list;
        }

        public void SendNotification()
        {
            List<INotification> list = GetList(DateTime.Today.AddMonths(1));

            foreach (INotification item in list)
            {
                item.SendNotification();
            }
        }

        private List<INotification> GetList(DateTime date)
        {
            var list = _list.ToList().Where((item) => (item.DateEnd == date && !item.IsNotificationSent && !IsExistNewItem(item)));

            return list.ToList();
        }

        private bool IsExistNewItem(INotification notification)
        {
            return _list.ToList().Exists(item => item.DriverID == notification.DriverID && item.DateEnd > notification.DateEnd);
        }

        public void SendNotificationOverdue()
        {
            if ((DateTime.Today.Day % 7) != 0)
                return;

            List<INotification> list = GetListOverdue(DateTime.Today);

            foreach (INotification item in list)
            {
                item.SendNotification();
            }
        }

        private List<INotification> GetListOverdue(DateTime date)
        {
            List<INotification> list = _list.ToList();

            var list1 = (list.Where(item => (item.DateEnd < date))).ToList();
            var list2 = (list.Where(item => (item.DateEnd >= date))).ToList();

            DriverList driverList = DriverList.getInstance();

            return (from item1 in list1
                    join item2 in list2 on item1.DriverID equals item2.DriverID into table1
                    from item3 in table1.DefaultIfEmpty()
                    where item3 == null && (!driverList.getItem(item1.DriverID).NotificationStop)
                    select item1).ToList();
        }

        public void SendNotificationNotExist()
        {
            if ((DateTime.Today.Day % 7) != 0)
                return;

            List<INotification> list = GetListNotExist();
            List<INotification> list2 = GetListWithoutFile();

            list.AddRange(list2);

            foreach (INotification item in list)
            {
                item.SendNotification();
            }
        }

        private List<INotification> GetListNotExist()
        {
            DriverList driverList = DriverList.getInstance();
            List<Driver> listDriver = driverList.ToList().Where(item => (!item.Fired && !item.Decret && !item.NotificationStop && item.IsDriver)).ToList();

            List<INotification> list = _list.ToList();

            List<Driver> listNotExist = (from itemDriver in listDriver
                                    join itemMC in list on itemDriver.ID equals itemMC.DriverID into table1
                                    from itemRes in table1.DefaultIfEmpty()
                                    where itemRes == null
                                    select itemDriver).ToList();

            List<INotification> listNotification = new List<INotification>();

            foreach (Driver item in listNotExist)
            {
                if (list.First() is MedicalCert)
                    listNotification.Add(new MedicalCert(item.ID));
                else if (list.First() is DriverLicense)
                    listNotification.Add(new DriverLicense(item.ID));
            }

            return listNotification;
        }

        private List<INotification> GetListWithoutFile()
        {
            DriverList driverList = DriverList.getInstance();
            List<Driver> listDriver = driverList.ToList().Where(item => (!item.Fired && !item.Decret && !item.NotificationStop && item.IsDriver)).ToList();

            List<INotification> list = _list.ToList();

            List<INotification> listExist = (from itemMC in list
                                         join itemDriver in listDriver on itemMC.DriverID equals itemDriver.ID into table1
                                         from itemRes in table1.DefaultIfEmpty()
                                         where itemRes != null
                                         select itemMC).ToList();
            
            List<INotification> listNotification = new List<INotification>();

            foreach (IActual item in listExist)
            {
                if (item.IsDateActual() && !item.IsHaveFile())
                {
                    if (list.First() is MedicalCert)
                        listNotification.Add(item as MedicalCert);
                    else if (list.First() is DriverLicense)
                        listNotification.Add(item as DriverLicense);
                }
            }
            
            return listNotification;
        }

        public void ClearStopIfNeed()
        {
            DriverList driverList = DriverList.getInstance();
            List<Driver> listDriver = driverList.ToList().Where(item => (item.NotificationStop && item.DateStopNotification == DateTime.Today)).ToList();

            foreach (Driver driver in listDriver)
                driver.DateStopNotification = new DateTime(1, 1, 1);
        }
    }
}