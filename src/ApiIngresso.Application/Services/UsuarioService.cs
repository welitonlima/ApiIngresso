using ApiIngresso.Application.Interfaces;
using ApiIngresso.Data.Interfaces;
using ApiIngresso.Domain;
using ApiIngresso.Domain.DTO.DtoUsuario;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiIngresso.Application.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _UsuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(IUsuarioRepository UsuarioRepository, IMapper mapper)
        {
            _UsuarioRepository = UsuarioRepository;
            _mapper = mapper;
        }

        public async Task<Usuario> Login(string Login, string Senha)
        {
            Senha = Util.Utilitarios.getHashSha256(Senha);
            var usuario = await _UsuarioRepository.Login(Login, Senha);
            if (usuario != null) usuario.Senha = "";
            return usuario;
        }

        public async Task<Usuario> Obter(int IdUsuario)
        {
            return await _UsuarioRepository.Obter(IdUsuario);
        }

        public async Task<List<UsuarioDto>> Listar(int IdEmpresa, int pagina, int rows)
        {
            return await _UsuarioRepository.Listar(IdEmpresa, pagina, rows);
        }

        public async Task<bool> Inserir(UsuarioInsertDto dados)
        {
            var form = _mapper.Map<Usuario>(dados);
            form.Senha = Util.Utilitarios.getHashSha256(dados.Senha);
            return await _UsuarioRepository.Inserir(form);
        }

        public async Task<bool> Alterar(UsuarioUpdateDto dados)
        {
            var form = _mapper.Map<Usuario>(dados);
            return await _UsuarioRepository.Alterar(form);
        }

        public async Task<bool> Remover(int IdUsuario)
        {
            return await _UsuarioRepository.Remover(IdUsuario);
        }

        public async Task<bool> Existe(string Login)
        {
            return await _UsuarioRepository.Existe(Login);
        }
    }
}
