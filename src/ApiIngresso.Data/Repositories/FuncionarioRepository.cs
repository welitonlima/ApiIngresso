using ApiIngresso.Data.Context;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoFuncionario;
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
    public class FuncionarioRepository : DbContext, IFuncionarioRepository
    {
        public FuncionarioRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<Funcionario> Obter(int IdFuncionario)
        {
            IEnumerable<Funcionario> lista;
            string sql = @" SELECT * FROM Funcionario WHERE IdFuncionario=@IdFuncionario ";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<Funcionario>(sql, new { IdFuncionario });

            return lista.FirstOrDefault();
        }

        public async Task<List<FuncionarioDto>> Listar(int IdEmpresa, int pagina, int rows)
        {
            IEnumerable<FuncionarioDto> lista;
            //string sql = @" SELECT * FROM Funcionario WHERE IdEmpresa=@IdEmpresa ";

            string offSet = "0";
            if (pagina > 0) offSet = ((pagina - 1) * rows).ToString();

            var sql = new StringBuilder();
            sql.AppendLine("DECLARE @q AS INTEGER SET @q= (SELECT count(IdFuncionario) FROM Funcionario WHERE IdEmpresa=@IdEmpresa);");
            sql.AppendLine("SELECT *, @q as TotalRows, @pagina as Pagina, @rows as Rows FROM Funcionario");
            sql.AppendLine("WHERE IdEmpresa=@IdEmpresa");
            sql.AppendLine("ORDER BY Nome");
            sql.AppendLine(string.Format("OFFSET {0} ROWS", offSet));
            sql.AppendLine(string.Format("FETCH NEXT {0} ROWS ONLY", rows));

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<FuncionarioDto>(sql.ToString(), new { IdEmpresa, pagina, rows });

            return lista.ToList();
        }

        public async Task<bool> Inserir(Funcionario dados)
        {
            try
            {
                string sql = @" INSERT INTO Funcionario (IdEmpresa, Nome, Cargo, Salario)
                                VALUES (@IdEmpresa, @Nome, @Cargo, @Salario) ";

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

        public async Task<bool> Alterar(Funcionario dados)
        {
            try
            {
                string sql = @" UPDATE Funcionario
                                   SET IdEmpresa=@IdEmpresa, Nome=@Nome, Cargo=@Cargo, Salario=@Salario
                                 WHERE IdFuncionario=@IdFuncionario ";

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

        public async Task<bool> Remover(int IdFuncionario)
        {
            try
            {
                string sql = @" DELETE FROM Funcionario WHERE IdFuncionario=@IdFuncionario ";

                bool rows = false;
                using (var con = new SqlConnection(this.GetConnection()))
                {
                    var x = await con.ExecuteAsync(sql, new { IdFuncionario });
                    rows = x > 0;
                }

                return rows;
            }
            catch
            {
                return false;
            }
        }
    }
}
