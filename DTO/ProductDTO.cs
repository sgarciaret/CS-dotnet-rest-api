using System.ComponentModel.DataAnnotations;

namespace ApiRest.DTO
{
    public class ProductDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string SKU { get; init; }
    }
}
