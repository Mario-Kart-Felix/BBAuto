using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Static;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Common;

namespace BBAuto
{
  public partial class Account_AddEdit : Form
  {
    private readonly Account _account;

    private WorkWithForm _workWithForm;

    public Account_AddEdit(Account account)
    {
      InitializeComponent();

      _account = account;

      cbPayment.SelectedIndex = 0;
    }

    private void aeAccount_Load(object sender, EventArgs e)
    {
      LoadDictionary();

      LoadData();

      ChangeEnableBtnAddPolicy();
      ChangeEnableComboBoxes();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_account.ID == 0);
      _workWithForm.SetEnableValue(btnSave, (!_account.Agreed));
    }

    private void LoadDictionary()
    {
      LoadOneStringDictionary(cbPolicyType, "PolicyType");
      LoadOneStringDictionary(cbOwner, "Owner");
    }

    private void LoadOneStringDictionary(ComboBox combo, string name)
    {
      combo.DataSource = OneStringDictionary.getDataTable(name);
      combo.DisplayMember = "Название";
      combo.ValueMember = name + "_id";
    }

    private void LoadData()
    {
      tbNumber.Text = _account.Number;
      cbOwner.SelectedValue = _account.IDOwner;
      cbPolicyType.SelectedValue = _account.IDPolicyType;
      cbPayment.SelectedIndex = _account.PaymentIndex;
      chbAgreed.Checked = _account.Agreed;
      chbBusinessTrip.Checked = _account.BusinessTrip;

      TextBox tbFile = (TextBox) ucFile.Controls["tbFile"];
      tbFile.Text = _account.File;

      FillTable();
    }

    private void FillTable()
    {
      CreateColumnsTable();

      PolicyList policyList = PolicyList.getInstance();
      DataTable dt = policyList.ToDataTable(_account);

      foreach (DataRow row in dt.Rows)
      {
        dgvPolicy.Rows.Add(row.ItemArray[0], row.ItemArray[7], row.ItemArray[3],
          _account.IsPolicyKaskoAndPayment2() ? row.ItemArray[12] : row.ItemArray[8]);
      }

      FormatDgv();

      tbSum.Text = _account.Sum.ToString();

      ChangeEnableComboBoxes();
    }

    private void FormatDgv()
    {
      PolicyList policyList = PolicyList.getInstance();

      foreach (DataGridViewRow row in dgvPolicy.Rows)
      {
        int idPolicy = Convert.ToInt32(row.Cells[0].Value);
        Policy policy = policyList.getItem(idPolicy);

        if (policy.File != string.Empty)
          row.Cells[1].Style.BackColor = Color.Purple;
      }
    }

    private void CreateColumnsTable()
    {
      dgvPolicy.Columns.Clear();

      dgvPolicy.Columns.Add("idPolicy", "idPolicy");
      dgvPolicy.Columns["idPolicy"].Visible = false;
      dgvPolicy.Columns.Add("policyNumber", "№ полиса");
      dgvPolicy.Columns.Add("grz", "Регистрационный знак");
      dgvPolicy.Columns.Add("payment", "Оплата, руб");
    }

    private void ChangeEnableBtnAddPolicy()
    {
      btnAddPolicy.Enabled = IsInit(cbPolicyType.SelectedValue) && IsInit(cbOwner.SelectedValue);
    }

    private void ChangeEnableComboBoxes()
    {
      bool tableEmpty = IsTableEmpty();

      cbOwner.Enabled = tableEmpty;
      cbPolicyType.Enabled = tableEmpty;
      cbPayment.Enabled = tableEmpty;
    }

    private bool IsTableEmpty()
    {
      return dgvPolicy.Rows.Count == 0;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
        DialogResult = DialogResult.OK;
      Save();
    }

    private void btnAddPolicy_Click(object sender, EventArgs e)
    {
      Save();

      if (GetPolicyType() != PolicyType.КАСКО)
        cbPayment.SelectedIndex = 0;

      formPolicyList fPolicyList = new formPolicyList(_account, GetPolicyType(), cbPayment.SelectedIndex,
        cbOwner.SelectedValue.ToString());
      if (fPolicyList.ShowDialog() == DialogResult.OK)
        FillTable();
    }

    private void Save()
    {
      trySave();
    }

    private void trySave()
    {
      try
      {
        if (_workWithForm.IsEditMode())
        {
          CopyFields();
          _account.Save();
        }
        else
          _workWithForm.SetEditMode(true);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void CopyFields()
    {
      _account.Number = tbNumber.Text;
      _account.IDOwner = cbOwner.SelectedValue.ToString();
      _account.IDPolicyType = cbPolicyType.SelectedValue.ToString();
      _account.PaymentIndex = cbPayment.SelectedIndex;
      _account.BusinessTrip = chbBusinessTrip.Checked;

      TextBox tbFile = (TextBox) ucFile.Controls["tbFile"];
      _account.File = tbFile.Text;
    }

    private void cbPolicyType_SelectedIndexChanged(object sender, EventArgs e)
    {
      bool IsKasko = (GetPolicyType() == PolicyType.КАСКО);

      lbPayment.Visible = IsKasko;
      cbPayment.Visible = IsKasko;

      chbBusinessTrip.Visible = (GetPolicyType() == PolicyType.расш_КАСКО);

      ChangeEnableBtnAddPolicy();
    }

    private PolicyType GetPolicyType()
    {
      if (IsInit(cbPolicyType.SelectedValue))
      {
        int idPolicyType;
        int.TryParse(cbPolicyType.SelectedValue.ToString(), out idPolicyType);

        return (PolicyType) idPolicyType;
      }
      else
        throw new NullReferenceException();
    }

    private bool IsInit(Object obj)
    {
      return obj != null;
    }

    private void cbOwner_SelectedIndexChanged(object sender, EventArgs e)
    {
      ChangeEnableBtnAddPolicy();
    }

    private void btnDelPolicy_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить полис из согласования?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
      {
        int idPolicy = Convert.ToInt32(dgvPolicy.Rows[dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value);

        PolicyList policyList = PolicyList.getInstance();
        Policy policy = policyList.getItem(idPolicy);
        policy.ClearAccountId(_account);

        FillTable();
      }
    }

    private void dgvPolicy_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idPolicy = Convert.ToInt32(dgvPolicy.Rows[dgvPolicy.SelectedCells[0].RowIndex].Cells[0].Value);

      PolicyList policyList = PolicyList.getInstance();
      Policy policy = policyList.getItem(idPolicy);

      if ((e.ColumnIndex == 1) && (policy.File != string.Empty))
        WorkWithFiles.OpenFile(policy.File);
      else
      {
        Policy_AddEdit policyAE = new Policy_AddEdit(policy);
        if (policyAE.ShowDialog() == DialogResult.OK)
          FillTable();
      }
    }
  }
}
