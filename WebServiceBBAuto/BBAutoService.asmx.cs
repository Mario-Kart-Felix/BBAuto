using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using DataLayer;

namespace WebServiceBBAuto
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://localhost")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BBAutoService : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet GetTable(string tableName)
        {
            IDataBase _db = new SQL();

            DataTable dt = _db.GetRecords("exec " + tableName + "_Select");

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
    }
}