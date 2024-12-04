var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DbContext with dependency injection

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Login}/{action=Login}/{id?}");
    //pattern: "{controller=LibrarianDashboard}/{action=Index}/{id?}");

/*app.MapControllerRoute(
    name: "student",
    pattern: "StudentDashboard/{action=Index}/{id?}");
*/


app.Run();
