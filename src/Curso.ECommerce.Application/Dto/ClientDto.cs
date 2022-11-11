using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Application.Dto
{
    public class ClientDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? Identification { get; set; }
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


        public ICollection<string>? Orders { get; set; }
    }
}