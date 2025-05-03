using Aula06.Application.Services;
using Aula06.Domain.Entities;
using Aula06.Domain.Interfaces;
using Aula06.Models.Usuario;
using Aula06.Response.Usuario;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aula06.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<BaseResponse<UsuarioResponse>>> Login(LoginRequest login)
        {
            try
            {
                Usuario? usuario = await _usuarioService.LogarAsync(login.Email, login.Senha);
                if (usuario == null)
                    return BadRequest(new BaseResponse<UsuarioResponse>());

                // Aqui você pode fazer a conversão do usuário para o formato esperado na resposta
                var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);

                return Ok(new BaseResponse<UsuarioResponse> { Data = usuarioResponse });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse<UsuarioResponse>
                {
                    Message = "Ocorreu um erro ao tentar realizar o login.",
                    Errors = new List<string> { ex.Message }
                });
            }
        }
    }
}
