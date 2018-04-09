using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;

namespace BBAuto
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
