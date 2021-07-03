using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName ="varchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

        //[ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        //reference navigation property
        public Category Category { get; set; }
    }
}
