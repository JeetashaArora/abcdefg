using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using art_gallery.Persistence;
using art_gallery.Models;
using art_gallery.Authentication;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Art Gallery",
        Description = "Backend service that provides resources for the beautiful art gallery.",
        Contact = new OpenApiContact
        {
            Name = "Tanay and Jeetasha",
            Email = "tanay4847.be22@chitkara.edu.in"
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthentication("BasicAuthentication")
 .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", default);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Admin"));

    options.AddPolicy("Curator", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Curator", "Admin"));

    options.AddPolicy("Artist", policy =>
        policy.RequireClaim(ClaimTypes.Role, "Artist", "Admin"));
});

builder.Services.AddScoped<IArtifactDataAccess, ArtifactEF>();
builder.Services.AddScoped<IArtStyleDataAccess, ArtStyleEF>();
builder.Services.AddScoped<IExhibitionDataAccess, ExhibitionEF>();
builder.Services.AddScoped<IArtistDataAccess, ArtistEF>();
builder.Services.AddScoped<IArtfactDataAccess, ArtfactEF>();
builder.Services.AddScoped<IArtgalleryDataAccess, ArtgalleryEF>();
builder.Services.AddScoped<IUserDataAccess, UserEF>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}
app.UseSwagger();

app.UseSwaggerUI(setup => setup.InjectStylesheet("/styles/theme-outline.css"));

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
