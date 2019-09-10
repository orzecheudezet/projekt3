using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt3
{
    [Table("Production.ProductSubcategory")]
    public class ProductionProductSubcategory
    {
        [Key]
        public int ProductSubcategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<ProductionProduct> ProductionProducts { get; set; }
    }
}
