using System.Linq;

namespace BBAuto.App.GUI
{
  internal static class DGVSpecialColumn
  {
    private static readonly string[] notIncludeColumnsInFilter = new string[]
    {
      "Бортовой номер", "VIN", "№ ПТС", "№ СТС", "Пробег",
      "Дата последней записи о пробеге", "Комментарий", "№ накладной", "Начало", "Начало действия", "№ постановления",
      "Сумма штрафа",
      "№ ДК", "Сумма", "Согласование", "Файл", "№ дела", "Дата окончания гарантии",
      "Дата обращения в страховую компанию",
      "Сумма возмещения", "Примечание", "Обстоятельства ДТП (со слов участника)", "Повреждения", "№ убытка страховой"
    };

    private static readonly string[] applyFilterColumnsActual =
      new string[] {"Марка", "Модель", "Регион", "Год выпуска", "Собственник"};

    private static readonly string[] applyFilterColumnsPolicy = new string[] {"Регистрационный знак", "Тип полиса"};

    internal static bool CanInclude(string columnName)
    {
      return !IsInArray(columnName, notIncludeColumnsInFilter);
    }

    internal static bool CanFiltredActual(string columnName)
    {
      return IsInArray(columnName, applyFilterColumnsActual);
    }

    internal static bool CanFiltredPolicy(string columnName)
    {
      return IsInArray(columnName, applyFilterColumnsPolicy);
    }

    private static bool IsInArray(string columnName, string[] array)
    {
      return array.Where(item => item == columnName).Count() > 0;
    }
  }
}
