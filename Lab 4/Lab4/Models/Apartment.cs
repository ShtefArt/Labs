using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace Lab4.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            Payment = new HashSet<Payment>();
            Tenants = new HashSet<Tenants>();
        }

        public int Id { get; set; }
        [Display(Name = "Площа")]
        public float? Area { get; set; }
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Display(Name = "Проживає осіб")]
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
