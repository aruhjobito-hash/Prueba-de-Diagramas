using Clases.Sistema;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LeonXIIICore
{
        public class UserClaimsService
        {
            private readonly AuthenticationStateProvider _authenticationStateProvider;

            public List<string> Roles { get; private set; } = new();
            public List<string> IdUsers { get; private set; } = new();
            public List<string> DatoTrabajador { get; private set; } = new();
            public string SelectedRole { get; private set; }
            public string IdUser { get; private set; }

            private readonly Dictionary<string, int> _datoTrabajadorMap = new(StringComparer.OrdinalIgnoreCase)
            {
                { "SSocio", 0 },
                { "SCargo", 1 },
                { "SArea", 2 },
                { "SAgencia", 3 },
                { "SIdSocio", 4 },
                { "SIdPersona", 5 },
                { "SIdCargo", 6 },
                { "SIdArea", 7 },
                { "SIdUser",8}
            };

            public UserClaimsService(AuthenticationStateProvider authenticationStateProvider)
            {
                _authenticationStateProvider = authenticationStateProvider;
            }

            public async Task InitializeAsync()
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity?.IsAuthenticated == true)
                {
                    Roles = user.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();

                    DatoTrabajador = user.Claims
                        .Where(c => c.Type == ClaimTypes.UserData)
                        .Select(c => c.Value)
                        .ToList();

                    if (Roles.Any())
                        SelectedRole = Roles.First();

                    IdUsers = user.Claims
                        .Where(c => c.Type == ClaimTypes.Name)
                        .Select(c => c.Value)
                        .ToList();

                    if (IdUsers.Any())
                        IdUser = IdUsers.First();
                }
            }

            public string? GetDatoTrabajadorByIdUser(string key)
            {
                if (_datoTrabajadorMap.TryGetValue(key, out int index))
                {
                    if (index >= 0 && index != 8  && index < DatoTrabajador.Count)
                    {
                        return DatoTrabajador[index];
                    }
                    if (index == 8)
                    {
                        return IdUser;
                    }
                }
            return null;
            }
        }

}
