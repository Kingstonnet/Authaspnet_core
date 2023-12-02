using Microsoft.AspNetCore.Authorization;
using Webapp_underthehood.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("MycookieAuth").AddCookie("MycookieAuth", options =>
{
    options.Cookie.Name = "MycookieAuth";
    //options.LoginPath = "/account1/whatever";
    options.ExpireTimeSpan = TimeSpan.FromSeconds(200);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireClaim("admin"));
    options.AddPolicy("belongtoHR", policy => policy.RequireClaim("Dept", "HR"));
    options.AddPolicy("HRmanageronly", policy => policy.RequireClaim("Dept", "HR")
                                                       .RequireClaim("manager")
                                                       .Requirements.Add(new HRManagerProbationRequirement(3)));
        

    
});

builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

builder.Services.AddHttpClient("ourwebapi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7270/");
});

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20); ;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

app.Run();
