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
    public partial class Route_AddEdit : Form
    {
        private Route _route;

        private WorkWithForm _workWithForm;

        public Route_AddEdit(Route route)
        {
            InitializeComponent();

            _route = route;
        }

        private void Route_AddEdit_Load(object sender, EventArgs e)
        {
            loadRegions();

            FillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_route.IsEqualsID(0));
        }

        private void FillFields()
        {
            if (!_route.IsEqualsID(0))
            {
                MyPointList myPointList = MyPointList.getInstance();
                MyPoint myPoint = myPointList.getItem(_route.MyPoint1ID);
                cbRegion.SelectedValue = myPoint.RegionID;
                loadPoints();
                cbMyPoint1.SelectedValue = _route.MyPoint1ID;
                cbMyPoint2.SelectedValue = _route.MyPoint2ID;
                tbDistance.Text = _route.Distance;
            }
        }

        private void loadRegions()
        {
            Regions regions = Regions.getInstance();
            DataTable dt = regions.ToDataTable();
            cbRegion.DataSource = dt;
            cbRegion.ValueMember = dt.Columns[0].ColumnName;
            cbRegion.DisplayMember = dt.Columns[1].ColumnName;
        }

        private void loadPoints()
        {
            if (cbRegion.SelectedValue == null)
                return;

            MyPointList myPointList = MyPointList.getInstance();
            DataTable dt = myPointList.ToDataTable(Convert.ToInt32(cbRegion.SelectedValue));

            cbMyPoint1.DataSource = dt;
            cbMyPoint1.ValueMember = dt.Columns[0].ColumnName;
            cbMyPoint1.DisplayMember = dt.Columns[1].ColumnName;

            cbMyPoint2.DataSource = dt;
            cbMyPoint2.ValueMember = dt.Columns[0].ColumnName;
            cbMyPoint2.DisplayMember = dt.Columns[1].ColumnName;
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPoints();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                if (cbRegion.SelectedValue == null)
                {
                    MessageBox.Show("Выберите регион", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (cbMyPoint1.SelectedValue == null)
                {
                    MessageBox.Show("Выберите пункт отправления", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (cbMyPoint2.SelectedValue == null)
                {
                    MessageBox.Show("Выберите пункт назначения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (tbDistance.Text == string.Empty)
                {
                    MessageBox.Show("Введите дистанцию", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    _route.MyPoint1ID = Convert.ToInt32(cbMyPoint1.SelectedValue);
                    _route.MyPoint2ID = Convert.ToInt32(cbMyPoint2.SelectedValue);
                    _route.Distance = tbDistance.Text;
                    _route.Save();
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
