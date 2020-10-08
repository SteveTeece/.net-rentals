using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


  public  class SlumLordRentalBase 
    {
        private int id;
        private byte statusTypeID = 1;
        private DateTime dateAdded = DateTime.Now;
        private DateTime dateUpdated;
        private int addedByID;

        public int Id { get => id; set => id = value; }
        public byte StatusTypeID { get => statusTypeID; set => statusTypeID = value; }
        public DateTime DateAdded { get => dateAdded; set => dateAdded = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
        public int EmployeeEmail { get => addedByID; set => addedByID = value; }

        public String getDateTimeString(DateTime dateTime)
        {
            // Get the String time from a DateTime object
            // Format:  01/01/2019 10:22 am
            return dateTime.ToString("MM/dd/yyyy h:mm tt");
        }



        public String getStatus( byte statusTypeID )
        {
            String status = "";
            // 1 = Active, 2=Deleted   Default to Deleted if NOT 1

            if (this.statusTypeID == 1)
                status = "Active";
            else
                status = "Deleted";

            return status;
        }
    }

