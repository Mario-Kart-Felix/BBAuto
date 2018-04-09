using System;

namespace BBAuto.App.Events
{
  public class EditModeEventArgs : EventArgs
  {
    public EditModeEventArgs(bool enabled)
    {
      Enabled = enabled;
    }

    public bool Enabled { get; }
  }
}
