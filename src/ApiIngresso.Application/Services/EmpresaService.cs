using ApiIngresso.Application.Interfaces;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoEmpresa;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _EmpresaRepository;
        private readonly IMapper _mapper;

        public EmpresaService(IEmpresaRepository EmpresaRepository, IMapper mapper)
        {
            _EmpresaRepository = EmpresaRepository;
            _mapper = mapper;
        }

        public Task<bool> Alterar(EmpresaUpdateDto dados)
        {
            return _EmpresaRepository.Alterar(dados);
        }

        public Task<bool> Inserir(EmpresaInsertDto dados)
        {
            var form = _mapper.Map<Domain.Empresa>(dados);
            return _EmpresaRepository.Inserir(form);
        }

        public Task<List<EmpresaDto>> Listar(int pagina, int rows)
        {
            return _EmpresaRepository.Listar(pagina, rows);
        }

        public Task<Empresa> Obter(int IdEmpresa)
        {
            return _EmpresaRepository.Obter(IdEmpresa);
        }

        public Task<bool> Remover(int IdEmpresa)
        {
            return _EmpresaRepository.Remover(IdEmpresa);
        }

        public async Task<bool> Existe(string Nome)
        {
            return await _EmpresaRepository.Existe(Nome);
        }

        public async Task<bool> ExisteUsuarioVinculado(int IdEmpresa)
        {
            return await _EmpresaRepository.ExisteUsuarioVinculado(IdEmpresa);
        }

        public async Task<bool> ExisteFuncionarioVinculado(int IdEmpresa)
        {
            return await _EmpresaRepository.ExisteFuncionarioVinculado(IdEmpresa);
        }
    }
}
