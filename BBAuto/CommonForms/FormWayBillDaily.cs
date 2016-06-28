using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryBBAuto;

namespace BBAuto
{
    public partial class FormWayBillDaily : Form
    {
        private List<Car> _carList;
        private int _index;
        private WayBillDaily _wayBillDaily;

        public FormWayBillDaily(MainDGV dgv)
        {
            InitializeComponent();

            _carList = new List<Car>();

            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                int idCar = dgv.GetCarID(cell.RowIndex);
                CarList carList = CarList.getInstance();
                Car car = carList.getItem(idCar);
                _carList.Add(car);

                lbCars.Items.Add(car);
            }
                        
            btnNext.Enabled = _carList.Count > 1;

            _index = 0;

            lbCar.Text = "Выбранный автомобиль: " + _carList[_index].ToString();

            LoadWayBillCurrentWithoutCreate();
        }

        private void btnLoadWayBillCurrent_Click(object sender, EventArgs e)
        {
            LoadWayBillCurrent();
        }

        private void btnCreateWayBill_Click(object sender, EventArgs e)
        {
            foreach(var car in _carList)
                LoadWayBillDaily(car);
        }

        private void LoadWayBillDaily(Car car)
        {
            DateTime date = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1);

            _wayBillDaily = new WayBillDaily(car, date);
            _wayBillDaily.Load();
        }

        private void LoadWayBillCurrentWithoutCreate()
        {
            _wayBillDaily = new WayBillDaily(_carList[_index], dtpDate.Value);
            
            dgv.DataSource = _wayBillDaily.ToDataTable();
        }

        private void btnOpenInExcelAllFields_Click(object sender, EventArgs e)
        {
            CreateWayBill(_carList[_index], Actions.Show, Fields.All);
        }

        private void btnOpenInExcelSomeFields_Click(object sender, EventArgs e)
        {
            CreateWayBill(_carList[_index], Actions.Show, Fields.Some);
        }

        private void btnPrintAllFieldsCurrent_Click(object sender, EventArgs e)
        {
            CreateWayBill(_carList[_index], Actions.Print, Fields.All);
        }

        private void btnPrintSomeFieldsCurrent_Click(object sender, EventArgs e)
        {
            CreateWayBill(_carList[_index], Actions.Print, Fields.Some);
        }

        private void btnPrintAllFieldsAll_Click(object sender, EventArgs e)
        {
            foreach(var car in _carList)
                CreateWayBill(car, Actions.Print, Fields.All);
        }

        private void btnPrintSomeFieldsAll_Click(object sender, EventArgs e)
        {
            foreach (var car in _carList)
                CreateWayBill(car, Actions.Print, Fields.Some);
        }

        private void CreateWayBill(Car car, Actions action, Fields fields)
        {
            CreateDocument excelWayBill = new CreateDocument(car);

            try
            {
                excelWayBill.createWaybill(dtpDate.Value, null);
                excelWayBill.AddRouteInWayBill(dtpDate.Value, fields);

                if (action == Actions.Print)
                    excelWayBill.Print();
                else
                    excelWayBill.Show();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                excelWayBill.Exit();
            }

            if (car == _carList[_index])
                LoadWayBillCurrent();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_index - 1 == -1)
                return;

            _index--;
            btnNext.Enabled = true;

            if (_index == 0)
                btnPrev.Enabled = false;

            LoadWayBillForNewCar();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_index + 1 == _carList.Count)
                return;

            _index++;
            btnPrev.Enabled = true;

            if (_index + 1 == _carList.Count)
                btnNext.Enabled = false;

            LoadWayBillForNewCar();
        }

        private void LoadWayBillForNewCar()
        {
            lbCar.Text = "Выбранный автомобиль: " + _carList[_index].ToString();

            LoadWayBillCurrentWithoutCreate();
        }
        
        private void LoadWayBillCurrent()
        {
            LoadWayBillDaily(_carList[_index]);

            dgv.DataSource = _wayBillDaily.ToDataTable();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            LoadWayBillCurrentWithoutCreate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _wayBillDaily.Clear();
        }
    }
}
