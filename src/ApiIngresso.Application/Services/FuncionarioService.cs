using ApiIngresso.Application.Interfaces;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoFuncionario;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Application.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _FuncionarioRepository;
        private readonly IMapper _mapper;
        public FuncionarioService(IFuncionarioRepository FuncionarioRepository, IMapper mapper)
        {
            _FuncionarioRepository = FuncionarioRepository;
            _mapper = mapper;
        }

        public async Task<Funcionario> Obter(int IdFuncionario)
        {
            return await _FuncionarioRepository.Obter(IdFuncionario);
        }

        public async Task<List<FuncionarioDto>> Listar(int IdEmpresa, int pagina, int rows)
        {
            return await _FuncionarioRepository.Listar(IdEmpresa, pagina, rows);
        }

        public async Task<bool> Inserir(FuncionarioInsertDto dados)
        {
            var form = _mapper.Map<Funcionario>(dados);
            return await _FuncionarioRepository.Inserir(form);
        }

        public async Task<bool> Alterar(FuncionarioUpdateDto dados)
        {
            var form = _mapper.Map<Funcionario>(dados);
            return await _FuncionarioRepository.Alterar(form);
        }

        public async Task<bool> Remover(int IdFuncionario)
        {
            return await _FuncionarioRepository.Remover(IdFuncionario);
        }
    }
}
