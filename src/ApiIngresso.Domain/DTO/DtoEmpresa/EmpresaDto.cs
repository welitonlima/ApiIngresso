namespace ApiIngresso.Domain.DTO.DtoEmpresa
{
    public class EmpresaDto:Empresa
    {
        public int TotalRows { get; set; }
        public int Rows { get; set; }
        public int Pagina { get; set; }
    }
}
