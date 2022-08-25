namespace ApiIngresso.Domain
{
    public class Funcionario
    {
        public int IdFuncionario { get; set; }
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
    }
}
