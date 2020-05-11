using AcmeCorporation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorporation.Data
{
    public class DBInitializer
    {
        public static void Init(AcmeCorporationContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Submission.Any())
            {

            }
        }
    }
}
