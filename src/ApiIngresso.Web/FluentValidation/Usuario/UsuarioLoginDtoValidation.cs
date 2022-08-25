using ApiIngresso.Domain.DTO.DtoUsuario;
using FluentValidation;

namespace ApiIngresso.Web.FluentValidation.Usuario
{
    public class UsuarioLoginDtoValidation : AbstractValidator<UsuarioLoginDto>
    {
        public UsuarioLoginDtoValidation()
        {
            RuleFor(x => x.Login).MinimumLength(6).MaximumLength(50).WithMessage("Campo Login, mínimo de 6 e máximo de 50 caracteres");
            RuleFor(x => x.Senha).MinimumLength(6).MaximumLength(12).WithMessage("Campo Senha, mínimo de 6 e máximo de 12 caracteres");
        }
    }
}
