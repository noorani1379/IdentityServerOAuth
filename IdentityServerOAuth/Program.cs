using IdentityServer4.Models;
using IdentityServerOAuth.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string cs = @"Data Source=.;Initial Catalog=IdentityServer;Integrated Security=true;";
var miAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(ConfigIdentityServer.GetUsers())
                .AddConfigurationStore(option =>
                {
                    option.ConfigureDbContext = b =>
                     b.UseSqlServer(cs, sql => sql.MigrationsAssembly(miAssembly));
                })
                .AddOperationalStore(option =>
                {
                    option.ConfigureDbContext = b =>
                    b.UseSqlServer(cs, sql => sql.MigrationsAssembly(miAssembly));
                    option.EnableTokenCleanup = true;
                });

//.AddInMemoryIdentityResources(ConfigIdentityServer.GetIdentityResources())
//.AddInMemoryApiResources(ConfigIdentityServer.GetApiResources())
//.AddInMemoryClients(ConfigIdentityServer.GetClients())
//.AddInMemoryApiScopes(ConfigIdentityServer.GetApiScopes());

//IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext
//IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
