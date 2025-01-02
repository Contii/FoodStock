using System.Net.Http.Json;
using FoodStock.Models;


public class StockService
{
    private readonly HttpClient _httpClient;

    public StockService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Fetch all stocks from the API
    public async Task<List<StockModel>> GetStocksAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<StockModel>>("api/Stock");
    }

    // Fetch a single stock by ID from the API
    public async Task<StockModel> GetStockByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<StockModel>($"api/Stock/{id}");
    }

    // Create a new stock via the API
    public async Task CreateStockAsync(StockModel stock)
    {
        await _httpClient.PostAsJsonAsync("api/Stock", stock);
    }

    // Update an existing stock via the API
    public async Task UpdateStockAsync(StockModel stock)
    {
        await _httpClient.PutAsJsonAsync($"api/Stock/{stock.StockID}", stock);
    }

    // Delete a stock by ID via the API
    public async Task DeleteStockAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/Stock/{id}");
    }
}