using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sample_webapi.Models
{
    public class ProductBindigTarget
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public DateTime MfgDate { get; set; }

        public Product ToProduct() => new Product()
        {
            Name = this.Name,
            Price =this.Price,
            Category = this.Category,
            Description = this.Description,
            MfgDate =  this.MfgDate
        };
    }
}
