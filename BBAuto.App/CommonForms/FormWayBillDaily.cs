using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BBAuto.App.GUI;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.CommonForms
{
  public partial class FormWayBillDaily : Form
  {
    private List<Car> list;
    private int index;
    private WayBillDaily wayBillDaily;
    private FuelList fuelList;
    private TabelList tabelList;
    private Driver driverCurrent;

    public FormWayBillDaily(MainDGV dgv)
    {
      InitializeComponent();

      list = new List<Car>();
      fuelList = FuelList.getInstance();

      foreach (DataGridViewCell cell in dgv.SelectedCells)
      {
        string fio = dgv.GetFIO(cell.RowIndex);
        DriverList dl = DriverList.getInstance();
        driverCurrent = dl.getItemByFullFIO(fio);

        int idCar = dgv.GetCarID(cell.RowIndex);
        CarList carList = CarList.getInstance();
        Car car = carList.getItem(idCar);
        list.Add(car);

        lbCars.Items.Add(car);
      }

      btnNext.Enabled = list.Count > 1;

      index = 0;

      lbCar.Text = "Выбранный автомобиль: " + list[index].ToString();
    }

    private void FormWayBillDaily_Load(object sender, EventArgs e)
    {
      LoadWayBillCurrentWithoutCreate();

      LoadFuel();
    }

    private void btnLoadWayBillCurrent_Click(object sender, EventArgs e)
    {
      LoadWayBillCurrent();
    }

    private void btnCreateWayBill_Click(object sender, EventArgs e)
    {
      foreach (var car in list)
      {
        LoadWayBillDaily(car);
      }
    }

    private void LoadWayBillDaily(Car car)
    {
      DateTime date = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, 1);

      wayBillDaily = new WayBillDaily(car, date);
      wayBillDaily.Load();
    }

    private void LoadWayBillCurrentWithoutCreate()
    {
      wayBillDaily = new WayBillDaily(list[index], dtpDate.Value);
      dgv.DataSource = wayBillDaily.ToDataTable();

      /* Отметить дни командировки - цветом */
      KomandByColor();
    }

    private void LoadFuel()
    {
      dgvFuel.DataSource = fuelList.ToDataTable(list[index], dtpDate.Value);
      dgvFuel.Columns[0].Visible = false;
    }

    private void btnOpenInExcelAllFields_Click(object sender, EventArgs e)
    {
      CreateWayBill(list[index], Logic.Static.Actions.Show, Fields.All);
    }

    private void btnOpenInExcelSomeFields_Click(object sender, EventArgs e)
    {
      CreateWayBill(list[index], Logic.Static.Actions.Show, Fields.Some);
    }

    private void btnPrintAllFieldsCurrent_Click(object sender, EventArgs e)
    {
      CreateWayBill(list[index], Logic.Static.Actions.Print, Fields.All);
    }

    private void btnPrintSomeFieldsCurrent_Click(object sender, EventArgs e)
    {
      CreateWayBill(list[index], Logic.Static.Actions.Print, Fields.Some);
    }

    private void btnPrintAllFieldsAll_Click(object sender, EventArgs e)
    {
      foreach (var car in list)
        CreateWayBill(car, Logic.Static.Actions.Print, Fields.All);
    }

    private void btnPrintSomeFieldsAll_Click(object sender, EventArgs e)
    {
      foreach (var car in list)
        CreateWayBill(car, Logic.Static.Actions.Print, Fields.Some);
    }

    private void CreateWayBill(Car car, Logic.Static.Actions action, Fields fields)
    {
      CreateDocument excelWayBill = new CreateDocument(car);

      try
      {
        excelWayBill.CreateWaybill(dtpDate.Value, null);
        excelWayBill.AddRouteInWayBill(dtpDate.Value, fields);

        if (action == Logic.Static.Actions.Print)
          excelWayBill.Print();
        else
          excelWayBill.Show();
      }
      catch (NullReferenceException ex)
      {
        MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        excelWayBill.Exit();
      }

      if (car == list[index])
        LoadWayBillCurrent();
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
      if (index - 1 == -1)
        return;

      index--;
      btnNext.Enabled = true;

      if (index == 0)
        btnPrev.Enabled = false;

      LoadWayBillForNewCar();
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      if (index + 1 == list.Count)
        return;

      index++;
      btnPrev.Enabled = true;

      if (index + 1 == list.Count)
        btnNext.Enabled = false;

      LoadWayBillForNewCar();
    }

    private void LoadWayBillForNewCar()
    {
      lbCar.Text = "Выбранный автомобиль: " + list[index].ToString();

      LoadWayBillCurrentWithoutCreate();
    }

    private void LoadWayBillCurrent()
    {
      LoadWayBillDaily(list[index]);
      dgv.DataSource = wayBillDaily.ToDataTable();

      KomandByColor();
    }

    private void KomandByColor()
    {
      /* Отметить дни командировки - цветом */
      tabelList = TabelList.GetInstance();
      List<Tabel> tL = tabelList.getItemWithoutDay("businessTrip", driverCurrent, dtpDate.Value);
      if (tL.Count != 0)
      {
        int i = 0;

        foreach (DataGridViewRow row in dgv.Rows)
        {
          DateTime date = Convert.ToDateTime(row.Cells[0].Value);
          if (tabelList.getItem("businessTrip", driverCurrent, date) != null)
          {
            row.DefaultCellStyle.BackColor = Color.FromArgb(115, 214, 186);
            tL = tL.Where(t => t.Date.Year == date.Year && t.Date.Month == date.Month && t.Date.Day != date.Day)
              .ToList();
          }
        }

        if (tL.Count != 0)
        {
          foreach (Tabel tab in tL)
          {
            while (dgv.Rows[i].DefaultCellStyle.BackColor == Color.FromArgb(115, 214, 186))
              i++;

            if (dgv.Rows[i].DefaultCellStyle.BackColor != Color.FromArgb(115, 214, 186))
            {
              dgv.Rows[i].Cells[0].Value = tab.Date;
              dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(115, 214, 186);
            }
            i++;
          }
        }
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      wayBillDaily.Clear();
      LoadWayBillCurrent();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      LoadWayBillCurrentWithoutCreate();
      LoadFuel();
    }
  }
}
