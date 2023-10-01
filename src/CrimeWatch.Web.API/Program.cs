using CrimeWatch.Application;
using CrimeWatch.AppSettings;
using CrimeWatch.Web.API.Helpers;
using CrimeWatch.Web.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

builder.Configuration.AddAppSettings();
builder.Services.ConfigureOptions();

// Domain
builder.Services.AddApplication();

// Core
builder.Services.ConfigureServices();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition(
            name: "oauth2",
            new()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Workaround for Vite + React client app not being served in development mode
    var clientAppPath = Path.Combine(Directory.GetCurrentDirectory(), path2: "../crimewatch.web.client/dist");
    PhysicalFileProvider fileProvider = new(clientAppPath);

    app.UseFileServer(new FileServerOptions { FileProvider = fileProvider });
    app.MapFallbackToFile(filePath: "index.html", new() { FileProvider = fileProvider });
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.UseHttpsRedirection();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
