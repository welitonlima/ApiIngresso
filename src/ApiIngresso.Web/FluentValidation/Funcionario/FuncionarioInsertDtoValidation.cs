using ApiIngresso.Domain.DTO.DtoFuncionario;
using FluentValidation;

namespace ApiIngresso.Web.FluentValidation.Funcionario
{
    public class FuncionarioInsertDtoValidation : AbstractValidator<FuncionarioInsertDto>
    {
        public FuncionarioInsertDtoValidation()
        {
            RuleFor(x => x.IdEmpresa).GreaterThan(0).WithMessage("O campo IdEmpresa é obrigatório");
            RuleFor(x => x.Nome).MinimumLength(5).MaximumLength(200).WithMessage("Nome do Funcionário deve conter entre 5 e 200 caracteres");
            RuleFor(x => x.Cargo).MinimumLength(2).MaximumLength(20).WithMessage("Cargo do Funcionário deve conter entre 2 e 20 caracteres");
            RuleFor(x => x.Salario).GreaterThan(0).WithMessage("O campo Salário é obrigatório");
        }
    }
}
