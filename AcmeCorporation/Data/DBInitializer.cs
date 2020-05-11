using AcmeCorporation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorporation.Data
{
    public class DBInitializer
    {
        public static void Init(AcmeCorporationContext context)
        {
            context.Database.EnsureCreated();

            if (!context.PurchasedProduct.Any())
            {
                List<PurchasedProduct> seedProducts = new List<PurchasedProduct>();
                
                for (int i = 0; i < 100; i++)
                {
                    seedProducts.Add(new PurchasedProduct { ProductSerial = Guid.NewGuid()});
                }

                context.AddRange(seedProducts);
                context.SaveChanges();
                
                using (StreamWriter writer = File.CreateText("ProductSerials"))
                {
                    foreach (PurchasedProduct product in seedProducts)
                    {
                        writer.WriteLine(product.ProductSerial.ToString());
                    }
                }
            }
        }
    }
}
