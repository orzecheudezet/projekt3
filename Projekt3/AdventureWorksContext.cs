using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt3
{
    class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext()
            : base("name=AdventureWorksContext")
        {
        }

        public DbSet<ProductionProductSubcategory>
            ProductionProductSubcategories { get; set; }

        public DbSet<ProductionProduct>
            ProductionProducts { get; set; }

    }
}
