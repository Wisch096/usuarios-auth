﻿
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dto;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {

        private CadastroService _cadastroService;

        public UsuarioController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }


        [HttpPost]
        public async Task<IActionResult> CadastraUsuario
            (CreateUsuarioDto dto)
        {
            await _cadastroService.CadastraUsuario(dto);
            return Ok("Usuário cadastrado!");

        }
    }
}