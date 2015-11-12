using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public interface INotification
    {
        DateTime DateEnd { get; }
        bool IsNotificationSent { get; }
        void SendNotification();
        int DriverID { get; }
    }
}
