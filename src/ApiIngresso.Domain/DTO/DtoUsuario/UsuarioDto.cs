using System.Collections.Generic;

namespace ApiIngresso.Domain.DTO.DtoUsuario
{
    public class UsuarioDto: Usuario
    {
        public int TotalRows { get; set; }
        public int Rows { get; set; }
        public int Pagina { get; set; }
    }
}
