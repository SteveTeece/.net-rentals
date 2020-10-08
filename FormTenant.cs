using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sluiter_Asg10 {
    public partial class FormTenant : Form {

        public Tenant gTenant;

        public void setID ( string id ) {
            textBoxID.Text = id;
        }

        public FormTenant () {
            InitializeComponent();
        }

        public FormTenant ( Tenant tenant ) {
            InitializeComponent();

            //SlumLordRentalSQL.getTenantFromID( tenant.Id, Properties.Settings.Default.DefaultEmployeeEmail );

            gTenant = tenant;

            textBoxID.Text = gTenant.Id.ToString();
            textBoxFirstName.Text = gTenant.FirstName;
            textBoxLastName.Text = gTenant.LastName;
            textBoxEmail.Text = gTenant.Email;
            textBoxPhone.Text = gTenant.Phone;
            textBoxCity.Text = gTenant.City;
            textBoxState.Text = gTenant.State;
            textBoxZip.Text = gTenant.Zip;
            textBoxAddedByID.Text = gTenant.EmployeeEmail;
            textBoxDateAdded.Text = gTenant.getDateTimeString( gTenant.DateAdded );
            textBoxDateUpdated.Text = gTenant.getDateTimeString( gTenant.DateUpdated );
            textBoxStatus.Text = gTenant.getStatus( gTenant.StatusTypeID );
        }

        private void buttonClose_Click ( object sender, EventArgs e ) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonSave_Click ( object sender, EventArgs e ) {
            gTenant.FirstName = textBoxFirstName.Text;
            gTenant.LastName = textBoxLastName.Text;
            gTenant.Email = textBoxEmail.Text;
            gTenant.Phone = textBoxPhone.Text;
            gTenant.City = textBoxCity.Text;
            gTenant.State = textBoxState.Text;
            gTenant.Zip = textBoxZip.Text;
            gTenant.DateUpdated = DateTime.Now;

            SlumLordRentalSQL.tenantUpdate( gTenant, Properties.Settings.Default.DefaultEmployeeEmail );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonDelete_Click ( object sender, EventArgs e ) {
            gTenant.StatusTypeID = 2;   //The assignment only said we needed to change from active to deleted, i can fix it so you can change it both ways if this is a bad way to do it

            SlumLordRentalSQL.tenantUpdate( gTenant, Properties.Settings.Default.DefaultEmployeeEmail );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
