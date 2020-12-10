using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab4.Models
{
    public partial class Services
    {
        public Services()
        {
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }
        [Display(Name = "Спосіб оплати")]
        public int? MethodId { get; set; }
        [Display(Name = "Ціна")]
        public decimal? Price { get; set; }
        [Display(Name = "Послуга")]
        public string Service { get; set; }

        public virtual PaymentMethod Method { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }

        public override string ToString()
        {
            StringBuilder service = new StringBuilder()
                .Append(Id)
                .Append("   ")
                .Append(Method)
                .Append("   ")
                .Append(Price)
                .Append("   ")
                .Append(Service);
            return service.ToString();
        }
    }
}
