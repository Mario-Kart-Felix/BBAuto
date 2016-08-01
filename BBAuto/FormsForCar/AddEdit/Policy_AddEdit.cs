using BBAuto.Domain.Common;
using BBAuto.Domain.ForCar;
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
    public partial class Policy_AddEdit : Form
    {
        private Policy _policy;
        private bool _loadCompleted;

        private WorkWithForm _workWithForm;

        public Policy_AddEdit(Policy policy)
        {
            InitializeComponent();
            _loadCompleted = false;

            _policy = policy;
        }

        private void Policy_AddEdit_Load(object sender, EventArgs e)
        {
            loadDictionary();

            if (_policy.ID == 0)
                changeDateEnd();
            else
                fillFields();

            setVisible();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_policy.ID == 0);
        }

        private void loadDictionary()
        {
            _loadCompleted = false;
            loadOneStringDictionary(cbPolicyType, "PolicyType");
            loadOneStringDictionary(cbOwner, "Owner");
            loadOneStringDictionary(cbComp, "Comp");
            _loadCompleted = true;
        }

        private void loadOneStringDictionary(ComboBox combo, string name)
        {
            combo.DataSource = OneStringDictionary.getDataTable(name);
            combo.DisplayMember = "Название";
            combo.ValueMember = name + "_id";
        }

        private void fillFields()
        {
            cbPolicyType.SelectedValue = (int)_policy.Type;
            cbOwner.SelectedValue = _policy.IdOwner;
            cbComp.SelectedValue = _policy.IdComp;
            tbNumber.Text = _policy.Number;
            dtpDateBegin.Value = _policy.DateBegin;
            dtpDateEnd.Value = _policy.DateEnd;
            tbPay.Text = _policy.Pay;
            tbComment.Text = _policy.Comment;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _policy.File;

            if ((_policy.Type == PolicyType.ДСАГО) || (_policy.Type == PolicyType.GAP))
                tbLimitCost.Text = _policy.LimitCost;
            else if (_policy.Type == PolicyType.КАСКО)
            {
                tbLimitCost.Text = _policy.LimitCost;
                tbPay2.Text = _policy.Pay2;
                dtpDatePay2.Value = _policy.DatePay2;
            }

            tb_Leave(tbPay);
            tb_Leave(tbPay2);
            tb_Leave(tbLimitCost);

            setVisible();
        }

        private void setVisible()
        {
            PolicyType policyType = GetPolicyType();

            if ((policyType == PolicyType.ОСАГО) || (policyType == PolicyType.расш_КАСКО))
            {
                lbLimitCost.Visible = false;
                tbLimitCost.Visible = false;
                lbPay2.Visible = false;
                tbPay2.Visible = false;
                lbPay2Date.Visible = false;
                dtpDatePay2.Visible = false;
            }
            if (policyType == PolicyType.КАСКО)
            {
                lbLimitCost.Text = "Страховая стоимость, руб:";
                lbLimitCost.Visible = true;
                tbLimitCost.Visible = true;
                lbPay2.Visible = true;
                tbPay2.Visible = true;
                lbPay2Date.Visible = true;
                dtpDatePay2.Visible = true;
            }
            else if ((policyType == PolicyType.ДСАГО) || (policyType == PolicyType.GAP))
            {
                lbLimitCost.Text = "Лимит, руб:";
                lbLimitCost.Visible = true;
                tbLimitCost.Visible = true;
                lbPay2.Visible = false;
                tbPay2.Visible = false;
                lbPay2Date.Visible = false;
                dtpDatePay2.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                copyFields();
                _policy.Save();
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private void copyFields()
        {            
            _policy.IdOwner = cbOwner.SelectedValue.ToString();
            _policy.IdComp = cbComp.SelectedValue.ToString();
            _policy.Type = GetPolicyType();
            _policy.Number = tbNumber.Text;
            _policy.DateBegin = dtpDateBegin.Value.Date;
            _policy.DateEnd = dtpDateEnd.Value.Date;
            _policy.Pay = tbPay.Text;
            _policy.Comment = tbComment.Text;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            _policy.File = tbFile.Text;

            if ((_policy.Type == PolicyType.ДСАГО) || (_policy.Type == PolicyType.GAP))
                _policy.LimitCost = tbLimitCost.Text;
            else if (_policy.Type == PolicyType.КАСКО)
            {
                _policy.LimitCost = tbLimitCost.Text;
                _policy.Pay2 = tbPay2.Text;
                _policy.DatePay2 = dtpDatePay2.Value.Date;
            }
        }
        
        private void cbPolicyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadCompleted)
                setVisible();
        }

        private void dtpDateBegin_ValueChanged(object sender, EventArgs e)
        {
            changeDateEnd();
        }

        private void changeDateEnd()
        {
            dtpDateEnd.Value = dtpDateBegin.Value.AddYears(1).AddDays(-1);
            dtpDatePay2.Value = dtpDateBegin.Value.AddMonths(6);
        }

        private PolicyType GetPolicyType()
        {
            return _loadCompleted ? (PolicyType)Convert.ToInt32(cbPolicyType.SelectedValue) : PolicyType.ОСАГО;
        }

        private void tbPay_Enter(object sender, EventArgs e)
        {
            tb_Enter(tbPay);
        }

        private void tbPay_Leave(object sender, EventArgs e)
        {
            tbPay2.Text = tbPay.Text;
            tb_Leave(tbPay);
        }

        private void tb_Leave(TextBox tb)
        {
            tb.Text = MyString.GetFormatedDigit(tb.Text);
        }

        private void tb_Enter(TextBox tb)
        {
            tb.Text = tb.Text.Replace(" ", "");
        }

        private void tbPay2_Enter(object sender, EventArgs e)
        {
            tb_Enter(tbPay2);
        }

        private void tbPay2_Leave(object sender, EventArgs e)
        {
            tb_Enter(tbPay2);
        }

        private void tbLimitCost_Leave(object sender, EventArgs e)
        {
            tb_Enter(tbLimitCost);
        }

        private void tbLimitCost_Enter(object sender, EventArgs e)
        {
            tb_Enter(tbLimitCost);
        }
    }
}
