using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ECommerce.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService service;
        public UserController(IUserAppService service)
        {
            this.service = service;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> LoginAsync(UserLoginDto user)
        {
            return await service.LoginAsync(user);
        }
    }
}