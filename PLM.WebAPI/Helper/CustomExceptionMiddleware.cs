using PLM.Entities.ValueObjects;

namespace PLM.WebAPI.Helper;
public class CustomExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "Ocurrió un error inesperado.";

        if (exception is JsonException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = "El formato del JSON enviado es incorrecto.";
        }
        else if (exception is ArgumentException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = "Uno o más parámetros proporcionados son inválidos.";
        }
        // Puedes agregar más excepciones específicas aquí
        OperationResponse response = new()
        {
            Code = -5,
            Message = message,
            Content = []
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var jsonResponse = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
}