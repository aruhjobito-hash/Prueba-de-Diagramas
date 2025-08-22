using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Sistemas
{
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos
    [Keyless]
    public class LoginRequest
    {
        public string? usuario { get; set; }
        public string? clave { get; set; }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}
