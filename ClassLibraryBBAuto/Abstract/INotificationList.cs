using System.Collections.Generic;

namespace BBAuto.Domain.Abstract
{
  public interface INotificationList
  {
    List<INotification> ToList();
  }
}
