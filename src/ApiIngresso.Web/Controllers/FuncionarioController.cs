using ApiIngresso.Application.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoFuncionario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Web.Controllers
{
    [Authorize]
    public class FuncionarioController : MainController
    {
        private readonly IFuncionarioService _funcionarioService;
        public FuncionarioController(IFuncionarioService funcionarioService, INotificador notificador) : base(notificador)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet("get/{IdFuncionario}")]
        public async Task<ActionResult<Funcionario>> Get(int IdFuncionario)
        {
            var result = await _funcionarioService.Obter(IdFuncionario);
            return CustomResponse(result);
        }

        [HttpGet("list/{IdEmpresa}/{Pagina}")]
        public async Task<ActionResult<List<FuncionarioDto>>> List(int IdEmpresa, int Pagina)
        {
            int rows = 5;
            var result = await _funcionarioService.Listar(IdEmpresa, Pagina, rows);
            return CustomResponse(result);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<bool>> Insert(FuncionarioInsertDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _funcionarioService.Inserir(dados);
            return CustomResponse(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<bool>> Update(FuncionarioUpdateDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _funcionarioService.Alterar(dados);
            return CustomResponse(result);
        }

        [HttpDelete("remove/{IdFuncionario}")]
        public async Task<ActionResult<bool>> Remove(int IdFuncionario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _funcionarioService.Remover(IdFuncionario);
            return CustomResponse(result);
        }
    }
}
