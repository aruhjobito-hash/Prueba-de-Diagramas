using Blazored.LocalStorage;
using LeonXIIICore;
using LeonXIIICore.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// Hacemos que el builder añada el Scoped a todos los servicios declarados en ScopedServiceRegistry
foreach (var serviceType in ScopedServiceRegistry.Services)
{
    builder.Services.AddScoped(serviceType);
}

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.SuppressXFrameOptionsHeader = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie();
builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Services.AddHttpClient("ApiProxy")
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });
builder.Services.AddBlazoredLocalStorage();//Configuración para Blazored LocalStorage variables y mas información requerida por el sistema
var app = builder.Build();

app.Map("/proxy-api/{**path}", async (HttpContext context, IHttpClientFactory clientFactory, ILogger<Program> logger) =>
{
    var path = context.Request.Path.ToString().Replace("/proxy-api", "");
    var targetUrl = $"http://172.17.2.55:8080/backend-activo-fijo-version.2.0.0{path}";

    logger.LogInformation($"Proxying to: {targetUrl}");

    // Log request method and headers
    logger.LogInformation($"Request Method: {context.Request.Method}");
    foreach (var header in context.Request.Headers)
    {
        logger.LogInformation($"{header.Key}: {string.Join(",", header.Value)}");
    }

    var client = clientFactory.CreateClient("ApiProxy");

    // Preserve the original method (POST, GET, etc.)
    var method = new HttpMethod(context.Request.Method); // Fix: Use the original method

    var requestMessage = new HttpRequestMessage(method, targetUrl);

    // Handle request body (if exists)
    if (context.Request.ContentLength > 0)
    {
        logger.LogInformation("Request has body (ContentLength > 0)");

        // A;adimos una comprobación para evitar problemas con el cuerpo de la solicitud
        var memoryStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(memoryStream);
        memoryStream.Position = 0; // Reseter la posición del MemoryStream para leerlo correctamente

        // Log the request body
        using (var reader = new StreamReader(memoryStream, leaveOpen: true)) // leaveOpen=true prevents disposal
        {
            var requestBody = await reader.ReadToEndAsync();
            logger.LogInformation($"Request Body: {requestBody}");
        }

        // Reset again for the proxy request
        memoryStream.Position = 0;
        requestMessage.Content = new StreamContent(memoryStream);

        // Ensure Content-Type is set (if not already provided)
        if (!context.Request.Headers.ContainsKey("Content-Type"))
        {
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
    else
    {
        logger.LogInformation("Request has NO body (ContentLength = 0)");
    }

    // Copy headers (as before)
    foreach (var header in context.Request.Headers)
    {
        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
        {
            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }
    }

    // Send the request
    var responseMessage = await client.SendAsync(requestMessage);
    logger.LogInformation($"Response Status: {responseMessage.StatusCode}");

    // Handle response (unchanged)
    context.Response.StatusCode = (int)responseMessage.StatusCode;

    var excludedHeaders = new[] { "transfer-encoding", "content-length", "content-encoding" };
    foreach (var header in responseMessage.Headers)
    {
        if (!excludedHeaders.Contains(header.Key.ToLower()))
        {
            context.Response.Headers[header.Key] = header.Value.ToArray();
        }
    }

    foreach (var header in responseMessage.Content.Headers)
    {
        if (!excludedHeaders.Contains(header.Key.ToLower()))
        {
            context.Response.Headers[header.Key] = header.Value.ToArray();
        }
    }

    context.Response.ContentType = responseMessage.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";
    await responseMessage.Content.CopyToAsync(context.Response.Body);
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Configuramos Middleware para redireccionar 
app.Use(async (context, next) =>
{
    // Verificamos si es la raiz del proyecto
    if (context.Request.Path == "/")  // Si es la raiz del proyecto 
    {
        // Redireccionamos a /login y desactivamos esta característica
        context.Response.Redirect("/login", permanent: false);
        return;
    }
    // Continuamos con las llamadas en cola
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
