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
        var categories = await _httpClient.GetFromJsonAsync<List<CategoryModel>>("api/Category");
        return categories ?? new List<CategoryModel>();
    }

    // Fetch a single category by ID from the API
    public async Task<CategoryModel?> GetCategoryByIdAsync(int id)
    {
        var category = await _httpClient.GetFromJsonAsync<CategoryModel>($"api/Category/{id}");
        return category;
    }

    // Create a new category via the API
    public async Task<bool> CreateCategoryAsync(CategoryModel category)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Category", category);
        return response.IsSuccessStatusCode;
    }

    // Update an existing category via the API
    public async Task<bool> UpdateCategoryAsync(CategoryModel category)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Category/{category.CategoryID}", category);
        return response.IsSuccessStatusCode;
    }

    // Delete a category by ID via the API
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Category/{id}");
        return response.IsSuccessStatusCode;
    }
}