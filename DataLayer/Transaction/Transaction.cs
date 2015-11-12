using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataLayer
{
    public interface ITransaction
    {
        DataTable GetDataTable(string query);
        string GetString(string query);
    }
}
