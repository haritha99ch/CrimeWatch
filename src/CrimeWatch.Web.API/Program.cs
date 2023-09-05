using CrimeWatch.Application;
using CrimeWatch.AppSettings;
using CrimeWatch.Web.API.Helpers;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Domain
builder.Configuration.AddAppSettings();
builder.Services.AddApplication(
    builder.Configuration.GetConnectionString("Database:DefaultConnection")!,
    builder.Configuration.GetConnectionString("Storage:DefaultConnection")!);

// Core
builder.Services.ConfigureServices();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
