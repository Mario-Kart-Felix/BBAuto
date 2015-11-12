using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public interface INotificationList
    {
        List<INotification> ToList();
    }
}
