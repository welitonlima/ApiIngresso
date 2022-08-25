using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoEmpresa;
using ApiIngresso.Domain.DTO.DtoFuncionario;
using ApiIngresso.Domain.DTO.DtoUsuario;
using AutoMapper;

namespace ApiIngresso.Web.Mapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<EmpresaInsertDto, Empresa>().ReverseMap();
            CreateMap<EmpresaUpdateDto, Empresa>().ReverseMap();
            CreateMap<UsuarioUpdateDto, Usuario>().ReverseMap();
            CreateMap<UsuarioInsertDto, Usuario>().ReverseMap();
            CreateMap<FuncionarioInsertDto, Funcionario>().ReverseMap();
            CreateMap<FuncionarioUpdateDto, Usuario>().ReverseMap();
        }
    }
}
