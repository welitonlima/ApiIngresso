using ApiIngresso.Application.Interfaces;
using ApiIngresso.Application.Util;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Web.Controllers
{
    [Authorize(Roles = "Adm")]
    public class UsuarioController : MainController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEmpresaService _empService;
        public UsuarioController(IUsuarioService usuarioService, IEmpresaService empService, INotificador notificador) : base(notificador)
        {
            _empService = empService;
            _usuarioService = usuarioService;
        }

        [HttpGet("get/{IdUsuario}")]
        public async Task<ActionResult<Usuario>> Get(int IdUsuario)
        {
            var result = await _usuarioService.Obter(IdUsuario);
            return CustomResponse(result);
        }

        [HttpGet("list/{IdEmpresa}/{Pagina}")]
        public async Task<ActionResult<List<Usuario>>> List(int IdEmpresa, int Pagina)
        {
            int rows = 5;
            var result = await _usuarioService.Listar(IdEmpresa, Pagina, rows);
            return CustomResponse(result);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<bool>> Insert(UsuarioInsertDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var existe = await _usuarioService.Existe(dados.Login);
            if (existe) return CustomResponseErro("Este login já está sendo utilizado");

            var result = await _usuarioService.Inserir(dados);
            return CustomResponse(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<bool>> Update(UsuarioUpdateDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _usuarioService.Alterar(dados);
            return CustomResponse(result);
        }        

        [HttpDelete("remove/{IdUsuario}")]
        public async Task<ActionResult<bool>> Remove(int IdUsuario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _usuarioService.Remover(IdUsuario);
            return CustomResponse(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Login(UsuarioLoginDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _usuarioService.Login(dados.Login, dados.Senha);

            if (result == null) return CustomResponse(result, 400, "Login ou senha inválida");

            string Token = Utilitarios.GenerateToken(result.IdUsuario, "Emp", result.IdEmpresa, result.Nome);

            return CustomResponse(new { Token, Nome = result.Nome, Papel = "Emp", Id = result.IdUsuario });
        }
    }
}
