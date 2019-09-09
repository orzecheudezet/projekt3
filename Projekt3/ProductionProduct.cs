using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt3
{
    [Table("Production.Product")]
    public class ProductionProduct
    {
        [Key]
        public int ProductID { get; set; }
        public String Name { get; set; }
        public String ProductNumber { get; set; }
        public String Color { get; set; }
        public short SafetyStockLevel { get; set; }
        [Column(TypeName = "money")]
        public decimal StandardCost { get; set; }
        [Column(TypeName = "money")]
        public decimal ListPrice { get; set; }
        public String Size { get; set; }

        [ForeignKey("ProductSubcategory")]
        public int? ProductSubcategoryID { get; set; }
        public ProductionProductSubcategory ProductSubcategory { get; set; }

    }
}
