using CrimeWatch.Application;
using CrimeWatch.AppSettings;
using CrimeWatch.Web.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

// Domain
builder.Configuration.AddAppSettings();
builder.Services.AddApplication(
    builder.Configuration.GetConnectionString("Database:DefaultConnection")!,
    builder.Configuration.GetConnectionString("Storage:DefaultConnection")!);

// Core
builder.Services.ConfigureOptions();
builder.Services.ConfigureServices();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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
    string clientAppPath = Path.Combine(Directory.GetCurrentDirectory(), "../crimewatch.web.client/dist");
    PhysicalFileProvider fileProvider = new(clientAppPath);

    app.UseFileServer(new FileServerOptions() { FileProvider = fileProvider });
    app.MapFallbackToFile("index.html", new StaticFileOptions { FileProvider = fileProvider });
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
