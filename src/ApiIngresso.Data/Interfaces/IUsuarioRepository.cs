using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoUsuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Login(string Login, string Senha);        
        Task<Usuario> Obter(int IdUsuario);
        Task<List<UsuarioDto>> Listar(int IdEmpresa, int pagina, int rows);
        Task<bool> Inserir(Usuario dados);
        Task<bool> Alterar(Usuario dados);
        Task<bool> Remover(int IdUsuario);
        Task<bool> Existe(string Login);
    }
}
