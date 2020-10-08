

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sluiter_Asg10 {

    public static class SlumLordRentalSQL {
        // NOTE:  You will need to make changes to this class.



        // Class used to talk to the SQL Server and our SlumLordDatabase

        // Connection String is used to tell C# what and where the database is located and 
        // who is the user and password
        // This is needed to connect to the SQL Server, everyone will use this same Connection String
        private static string connectionString =
            "Data Source=witc-slumlordshared.clo0si2qxav0.us-west-2.rds.amazonaws.com,1433;" +
            "Network Library = DBMSSOCN;" +
            "Initial Catalog = SlumLordRentals;" +
            "User ID = witcstudent; Password = 123student456";


        public static string ConnectionString { get => connectionString; }



        // **** Tenants ***************************************************************

        public static void loadTenantRecordsToListBox ( ListBox listBox, byte statusTypeID, String employeeEmail, bool ignoreStatusType = false) {
            // listBox is the name of the listBox you will to fill with the results
            // statusTypeID 1=Active, 2 = Cancelled
            // employeeEmail is your email address stored in your defaults, this is used to show only your records
            // Also Andy's records will be shown


            // Get data from the database and fill the listbox.
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader dataReader;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spTenantList";  // Just lists a few fields, enough for our ListBoxes
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@StatusTypeID", statusTypeID );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );
            sqlCommand.Parameters.AddWithValue( "@IgnoreStatusType", ignoreStatusType );

            sqlConnection.Open();

            dataReader = sqlCommand.ExecuteReader();

            Tenant tenant;

            while (dataReader.Read()) {
                // Create a Teanat object by calling the three parameter constructor
                tenant = new Tenant();
                // Set the values from the database record.
                tenant.Id = (int)dataReader["ID"];
                tenant.FirstName = dataReader["FirstName"].ToString();
                tenant.LastName = dataReader["LastName"].ToString();
                tenant.StatusTypeID = (byte)dataReader["StatusTypeID"]; // StatusTypeID is a SQL tinyInt = C# byte
                tenant.DateAdded = (DateTime)dataReader["DateAdded"];

                listBox.Items.Add( tenant );
            }

            sqlConnection.Close();
        }

        public static Tenant getTenantFromID ( int id, String employeeEmail ) {
            Tenant tenant = new Tenant();


            // Get data from the database and fill the listbox.
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader dataReader;


            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spTenantSelect";
            sqlCommand.Connection = sqlConnection;

            // Set the parameters for the Stored Procedure, notice both a name "@ID" and value
            sqlCommand.Parameters.AddWithValue( "@ID", id );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            // Open connection to the SQL Server
            sqlConnection.Open();

            // Try and get the data
            dataReader = sqlCommand.ExecuteReader();

            // If we have a record, set the fields into the Tenant object
            if (dataReader.Read()) {
                tenant.Id = (int)dataReader["ID"];
                tenant.FirstName = dataReader["FirstName"].ToString();
                tenant.LastName = dataReader["LastName"].ToString();
                tenant.Email = dataReader["Email"].ToString();
                tenant.Phone = dataReader["Phone"].ToString();
                tenant.City = dataReader["City"].ToString();
                tenant.State = dataReader["State"].ToString();
                tenant.Zip = dataReader["Zip"].ToString();
                tenant.StatusTypeID = (byte)dataReader["StatusTypeID"]; // StatusTypeID is a SQL tinyInt = C# byte
                tenant.DateAdded = (DateTime)dataReader["DateAdded"];
                tenant.DateUpdated = (DateTime)dataReader["DateUpdated"];
                tenant.EmployeeEmail = dataReader["AddedByEmployeeID"].ToString();
                //tenant.AddedByName = dataReader["AddedByName"].ToString();
            }

            sqlConnection.Close();

            return tenant;
        }

        public static void tenantUpdate ( Tenant tenant, String employeeEmail ) {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spTenantUpdate";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@ID", tenant.Id );
            sqlCommand.Parameters.AddWithValue( "@FirstName", tenant.FirstName );
            sqlCommand.Parameters.AddWithValue( "@LastName", tenant.LastName );
            sqlCommand.Parameters.AddWithValue( "@Email", tenant.Email );
            sqlCommand.Parameters.AddWithValue( "@Phone", tenant.Phone );
            sqlCommand.Parameters.AddWithValue( "@City", tenant.City );
            sqlCommand.Parameters.AddWithValue( "@State", tenant.State );
            sqlCommand.Parameters.AddWithValue( "@Zip", tenant.Zip );
            sqlCommand.Parameters.AddWithValue( "@StatusTypeID", tenant.StatusTypeID );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            sqlConnection.Open();

            returnValue = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static void tenantInsert ( Tenant tenant, String employeeEmail ) {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spTenantInsert";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@FirstName", tenant.FirstName );
            sqlCommand.Parameters.AddWithValue( "@LastName", tenant.LastName );
            sqlCommand.Parameters.AddWithValue( "@Email", tenant.Email );
            sqlCommand.Parameters.AddWithValue( "@Phone", tenant.Phone );
            sqlCommand.Parameters.AddWithValue( "@City", tenant.City );
            sqlCommand.Parameters.AddWithValue( "@State", tenant.State );
            sqlCommand.Parameters.AddWithValue( "@Zip", tenant.Zip );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            // Because we need to specify output we create the parameter diferently
            sqlCommand.Parameters.Add( "@ID", SqlDbType.Int ).Direction = ParameterDirection.Output;
            sqlCommand.Parameters["@ID"].Value = tenant.Id;


            sqlConnection.Open();

            returnValue = sqlCommand.ExecuteNonQuery();

            // Get the ID if the record inserted.  Note Currently we are not using it.
            // This is just an example of how to use an OUTPUT Parameter
            int tenantID = (int)sqlCommand.Parameters["@ID"].Value;

            sqlConnection.Close();
        }



        // **** Property ***************************************************************

        public static Property getPropertyFromID ( int id, String employeeEmail ) {
            // Create property object using the Zero parameter constructor
            Property property = new Property();

            // Set the values of the property object
            // uncomment and fix

            // Get data from the database and fill the listbox.
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader dataReader;


            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spPropertySelect";
            sqlCommand.Connection = sqlConnection;

            // Set the parameters for the Stored Procedure, notice both a name "@ID" and value
            sqlCommand.Parameters.AddWithValue( "@ID", id );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            // Open connection to the SQL Server
            sqlConnection.Open();

            // Try and get the data
            dataReader = sqlCommand.ExecuteReader();

            // If we have a record, set the fields into the Property object
            if (dataReader.Read()) {
                property.Id = (int)dataReader["ID"];
                property.Address = dataReader["Address"].ToString();
                property.City = dataReader["City"].ToString();
                property.State = dataReader["State"].ToString();
                property.Zip = dataReader["Zip"].ToString();
                property.Price = (Decimal)dataReader["Price"];
                property.DescriptionBrief = dataReader["DescriptionBrief"].ToString();
                property.DescriptionFull = dataReader["DescriptionFull"].ToString();
                property.Bedrooms = (byte)dataReader["Bedrooms"];
                property.Bathrooms = (byte)dataReader["Bathrooms"];
                property.OnsiteParking = (bool)dataReader["OnsiteParking"];
                property.OnsiteLaundry = (bool)dataReader["OnsiteLaundry"];
                property.AvailableOn = (DateTime)dataReader["DateAvailable"]; // StatusTypeID is a SQL tinyInt = C# byte
                property.DateAdded = (DateTime)dataReader["DateAdded"];
                property.DateUpdated = (DateTime)dataReader["DateUpdated"];
                property.StatusTypeID = (byte)dataReader["StatusTypeID"];
                property.EmployeeEmail = dataReader["AddedByEmployeeID"].ToString();
                //property.AddedByName = dataReader["AddedByName"].ToString();
            }

            sqlConnection.Close();

            return property;
        }

        public static void loadPropertyRecordsToListBox ( ListBox listBox, byte statusTypeID, String employeeEmail, bool ignoreStatusType = false ) {
            // listBox is the name of the listBox you will to fill with the results
            // statusTypeID 1=Active, 2 = Cancelled
            // employeeEmail is your email address stored in your defaults, this is used to show only your records
            // Also Andy's records will be shown

            // Get data from the database and fill the listbox.
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader dataReader;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spPropertyList";
            sqlCommand.Parameters.AddWithValue( "@StatusTypeID", statusTypeID );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );
            sqlCommand.Parameters.AddWithValue( "@IgnoreStatusType", ignoreStatusType );

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            dataReader = sqlCommand.ExecuteReader();

            Property property;  // Delcare a property object, but we will create new instanced of it in the while loop
            while (dataReader.Read()) {
                // Create new property object using the 3 parameter constructor
                // This might not work on your project, if not, add a three parameter constructor, or add the the fields one at a time

                property = new Property();

                property.Id = (int)dataReader["ID"];
                property.Address = dataReader["Address"].ToString();
                property.City = dataReader["City"].ToString();
                property.State = dataReader["State"].ToString();
                property.DateAdded = (DateTime)dataReader["DateAdded"];
                property.StatusTypeID = (byte)dataReader["StatusTypeID"];

                // Add the proerty object to the listbox (which was passed to this method, passed by reference, not by value)
                listBox.Items.Add( property );

            }

            sqlConnection.Close();
        }

        public static void propertyUpdate ( Property property, String employeeEmail ) {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spPropertyUpdate";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@ID", property.Id );
            sqlCommand.Parameters.AddWithValue( "@Address", property.Address );
            sqlCommand.Parameters.AddWithValue( "@City", property.City );
            sqlCommand.Parameters.AddWithValue( "@State", property.State );
            sqlCommand.Parameters.AddWithValue( "@Zip", property.Zip );
            sqlCommand.Parameters.AddWithValue( "@Price", property.Price );
            sqlCommand.Parameters.AddWithValue( "@DescriptionBrief", property.DescriptionBrief );
            sqlCommand.Parameters.AddWithValue( "@DescriptionFull", property.DescriptionFull );
            sqlCommand.Parameters.AddWithValue( "@Bathrooms", property.Bathrooms );
            sqlCommand.Parameters.AddWithValue( "@Bedrooms", property.Bedrooms );
            sqlCommand.Parameters.AddWithValue( "@OnsiteParking", property.OnsiteParking );
            sqlCommand.Parameters.AddWithValue( "@OnsiteLaundry", property.OnsiteLaundry );
            sqlCommand.Parameters.AddWithValue( "@DateAvailable", property.AvailableOn );
            sqlCommand.Parameters.AddWithValue( "@StatusTypeID", property.StatusTypeID );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            sqlConnection.Open();

            returnValue = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static void propertyInsert ( Property property, String employeeEmail ) {

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spPropertyInsert";
            sqlCommand.Connection = sqlConnection;


            sqlCommand.Parameters.AddWithValue( "@Address", property.Address );
            sqlCommand.Parameters.AddWithValue( "@City", property.City );
            sqlCommand.Parameters.AddWithValue( "@State", property.State );
            sqlCommand.Parameters.AddWithValue( "@Zip", property.Zip );
            sqlCommand.Parameters.AddWithValue( "@Price", property.Price );
            sqlCommand.Parameters.AddWithValue( "@DescriptionBrief", property.DescriptionBrief );
            sqlCommand.Parameters.AddWithValue( "@DescriptionFull", property.DescriptionFull );
            sqlCommand.Parameters.AddWithValue( "@Bathrooms", property.Bathrooms );
            sqlCommand.Parameters.AddWithValue( "@Bedrooms", property.Bedrooms );
            sqlCommand.Parameters.AddWithValue( "@OnsiteParking", property.OnsiteParking );
            sqlCommand.Parameters.AddWithValue( "@OnsiteLaundry", property.OnsiteLaundry );
            sqlCommand.Parameters.AddWithValue( "@DateAvailable", property.AvailableOn );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            // Because we need to specify output we create the parameter diferently
            sqlCommand.Parameters.Add( "@ID", SqlDbType.Int ).Direction = ParameterDirection.Output;
            sqlCommand.Parameters["@ID"].Value = property.Id;


            sqlConnection.Open();

            returnValue = sqlCommand.ExecuteNonQuery();

            // Get the ID if the record inserted.  Note Currently we are not using it.
            // This is just an example of how to use an OUTPUT Parameter
            int propertyID = (int)sqlCommand.Parameters["@ID"].Value;

            sqlConnection.Close();
        }



        // **** Lease ***************************************************************

        public static void loadLeaseRecordsToListBox ( ListBox listBox, byte statusTypeID, String employeeEmail , bool ignoreStatusType = false) {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader dataReader;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spLeaseList";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@StatusTypeID", statusTypeID );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );
            sqlCommand.Parameters.AddWithValue( "@IgnoreStatusType", ignoreStatusType );

            sqlConnection.Open();

            dataReader = sqlCommand.ExecuteReader();

            Lease lease;

            while (dataReader.Read()) {
                lease = new Lease();

                lease.Id = (int)dataReader["ID"];
                lease.Description = dataReader["LeaseDescription"].ToString();
                lease.PropertyID = (int)dataReader["PropertyID"];
                lease.TenantID = (int)dataReader["TenantID"];
                lease.StatusTypeID = (byte)dataReader["StatusTypeID"];


                listBox.Items.Add( lease );
            }

            sqlConnection.Close();
        }

        public static void leaseInsert ( Lease lease, String employeeEmail ) {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spLeaseInsert";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@PropertyID", lease.PropertyID );
            sqlCommand.Parameters.AddWithValue( "@TenantID", lease.TenantID);
            sqlCommand.Parameters.AddWithValue( "@Description", lease.Description);
            sqlCommand.Parameters.AddWithValue( "@DateLeaseStarts", lease.DateLeaseStarts);
            sqlCommand.Parameters.AddWithValue( "@DateLeaseEnds", lease.DateLeaseEnds);
            sqlCommand.Parameters.AddWithValue( "@MonthlyRate", lease.MonthlyRate);
            sqlCommand.Parameters.AddWithValue( "@Notes", lease.Notes);
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail);

            sqlCommand.Parameters.Add( "@ID", SqlDbType.Int ).Direction = ParameterDirection.Output;
            sqlCommand.Parameters["@ID"].Value = lease.Id;

            sqlConnection.Open();

            returnValue = sqlCommand.ExecuteNonQuery();

            //example output parameter

            int leaseID = (int)sqlCommand.Parameters["@ID"].Value;

            sqlConnection.Close();
        }

        public static void leaseUpdate ( Lease lease, String employeeEmail ) {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            int returnValue = 0;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spLeaseUpdate";
            sqlCommand.Connection = sqlConnection;

            sqlCommand.Parameters.AddWithValue( "@ID", lease.Id );
            sqlCommand.Parameters.AddWithValue( "@Description", lease.Description );
            sqlCommand.Parameters.AddWithValue( "@DateLeaseStarts", lease.DateLeaseStarts );
            sqlCommand.Parameters.AddWithValue( "@DateLeaseEnds", lease.DateLeaseEnds );
            sqlCommand.Parameters.AddWithValue( "@MonthlyRate", lease.MonthlyRate );
            sqlCommand.Parameters.AddWithValue( "@Notes", lease.Notes );
            sqlCommand.Parameters.AddWithValue( "@StatusTypeID", lease.StatusTypeID );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            sqlConnection.Open();

            returnValue = sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static Lease getLeaseFromID ( int id, String employeeEmail ) {
            // Create property object using the Zero parameter constructor
            Lease lease = new Lease();


            // Get data from the database and fill the listbox.
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = SlumLordRentalSQL.ConnectionString;

            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader dataReader;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "spLeaseSelect";
            sqlCommand.Connection = sqlConnection;

            // Set the parameters for the Stored Procedure, notice both a name "@ID" and value
            sqlCommand.Parameters.AddWithValue( "@ID", id );
            sqlCommand.Parameters.AddWithValue( "@EmployeeEmail", employeeEmail );

            // Open connection to the SQL Server
            sqlConnection.Open();

            // Try and get the data
            dataReader = sqlCommand.ExecuteReader();

            // If we have a record, set the fields into the Tenant object
            if (dataReader.Read()) {
                // Set the values of the property object
                lease.Id = (int)dataReader["ID"];
                lease.PropertyID = (int)dataReader["PropertyID"];
                lease.TenantID = (int)dataReader["TenantID"];
                lease.DateLeaseStarts = (DateTime)dataReader["DateLeaseStarts"];
                lease.DateLeaseEnds = (DateTime)dataReader["DateLeaseEnds"];
                lease.MonthlyRate = (Decimal)dataReader["MonthlyRate"];
                lease.Notes = dataReader["Notes"].ToString();
                lease.Description = dataReader["Description"].ToString();
                lease.StatusTypeID = (byte)dataReader["StatusTypeID"];
                lease.DateAdded = (DateTime)dataReader["DateAdded"];
                lease.DateUpdated = (DateTime)dataReader["DateUpdated"];
                lease.EmployeeEmail = (int)dataReader["AddedByEmployeeID"];
                //  lease.AddedByName = dataReader["AddedByName"].ToString();
            }

            sqlConnection.Close();

            return lease;
        }


    }  // End of Class
}