using FluentValidation;

namespace ApiIngresso.Web.FluentValidation.Empresa
{
    public class EmpresaInsertDtoValidation : AbstractValidator<Domain.DTO.DtoEmpresa.EmpresaInsertDto>
    {
        public EmpresaInsertDtoValidation()
        {
            RuleFor(x => x.CEP).MinimumLength(8).MaximumLength(8).WithMessage("nulo ou inválido");
            RuleFor(x => x.Cidade).MinimumLength(3).WithMessage("Cidade obrigatória");
            RuleFor(x => x.Cidade).MaximumLength(50).WithMessage("Campo Cidade excede 50 caracteres");
            RuleFor(x => x.Logradouro).MaximumLength(200).WithMessage("Campo Logradouro excede 200 caracteres");
            RuleFor(x => x.UF).MinimumLength(2).MaximumLength(2).WithMessage("UF nula ou inválido");
            RuleFor(x => x.Bairro).MinimumLength(2).MaximumLength(50).WithMessage("Campo Bairro, mínimo de 2 e máximo de 50 caracteres");
            RuleFor(x => x.Numero).MaximumLength(10).WithMessage("Campo Número excede 10 caracteres");
            RuleFor(x => x.Telefone).MinimumLength(10).MaximumLength(11).WithMessage("Telefone nulo ou inválido");
        }
    }
}
