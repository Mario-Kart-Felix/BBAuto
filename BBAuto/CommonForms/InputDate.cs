using ClassLibraryBBAuto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto
{
    public partial class InputDate : Form
    {
        private MainDGV _dgvMain;
        private Actions _action;

        public InputDate(MainDGV dgvMain, Actions action)
        {
            InitializeComponent();

            _dgvMain = dgvMain;
            _action = action;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int idCar = 0;

            MainStatus _mainStatus = MainStatus.getInstance();
            Status status = _mainStatus.Get();

            foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
            {
                idCar = _dgvMain.GetCarID(cell.RowIndex);

                DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

                CreateDocument excelWayBill;
                if (status == Status.Invoice)
                    excelWayBill = CreateWayBill(idCar, date, _dgvMain.GetID(cell.RowIndex));
                else
                    excelWayBill = CreateWayBill(idCar, date);

                if (_action == Actions.Print)
                    excelWayBill.Print();
                else
                    excelWayBill.Show();
            }

            if (_action == Actions.Print)
            {
                MyPrinter printer = new MyPrinter();
                MessageBox.Show("Документы отправлены на печать на принтер " + printer.GetDefaultPrinterName(), "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private CreateDocument CreateWayBill(int idCar, DateTime date, int idInvoice = 0)
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);

            CreateDocument waybill = new CreateDocument(car);

            Driver driver = null;
            if (idInvoice != 0)
            {
                InvoiceList invoiceList = InvoiceList.getInstance();
                Invoice invoice = invoiceList.getItem(idInvoice);
                DriverList driverList = DriverList.getInstance();
                driver = driverList.getItem(Convert.ToInt32(invoice.DriverToID));
            }

            waybill.createWaybill(date, driver);

            return waybill;
        }
    }
}
