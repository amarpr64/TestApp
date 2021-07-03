using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Column(TypeName ="varchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

       // [ForeignKey("Category")] //props name
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")] //props name
        public virtual Category Category { get; set; }
    }
}
