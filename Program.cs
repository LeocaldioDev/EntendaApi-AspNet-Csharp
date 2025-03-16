using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrimeiraApi;
using PrimeiraApi.Application.Mapping;
using PrimeiraApi.Domain.Model.UserAggregate;
using PrimeiraApi.Infrastructura.Repositories;
using PrimeiraApi.SwaggerConfig;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => //[versionamento] Versionamento
{
c.OperationFilter<SwaggerDefaultValues>();


});
builder.Services.AddAutoMapper(typeof(DomainToDTOsMapping)); //[AutoMapper] Injectando o Auto Mapper
builder.Services.AddApiVersioning().AddMvc().AddApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
}); //[versionamento] Configuração do Versionamento
builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>(); //[versionamento] Configuração do Versionamento

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>    //[Token] configuração para exibir o token no swagger (Toda essa configuração é opcional)
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
builder.Services.AddCors(options =>  //[cors]Cors 
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.WithOrigins("http://TeuDominio")// aqui fiva a rota dos dominios que podem ter acesso a api
            .AllowAnyHeader()
            .AllowAnyMethod(); 
        });
});

var key = Encoding.ASCII.GetBytes(Key.Secret);

builder.Services.AddAuthentication(x =>  //[Token JWT] processo de autenticação na api
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
    app.UseExceptionHandler("/error-development"); //[Handler Error] exceção para ambiente de densenvolvimento
    app.UseSwaggerUI(options => //[Versionamento] Versionamento para abrir no swagger
    {
        var version = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in version.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"Web Api - {description.GroupName.ToUpper()}");
        }
    });
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error"); // [Handler Error] exceção para ambiente de produção
}

app.UseCors("MyPolicy"); //[Cor] Definir o Cors

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
