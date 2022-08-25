using ApiIngresso.Application.Interfaces;
using ApiIngresso.Application.Util;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiIngresso.Web.Controllers
{
    public class AdminController : MainController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService, INotificador notificador) : base(notificador)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Admin>> InserirUsuarioPadrao(AdminLoginDto dados)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var admin = await _adminService.Login(dados.Login, dados.Senha);
            if (admin == null) return CustomResponse(admin, 400, "Login ou senha inválida");

            string Token = Utilitarios.GenerateToken(admin.IdAdmin, "Adm", 0, admin.Login);
            admin.Senha = "";

            return CustomResponse(new { Token, Nome = admin.Login, Papel = "Adm", Id = admin.IdAdmin });
        }

        [HttpGet("buscacep/{cep}")]
        public async Task<ActionResult<ViaCepDto>> BuscaCep(string cep)
        {
            string url = string.Format("https://viacep.com.br/ws/{0}/json/", cep);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            ViaCepDto? viacep =
                JsonSerializer.Deserialize<ViaCepDto>(responseBody);

            return CustomResponse(viacep);
        }


    }
}
