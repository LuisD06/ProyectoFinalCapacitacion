using System.ComponentModel.DataAnnotations;
namespace Curso.ECommerce.Domain.Models
{
    public class Product
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string? Notes { get; set; }
        public DateTime? Expiration { get; set; }
        [Required]
        public int? Stock { get; set; }
        [Required]
        public bool HasTax { get; set; }

        [Required]
        public string BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public string ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}