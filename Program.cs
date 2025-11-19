using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.IO;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// build an absolute path so both dotnet-ef and runtime use the same file
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "app.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

Console.WriteLine($"Using SQLite DB at: {dbPath}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register API controllers
builder.Services.AddControllers(); // nếu đã dùng AddControllersWithViews() thì vẫn OK

var app = builder.Build();

// apply fresh DB on startup (drops and recreates the DB)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine($"Using SQLite DB at: {dbPath}");

    // Apply pending migrations without deleting existing data
    var pending = db.Database.GetPendingMigrations().ToList();
    if (pending.Any())
    {
        Console.WriteLine("Applying migrations: " + string.Join(", ", pending));
        db.Database.Migrate();
    }
    else
    {
        Console.WriteLine("No pending migrations");
    }

    WebApplication1.Data.DataSeeder.Seed(db);   // <-- đảm bảo có dòng này
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // serve wwwroot

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();