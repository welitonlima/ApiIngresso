using ApiIngresso.Application.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoEmpresa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Web.Controllers
{
    
    public class EmpresaController : MainController
    {
        private readonly IEmpresaService _empresaService;
        public EmpresaController(IEmpresaService empresaService, INotificador notificador):base(notificador)
        {
            _empresaService = empresaService;
        }

        [HttpGet("get/{IdEmpresa}")]
        [Authorize(Roles = "Adm,Emp")]
        public async Task<ActionResult<Empresa>> Get(int IdEmpresa)
        {
            var result = await _empresaService.Obter(IdEmpresa);
            return CustomResponse(result);
        }
        
        [HttpGet("list/{Pagina}")]
        [Authorize(Roles = "Adm")]
        public async Task<ActionResult<List<EmpresaDto>>> List(int Pagina)
        {
            int rows = 5;
            var result = await _empresaService.Listar(Pagina, rows);
            return CustomResponse(result);
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Adm")]
        public async Task<ActionResult<bool>> Insert(EmpresaInsertDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var existe = await _empresaService.Existe(dados.Nome);
            if (existe) return CustomResponseErro("Este nome já está sendo utilizado");

            var result = await _empresaService.Inserir(dados);
            return CustomResponse(result);
        }

        [HttpPost("update")]
        [Authorize(Roles = "Adm")]
        public async Task<ActionResult<bool>> Update(EmpresaUpdateDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _empresaService.Alterar(dados);
            return CustomResponse(result);
        }

        [HttpDelete("remove/{IdEmpresa}")]
        [Authorize(Roles = "Adm")]
        public async Task<ActionResult<bool>> Remove(int IdEmpresa)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var existeUsuarioVinculado = await _empresaService.ExisteUsuarioVinculado(IdEmpresa);
            if (existeUsuarioVinculado) return CustomResponseErro("Não é possível remover, Existe usuário vinculado.");

            var existeFuncionariooVinculado = await _empresaService.ExisteFuncionarioVinculado(IdEmpresa);
            if (existeFuncionariooVinculado) return CustomResponseErro("Não é possível remover, Existe funcionário vinculado.");

            var result = await _empresaService.Remover(IdEmpresa);
            return CustomResponse(result);
        }
    }
}
