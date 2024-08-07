using EasyNetQ;
using LojaVirtual.Catalogo.Services;
using LojaVirtual.Produtos.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton(RabbitHutch.CreateBus(builder.Configuration.GetConnectionString("RabbitMQ")));

builder.Services.AddHostedService<EstoqueService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKeys = new[]
        {
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenCliente:Key") ?? string.Empty)),
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenColaborador:Key") ?? string.Empty))
        }
    };

});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("VisualizarCategoria", policy => policy.RequireClaim("VisualizarCategoria", "true"))
    .AddPolicy("AdicionarCategoria", policy => policy.RequireClaim("AdicionarCategoria", "true"))
    .AddPolicy("EditarCategoria", policy => policy.RequireClaim("EditarCategoria", "true"))
    .AddPolicy("ExcluirCategoria", policy => policy.RequireClaim("ExcluirCategoria", "true"))
    .AddPolicy("VisualizarProduto", policy => policy.RequireClaim("VisualizarProduto", "true"))
    .AddPolicy("AdicionarProduto", policy => policy.RequireClaim("AdicionarProduto", "true"))
    .AddPolicy("EditarProduto", policy => policy.RequireClaim("EditarProduto", "true"))
    .AddPolicy("ExcluirProduto", policy => policy.RequireClaim("ExcluirProduto", "true"));

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new()
        {
            Description = "Insira o token JWT da seguinte maneira: Bearer {seu token}",
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });
    options.AddSecurityRequirement(new() { { new() { Reference = new() { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
