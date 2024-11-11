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



// Define the Stock API CRUD routes.
// Obtaining all stocks.
app.MapGet("/api/stocks", async (EFCoreContext context) =>
{
    return await context.Stocks.Include(s => s.Category).Include(s => s.Items).ToListAsync();
});

// Obtaining a specific stock by ID.
app.MapGet("/api/stocks/{id}", async (EFCoreContext context, int id) =>
{
    var stock = await context.Stocks.Include(s => s.Category).Include(s => s.Items).FirstOrDefaultAsync(s => s.StockID == id); // Verify if the stock exists and include related category and items.
    return stock is not null ? Results.Ok(stock) : Results.NotFound();
});

// Create a new stock.
app.MapPost("/api/stocks", async (EFCoreContext context, StockModel stock) =>
{
    if (stock.CategoryID.HasValue)
    {
        var category = await context.Categories.FindAsync(stock.CategoryID); // Verify if the category exists.
        if (category is null) return Results.BadRequest("Invalid CategoryID");
    }

    if (stock.Items.Count > 0)
    {
        foreach (var item in stock.Items)
        {
            var itemExists = await context.Items.FindAsync(item.ItemID); // Verify if the item exists.
            if (itemExists is null) return Results.BadRequest("Invalid ItemID");
        }
    }

    context.Stocks.Add(stock); // Add the new stock to the context.
    await context.SaveChangesAsync(); // Save the changes to the database.
    return Results.Created($"/api/stocks/{stock.StockID}", stock);
});

// Update an existing stock.
app.MapPut("/api/stocks/{id}", async (EFCoreContext context, int id, StockModel updatedStock) =>
{
    var stock = await context.Stocks.Include(s => s.Category).Include(s => s.Items).FirstOrDefaultAsync(s => s.StockID == id); // Verify if the stock exists.
    if (stock is null) return Results.NotFound();

    if (updatedStock.CategoryID.HasValue)
    {
        var category = await context.Categories.FindAsync(updatedStock.CategoryID); // Verify if the category exists.
        if (category is null) return Results.BadRequest("Invalid CategoryID");
    }

    if (updatedStock.Items.Count > 0)
    {
        foreach (var item in updatedStock.Items)
        {
            var itemExists = await context.Items.FindAsync(item.ItemID); // Verify if the item exists.
            if (itemExists is null) return Results.BadRequest("Invalid ItemID");
        }
    }

    stock.Name = updatedStock.Name;
    stock.Description = updatedStock.Description;
    stock.Quantity = updatedStock.Quantity;
    stock.MinQuantity = updatedStock.MinQuantity;
    stock.MaxQuantity = updatedStock.MaxQuantity;
    stock.Items = updatedStock.Items;
    stock.CategoryID = updatedStock.CategoryID;
    stock.Category = updatedStock.Category;
    stock.MeasureType = updatedStock.MeasureType;

    await context.SaveChangesAsync();
    return Results.Ok(stock);
});

// Delete a stock.
app.MapDelete("/api/stocks/{id}", async (EFCoreContext context, int id) =>
{
    var stock = await context.Stocks.FindAsync(id);
    if (stock is null) return Results.NotFound();

    context.Stocks.Remove(stock);
    await context.SaveChangesAsync();
    return Results.NoContent();
});


// Define the Item API CRUD routes.
// Obtaining all items.
app.MapGet("/api/items", async (EFCoreContext context) =>
{
    return await context.Items.Include(i => i.Stock).ToListAsync();
});

// Obtaining a specific item by ID.
app.MapGet("/api/items/{id}", async (EFCoreContext context, int id) =>
{
    var item = await context.Items.Include(i => i.Stock).FirstOrDefaultAsync(i => i.ItemID == id); // Verify if the item exists and include the related stock info in the context.
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

// Create a new item.
app.MapPost("/api/items", async (EFCoreContext context, ItemModel item) =>
{
    var stock = await context.Stocks.FindAsync(item.StockID); // Verify if the stock exists.
    if (stock is null) return Results.BadRequest("Invalid StockID");

    item.Stock = stock; // Associate the item with the stock.
    context.Items.Add(item); // Add the new item to the context.
    await context.SaveChangesAsync(); // Save the changes to the database.
    return Results.Created($"/api/items/{item.ItemID}", item);
});

// Update an existing item.
app.MapPut("/api/items/{id}", async (EFCoreContext context, int id, ItemModel updatedItem) =>
{
    var item = await context.Items.Include(i => i.Stock).FirstOrDefaultAsync(i => i.ItemID == id); // Verify if the item exists.
    if (item is null) return Results.NotFound();

    var stock = await context.Stocks.FindAsync(updatedItem.StockID); // Verify if the stock exists.
    if (stock is null) return Results.BadRequest("Invalid StockID");

    item.ItemDescription = updatedItem.ItemDescription;
    item.SpoilDate = updatedItem.SpoilDate;
    item.Measure = updatedItem.Measure;
    item.StockID = updatedItem.StockID;
    item.Stock = stock; // Associate the item with the stock.

    await context.SaveChangesAsync();
    return Results.Ok(item);
});

// Delete an item.
app.MapDelete("/api/items/{id}", async (EFCoreContext context, int id) =>
{
    var item = await context.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    context.Items.Remove(item);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();