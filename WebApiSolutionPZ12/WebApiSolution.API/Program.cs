using Microsoft.EntityFrameworkCore;
using WebApiSolution.Application.Mappings;
using WebApiSolution.Application.Services;
using WebApiSolution.Application.Interfaces;
using WebApiSolution.Infrastructure.Data;
using WebApiSolution.Infrastructure.Repositories;
using WebApiSolution.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new() { Title = "WebApiSolution", Version = "v1" });
});

// SQLite + EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=webapi.db"));

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();

var app = builder.Build();

// Автоматично створити БД при старті
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// ── Middleware pipeline ───────────────────────────────────────────────────────
app.UseMiddleware<RequestLoggingMiddleware>(); // ← логування першим

app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiSolution v1");
    o.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();