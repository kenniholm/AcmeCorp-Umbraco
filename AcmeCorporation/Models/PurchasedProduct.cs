using System;
using System.ComponentModel.DataAnnotations;

namespace AcmeCorporation.Models
{
    public class PurchasedProduct
    {
        [Key]
        public int ProductId { get; set; }

        public Guid ProductSerial { get; set; }
    }
}
