using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3.Models
{
    public partial class Tenants
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public long? AccountNumber { get; set; }
        public int? ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }

        public override string ToString()
        {
            StringBuilder tenant = new StringBuilder();
            tenant.Append(Id)
                .Append("   ")
                .Append(FirstName)
                .Append("   ")
                .Append(FatherName)
                .Append("   ")
                .Append(LastName)
                .Append("   ")
                .Append(AccountNumber)
                .Append("   ")
                .Append(Apartment);
            return tenant.ToString();
        }
    }
}
