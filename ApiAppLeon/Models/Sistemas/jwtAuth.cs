using ApiAppLeon.Entidades;
using Microsoft.IdentityModel.Tokens;
namespace ApiAppLeon.Models.Sistemas
{
    public class jwtAuth
    {
        public string? codigo { get; set; }
        public string? mensaje { get; set; }
        public string? expires_in { get; set; }
        //public int? expires_out { get; set; }
        public string? token { get; set; }
        public string? refresh_token { get; set; }
        //public JwtSecurityTokenHandler? token { get; set; }
        //public JwtSecurityTokenHandler? refres_token { get; set; }

    }
    public class jwtError
    {
        public string? codigo { get; set; }
        public string? mensaje { get; set; }
    }
    public class jwtDBLogin
    {
        public string? mensaje { get; set; }
    }

    public class ConsultaDeudaErrorResponse
    {
        public string? codigo { get; set; }
        public string? mensaje { get; set; } = "";
    }

}