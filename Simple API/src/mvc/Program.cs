using Infra;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using mvc.Configuration;
using mvc.Data;
using mvc.Services;
using NuGet.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


//Dependency Injection DI
builder.Services.AddHttpClient<ICategoriaService, CategoriaServices>();
builder.Services.AddHttpClient<IProdutoServices, ProdutoServices>();
//


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Leia do appsettings.json e faz bind na classe AppServicesSettings
builder.Services.Configure<AppServicesSettings>(builder.Configuration);

//configuração de ambiente
builder.Environment.ConfigureAppSettings();

//somente teste parar ver as config, de ambiente
var environment =  builder.Environment;

//config do NGNIX
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


var app = builder.Build();

//config do NGNIX precisa ser o primeiro de todos
app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    Console.WriteLine("IsDevelopment");
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


public partial class Program { }
