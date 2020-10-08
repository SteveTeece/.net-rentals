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
    public partial class FormMain : Form {

        FormDefault formDefault;
        FormAbout formAbout;

        public FormMain () {
            InitializeComponent();
            loadTenantListBox();
            loadPropertyListBox();
            loadLeaseListBox();
        }

        private void loadTenantListBox () {
            listBoxTenant.Items.Clear();

            if (radioButtonTenantActive.Checked) {
                SlumLordRentalSQL.loadTenantRecordsToListBox( listBoxTenant, 1, Properties.Settings.Default.DefaultEmployeeEmail );
            } else if (radioButtonTenantDeleted.Checked) {
                SlumLordRentalSQL.loadTenantRecordsToListBox( listBoxTenant, 2, Properties.Settings.Default.DefaultEmployeeEmail );
            } else if (radioButtonTenantBoth.Checked) {
                SlumLordRentalSQL.loadTenantRecordsToListBox( listBoxTenant, 1, Properties.Settings.Default.DefaultEmployeeEmail, true );
            }
        }

        private void loadPropertyListBox () {
            listBoxProperty.Items.Clear();

            if (radioButtonPropertyActive.Checked) {
                SlumLordRentalSQL.loadPropertyRecordsToListBox( listBoxProperty, 1, Properties.Settings.Default.DefaultEmployeeEmail );
            } else if (radioButtonPropertyDeleted.Checked) {
                SlumLordRentalSQL.loadPropertyRecordsToListBox( listBoxProperty, 2, Properties.Settings.Default.DefaultEmployeeEmail );
            } else if (radioButtonPropertyBoth.Checked) {
                SlumLordRentalSQL.loadPropertyRecordsToListBox( listBoxProperty, 1, Properties.Settings.Default.DefaultEmployeeEmail, true );
            }
        }

        private void loadLeaseListBox () {
            listBoxLease.Items.Clear();

            if (radioButtonLeaseActive.Checked) {
                SlumLordRentalSQL.loadLeaseRecordsToListBox( listBoxLease, 1, Properties.Settings.Default.DefaultEmployeeEmail );
            } else if (radioButtonLeaseDeleted.Checked) {
                SlumLordRentalSQL.loadLeaseRecordsToListBox( listBoxLease, 2, Properties.Settings.Default.DefaultEmployeeEmail );
            } else if (radioButtonLeaseBoth.Checked) {
                SlumLordRentalSQL.loadLeaseRecordsToListBox( listBoxLease, 1, Properties.Settings.Default.DefaultEmployeeEmail, true );
            }
        }

        private void buttonAddTenant_Click ( object sender, EventArgs e ) {
            AddTenant();
            loadTenantListBox();
        }

        private void buttonEditTennant_Click ( object sender, EventArgs e ) {

            if (listBoxTenant.SelectedIndex > -1) {
                Tenant tenantToEdit = new Tenant();

                Tenant tenant = (Tenant)listBoxTenant.SelectedItem;

                tenantToEdit = SlumLordRentalSQL.getTenantFromID( tenant.Id, Properties.Settings.Default.DefaultEmployeeEmail );     //(Tenant)listBoxTenant.SelectedItem;

                FormTenant formTenant = new FormTenant( tenantToEdit );

                if (formTenant.ShowDialog() == DialogResult.OK) {
                    //Refresh the tenant object in the list box
                    int index = listBoxTenant.SelectedIndex;   //the thing we just selected
                    listBoxTenant.Items[index] = listBoxTenant.Items[index];
                }
            }

            loadTenantListBox();
        }

        private void AddTenant () {
            Tenant newTenant = new Tenant( "", "", "", "",
                Properties.Settings.Default.DefaultCity,
                Properties.Settings.Default.DefaultState,
                Properties.Settings.Default.DefaultZip,
                Properties.Settings.Default.DefaultEmployeeEmail );

            FormTenant formEdit = new FormTenant( newTenant );

            formEdit.ShowDialog();

            //Add newTenant to the listBox
            if (formEdit.DialogResult == DialogResult.OK) {
                SlumLordRentalSQL.tenantInsert( newTenant, Properties.Settings.Default.DefaultEmployeeEmail );
            } else {
                Tenant.tenantObjectCount--;
            }
        }

        private void defaultsToolStripMenuItem_Click ( object sender, EventArgs e ) {
            formDefault = new FormDefault();
            formDefault.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click ( object sender, EventArgs e ) {
            formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void buttonAddProperty_Click ( object sender, EventArgs e ) {
            AddProperty();
            loadPropertyListBox();
        }

        private void AddProperty () {
            Property newProperty = new Property( "", Properties.Settings.Default.DefaultCity, Properties.Settings.Default.DefaultState, Properties.Settings.Default.DefaultZip, 0, "", "", 0, 0, false, false, DateTime.Now, Properties.Settings.Default.DefaultEmployeeEmail );

            FormProperty formProperty = new FormProperty( newProperty );

            formProperty.ShowDialog();

            //To save or not to save
            if (formProperty.DialogResult == DialogResult.OK) {
                SlumLordRentalSQL.propertyInsert( newProperty, Properties.Settings.Default.DefaultEmployeeEmail );
            } else {
                Property.propertyObjectCount--;
            }
        }

        private void buttonEditProperty_Click ( object sender, EventArgs e ) {
            if (listBoxProperty.SelectedIndex > -1) {
                Property propertyToEdit = new Property();

                Property property = (Property)listBoxProperty.SelectedItem;

                propertyToEdit = SlumLordRentalSQL.getPropertyFromID( property.Id, Properties.Settings.Default.DefaultEmployeeEmail );

                FormProperty formProperty = new FormProperty( propertyToEdit );

                if (formProperty.ShowDialog() == DialogResult.OK) {
                    //Refresh the property object in the list box
                    int index = listBoxProperty.SelectedIndex;   //the thing we just selected
                    listBoxProperty.Items[index] = listBoxProperty.Items[index];
                }
            }

            loadPropertyListBox();
        }

        private void buttonAddLease_Click ( object sender, EventArgs e ) {
            AddLease();
            loadLeaseListBox();
        }

        private void AddLease () {
            if ((listBoxTenant.SelectedIndex > -1) && (listBoxProperty.SelectedIndex > -1)) {
                Tenant tenant = (Tenant)listBoxTenant.SelectedItem;
                Property property = (Property)listBoxProperty.SelectedItem;

                Lease lease = new Lease();
                lease.PropertyID = property.Id;
                lease.TenantID = tenant.Id;

                //lease.EmployeeEmail = Properties.Settings.Default.DefaultEmployeeEmail;

                FormLease addLease = new FormLease( lease, property, tenant );

                addLease.ShowDialog();

                if (addLease.DialogResult == DialogResult.OK) {
                    SlumLordRentalSQL.leaseInsert( lease, Properties.Settings.Default.DefaultEmployeeEmail );
                } else {
                    Lease.leaseObjectCount--;
                }
            }
        }

        private void buttonEditLease_Click ( object sender, EventArgs e ) {
            if (listBoxLease.SelectedIndex > -1) {
                Lease lease = (Lease)listBoxLease.SelectedItem;

                Lease leaseToEdit = SlumLordRentalSQL.getLeaseFromID( lease.Id, Properties.Settings.Default.DefaultEmployeeEmail );

                Property property = SlumLordRentalSQL.getPropertyFromID( leaseToEdit.PropertyID, Properties.Settings.Default.DefaultEmployeeEmail );
                Tenant tenant = SlumLordRentalSQL.getTenantFromID( leaseToEdit.TenantID, Properties.Settings.Default.DefaultEmployeeEmail );

                FormLease formLease = new FormLease( lease, property, tenant );

                if (formLease.ShowDialog() == DialogResult.OK) {
                    int index = listBoxLease.SelectedIndex;
                    listBoxLease.Items[index] = listBoxLease.Items[index];
                }
            }

            loadLeaseListBox();
        }

        private void exitToolStripMenuItem_Click ( object sender, EventArgs e ) {
            this.Close();
        }

        private void radioButtonTenantActive_Click ( object sender, EventArgs e ) {
            loadTenantListBox();
        }
        private void radioButtonTenantDeleted_Click ( object sender, EventArgs e ) {
            loadTenantListBox();
        }
        private void radioButtonTenantBoth_Click ( object sender, EventArgs e ) {
            loadTenantListBox();
        }
        private void radioButtonPropertyActive_Click ( object sender, EventArgs e ) {
            loadPropertyListBox();
        }
        private void radioButtonPropertyDeleted_Click ( object sender, EventArgs e ) {
            loadPropertyListBox();
        }
        private void radioButtonPropertyBoth_Click ( object sender, EventArgs e ) {
            loadPropertyListBox();
        }
        private void radioButtonLeaseActive_Click ( object sender, EventArgs e ) {
            loadLeaseListBox();
        }
        private void radioButtonLeaseDeleted_Click ( object sender, EventArgs e ) {
            loadLeaseListBox();
        }
        private void radioButtonLeaseBoth_Click ( object sender, EventArgs e ) {
            loadLeaseListBox();
        }
    }
}
