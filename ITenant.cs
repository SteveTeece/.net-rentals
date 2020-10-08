using System;

public interface ITenant {
    // Andy Bangsberg, used for ASG 7

    // And Interface is used to ensure your Tenant class has these property
    /*

    Usage:    Class YourClassName : ITenant

    // You will need these properties    
    //  Because I am kind, you can copy and paste these in your Employee Class

    private static int tenantObjectCount; // This is Static, it is a member of the Class, not an instance of the class.
    private int id;
    private String firstName;
    private String lastName;
    private String phone;
    private String email;
    private String city;
    private String state;
    private string zip;
    private byte statusTypeID;
    private DateTime dateAdded;
    private DateTime dateUpdated;
    private int addedByID;
*/

    // PROPERTIES that you will need in your Tenant class
        int Id { get; }
        String FirstName { get; set; }
        String LastName { get; set; }
        String Phone { get; set; }
        String Email { get; set; }
        String City { get; set; }
        String State { get; set; }
        String Zip { get; set; }
        DateTime DateAdded { get; set; }
        DateTime DateUpdated { get; set; }
        byte StatusTypeID { get; set; }  // A byte is 8 bits, values 0 = 255  which is a tinyint in SQL Server
        int EmployeeEmail { get; set; }  // the ID of the employee who added the record



    // SAMPLE CODE THAT YOU WILL WANT IN YOUR CONSTRUCTORS FOR THE Tenant class.

    /*

        // Zero parameter constructor
        public Tenant()
        {
            tenantObjectCount++;  // Increase the STATIC tenantObjectCount

            this.id = tenantObjectCount;
            this.dateAdded = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"); 
        }


        // Example of three parameter constructor
        public Tenant( String firstName, String lastName, int addedByID)
        {
            tenantObjectCount++;  // Increase the STATIC tenantObjectCount

            this.id = tenantObjectCount;
            this.firstName = firstName;
            this.lastName = lastName;
            this.addedByID = addedByID;
            this.dateAdded = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");  


        }


        // Example of eight parameter contstructor
        public Tenant(String firstName, String lastName, 
                        String email, 
                        String phone,
                        String city,
                        String state,
                        String zip,
                        int addedByID)
            {
            tenantObjectCount++;   // Increase the STATIC tenantObjectCount

            this.id = tenantObjectCount;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.statusTypeID = 1; // Active
            this.dateAdded = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");  
            this.addedByID = addedByID;

            }
    */
}