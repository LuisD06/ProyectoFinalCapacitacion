using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Domain.Models
{
    public class Client
    {
        [Required]
        [StringLength(32)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Identification { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [Required]
        public string? ZipCode { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Phone]
        public string? Phone { get; set; }

    }
}