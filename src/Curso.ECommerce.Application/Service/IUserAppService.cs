using Curso.ECommerce.Application.Dto;

namespace Curso.ECommerce.Application.Service
{
    public interface IUserAppService
    {
        Task<string> LoginAsync(UserLoginDto user);
    }
}