using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt3
{
    class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public DbSet<ProductSubcategory>
            ProductSubcategories { get; set; }

        public DbSet<ProductionProduct>
            ProductionProducts { get; set; }

    }
}
