using ClassLibraryBBAuto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto
{
    public class StatusEventArgs : EventArgs
    {
        private readonly Status _status;

        public StatusEventArgs(Status status)
        {
            _status = status;
        }

        public Status status { get { return _status; } }
    }
}
