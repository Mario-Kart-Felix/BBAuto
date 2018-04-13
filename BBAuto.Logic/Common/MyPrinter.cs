using System.Linq;

namespace BBAuto.Logic.Common
{
  public class MyPrinter
  {
    public string GetDefaultPrinterName()
    {
      string[] printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToArray();

      foreach (string printer in printers)
      {
        if (new System.Drawing.Printing.PrinterSettings() {PrinterName = printer}.IsDefaultPrinter)
          return printer;
      }

      return string.Empty;
    }
  }
}
