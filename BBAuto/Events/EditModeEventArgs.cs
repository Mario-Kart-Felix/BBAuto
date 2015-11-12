using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto
{
    public class EditModeEventArgs : EventArgs
    {
        private readonly bool _enabled;

        public EditModeEventArgs(bool enabled)
        {
            _enabled = enabled;
        }

        public bool Enabled { get { return _enabled; } }
    }
}
