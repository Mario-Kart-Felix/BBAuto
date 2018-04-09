using System.Collections.Generic;

namespace BBAuto.Logic.Abstract
{
  public interface INotificationList
  {
    List<INotification> ToList();
  }
}
