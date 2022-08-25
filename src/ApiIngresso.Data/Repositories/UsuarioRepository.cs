using ApiIngresso.Data.Context;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoUsuario;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiIngresso.Data.Repositories
{
    public class UsuarioRepository : DbContext, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<Usuario> Login(string Login, string Senha)
        {
            IEnumerable<Usuario> lista;
            string sql = @" SELECT * FROM Usuario WHERE Login=@Login and Senha=@Senha ";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<Usuario>(sql, new { Login, Senha });

            return lista.FirstOrDefault();
        }

        public async Task<Usuario> Obter(int IdUsuario)
        {
            IEnumerable<Usuario> lista;
            string sql = @" SELECT * FROM Usuario WHERE IdUsuario=@IdUsuario ";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<Usuario>(sql, new { IdUsuario });

            return lista.FirstOrDefault();
        }

        public async Task<List<UsuarioDto>> Listar(int IdEmpresa, int pagina, int rows)
        {
            IEnumerable<UsuarioDto> lista;

            string offSet = "0";
            if (pagina > 0) offSet = ((pagina - 1) * rows).ToString();

            var sql = new StringBuilder();
            sql.AppendLine("DECLARE @q AS INTEGER SET @q= (SELECT count(IdUsuario) FROM Usuario WHERE IdEmpresa=@IdEmpresa);");
            sql.AppendLine("SELECT *, @q as TotalRows, @pagina as Pagina, @rows as Rows FROM Usuario");
            sql.AppendLine("WHERE IdEmpresa=@IdEmpresa");
            sql.AppendLine("ORDER BY Nome");
            sql.AppendLine(string.Format("OFFSET {0} ROWS", offSet));
            sql.AppendLine(string.Format("FETCH NEXT {0} ROWS ONLY", rows));

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<UsuarioDto>(sql.ToString(), new { IdEmpresa, pagina, rows });

            return lista.ToList();
        }

        public async Task<bool> Inserir(Usuario dados)
        {
            try
            {
                string sql = @" INSERT INTO Usuario(IdEmpresa, Nome, Login, Senha)
                                VALUES (@IdEmpresa, @Nome, @Login, @Senha) ";

                using (var con = new SqlConnection(this.GetConnection()))
                {
                    var x = await con.ExecuteScalarAsync<int>(sql, dados);
                    return x > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Alterar(Usuario dados)
        {
            try
            {
                string sql = @" UPDATE Usuario SET IdEmpresa=@IdEmpresa, Nome=@Nome
                                WHERE IdUsuario=@IdUsuario ";

                using (var con = new SqlConnection(this.GetConnection()))
                {
                    await con.ExecuteAsync(sql, dados);
                }

                return true;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return false;
            }
        }

        public async Task<bool> Remover(int IdUsuario)
        {
            try
            {
                string sql = @" DELETE FROM Usuario WHERE IdUsuario=@IdUsuario ";

                bool rows = false;
                using (var con = new SqlConnection(this.GetConnection()))
                {
                    var x = await con.ExecuteAsync(sql, new { IdUsuario });
                    rows = x > 0;
                }

                return rows;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Existe(string Login)
        {
            IEnumerable<string> lista;
            string sql = @" SELECT top 1 1 FROM Usuario WHERE Login=@Login ";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<string>(sql, new { Login });

            return lista.ToList().Count > 0;
        }
    }
}
