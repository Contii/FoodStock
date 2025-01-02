using System.Net.Http.Json;
using FoodStock.Models;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient) // Inject HttpClient into the service
    {
        _httpClient = httpClient;
    }

    // Fetch all categories from the API
    public async Task<List<CategoryModel>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoryModel>>("api/Category");
    }

    // Fetch a single category by ID from the API
    public async Task<CategoryModel> GetCategoryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CategoryModel>($"api/Category/{id}");
    }

    // Create a new category via the API
    public async Task CreateCategoryAsync(CategoryModel category)
    {
        await _httpClient.PostAsJsonAsync("api/Category", category);
    }

    // Update an existing category via the API
    public async Task UpdateCategoryAsync(CategoryModel category)
    {
        await _httpClient.PutAsJsonAsync($"api/Category/{category.CategoryID}", category);
    }

    // Delete a category by ID via the API
    public async Task DeleteCategoryAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/Category/{id}");
    }
}