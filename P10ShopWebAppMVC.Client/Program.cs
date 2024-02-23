using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using P06Shop.Shared.Confguration;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.Services.ProductService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ShopContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDBConnection"));
//});

var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
var appSettingsSection = appSettings.Get<AppSettings>();

var uriBuilder = new UriBuilder(appSettingsSection.BaseApiUrl)
{
    Path = appSettingsSection.ProductEndpoint.BaseUrl 
};

builder.Services.AddHttpClient<IProductService, ProductService>(client => client.BaseAddress = uriBuilder.Uri);
builder.Services.Configure<AppSettings>(appSettings);


// Konfiguracja HttpClient dla AuthService
builder.Services.AddHttpClient<IAuthService, AuthService>(client => client.BaseAddress = uriBuilder.Uri);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddSession(); // Dodanie sesji


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


app.UseSession();  // Użyj sesji przed routingiem
app.UseRouting();

app.UseAuthentication(); // Dodanie autentykacji
app.UseAuthorization();  // Dodanie autoryzacji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
