using System;
using BBAuto.Logic.Entities;

namespace BBAuto.Logic.Abstract
{
  public interface INotification
  {
    DateTime DateEnd { get; }
    bool IsNotificationSent { get; }
    void SendNotification();
    Driver Driver { get; set; }
  }
}
