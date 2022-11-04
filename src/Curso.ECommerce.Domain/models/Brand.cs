using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain.models
{
    public class Brand
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
        
    }
}