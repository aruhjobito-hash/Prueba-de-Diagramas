using ApiAppLeon;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
//string MiCors = "MiCors";
string MiCors = "AllowAll";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader());

}
);


var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };

        // Opcional: Para manejo adicional de JWT en caso sea necesario agregar mas capas de seguridad a travéz de mas tokens
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                //// Validación de que el token sea válido y este dentro de los roles respectivos
                var user = context.Principal;
                if (user == null || !user.HasClaim(c => c.Type == ClaimTypes.Name))
                {
                    context.Fail("Invalid token claims.");
                }

                return Task.CompletedTask;
            }
        };
    });

// Servicio de authenticación con Active Directory para posible acceso y manejo de Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "YourAppCookie"; // Setea el nombre del cookie en caso sea necesario
        options.LoginPath = "/account/login";  // Dirección para redireccionar una vez authenticado (en caso sea web)
        options.LogoutPath = "/account/logout"; // Direccion para redireccionar en caso no sea authenticado
    });


// Asigna los endpoints al explorador en formato SWAGGER
builder.Services.AddEndpointsApiExplorer();
//Builder para connection string a DBLEONXIII a través del DBcontext
builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DESARROLLO")));

// Configure the Swagger tags and settings
var swaggerConfig = new SwaggerConfig();

// Add services to the container
builder.Services.AddControllers();

// Add Swagger services with dynamic grouping
builder.Services.AddSwaggerGen(c =>
{
    // Generate SwaggerDocs dynamically from the SwaggerConfig model
    foreach (var group in swaggerConfig.Groups)
    {
        VersionSwagger SwaggerVersion = new VersionSwagger();
        c.SwaggerDoc(group.Name, new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = $"API - {group.Name}",
            Version = SwaggerVersion.version,
            Description = group.Description
        });
    }

    // Dynamically tag actions based on the GroupName in ApiExplorerSettings
    c.TagActionsBy(api =>
    {
        // Check if api.GroupName is not null and find the corresponding group
        var group = swaggerConfig.Groups.FirstOrDefault(g => api.GroupName?.Contains(g.Name) == true);

        // If a group is found, return the group's name
        if (group != null)
        {
            return new[] { group.Name };
        }

        // Si el endpoint (controllador) no tiene un grupo preestablecido lo redirige automaticamente al grupo "Desarrollo"
        return new[] { "Desarrollo" };  // Default group if no match
    });
});

// Configure Kestrel to use specific ports
builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.ListenLocalhost(8084); // HTTPS
    serverOptions.ListenLocalhost(8085); // HTTP
});

var app = builder.Build();
var VersionSwagger = new VersionSwagger();
// Configuración HTTP en caso de quiera incluir pipelines para el desarrollo y despliegue de proyecto.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    // Aqui se habilita Swagger para documentación JSON
    app.UseSwagger();

    // Enable Swagger UI middleware
    app.UseSwaggerUI(options =>
    {
        // Set Swagger JSON endpoint for each group
        foreach (var group in swaggerConfig.Groups)
        {
            options.SwaggerEndpoint($"/swagger/{group.Name}/swagger.json", group.Name);
        }

        // Set the custom route for Swagger UI to "swagger/index.html"
        options.RoutePrefix = "swagger/index.html"; // Swagger UI will be available at /swagger/index.html
        options.DocumentTitle = "CACLEON XIII Documentación "+ VersionSwagger;
        //options.DefaultModelExpandDepth(-1);  // Optionally hide schema models if desired
    });
}

// Añadimos las rutas en el mapeo de controladores 

app.UseHttpsRedirection();

app.UseCors("AllowAll");//Configuración  para request en puntos de red o distintas IP's 

app.UseAuthentication();// Uso de autenticación para JWTToken 

app.UseAuthorization();// Uso de Autorización para JWTToken 

//app.UseMiddleware<PermissionTokenMiddleware>(); //Configuración para multiples tokens según sea necesario.

app.MapControllers();

app.Run();
