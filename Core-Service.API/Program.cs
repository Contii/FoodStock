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

// Define the Category API CRUD routes.
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

// Update an existing category.
app.MapPut("/api/categories/{id}", async (EFCoreContext context, int id, CategoryModel updatedCategory) =>
{
    var category = await context.Categories.FindAsync(id); 
    if (category is null) return Results.NotFound(); 

    category.Name = updatedCategory.Name;
    category.Description = updatedCategory.Description;

    await context.SaveChangesAsync();
    return Results.Ok(category); 
});

// Delete a category.
app.MapDelete("/api/categories/{id}", async (EFCoreContext context, int id) =>
{
    var category = await context.Categories.FindAsync(id);
    if (category is null) return Results.NotFound();

    context.Categories.Remove(category);
    await context.SaveChangesAsync();
    return Results.NoContent();
});



// Define the Item API CRUD routes.
// Obtaining all items.
app.MapGet("/api/items", async (EFCoreContext context) =>
{
    return await context.Items.ToListAsync();
});

// Obtaining a specific item by ID.
app.MapGet("/api/items/{id}", async (EFCoreContext context, int id) =>
{
    var item = await context.Items.Include(i => i.Category).FirstOrDefaultAsync(i => i.ItemID == id); // Verify if the item exists and include the related category info in the context.
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

// Create a new item.
app.MapPost("/api/items", async (EFCoreContext context, ItemModel item) =>
{
    if (item.CategoryID.HasValue) // Check if CategoryID is provided.
    {
        var category = await context.Categories.FindAsync(item.CategoryID); // Verify if the category exists.
        if (category is null) return Results.BadRequest("Invalid CategoryID");
    }
    
    context.Items.Add(item); // Add the new item to the context.
    await context.SaveChangesAsync(); // Save the changes to the database.
    return Results.Created($"/api/items/{item.ItemID}", item);
});

app.Run();