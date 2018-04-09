using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto
{
  public static class Filters
  {
    public static List<string> GetDataSource(DataGridView dgv, string columnName)
    {
      if (dgv == null)
        return null;

      List<string> list = new List<string>();

      foreach (DataGridViewRow row in dgv.Rows)
      {
        string value = row.Cells[columnName].Value.ToString();

        string endColumnHeader = columnName.Substring(columnName.Length - 3, 3);

        if ((endColumnHeader == "руб") || (value.Trim() == string.Empty))
          continue;

        if (IsDate(value))
        {
          MyDateTime myDate = new MyDateTime(value);
          value = myDate.ToString();
        }

        if ((row.Visible) && (!IsInList(list, value)))
          list.Add(value);
      }

      DataTable dt = new DataTable();
      dt.Columns.Add("Название");

      list.Sort();

      if (list.Count > 0)
        list.Insert(0, "(все)");

      return list;
    }

    private static bool IsInList(List<string> list, string value)
    {
      return list.Where(item => item == value).Count() > 0;
    }

    private static bool IsDate(string value)
    {
      DateTime val;
      return DateTime.TryParse(value, out val);
    }
  }
}
