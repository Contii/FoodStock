using FoodStock.Models;
using FoodStock.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext with SQLite (?? means default connection string).
builder.Services.AddDbContext<EFCoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=../utfpr.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization(); // Auth Middleware, not used in this project but good for future security implementations.

// Define the API CRUD routes.
// Obtaining all categories.
app.MapGet("/api/categories", async (EFCoreContext context) =>
{
    return await context.Categories.ToListAsync();
});

// Obtaining a specific category by ID.
app.MapGet("/api/categories/{id}", async (EFCoreContext context, int id) =>
{
    var category = await context.Categories.FindAsync(id); // Verify if the category exists.
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

// Create a new category.
app.MapPost("/api/categories", async (EFCoreContext context, CategoryModel category) =>
{
    context.Categories.Add(category); // Add the new category to the context.
    await context.SaveChangesAsync(); // Save the changes to the database.
    return Results.Created($"/api/categories/{category.CategoryID}", category);
});

app.Run();