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
    public partial class FormLease : Form {

        private Lease gLease;
        private Property property;
        private Tenant tenant;

        public FormLease () {
            InitializeComponent();
        }

        public FormLease ( Lease lease, Property property, Tenant tenant ) {
            this.property = SlumLordRentalSQL.getPropertyFromID( property.Id, Properties.Settings.Default.DefaultEmployeeEmail );
            this.tenant = SlumLordRentalSQL.getTenantFromID( tenant.Id, Properties.Settings.Default.DefaultEmployeeEmail );
            gLease = lease;

            InitializeComponent();

            loadValues();
        }

        private void loadValues () {
            //Static Info//
            //-----------//
            //Lease Info
            textBoxLeaseID.Text = gLease.Id.ToString();
            textBoxLeasePropertyID.Text = property.Id.ToString();
            textBoxLeaseTenantID.Text = tenant.Id.ToString();

            if (gLease.DateUpdated == DateTime.Parse( "1/1/0001 12:00:00 AM" ))
                gLease.DateUpdated = DateTime.Now;

            textBoxLeaseDateAdded.Text = gLease.getDateTimeString( gLease.DateAdded );
            textBoxLeaseDateUpdated.Text = gLease.getDateTimeString( gLease.DateUpdated );
            textBoxLeaseAddedByID.Text = gLease.EmployeeEmail.ToString();
            textBoxLeaseStatus.Text = gLease.getStatus( gLease.StatusTypeID );

            //Tenant Info
            textBoxTenantID.Text = tenant.Id.ToString();
            textBoxTenantFirstName.Text = tenant.FirstName;
            textBoxTenantLastName.Text = tenant.LastName;
            textBoxTenantEmail.Text = tenant.Email;
            textBoxTenantPhone.Text = tenant.Phone;

            //Property Info
            textBoxPropertyID.Text = property.Id.ToString();
            textBoxPropertyAddress.Text = property.Address;
            textBoxPropertyPrice.Text = property.Price.ToString( "c" );
            textBoxPropertyCity.Text = property.City;
            textBoxPropertyState.Text = property.State;
            textBoxPropertyZip.Text = property.Zip;
            checkBoxPropertyParking.Checked = property.OnsiteParking;
            checkBoxPropertyLaundry.Checked = property.OnsiteLaundry;
            numericUpDownPropertyBedrooms.Value = property.Bedrooms;
            numericUpDownPropertyBathrooms.Value = property.Bathrooms;

            //Non Static Info//
            //---------------//
            textBoxLeaseMonthlyRate.Text = gLease.MonthlyRate.ToString( "c" );

            if (gLease.DateLeaseStarts == DateTime.Parse( "1/1/0001 12:00:00 AM" ))
                gLease.DateLeaseStarts = DateTime.Now;

            if (gLease.DateLeaseEnds == DateTime.Parse( "1/1/0001 12:00:00 AM" ))
                gLease.DateLeaseEnds = DateTime.Now;

            dateTimePickerLeaseStarts.Value = gLease.DateLeaseStarts;
            dateTimePickerLeaseEnds.Value = gLease.DateLeaseEnds;

            textBoxLeaseDescription.Text = gLease.Description;
            textBoxLeaseNotes.Text = gLease.Notes;
        }

        private void buttonClose_Click ( object sender, EventArgs e ) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonSave_Click ( object sender, EventArgs e ) {
            if (Decimal.TryParse( textBoxLeaseMonthlyRate.Text, out decimal tempRate ))
                gLease.MonthlyRate = tempRate;

            gLease.DateLeaseStarts = dateTimePickerLeaseStarts.Value;
            gLease.DateLeaseEnds = dateTimePickerLeaseEnds.Value;
            gLease.Description = textBoxLeaseDescription.Text;
            gLease.Notes = textBoxLeaseNotes.Text;

            SlumLordRentalSQL.leaseUpdate( gLease, Properties.Settings.Default.DefaultEmployeeEmail );

            gLease.DateUpdated = DateTime.Now;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonDelete_Click ( object sender, EventArgs e ) {
            gLease.StatusTypeID = 2;   //The assignment only said we needed to change from active to deleted, i can fix it so you can change it both ways if this is a bad way to do it

            SlumLordRentalSQL.leaseUpdate( gLease, Properties.Settings.Default.DefaultEmployeeEmail );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
