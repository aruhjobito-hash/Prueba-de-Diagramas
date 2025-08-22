using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Clases.Sistema;
using LeonXIIICore.Models.Sistema;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Security.Claims;
using System.Xml;
using Newtonsoft.Json;
using System.Data;
using LeonXIIICore;

namespace LeonXIIICore.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            try
            {
                
                var memberOfList = new List<string>();

                var apiLeon = new APILEON();
                int isAnonymous = 0;
                string url= isAnonymous == 1
                        ? "/api/Sistemas/Login/login/with-credentials"
                        : "/api/Sistemas/Login/login/anonymous";

                // Pasamos los datos y construimos el objeto del request()
                var requestData = new LoginRequest
                {
                    username = credentials.Username,
                    password = isAnonymous == 1 ? credentials.Password : null  // Solo añadirá password en caso se indique 1 
                };

                // Utilizamos la función APILEONPOST (Url , object) y señalamos que lo esperado es una lista List<> del modelo MenuClass
                var activeResponse = await apiLeon.APILEONPOST<dynamic>(url, requestData);
                            
                if (activeResponse != null)
                {
                    // Almacenamos la información relevante otorgada por el Active Directory
                    var userModel = new UserModel{
                        Message = activeResponse.message,
                        Username = activeResponse.username,
                        Role = activeResponse.role,
                        DisplayName = activeResponse.displayName,
                        Email = activeResponse.email
                    };
                    // Steamos la respuesta del endpoint 
                    foreach (var item in activeResponse.memberOf)
                    {

                        var profile = item.ToString().Split(',')[0].Split('=')[1];
                        // Capturamos solo los perfiles los cuales empiezan con la letra P, añadimos OrdinalIgnoreCase (con o sin Mayus) 
                        if (profile.StartsWith("p", StringComparison.OrdinalIgnoreCase))
                        {
                            memberOfList.Add(profile);
                        }
                    }
                    // Añadimos a la variable claims(variable que almacenará toda la información sensible e imporante del usuario logeado)
                    var claims = new List<Claim>
                    {
                        // Agregamos el IdUser 
                        new Claim(ClaimTypes.Name, credentials.Username),
                    };

                    // Añadimos todos los roles obtenidos del active
                    foreach (var role in memberOfList)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    // Llamamos al endpoint para obtener datos del Usuario Logeado
                    requestDatosTrabajadorModel DatosTrabajador = new ();
                    DatosTrabajador.IdUser = credentials.Username;
                    url = "/api/Sistemas/DatosTrabajador";
                    var Trabajador = await apiLeon.APILEONPOST<List<DatosTrabajadorDBModel>>(url, DatosTrabajador);
                    if (Trabajador != null) {
                        foreach(var item in Trabajador)
                        {
                            claims.Add(new Claim(ClaimTypes.UserData, item.Socio));
                            claims.Add(new Claim(ClaimTypes.UserData, item.Cargo));
                            claims.Add(new Claim(ClaimTypes.UserData, item.Area));
                            claims.Add(new Claim(ClaimTypes.UserData, item.Agencia));
                            claims.Add(new Claim(ClaimTypes.UserData, item.IdSocio));
                            claims.Add(new Claim(ClaimTypes.UserData, item.IdPersona));
                            claims.Add(new Claim(ClaimTypes.UserData, item.IdCargo));
                            claims.Add(new Claim(ClaimTypes.UserData, item.IdArea));
                            claims.Add(new Claim(ClaimTypes.UserData, item.IdAgencia));
                        }
                    }
                    else
                    {
                        return LocalRedirect("/login/Invalid credentials");
                    }
                    //Creamos el principal Claim y lo declaramos como Identity de Authenticación
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    //Generamos la cookie. SignInAsync es un método de extensión del contexto para ser utilizada en todo el proyecto.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                }
                else
                {
                    return LocalRedirect("/login/Invalid credentials");
                }                
                //Redirigimos a la Home de no haber ningun error
                return LocalRedirect("/home");

            }
            catch (Exception ex)
            {
                return LocalRedirect("/login/Invalid credentials");
            }
        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
