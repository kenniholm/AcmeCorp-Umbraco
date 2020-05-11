using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorporation.Models
{
    public class PurchasedProduct
    {
        [Key]
        public int ProductId { get; set; }

        public Guid ProductSerial { get; set; }
    }
}
