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

    // Workaround for client app not being served in development mode
    FileServerOptions options = new()
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "../crimewatch.web.client/dist"))
    };
    options.DefaultFilesOptions.DefaultFileNames.Clear();
    options.DefaultFilesOptions.DefaultFileNames.Add("index.html");

    app.UseFileServer(options);
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
