using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DataLayer
{
    public interface IDataBase
    {
        DataTable GetRecords(String SQL, params Object[] Params);
        string GetRecordsOne(String SQL, params Object[] Params);
    }
}
