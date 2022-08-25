using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoEmpresa;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Data.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<Empresa> Obter(int IdEmpresa);
        Task<List<EmpresaDto>> Listar(int pagina, int rows);
        Task<bool> Inserir(Empresa dados);
        Task<bool> Alterar(Empresa dados);
        Task<bool> Remover(int IdEmpresa);
        Task<bool> Existe(string Nome);
        Task<bool> ExisteUsuarioVinculado(int IdEmpresa);
        Task<bool> ExisteFuncionarioVinculado(int IdEmpresa);
    }
}
