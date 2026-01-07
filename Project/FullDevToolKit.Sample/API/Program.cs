using API.Code;
using FullDevToolKit.ApplicationHelpers;
using FullDevToolKit.Core;
using FullDevToolKit.Core.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MyApp.API;
using MyApp.Context;
using MyApp.Contracts.Managers;
using MyApp.Managers;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<ISettings, MyAppSettings>();
builder.Services.AddScoped<IContext, DapperContext>();
builder.Services.AddScoped<IMyAppManager, MyAppManager>();
builder.Services.AddScoped<MailManager, MyApMailCenter>();
builder.Services.AddSingleton<ISystemContext, SystemContext>();
builder.Services.AddScoped<IFileService, LocalFileService>();

//configure auth

var key = Encoding.ASCII.GetBytes(TokenService.PRIVATEKEY);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Configuração OpenAPI com Scalar
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "FullDevToolKit Template API",
            Version = "v1",
            Description = "API Template com autenticação JWT"
        };
        return Task.CompletedTask;
    });

    options.AddOperationTransformer((operation, context, cancellationToken) =>
    {
        // Filtrar métodos sem HttpMethod
        if (string.IsNullOrEmpty(context.Description.HttpMethod))
        {
            return Task.CompletedTask;
        }
     
        return Task.CompletedTask;
    });
});

// .NET 10 - Simplificação da configuração de serialização JSON
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// Mantém compatibilidade com MVC Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Expor OpenAPI
    app.MapOpenApi();

    // UI do Scalar
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("FullDevToolKit API")
            .WithTheme(ScalarTheme.Default)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
            .WithPreferredScheme("Bearer");
    });
}
else
{
    // Em produção, não expõe Swagger
    app.UseExceptionHandler("/Error");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
