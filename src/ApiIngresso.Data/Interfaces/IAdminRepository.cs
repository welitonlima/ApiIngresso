using ApiIngresso.Domain;
using System.Threading.Tasks;

namespace ApiIngresso.Data.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> Login(string Login, string Senha);
    }
}
