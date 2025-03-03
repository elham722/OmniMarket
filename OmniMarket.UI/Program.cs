using System.Reflection;
using Hanssens.Net;
using Microsoft.Extensions.DependencyInjection;
using OmniMarket.UI.Contracts;
using OmniMarket.UI.Services;
using OmniMarket.UI.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IClient, Client>(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiAddress").Value));

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
