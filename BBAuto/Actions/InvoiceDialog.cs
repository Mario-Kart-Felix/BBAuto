using ClassLibraryBBAuto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBAuto
{
    internal static class InvoiceDialog
    {
        internal static bool CreateNewInvoiceAndOpen(int idCar)
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);

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
