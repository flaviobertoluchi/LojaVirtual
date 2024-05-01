using LojaVirtual.Site.Areas.Administracao.Services;
using LojaVirtual.Site.Areas.Administracao.Services.Interfaces;
using LojaVirtual.Site.Extensions;
using LojaVirtual.Site.Services;
using LojaVirtual.Site.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var localhost = builder.Configuration.GetValue<string>("Certificados:Localhost");
var externo = builder.Configuration.GetValue<string>("Certificados:Externo");
_ = int.TryParse(builder.Configuration.GetValue<string>("Certificados:Porta"), out var porta);
if (porta <= 0) porta = 443;

if (localhost is not null && externo is not null)
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ListenAnyIP(porta, listenOptions =>
        {
            listenOptions.UseHttps(httpsOptions =>
            {
                var certificadoLocalhost = CertificateLoader.LoadFromStoreCert(localhost, "My", StoreLocation.CurrentUser, allowInvalid: true);
                var certificadoExterno = CertificateLoader.LoadFromStoreCert(externo, "My", StoreLocation.CurrentUser, allowInvalid: true);

                var certificados = new Dictionary<string, X509Certificate2>(StringComparer.OrdinalIgnoreCase)
                {
                    [localhost] = certificadoLocalhost,
                    [externo] = certificadoExterno
                };

                httpsOptions.ServerCertificateSelector = (connectionContext, nome) =>
                {
                    if (nome is not null && certificados.TryGetValue(nome, out var certificado)) return certificado;

                    return certificadoExterno;
                };
            });
        });
    });
}

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpClient<IClienteService, ClienteService>();
builder.Services.AddHttpClient<IColaboradorService, ColaboradorService>();
builder.Services.AddHttpClient<IProdutoService, ProdutoService>();
builder.Services.AddHttpClient<ICategoriaService, CategoriaService>();

var redis = builder.Configuration.GetConnectionString("Redis");
if (redis is not null) builder.Services.AddStackExchangeRedisCache(options => options.Configuration = redis);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Sessao>();
builder.Services.AddScoped<Cookie>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido.");
    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico");
    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "O valor preenchido é inválido para este campo.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo deve ser numérico.");
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo precisa ser preenchido.");
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddMvc();

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var ptBR = new CultureInfo("pt-BR");
app.UseRequestLocalization(options: new()
{
    DefaultRequestCulture = new(ptBR),
    SupportedCultures = [ptBR],
    SupportedUICultures = [ptBR]
});

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
