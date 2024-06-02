using Chinook;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Chinook.Areas.Identity;
using Chinook.Infrastructure.Extension;
using Chinook.Models;
using Chinook.Infrastructure.Contracts.Repositories;
using Chinook.DataAccess;
using Chinook.Infrastructure.Contracts.Services;
using Chinook.Services;
using Chinook.ClientServices.Interfaces;
using Chinook.ClientServices;
using Chinook.ClientServices.API;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ChinookContext>(opt => opt.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ChinookUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ChinookContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ChinookUser>>();

// To register all the services and repositories by type
//builder.Services.AddServiceImplementations(Assembly.GetExecutingAssembly());

//Repositories
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<ITrackRepository, TrackRepository>();

//Services
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<ITrackService, TrackService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAppState,AppState>();

builder.Services.AddScoped<IApiServices, ApiServices>();
builder.Services.AddScoped<IClientPlaylistService, ClientPlaylistService>();
builder.Services.AddScoped<IClientArtistService, ClientArtistService>();
builder.Services.AddScoped<IClientTrackService, ClientTrackService>();
builder.Services.AddHttpClient<ApiServices>(client =>
{
    
});

builder.Services.AddLogging();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
