using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoFuncionario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Application.Interfaces
{
    public interface IFuncionarioService
    {
        Task<Funcionario> Obter(int IdFuncionario);
        Task<List<FuncionarioDto>> Listar(int IdEmpresa, int pagina, int rows);
        Task<bool> Inserir(FuncionarioInsertDto dados);
        Task<bool> Alterar(FuncionarioUpdateDto dados);
        Task<bool> Remover(int IdFuncionario);
    }
}
