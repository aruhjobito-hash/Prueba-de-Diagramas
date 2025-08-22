using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;



namespace ApiAppLeon.Controllers.Sistema
{
    [ApiExplorerSettings(GroupName = "Sistemas")]
    [Route("api/Sistemas/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly DBContext _dbContext;
        public JWTAuthController(IConfiguration config, DBContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        private static List<jwtAuth> jwtAuth_ { get; set; } = new List<jwtAuth>();

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<jwtAuth>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<jwtError>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequest loginRequest)
        {
            ConsultaDeudaErrorResponse Error = new ConsultaDeudaErrorResponse();
            //login logic for login process
            jwtAuth jwtAuth_ = new jwtAuth();
            jwtError jwtError_ = new jwtError();
            if (loginRequest == null)
            {
                jwtError_.codigo = "61";
                jwtError_.mensaje = "PARAMETROS VACIOS";
                return Ok(jwtError_);
            }
            try
            {
                if (loginRequest.clave.Length > 6)
                {
                    Error.codigo = "65";
                    Error.mensaje = Error.mensaje + "LA CLAVE DE AUTENTICACION ES DE MAXIMO 6 CARACTERES";
                    return Ok(Error);
                }
                if (loginRequest.usuario != "admin")
                {
                    jwtError_.codigo = "62";
                    jwtError_.mensaje = "USUARIO NO ENCONTRADO";
                    return Ok(jwtError_);
                }
                if (loginRequest.clave != "123456")
                {
                    jwtError_.codigo = "63";
                    jwtError_.mensaje = "CLAVE INCORRECTA";
                    return Ok(jwtError_);
                }
                //If login usrename and password are correct then proceed to generate token

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                DateTime dtFrom = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                //DateTime.Now.AddMinutes(1440 - (DateTime.Now.Hour * 60 + DateTime.Now.Minute)), //JosAra 20/08/2024 Validación del tiempo de expieración del token
                var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: dtFrom,
                signingCredentials: credentials);
                jwtAuth_.codigo = "00";
                jwtAuth_.mensaje = "EXITOSO";
                //jwtAuth_.expires_in = DateTime.Now.AddMinutes(340).ToString();
                string horafinaliza;
                //horafinaliza = (1440 - ( DateTime.Now.Hour*60+ DateTime.Now.Minute)).ToString(); //1440                
                horafinaliza = (dtFrom - DateTime.Now).TotalMinutes.ToString().Split('.')[0];
                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                jwtAuth_.expires_in = horafinaliza;
                jwtAuth_.token = token.ToString();
                jwtAuth_.refresh_token = token.ToString();
                //string StoreProc = " exec sp_regLoginKasNet " +
                //                    " @usuario ='" + loginRequest.usuario + "'" +
                //                    ",@clave ='" + loginRequest.clave + "'" +
                //                    ",@iduser ='" + usuario + "'" +
                //                    ",@jwtToken ='" + jwtAuth_.token + "'"
                //    ;
                //List<jwtDBLogin> GetPagos_;
                //GetPagos_ = await _dbContext.jwtRegLogin.FromSqlRaw(StoreProc).ToListAsync();

                //return Ok(token);
                return Ok(jwtAuth_);
            }
            catch (Exception ex)
            {
                Error.mensaje = Error.mensaje + ex;         // Capturamos el error emitido 
                Error.mensaje = Error.mensaje.ToUpper();    // Establecemos el mensaje de error capturado a MAYUSCULAS
                return Ok(Error);
            }
        }
    }
}
