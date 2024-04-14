using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using art_gallery.Persistence;
using art_gallery.Models;
using System.Reflection;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IArtifactDataAccess, ArtifactEF>();
builder.Services.AddScoped<IArtStyleDataAccess, ArtStyleEF>();
builder.Services.AddScoped<IExhibitionDataAccess, ExhibitionEF>();
builder.Services.AddScoped<IArtistDataAccess, ArtistEF>();
builder.Services.AddScoped<IArtfactDataAccess, ArtfactEF>();
builder.Services.AddScoped<IArtgalleryDataAccess, ArtgalleryEF>();


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
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
