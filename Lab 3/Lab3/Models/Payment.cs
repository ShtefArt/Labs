using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
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
