namespace ApiIngresso.Domain
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }        
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
