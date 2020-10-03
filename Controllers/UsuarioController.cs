using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DojoNetCore.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace DojoNetCore.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        FireBaseAccount Instancia =FireBaseAccount.instancia;
        [HttpGet]
        public async Task<List<usuario>> Get()
        {
            return await Instancia.GetUser();
        }

        [HttpPost]
        public async Task<String> Post(usuario user)
        {
            return await Instancia.AddUser(user);
        }

        [HttpDelete]
        public async Task<String> Delete([FromQuery]String id)
        {
            return await Instancia.DeleteUser(id);
        }
    }
}