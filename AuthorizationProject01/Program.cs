using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure authentication and authorization
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
});


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddAuthentication("CookieAuth")
//  .AddCookie("CookieAuth", options =>

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Set your login path
        options.LogoutPath = "/Account/Logout"; // Set your logout path
                                                // options.LoginPath = "/Account/Login";
    });

builder.Services.AddAuthorization(options =>
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Load policy definitions from the database
        var policies = dbContext.PolicyDefinition;

        // Iterate through each policy and add them to the authorization options
        foreach (var policy in policies)
        {
            // If a role is specified, add a policy requiring the role and the claim
            if (!string.IsNullOrEmpty(policy.Role))
            {
                options.AddPolicy(policy.PolicyName, policyBuilder =>
                    policyBuilder.RequireRole(policy.Role)
                                 .RequireClaim(policy.ClaimType, policy.ClaimValue));
            }
            else
            {
                // If no role is specified, just require the claim
                options.AddPolicy(policy.PolicyName, policyBuilder =>
                    policyBuilder.RequireClaim(policy.ClaimType, policy.ClaimValue));
            }
        }
    }

    //options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    //options.AddPolicy("CanViewReports", policy => policy.RequireClaim("CanViewReports", "true"));
    //options.AddPolicy("CanViewReports", policy => policy.RequireClaim("CanViewReports"));
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
