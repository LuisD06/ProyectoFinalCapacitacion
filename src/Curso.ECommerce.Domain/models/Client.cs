using System.ComponentModel.DataAnnotations;

namespace Curso.ECommerce.Domain.Models
{
    public class Client
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [Required]
        public string? ZipCode { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }

    }
}