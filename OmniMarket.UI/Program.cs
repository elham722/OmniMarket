
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(option =>
    {
        option.LoginPath = "/Users/Login";
    });


builder.Services.AddHttpClient<IClient, Client>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7029");
});

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program), typeof(MappingProfile));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

//app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
   // .WithStaticAssets();

app.Run();