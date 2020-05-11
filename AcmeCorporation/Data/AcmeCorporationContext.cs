using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorporation.Models
{
    public class AcmeCorporationContext : DbContext
    {
        public AcmeCorporationContext (DbContextOptions<AcmeCorporationContext> options)
            : base(options)
        {
        }

        public DbSet<AcmeCorporation.Models.Submission> Submission { get; set; }
    }
}
