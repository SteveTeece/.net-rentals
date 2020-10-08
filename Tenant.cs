using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluiter_Asg10 {
    public class Tenant : SlumLordRentalBase, ITenant {

        public static int tenantObjectCount;
        
        private int id;
        private string firstName;
        private string lastName;
        private string phone;
        private string email;
        private string city;
        private string state;
        private string zip;
        private DateTime dateAdded;
        private DateTime dateUpdated;
        private byte statusTypeID;
        private string employeeEmail;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zip { get => zip; set => zip = value; }
        public DateTime DateAdded { get => dateAdded; set => dateAdded = value; }
        public DateTime DateUpdated { get => dateUpdated; set => dateUpdated = value; }
        public byte StatusTypeID { get => statusTypeID; set => statusTypeID = value; }
        public string EmployeeEmail { get => employeeEmail; set => employeeEmail = value; }

        public Tenant () {
            tenantObjectCount++;  // Increase the STATIC tenantObjectCount

            this.id = tenantObjectCount;
            this.dateAdded = DateTime.Now;
        }

        // Example of three parameter constructor
        public Tenant ( String firstName, String lastName, string employeeEmail ) {
            tenantObjectCount++;  // Increase the STATIC tenantObjectCount

            this.id = tenantObjectCount;
            this.firstName = firstName;
            this.lastName = lastName;
            this.employeeEmail = employeeEmail;
            this.dateAdded = DateTime.Now;
        }

        // Example of eight parameter contstructor
        public Tenant ( String firstName, 
                        String lastName,
                        String email,
                        String phone,
                        String city,
                        String state,
                        String zip,
                        String employeeEmail ) {
            tenantObjectCount++;   // Increase the STATIC tenantObjectCount

            this.id = tenantObjectCount;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.statusTypeID = 1; // Active
            this.dateAdded = DateTime.Now;
            this.employeeEmail = employeeEmail;
        }

        public override string ToString () {
            //return base.ToString();

            //wasnt working without this for some reason but its functional now
            Tenant tempTenant = SlumLordRentalSQL.getTenantFromID( this.id, Properties.Settings.Default.DefaultEmployeeEmail );

            string info = "";
            info = "TENNANT: ID: " + tempTenant.id.ToString() + ", " + tempTenant.firstName + " " + tempTenant.lastName + " - " + tempTenant.city + ", " + tempTenant.state + " " + tempTenant.zip + (StatusTypeID == 1 ? " (Active)" : " (Deleted)");

            return info;
        }
    }
}
