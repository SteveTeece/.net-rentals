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
    public partial class FormDefault : Form {
        public FormDefault () {
            InitializeComponent();
        }

        private void FormDefault_Load(object sender, EventArgs e) {
            loadSettings();
        }

        private void buttonClose_Click ( object sender, EventArgs e ) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonSave_Click ( object sender, EventArgs e ) {
            saveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void loadSettings () {
            textBoxEmployeeEmail.Text = Properties.Settings.Default.DefaultEmployeeEmail.ToString();
            textBoxCity.Text = Properties.Settings.Default.DefaultCity;
            textBoxState.Text = Properties.Settings.Default.DefaultState;
            textBoxZip.Text = Properties.Settings.Default.DefaultZip;
        }

        private bool saveSettings () {

  

                Properties.Settings.Default.DefaultEmployeeEmail = textBoxEmployeeEmail.Text;
                Properties.Settings.Default.DefaultCity = textBoxCity.Text;
                Properties.Settings.Default.DefaultState = textBoxState.Text;
                Properties.Settings.Default.DefaultZip = textBoxZip.Text;

                Properties.Settings.Default.Save(); //Very important or nothing is saved
            


            return true;
        }
    }
}
