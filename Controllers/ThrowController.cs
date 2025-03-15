using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PrimeiraApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController : Controller
    {
        [Route("/error")] // para problemas em produção
        public IActionResult HandleError() =>
            Problem();



        [Route("/error-development")] // para problemas em desenvolvimento
        public IActionResult HandleErrorDevelopment(
        [FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =
                HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }
    }
}
