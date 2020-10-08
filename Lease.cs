using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sluiter_Asg10 {
    public class Lease : SlumLordRentalBase, ILease {
        public static int leaseObjectCount;

        private int id;
        private int propertyID;
        private int tenantID;
        private string description;
        private DateTime dateLeaseStarts;
        private DateTime dateLeaseEnds;
        private decimal monthlyRate;
        private string notes;
        private byte statusTypeID;

        public int PropertyID { get => propertyID; set => propertyID = value; }
        public int TenantID { get => tenantID; set => tenantID = value; }
        public string Description { get => description; set => description = value; }
        public DateTime DateLeaseStarts { get => dateLeaseStarts; set => dateLeaseStarts = value; }
        public DateTime DateLeaseEnds { get => dateLeaseEnds; set => dateLeaseEnds = value; }
        public decimal MonthlyRate { get => monthlyRate; set => monthlyRate = value; }
        public string Notes { get => notes; set => notes = value; }
        public byte StatusTypeID { get => statusTypeID; set => statusTypeID = value; }

        public Lease () {
            leaseObjectCount++;
            id = leaseObjectCount;
            DateAdded = DateTime.Now;
        }

        public  override string ToString () {
            string info = "";

            info = "Lease ID: " + id.ToString() + ", PropertyID: " + PropertyID.ToString() + ", TenantID: " + TenantID.ToString() + " - " + Description + (StatusTypeID == 1 ? " (Active)" : " (Deleted)");

            return info;
        }
    }
}
