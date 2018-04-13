using System;
using BBAuto.Logic.Static;

namespace BBAuto.App.Events
{
  public class StatusEventArgs : EventArgs
  {
    public StatusEventArgs(Status status)
    {
      Status = status;
    }

    public Status Status { get; }
  }
}
