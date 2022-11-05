using System.ComponentModel.DataAnnotations;
namespace Curso.ECommerce.Domain.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Notes { get; set; }
        public DateTime? Expiration { get; set; }
        public int? Stock { get; set; }

        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}