using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public interface INotificationList
    {
        List<INotification> ToList();
    }
}
