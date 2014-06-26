
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcWorking.Models
{
  

    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(10)]
        public string Description { get; set; }
        public string Image { get; set; }

        public Nullable<int> CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}
