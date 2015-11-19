using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto;
using ClassLibraryBBAuto;

namespace BBAuto
{
    public partial class Driver_AddEdit : Form
    {
        private Driver _driver;
        private Ldap ldap = new Ldap();
        
        private WorkWithForm _workWithForm;

        public Driver_AddEdit(Driver driver)
        {
            InitializeComponent();
            
            _driver = driver;
        }

        private void Driver_AddEdit_Load(object sender, EventArgs e)
        {
            loadData();

            tbNumber.Visible = lbNumber.Visible = (_driver.OwnerID < 3);
            
            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.EditModeChanged += SetEnable;
            _workWithForm.SetEditMode(_driver.IsEqualsID(0));
        }

        private void loadData()
        {
            loadDictionary(cbRegion, "Region");
            
            DriverList driverList = DriverList.getInstance();
            
            fillCommonFields();
            fillExtraFields();
        }

        private void SetEnable(Object sender, EditModeEventArgs e)
        {            
            if (User.GetRole() == RolesList.AccountantWayBill)
            {
                this.Size = new Size(410, 430);
                _workWithForm.SetEnableValue(btnSave, true);
            }
            
            if (_workWithForm.IsEditMode())
            {
                if (_driver.From1C)
                {
                    tbCompany.ReadOnly = true;

                    rbMan.Enabled = false;
                    rbWoman.Enabled = false;

                    tbFio.ReadOnly = true;
                    cbRegion.Enabled = false;
                    tbDept.ReadOnly = true;
                    tbPosition.ReadOnly = true;
                    chbDecret.Enabled = false;
                    chbFired.Enabled = false;

                    mtbDateBirth.ReadOnly = true;
                    tbLogin.ReadOnly = true;
                }

                tbNumber.ReadOnly = _driver.From1C;
            }
        }

        private void loadDictionary(ComboBox combo, string name)
        {
            combo.DataSource = OneStringDictionary.getDataTable(name);
            combo.ValueMember = name + "_id";
            combo.DisplayMember = "Название";
        }

        private void fillCommonFields()
        {
            tbFio.Text = _driver.GetName(NameType.Full);
            mtbMobile.Text = _driver.Mobile;
            tbEmail.Text = _driver.email;
            mtbDateBirth.Text = _driver.DateBirth;
            cbRegion.SelectedValue = _driver.RegionID;
            tbCompany.Text = _driver.CompanyName;
            chbFired.Checked = _driver.Fired;
            tbPosition.Text = _driver.Position;
            tbExpSince.Text = _driver.ExpSince;
            tbDept.Text = _driver.Dept;
            tbLogin.Text = _driver.Login;
            tbSuppyAddress.Text = _driver.suppyAddress;

            rbMan.Checked = (_driver.SexIndex == 0);
            rbWoman.Checked = (_driver.SexIndex == 1);
            
            chbDecret.Checked = _driver.Decret;
            chbNotificationStop.Checked = _driver.NotificationStop;
            if (_driver.NotificationStop)
                dtpStopNotificationDate.Value = _driver.DateStopNotification;
            tbNumber.Text = _driver.Number;
        }

        private void fillExtraFields()
        {
            FillCar();
            FillInstraction();
            FillMedicalCert();
            FillDriverLicense();
            FillPassport();
            FillDTP();
            FillViolation();
            FillFuelCardDriver();
        }

        private void FillCar()
        {
            DriverCarList driverCarList = DriverCarList.getInstance();
            Car car = driverCarList.GetCar(_driver);
            carInfo.Text = (car == null) ? string.Empty : car.ToString();
        }

        private void FillInstraction()
        {
            InstractionList instractionList = InstractionList.getInstance();
            Instraction instraction = instractionList.getItem(_driver);
            instractionInfo.Text = instraction.ToString();
        }

        private void FillMedicalCert()
        {
            MedicalCertList medicalCertList = MedicalCertList.getInstance();
            MedicalCert medicalCert = medicalCertList.getItem(_driver);
            medicalCertInfo.Text = medicalCert.ToString();
        }

        private void FillDriverLicense()
        {
            LicenseList licencesList = LicenseList.getInstance();
            DriverLicense driverLicense = licencesList.getItem(_driver);
            licenceInfo.Text = driverLicense.ToString();
        }

        private void FillPassport()
        {
            PassportList passportList = PassportList.getInstance();
            Passport passport = passportList.getLastPassport(_driver);
            labelPassport.Text = passport.ToString();
        }

