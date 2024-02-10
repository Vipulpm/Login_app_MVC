using Kharpan.Application.Repositories.Abstract;
using Kharpan.Application.Repositories.Implementation;
using Kharpan.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<KharpanDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddControllersWithViews();

//for identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<KharpanDbContext>()
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(op =>
    {
     op.LoginPath = "/Account/Login";
     op.LogoutPath = "/Account/Logout";
}
    ) ;
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
