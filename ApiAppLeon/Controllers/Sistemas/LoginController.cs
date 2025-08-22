using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static ApiAppLeon.Controllers.Sistemas.GenerateToken;

namespace ApiAppLeon.Controllers.Sistema
{
    [ApiExplorerSettings(GroupName = "Sistemas")]
    [Route("api/Sistemas/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string _ldapPath = "LDAP://cacleonxiii.com.pe/DC=cacleonxiii,DC=com,DC=pe";

        // Producción Con Credenciales
        [HttpPost("login/with-credentials")]
        public async Task<IActionResult> LoginWithCredentials(UserCredentials credentials)
        {
            try
            {
                // Using credentials to bind to LDAP
                using (DirectoryEntry entry = new DirectoryEntry(_ldapPath, credentials.Username, credentials.Password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = "(samaccountname=" + credentials.Username + ")";
                        searcher.PropertiesToLoad.Add("samaccountname");
                        searcher.PropertiesToLoad.Add("displayName");
                        searcher.PropertiesToLoad.Add("mail");
                        searcher.PropertiesToLoad.Add("memberOf");
                        searcher.PropertiesToLoad.Add("employeetype");

                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            return await HandleLoginResult(result, credentials.Username);
                        }
                        else
                        {
                            return Unauthorized(new { Message = "Invalid credentials" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        // Desarrollo - Anonymous bind (sin credenciales)
        [HttpPost("login/anonymous")]
        public async Task<IActionResult> LoginAnonymous(UserNoCredentials credentials)
        {
            try
            {
                JwtTokenRequest TokenProfile = new();
                // Anonymous bind - Establece el vinculo al Active directory para obtener la información del pérfíl requerido
                using (DirectoryEntry entry = new DirectoryEntry(_ldapPath))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = "(samaccountname=" + credentials.Username + ")";
                        searcher.PropertiesToLoad.Add("samaccountname");
                        searcher.PropertiesToLoad.Add("displayName");
                        searcher.PropertiesToLoad.Add("mail");
                        searcher.PropertiesToLoad.Add("memberOf");
                        searcher.PropertiesToLoad.Add("employeetype");

                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            return await HandleLoginResult(result, credentials.Username);
                        }
                        else
                        {
                            return Unauthorized(new { Message = "Invalid credentials" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
            }
        }

        private async Task<IActionResult> HandleLoginResult(SearchResult result, string username)
        {
            string role = string.Empty;
            var memberOfGroups = result.Properties["memberOf"].Cast<string>().ToList(); // "Miembro de" (grupos del active directory)
            var displayName = result.Properties["displayName"].Cast<string>().FirstOrDefault(); // Nombre del usuario
            var email = result.Properties["mail"].Cast<string>().FirstOrDefault(); // Email corporativo
            var employeeType = result.Properties["employeetype"].Cast<string>().FirstOrDefault(); // Roles del usuario

            // Create claims for username, role, and the retrieved profile information
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, employeeType ?? "User"), 
                new Claim(ClaimTypes.Email, email ?? ""),  
                new Claim("DisplayName", displayName ?? "") 
            };

            // Add the "Member Of" groups to claims 
            foreach (var group in memberOfGroups)
            {
                claims = claims.Concat(new[]
                {
                    new Claim(ClaimTypes.GroupSid, group)
                }).ToArray();
            }

            // Create ClaimsIdentity and ClaimsPrincipal
            var claimsIdentity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Methodo para generar cookies en caso de requerir interfaz web de administración de las mismas (ej: dashboard para apis)
            // await HttpContext.SignInAsync(claimsPrincipal);

            // Retorna Mensaje ok (200) con la información obtenida en la petición al Active Directory (Dominio)
            return Ok(new
            {
                Message = "Login successful",
                Username = username,
                Role = employeeType ?? "User",
                DisplayName = displayName,
                Email = email,
                MemberOf = memberOfGroups
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok(new { Message = "Logout successful" });
        }
    }

    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserNoCredentials
    {
        [Required]
        public string Username { get; set; }

    }
}
