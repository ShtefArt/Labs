using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab4.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        [Display(Name = "Послуга")]
        public int? ServiceId { get; set; }
        [Display(Name = "Квартира")]
        public int? ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Services Service { get; set; }

        public override string ToString()
        {
            StringBuilder payment = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(Service)
                .Append("   ")
                .Append(Apartment);
            return payment.ToString();
        }
    }
}
