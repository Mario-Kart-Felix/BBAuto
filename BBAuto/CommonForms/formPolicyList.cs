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
    public partial class formPolicyList : Form
    {
        private PolicyType _policyType;
        private Account _account;
        private int _paymentNumber;
        private string _idOwner;

        public formPolicyList(Account account, PolicyType policyType, int paymentNumber, string idOwner)
        {
            InitializeComponent();
            
            _account = account;
            _policyType = policyType;

            _paymentNumber = paymentNumber + 1;
            
            _idOwner = idOwner;
        }

        private void formPolicyList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            FillTable();
        }

        private void FillTable()
        {
            CreateColumnsTable();

            PolicyList policyList = PolicyList.getInstance();
            DataTable dt = policyList.ToDataTable(_policyType, _idOwner, _paymentNumber);
                        
            foreach (DataRow row in dt.Rows)
            {
                int idPolicy;
                int.TryParse(row.ItemArray[0].ToString(), out idPolicy);

                Policy policy = policyList.getItem(idPolicy);

                if (_account.IsPolicyKaskoAndPayment2())
                    dgvPolicy.Rows.Add(policy.IsInList(_account), idPolicy, row.ItemArray[7], row.ItemArray[3], row.ItemArray[12]);
                else
                    dgvPolicy.Rows.Add(policy.IsInList(_account), idPolicy, row.ItemArray[7], row.ItemArray[3], row.ItemArray[8]);
            }
        }

        private void CreateColumnsTable()
        {
            dgvPolicy.Columns.Clear();

            DataGridViewCheckBoxColumn checkCol = new DataGridViewCheckBoxColumn();
            checkCol.DataPropertyName = "colcheck";
            checkCol.Name = "check";
            dgvPolicy.Columns.Add(checkCol);
            dgvPolicy.Columns["check"].HeaderText = " ";
            dgvPolicy.Columns["check"].Width = 50;
            dgvPolicy.Columns.Add("idPolicy", "idPolicy");
            dgvPolicy.Columns["idPolicy"].Visible = false;
            dgvPolicy.Columns.Add("policyNumber", "№ полиса");
            dgvPolicy.Columns.Add("grz", "Регистрационный знак");
            dgvPolicy.Columns.Add("payment", "Оплата, руб");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvPolicy.Rows)
            {
                bool check;
                bool.TryParse(row.Cells["check"].Value.ToString(), out check);

                if (check)
                {
                    int idPolicy;
                    int.TryParse(row.Cells["idPolicy"].Value.ToString(), out idPolicy);

                    _account.BindWithPolicy(idPolicy, _paymentNumber);
                }
            }

            PolicyList policyList = PolicyList.getInstance();
            policyList.ReLoad();
        }
    }
}
