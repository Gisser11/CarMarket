using Market.DAL;
using Market.DAL.Interfaces;
using Market.DAL.Interfaces.IServices;
using Market.DAL.Repositories;
using Market.DAL.Repositories.Services;
using Market.Service.Implementation;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IStudiaRepository, StudiaRepository>();
builder.Services.AddScoped<IStudiaService, StudiaService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("https://localhost:3000")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "swagger"; // Опционально: изменение базового пути Swagger UI
    });
}


app.UseAuthorization();
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();