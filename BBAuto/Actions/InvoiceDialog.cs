using BBAuto.App.FormsForCar.AddEdit;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.App.Actions
{
  internal static class InvoiceDialog
  {
    internal static bool CreateNewInvoiceAndOpen(Car car)
    {
      if (car == null)
        return false;

      Invoice invoice = car.createInvoice();

      return Open(invoice);
    }

    internal static bool Open(Invoice invoice)
    {
      Invoice_AddEdit invoiceAE = new Invoice_AddEdit(invoice);

      return (invoiceAE.ShowDialog() == System.Windows.Forms.DialogResult.OK);
    }
  }
}
