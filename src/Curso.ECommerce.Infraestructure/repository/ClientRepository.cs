using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Curso.ECommerce.Infraestructure.Repository
{
    public class ClientRepository : EfRepository<Client, Guid>, IClientRepository
    {
        public ClientRepository(ECommerceDbContext context) : base(context)
        {
        }

        public async Task<bool> ClientExist(string clientName)
        {
            var response = await this.context.Set<Client>()
                           .AnyAsync(c => c.Name.ToUpper() == clientName.ToUpper());

            return response;
        }

        public async Task<bool> ClientExist(string clientName, Guid clientId)
        {
            var query = this.context.Set<Client>()
                           .Where(c => c.Id != clientId)
                           .Where(c => c.Name.ToUpper() == clientName.ToUpper())
                           ;

            var response = await query.AnyAsync();

            return response;
        }

        public async Task<bool> IdentificationExist(string clientIdentification)
        {
            var response = await this.context.Set<Client>()
                .AnyAsync(c => c.Identification.ToLower() == clientIdentification.ToUpper());
            return response;
        }
        public async Task<bool> IdentificationExist(string clientIdentification, Guid clientId)
        {
            var query = this.context.Set<Client>()
                           .Where(c => c.Id != clientId)
                           .Where(c => c.Identification.ToUpper() == clientIdentification.ToUpper())
                           ;

            var response = await query.AnyAsync();

            return response;
        }
    }
}