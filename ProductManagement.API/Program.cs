using MediatR;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.Storage.SQLite;
using ProductManagement.Application;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Controllers ve Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

// EF Core + SQLite
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite("Data Source=/app/data/product.db;Cache=Shared"));

// Hangfire (SQLite)
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSQLiteStorage("/app/data/hangfire.db"));
builder.Services.AddHangfireServer();

// Dependency Injection
builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ILowStockLogRepository, EfLowStockLogRepository>();
builder.Services.AddScoped<CheckLowStockJob>();

var app = builder.Build();

// ✅ Swagger: Her ortamda aktif
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Hangfire Dashboard
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new AllowAllDashboardAuthorizationFilter() }
});

// ✅ HTTP Ayarları
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ✅ Veritabanı Migration işlemi (ilk açılışta otomatik)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    db.Database.Migrate();
}

// ✅ Job Tanımlaması (app ayağa kalkınca)
app.Lifetime.ApplicationStarted.Register(() =>
{
    using var scope = app.Services.CreateScope();
    var job = scope.ServiceProvider.GetRequiredService<CheckLowStockJob>();

    RecurringJob.AddOrUpdate(
        "check-low-stock-job",
        () => job.ExecuteAsync(),
        "0 8 * * *", // her sabah 08:00
        new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local
        });
});

app.Run();
