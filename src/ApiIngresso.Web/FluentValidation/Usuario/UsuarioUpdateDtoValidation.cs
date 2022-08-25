using ApiIngresso.Domain.DTO.DtoUsuario;
using FluentValidation;

namespace ApiIngresso.Web.FluentValidation.Usuario
{
    public class UsuarioUpdateDtoValidation : AbstractValidator<UsuarioUpdateDto>
    {
        public UsuarioUpdateDtoValidation()
        {
            RuleFor(x => x.IdUsuario).GreaterThan(0).WithMessage("IdUsuario obrigatório");
            RuleFor(x => x.IdEmpresa).GreaterThan(0).WithMessage("IdEmpresa obrigatório");
            RuleFor(x => x.Nome).MinimumLength(3).MaximumLength(300).WithMessage("Campo Nome, mínimo de 3 e máximo de 200 caracteres");
            RuleFor(x => x.Login).MinimumLength(6).MaximumLength(50).WithMessage("Campo Login, mínimo de 6 e máximo de 50 caracteres");
            //RuleFor(x => x.Senha).MinimumLength(6).MaximumLength(12).WithMessage("Campo Senha, mínimo de 6 e máximo de 12 caracteres");
        }
    }
}
