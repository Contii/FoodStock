using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodStock.Models;

public class ItemService
{
    private readonly HttpClient _httpClient;

    public ItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetch all items from the API
    public async Task<List<ItemModel>> GetItemsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ItemModel>>("api/Item");
    }

    // Fetch a single item by ID from the API
    public async Task<ItemModel> GetItemByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ItemModel>($"api/Item/{id}");
    }

    // Create a new item via the API
    public async Task CreateItemAsync(ItemModel item)
    {
        await _httpClient.PostAsJsonAsync("api/Item", item);
    }

    // Update an existing item via the API
    public async Task UpdateItemAsync(ItemModel item)
    {
        await _httpClient.PutAsJsonAsync($"api/Item/{item.ItemID}", item);
    }

    // Delete an item by ID via the API
    public async Task DeleteItemAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/Item/{id}");
    }
}