using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public static class MyExtension
    {
        public static bool IsEqualsByYearAndMonth(this DateTime date, DateTime value)
        {
            return date.Year == value.Year && date.Month == value.Month;
        }
    }
}
