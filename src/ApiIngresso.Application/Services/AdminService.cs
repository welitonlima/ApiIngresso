using ApiIngresso.Application.Interfaces;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using System.Threading.Tasks;

namespace ApiIngresso.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _AdminRepository;

        public AdminService(IAdminRepository AdminRepository)
        {
            _AdminRepository = AdminRepository;
        }

        public async Task<Admin> Login(string Login, string Senha)
        {
            Senha = Util.Utilitarios.getHashSha256(Senha);
            return await _AdminRepository.Login(Login, Senha);
        }
    }
}
