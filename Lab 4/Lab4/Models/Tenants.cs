using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab4.Models
{
    public partial class Tenants
    {
        public int Id { get; set; }
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Display(Name = "Побатькові")]
        public string FatherName { get; set; }
        [Display(Name = "№")]
        public long? AccountNumber { get; set; }
        [Display(Name = "Квартира")]
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
