using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Configure CORS to allow any origin, method, and header
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:8120") //.AllowAnyOrigin() // .WithOrigins("http://localhost:8162", "http://localhost:8163") // frontend URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure the DbContext with SQLite (?? means default connection string).
builder.Services.AddDbContext<EFCoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=../utfpr.db"));

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EFCoreContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => // Set Swagger UI at the app's root
    {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core-Service API");
    c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization(); // Auth Middleware, not used in this project but good for future security implementations.
app.UseCors("AllowAll"); // Use the CORS policy defined above
app.MapControllers();

// Configure the application to listen on a specific port
//app.Urls.Add("https://localhost:8171");
app.Urls.Add("http://localhost:8110");

app.Run();