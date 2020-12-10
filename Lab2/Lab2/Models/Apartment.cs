using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Lab2.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Payment = new HashSet<Payment>();
            Tenants = new HashSet<Tenants>();
        }

        public int Id { get; set; }
        public float? Area { get; set; }
        public string Address { get; set; }
        public int? TenantsCount { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Tenants> Tenants { get; set; }

        public override string ToString()
        {
            StringBuilder apartment = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(Area)
                .Append("   ")
                .Append(Address)
                .Append("   ")
                .Append(TenantsCount);
            return apartment.ToString();
        }
    }
}
