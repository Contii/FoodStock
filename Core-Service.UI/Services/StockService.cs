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
        var stocks = await _httpClient.GetFromJsonAsync<List<StockModel>>("api/Stock");
        return stocks ?? new List<StockModel>();
    }

    // Fetch a single stock by ID from the API
    public async Task<StockModel?> GetStockByIdAsync(int id)
    {
        var stock = await _httpClient.GetFromJsonAsync<StockModel>($"api/Stock/{id}");
        return stock;
    }

    // Create a new stock via the API
    public async Task<bool> CreateStockAsync(StockModel stock)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Stock", stock);
        return response.IsSuccessStatusCode;
    }

    // Update an existing stock via the API
    public async Task<bool> UpdateStockAsync(StockModel stock)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Stock/{stock.StockID}", stock);
        return response.IsSuccessStatusCode;
    }

    // Delete a stock by ID via the API
    public async Task<bool> DeleteStockAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Stock/{id}");
        return response.IsSuccessStatusCode;
    }
}