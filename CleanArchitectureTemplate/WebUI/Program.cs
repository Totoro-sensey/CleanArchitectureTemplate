using System.Net;
using System.Reflection;
using Application;
using Domain.Identity.Entities;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SystemOfWidget.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
DevExtreme.AspNet.Data.DataSourceLoadOptionsBase.StringToLowerDefault = true;
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddSession();
builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc(builder.Configuration["Version"], new OpenApiInfo
    {
        Title = builder.Configuration["ProjectName"], 
        Version = builder.Configuration["Version"]
    });
    // To Enable authorization using Swagger (JWT)    
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
    {  
        Name = "Authorization",  
        Type = SecuritySchemeType.ApiKey,  
        Scheme = "Bearer",  
        BearerFormat = "JWT",  
        In = ParameterLocation.Header,  
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",  
    });  
    option.AddSecurityRequirement(new OpenApiSecurityRequirement  
    {  
        {  
            new OpenApiSecurityScheme  
            {  
                Reference = new OpenApiReference  
                {  
                    Type = ReferenceType.SecurityScheme,  
                    Id = "Bearer"  
                }  
            },
            Array.Empty<string>()
        }  
    });
                
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // option.IncludeXmlComments(xmlPath);
    // option.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(app.Configuration["SwaggerEndpoint"], app.Configuration["Version"]);
    });
}

app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized 
        || response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        response.Redirect("/account/login");
    }
});
app.UseStatusCodePagesWithReExecute("/error", "?code={0}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.IsNpgsql())
            await context.Database.MigrateAsync();

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

        await ApplicationDbContextSeed.SeedRolesAsync(roleManager);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        throw;
    }
}

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

await app.RunAsync();