        private void FillDTP()
        {
            DTPList dtpList = DTPList.getInstance();
            DTP dtp = dtpList.getLastByDriver(_driver);
            dtpInfo.Text = dtp.ToString();
        }

        private void FillViolation()
        {
            ViolationList violationList = ViolationList.getInstance();
            Violation violation = violationList.getItem(_driver);
            ViolationInfo.Text = violation.ToString();
        }

        private void FillFuelCardDriver()
        {
            FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

            FuelCardDriver fuelCardDriver = fuelCardDriverList.getItemFirst(_driver);
            lbFuelCard1.Text = fuelCardDriver.ToString();

            fuelCardDriver = fuelCardDriverList.getItemSecond(_driver);
            lbFuelCard2.Text = fuelCardDriver.ToString();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (User.GetRole() == RolesList.AccountantWayBill)
            {
                if (btnSave.Text == "Редактировать")
                {
                    _workWithForm.SetEnableValue(tbSuppyAddress, true);
                    btnSave.Text = "Сохранить";
                }
                else
                {
                    if (trySave())
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                if (_workWithForm.IsEditMode())
                {
                    if (trySave())
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    _workWithForm.SetEditMode(true);
                }
            }
        }

        private void instractionLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (trySave())
            {
                formInstractionList instList = new formInstractionList(_driver);
                if (instList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    FillInstraction();
            }
        }

        private void medicalCertLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (trySave())
            {
                formMedicalCertList mcList = new formMedicalCertList(_driver);
                if (mcList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    FillMedicalCert();
            }
        }

        private void licenceLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (trySave())
            {
                formLicenseList licList = new formLicenseList(_driver);
                if (licList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    FillDriverLicense();
            }
        }

        private void linkPassport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (trySave())
            {
                formPassportList passList = new formPassportList(_driver);
                if (passList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    FillPassport();
            }
        }

        private bool trySave()
        {
            try
            {
                Save();
                return true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Для продолжения заполните поля формы \"Водитель\"", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(_driver.Number) && (!_driver.From1C) && (_driver.OwnerID < 3) && (!string.IsNullOrEmpty(tbNumber.Text)))
            {
                DriverList driverList = DriverList.getInstance();
                if (!driverList.IsUniqueNumber(tbNumber.Text))
                {
                    MessageBox.Show("Сохранение невозможно. Сотрудник с таким номером уже есть.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            CopyFields();
            _driver.Save();
        }

        private void CopyFields()
        {
            if (!_driver.From1C)
            {
                _driver.Fio = tbFio.Text;
                _driver.RegionID = (cbRegion.SelectedValue == null) ? 0 : Convert.ToInt32(cbRegion.SelectedValue.ToString());
                _driver.CompanyName = tbCompany.Text;
                _driver.Position = tbPosition.Text;
                _driver.Dept = tbDept.Text;
                _driver.SexIndex = (rbMan.Checked) ? 0 : 1;
                _driver.Fired = chbFired.Checked;
                _driver.Decret = chbDecret.Checked;
                _driver.DateBirth = mtbDateBirth.Text;
                _driver.Login = tbLogin.Text;
                _driver.Number = tbNumber.Text;
            }

            _driver.email = tbEmail.Text;
            _driver.ExpSince = tbExpSince.Text;
            _driver.Mobile = mtbMobile.Text;
            _driver.suppyAddress = tbSuppyAddress.Text;
            _driver.DateStopNotification = (chbNotificationStop.Checked) ? dtpStopNotificationDate.Value : new DateTime(1, 1, 1);

            _driver.IsDriver = true;
        }

        private void carLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formDriverCar formDriverCar = new formDriverCar(_driver);
            formDriverCar.ShowDialog();
        }

        private void dtpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formDTPList dtpList = new formDTPList(_driver);
            dtpList.ShowDialog();
        }

        private void violationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formViolationList vList = new formViolationList(_driver);
            vList.ShowDialog();
        }

        private void linkFuelCard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            formFuelCardDriver fFuelCardDriver = new formFuelCardDriver(_driver);
            fFuelCardDriver.ShowDialog();
        }

        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            if (tbEmail.Text == string.Empty)
            {
                tbEmail.Text = ldap.GetEmail(tbLogin.Text);
                Save();
            }
        }

        private void chbNotificationStop_CheckedChanged(object sender, EventArgs e)
        {
            dtpStopNotificationDate.Visible = chbNotificationStop.Checked;
        }
    }
}