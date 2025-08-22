namespace ApiAppLeon
{
    public class PermissionTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // obtiene el token designado en los parametros del request
            var permissionToken = context.Request.Headers["X-profile-Token"].FirstOrDefault();

            if (string.IsNullOrEmpty(permissionToken))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Permission token is missing.");
                return;
            }

            // Valida el primer token ingresado
            if (ValidatePermissionToken(permissionToken)==false)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid permission token.");
                return;
            }

            // Si el token es válido regresa el contexto obtenido
            await _next(context);
        }

        private bool ValidatePermissionToken(string token)
        {
            if ( token== "prueba123")
            {
                return true;
            }else { 
                return  false;
            }
        }
    }

}
