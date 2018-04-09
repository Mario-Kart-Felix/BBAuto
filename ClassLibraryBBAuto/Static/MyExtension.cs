using System;

namespace BBAuto.Domain.Static
{
  public static class MyExtension
  {
    public static bool IsEqualsByYearAndMonth(this DateTime date, DateTime value)
    {
      return date.Year == value.Year && date.Month == value.Month;
    }
  }
}
