using ApiIngresso.Application.Interfaces;
using ApiIngresso.Application.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace ApiIngresso.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null, int statusCode = 0, string msg = "")
        {
            if (statusCode == 404)
            {
                return NotFound(new
                {
                    success = false,
                    errors = msg == "" ? "Nenhum registro encontrado" : msg
                });
            }

            if (statusCode == 400)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = msg == "" ? result : msg
                });
            }

            if (statusCode == 401)
            {
                return Unauthorized();
            }

            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponseErro(string msg = "", int statusCode = 400)
        {
            if (statusCode == 404)
            {
                return NotFound(new
                {
                    success = false,
                    erro = "Nenhum registro encontrado"
                });
            }

            if (statusCode == 200)
            {
                return Ok(new
                {
                    success = false,
                    erro = msg
                });
            }

            return BadRequest(new
            {
                success = false,
                erro = msg
            });
        }


        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
