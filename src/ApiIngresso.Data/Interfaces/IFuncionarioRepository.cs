using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoFuncionario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Data.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario> Obter(int IdFuncionario);
        Task<List<FuncionarioDto>> Listar(int IdEmpresa, int pagina, int rows);
        Task<bool> Inserir(Funcionario dados);
        Task<bool> Alterar(Funcionario dados);
        Task<bool> Remover(int IdFuncionario);
    }
}
