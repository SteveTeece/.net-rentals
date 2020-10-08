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
    public partial class FormProperty : Form {

        public Property gProperty;
        public FormProperty () {
            InitializeComponent();
        }

        public FormProperty ( Property property ) {
            InitializeComponent();

            gProperty = property;

            if (gProperty.AvailableOn == DateTime.Parse( "1/1/0001 12:00:00 AM" ))
                gProperty.AvailableOn = DateTime.Today;

            textBoxID.Text = gProperty.Id.ToString();
            textBoxAddress.Text = gProperty.Address;
            textBoxCity.Text = gProperty.City;
            textBoxState.Text = gProperty.State;
            textBoxZip.Text = gProperty.Zip;
            textBoxPrice.Text = gProperty.Price.ToString();
            textBoxBriefDesc.Text = gProperty.DescriptionBrief;
            textBoxFullDesc.Text = gProperty.DescriptionFull;
            numericUpDownBathrooms.Value = gProperty.Bathrooms;
            numericUpDownBedrooms.Value = gProperty.Bedrooms;
            checkBoxParking.Checked = gProperty.OnsiteParking;
            checkBoxLaundry.Checked = gProperty.OnsiteLaundry;
            dateTimePickerAvailable.Value = gProperty.AvailableOn;

            textBoxAddedByID.Text = gProperty.EmployeeEmail.ToString();
            textBoxDateAdded.Text = gProperty.DateAdded.ToString();
            textBoxDateUpdated.Text = gProperty.DateUpdated.ToString();
            textBoxStatus.Text = gProperty.getStatus( gProperty.StatusTypeID );
        }

        private void buttonClose_Click ( object sender, EventArgs e ) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonSave_Click ( object sender, EventArgs e ) {
            if (Decimal.TryParse( textBoxPrice.Text, out Decimal tempPrice )) {
                gProperty.Price = tempPrice;
            }

            gProperty.Address = textBoxAddress.Text;
            gProperty.City = textBoxCity.Text;
            gProperty.State = textBoxState.Text;
            gProperty.Zip = textBoxZip.Text;
            gProperty.DescriptionBrief = textBoxBriefDesc.Text;
            gProperty.DescriptionFull = textBoxFullDesc.Text;
            gProperty.Bathrooms = (int)numericUpDownBathrooms.Value;
            gProperty.Bedrooms = (int)numericUpDownBedrooms.Value;
            gProperty.OnsiteParking = checkBoxParking.Checked;
            gProperty.OnsiteLaundry = checkBoxLaundry.Checked;
            gProperty.AvailableOn = dateTimePickerAvailable.Value;
            gProperty.DateUpdated = DateTime.Now;

            SlumLordRentalSQL.propertyUpdate( gProperty, Properties.Settings.Default.DefaultEmployeeEmail );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonDelete_Click ( object sender, EventArgs e ) {
            gProperty.StatusTypeID = 2;   //The assignment only said we needed to change from active to deleted, i can fix it so you can change it both ways if this is a bad way to do it

            SlumLordRentalSQL.propertyUpdate( gProperty, Properties.Settings.Default.DefaultEmployeeEmail );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
