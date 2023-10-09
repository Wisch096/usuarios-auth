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

        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastraUsuario
            (CreateUsuarioDto dto)
        {
            await _usuarioService.CadastraUsuario(dto);
            return Ok("Usuário cadastrado!");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            await _usuarioService.Login(dto);
            return Ok("Usuário autenticado!");
        }
    }
}