using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain;

namespace Curso.ECommerce.Application.Dto
{
    public class ProductTypeCreateUpdateDto
    {
        
        [Required]
        [MaxLength(PropertySettings.NAME_MAX_LENGHT)]
        public string? Name { get; set; }
    }
}