using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Domain.Repository
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
        ///<summary>
        ///Verify if a client name exists in database
        ///</summary>
        Task<bool> ClientExist(string clientName);
        ///<summary>
        ///Verify if a client exists in database, excluding itself
        ///</summary>
        Task<bool> ClientExist(string clientName, Guid clientId);
        ///<summary>
        ///Verify if a client exists in database, 
        ///</summary>
        Task<bool> IdentificationExist(string clientIdentification);
        ///<summary>
        ///Verify if a client exists in database, excluding itself
        ///</summary>
        Task<bool> IdentificationExist(string clientName, Guid clientId);


    }
}