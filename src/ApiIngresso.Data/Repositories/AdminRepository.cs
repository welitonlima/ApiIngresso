using ApiIngresso.Data.Context;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIngresso.Data.Repositories
{
    public class AdminRepository : DbContext, IAdminRepository
    {
        public AdminRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<Admin> Login(string Login, string Senha)
        {
            IEnumerable<Admin> lista;
            string sql = @" SELECT * FROM Admin WHERE Login=@Login AND Senha=@Senha ";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<Admin>(sql, new { Login, Senha });

            return lista.FirstOrDefault();
        }
    }
}
