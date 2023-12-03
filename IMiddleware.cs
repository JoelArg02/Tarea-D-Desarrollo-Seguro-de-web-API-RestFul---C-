using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class CustomMiddleware : IMiddleware
{
    private readonly ILogger<CustomMiddleware> _logger;

    public CustomMiddleware(ILogger<CustomMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Puedes agregar código personalizado aquí antes de que la solicitud alcance los controladores.
        _logger.LogInformation("Middleware processing before reaching controllers.");

        await next(context); // Esto pasa la solicitud al siguiente middleware o al controlador.

        // Puedes agregar código personalizado aquí después de que la solicitud haya sido procesada por los controladores.
        _logger.LogInformation("Middleware processing after reaching controllers.");
    }
}
