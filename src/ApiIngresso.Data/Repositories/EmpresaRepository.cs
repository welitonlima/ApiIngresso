using ApiIngresso.Data.Context;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoEmpresa;
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
    public class EmpresaRepository : DbContext, IEmpresaRepository
    {
        public EmpresaRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<Empresa> Obter(int IdEmpresa)
        {
            IEnumerable<Empresa> lista;
            string sql = @" SELECT * FROM EMPRESA WHERE IdEmpresa=@IdEmpresa ";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<Empresa>(sql, new { IdEmpresa });

            return lista.FirstOrDefault();
        }

        public async Task<List<EmpresaDto>> Listar(int pagina, int rows)
        {
            IEnumerable<EmpresaDto> lista;

            string offSet = "0";
            if (pagina > 0) offSet = ((pagina - 1) * rows).ToString();

            var sql = new StringBuilder();
            sql.AppendLine("DECLARE @q AS INTEGER SET @q= (SELECT count(IdEMPRESA) FROM EMPRESA);");
            sql.AppendLine("SELECT *, @q as TotalRows, @pagina as Pagina, @rows as Rows FROM EMPRESA");
            sql.AppendLine("ORDER BY Nome");
            sql.AppendLine(string.Format("OFFSET {0} ROWS", offSet));
            sql.AppendLine(string.Format("FETCH NEXT {0} ROWS ONLY", rows));

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<EmpresaDto>(sql.ToString(), new { pagina, rows });

            return lista.ToList();
        }

        public async Task<bool> Inserir(Empresa dados)
        {
            try
            {
                string sql = @" INSERT INTO Empresa (Nome, Logradouro, Cidade, UF, Numero, Complemento, CEP, Telefone, Bairro)
                                VALUES (@Nome, @Logradouro, @Cidade, @UF, @Numero, @Complemento, @CEP, @Telefone, @Bairro) ";

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

        public async Task<bool> Alterar(Empresa dados)
        {
            try
            {
                string sql = @" UPDATE Empresa
                                   SET Nome=@Nome, Logradouro=@Logradouro, Cidade=@Cidade, UF=@UF, Numero=@Numero, Complemento=@Complemento, CEP=@CEP, Telefone=@Telefone, Bairro=@Bairro
                                 WHERE IdEmpresa=@IdEmpresa ";

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

        public async Task<bool> Remover(int IdEmpresa)
        {
            try
            {
                string sql = @" DELETE FROM Empresa WHERE IdEmpresa=@IdEmpresa ";

                bool rows = false;
                using (var con = new SqlConnection(this.GetConnection()))
                {
                    var x = await con.ExecuteAsync(sql, new { IdEmpresa });
                    rows = x > 0;
                }

                return rows;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Existe(string Nome)
        {
            IEnumerable<string> lista;
            string sql = @" SELECT top 1 1 FROM EMPRESA where Nome=@Nome";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<string>(sql, new { Nome });

            return lista.ToList().Count > 0;
        }

        public async Task<bool> ExisteUsuarioVinculado(int IdEmpresa)
        {
            IEnumerable<string> lista;
            string sql = @" SELECT top 1 1 FROM Usuario where IdEmpresa=@IdEmpresa";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<string>(sql, new { IdEmpresa });

            return lista.ToList().Count > 0;
        }

        public async Task<bool> ExisteFuncionarioVinculado(int IdEmpresa)
        {
            IEnumerable<string> lista;
            string sql = @" SELECT top 1 1 FROM Funcionario where IdEmpresa=@IdEmpresa";

            using (var con = new SqlConnection(this.GetConnection()))
                lista = await con.QueryAsync<string>(sql, new { IdEmpresa });

            return lista.ToList().Count > 0;
        }
    }
}
