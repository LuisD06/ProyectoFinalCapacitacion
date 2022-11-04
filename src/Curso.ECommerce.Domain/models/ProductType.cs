using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ComercioElectronico.Domain;

namespace Curso.ECommerce.Domain.models
{
    public class ProductType
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
    }
}