using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimeiraApi;
using PrimeiraApi.Application.Mapping;
using PrimeiraApi.Domain.Model;
using PrimeiraApi.Infrastructura.Repositories;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(DomainToDTOsMapping)); // Injectando o Auto Mapper

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>    // configuração para exibir o token no swagger (Toda essa configuração é opcional)
{

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme // definir o tipo de autenticação no caso o bearer
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()  // requerimento de autenticação para fazer a autenticação
    {
    {
        new OpenApiSecurityScheme  //define que o codigo passado no swagger possa percorrer ou ser usado pela api
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });


});



builder.Services.AddTransient<IUserRepository, UserRepository>();

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>  // processo de autenticação na api
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Definindo o JWT para uso
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  // Definindo o JWT para uso
}).AddJwtBearer(x =>  // define como vai ser a autenticação
{
    x.RequireHttpsMetadata = false; // não precisa requerer https no momento
    x.SaveToken = true; // salve o token
    x.TokenValidationParameters = new TokenValidationParameters // Criação de parâmetros de validação
    {
        ValidateIssuerSigningKey = true, //vai ter uma validação de assinatura, ele vai pegar a chave rivada e verificar se o token é correto
        IssuerSigningKey = new SymmetricSecurityKey(key), // Aqui, você fornece a chave que será usada para validar a assinatura do token
        ValidateIssuer = false, // Validação do emissor
        ValidateAudience = false//Validação do público
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development"); //exceção para ambiente de densenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error"); // exceção para ambiente de produção
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
