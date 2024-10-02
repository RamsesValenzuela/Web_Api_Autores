using Microsoft.Extensions.Logging;

namespace Web_Api_Autores.Middleware
{
    public class LoggearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoggearRespuestaHTTPMiddleware> logger;

        public LoggearRespuestaHTTPMiddleware(RequestDelegate siguiente, ILogger<LoggearRespuestaHTTPMiddleware>logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        //invoke or InvokeAsync debe de retornar un task y como parametro un http context 
        public async Task InvokeAsync(HttpContext context)
        {

                using (var ms = new MemoryStream())
                {
                    var cuerpoOriginalResp = context.Response.Body;
                    context.Response.Body = ms;

                    await siguiente(context);

                    ms.Seek(0, SeekOrigin.Begin);
                    string respuesta = new StreamReader(ms).ReadToEnd();
                    ms.Seek(0, SeekOrigin.Begin);
                    await ms.CopyToAsync(cuerpoOriginalResp);
                    context.Response.Body = cuerpoOriginalResp;

                    logger.LogInformation(respuesta);
                };
            
        }
    }
}
