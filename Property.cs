using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluiter_Asg10 {
    public class Property : SlumLordRentalBase, IProperty {
        public static int propertyObjectCount;

        private int id;
        private string address;
        private string city;
        private string state;
        private string zip;
        private Decimal price;
        private string descriptionBrief;
        private string descriptionFull;
        private int bathrooms;
        private int bedrooms;
        private bool onsiteParking;
        private bool onsiteLaundry;
        private DateTime availableOn;
        private DateTime dateAdded;
        private DateTime dateUpdated;
        private byte statusTypeID;
        private string employeeEmail;

        public int Id { get => id; set => id = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zip { get => zip; set => zip = value; }
        public decimal Price { get => price; set => price = value; }
        public string DescriptionBrief { get => descriptionBrief; set => descriptionBrief = value; }
        public string DescriptionFull { get => descriptionFull; set => descriptionFull = value; }
        public int Bathrooms { get => bathrooms; set => bathrooms = value; }
        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public bool OnsiteParking { get => onsiteParking; set => onsiteParking = value; }
        public bool OnsiteLaundry { get => onsiteLaundry; set => onsiteLaundry = value; }
        public DateTime AvailableOn { get => availableOn; set => availableOn = value; }
        public DateTime DateAdded { get => dateAdded; set => dateAdded = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
        public byte StatusTypeID { get => statusTypeID; set => statusTypeID = value; }
        public string EmployeeEmail { get => employeeEmail; set => employeeEmail = value; }

        public Property () {
            propertyObjectCount++;

            this.id = propertyObjectCount;

            this.availableOn = DateTime.MinValue;

            this.statusTypeID = 1; //Active
            this.dateAdded = DateTime.Now;
        }

        public Property ( string address,
                          string city,
                          string state,
                          string zip,
                          decimal price,
                          string descriptionBrief,
                          string descriptionFull,
                          int bathrooms, int bedrooms,
                          bool onsiteParking,
                          bool onsiteLaundry,
                          DateTime availableOn,
                          string employeeEmail ) {
            propertyObjectCount++;    //Increase the STATIC propertyObjectCount

            this.id = propertyObjectCount;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.price = price;
            this.descriptionBrief = descriptionBrief;
            this.descriptionFull = descriptionFull;
            this.bathrooms = bathrooms;
            this.bedrooms = bedrooms;
            this.onsiteParking = onsiteParking;
            this.onsiteLaundry = onsiteLaundry;
            this.availableOn = availableOn;

            this.statusTypeID = 1; //Active
            this.dateAdded = DateTime.Now;
            this.employeeEmail = employeeEmail;
        }

        public override string ToString () {
            string info = "";

            Property tempProperty = SlumLordRentalSQL.getPropertyFromID( this.id, Properties.Settings.Default.DefaultEmployeeEmail );

            info = "PROP: ID: " + tempProperty.id.ToString() + ", " + tempProperty.address + " " + tempProperty.city + ", " + tempProperty.state + " " + tempProperty.zip + (StatusTypeID == 1 ? " (Active)" : " (Deleted)");

            return info;
        }
    }
}
