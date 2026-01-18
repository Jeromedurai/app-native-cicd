using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/WebApp/Error");
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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "Index",
    pattern: "{controller=WebApp}/{action=Home}/{id?}");

// SPA Fallback Route - Catches ALL unmatched routes
// This ensures client-side routes (like /wishlist, /cart, /products, etc.) 
// are handled by React Router instead of returning 404
// Browser back/forward buttons will work correctly for all pages
app.MapFallbackToController("Home", "WebApp");

app.Run();

