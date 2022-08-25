namespace ApiIngresso.Domain.DTO.DtoFuncionario
{
    public class FuncionarioDto: Funcionario
    {
        public int TotalRows { get; set; }
        public int Rows { get; set; }
        public int Pagina { get; set; }
    }
}
