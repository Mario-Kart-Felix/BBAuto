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
        private WayBillType _type;

        public InputDate(MainDGV dgvMain, Actions action, WayBillType type)
        {
            InitializeComponent();

            _dgvMain = dgvMain;
            _action = action;
            _type = type;
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

                try
                {
                    excelWayBill = (status == Status.Invoice) ? CreateWayBill(idCar, date, _dgvMain.GetID(cell.RowIndex)) : CreateWayBill(idCar, date);
                }
                catch (NullReferenceException)
                {
                    continue;
                }

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

            try
            {
                if (_type == WayBillType.Day)
                    waybill.AddRouteInWayBill(date);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                waybill.Exit();
                throw;
            }

            return waybill;
        }
    }
}
