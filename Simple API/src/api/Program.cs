using Application.API.Config;
using Application.API.Data;
using Application.API.Interface;
using Application.API.Model;
using Application.API.Services;
using Domain.Interface;
using Domain.Notificacoes;
using Infra;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Scrutor;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.

builder.Services.AddDbContext<XGamesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    //.AddErrorDescriber<IdentityMensagensPortugues>()
    .AddDefaultTokenProviders();

//var m = builder.Configuration.GetSection("MessageQueueConnection");
//builder.Services.Configure<MessageBusSettings>(m);
//var ms = teste0.Get<MessageBusSettings>();

builder.Services.AddMessageBusConfiguration(builder.Configuration);

//config do Redis
builder.Services.AddStackExchangeRedisCache(o => {

    o.InstanceName = "instance";
    o.Configuration = "localhost:6379";
});


// JWT Config

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = appSettings.ValidoEm,
        ValidIssuer = appSettings.Emissor
    };
});

// End JWT

builder.Services.AddHttpContextAccessor();
// configure DI for application services
builder.Services.AddScoped<XGamesContext>();

//DI padrao
//builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
//builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
//builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
//builder.Services.AddScoped<ICategoriaService, CategoriaService>();
//builder.Services.AddScoped<IProdutoService, ProdutoService>();
//builder.Services.AddScoped<INotificador, Notificador>();
//builder.Services.AddScoped<ICacheService, CacheService>();


//DI com Scrutor
//crio um array de assemblies para fazer o scan
Assembly[] assemblies = new[] {
    typeof(ProdutoRepository).Assembly,
    typeof(CategoriaService).Assembly,
    typeof(Notificador).Assembly,
    typeof(CacheService).Assembly };

builder.Services.Scan(selector => selector
    .FromAssemblies(assemblies)
    .AddClasses(publicOnly: false) //se não colocar false, ele não pega as classes internas  
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)// Skip , para nao registrar caso o servico ja esteja registrado
    .AsMatchingInterface() // Pega todas as classes que fazem match entre a classe e a interface": ex: ProdutoRepository e IProdutoRepository
    .WithScopedLifetime());
// End DI com Scrutor


builder.Services.Scan(selector =>
    selector
        .FromCallingAssembly()
        .AddClasses(
            classSelector =>
                classSelector.InNamespaces("Application.API.Interface")
        )
        .AsImplementedInterfaces()
        .AddClasses(classSelector => classSelector.AssignableTo(typeof(IRepository<>)))
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .FromAssemblyOf<ICategoriaRepository>()
        .AddClasses(classSelector =>
            classSelector.AssignableTo<ICategoriaRepository>())
        .AsMatchingInterface()
        .WithScopedLifetime()
);



//configuração de ambiente
builder.Environment.ConfigureAppSettings();


//somente teste parar ver as config, de ambiente
var environment = builder.Environment;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
