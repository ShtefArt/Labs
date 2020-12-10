using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Services = new HashSet<Services>();
        }

        public int Id { get; set; }
        public string Method { get; set; }

        public virtual ICollection<Services> Services { get; set; }

        public override string ToString()
        {
            StringBuilder paymentMethod = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(Method);
            return paymentMethod.ToString();
        }
    }
}
