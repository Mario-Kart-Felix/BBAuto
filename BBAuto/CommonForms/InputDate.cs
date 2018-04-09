using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
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
            MainStatus _mainStatus = MainStatus.getInstance();
            Status status = _mainStatus.Get();

            foreach (DataGridViewCell cell in _dgvMain.SelectedCells)
            {
                Car car = _dgvMain.GetCar(cell);

                DateTime date = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, 1);

                CreateDocument excelWayBill;

                try
                {
                    excelWayBill = (status == Status.Invoice) ? CreateWayBill(car, date, _dgvMain.GetID(cell.RowIndex)) : CreateWayBill(car, date);
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

        private CreateDocument CreateWayBill(Car car, DateTime date, int idInvoice = 0)
        {
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
                    waybill.AddRouteInWayBill(date, Fields.All);
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
