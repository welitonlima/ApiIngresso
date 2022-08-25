using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoUsuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> Login(string Login, string Senha);
        Task<Usuario> Obter(int IdUsuario);
        Task<List<UsuarioDto>> Listar(int IdEmpresa, int pagina, int rows);
        Task<bool> Inserir(UsuarioInsertDto dados);
        Task<bool> Alterar(UsuarioUpdateDto dados);
        Task<bool> Remover(int IdUsuario);
        Task<bool> Existe(string Login);
    }
}
