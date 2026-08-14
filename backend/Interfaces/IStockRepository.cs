using backend.DTOs;
using backend.Models;

namespace backend.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllStocksAsync();
    Task<Stock> GetStockByIdAsync(int stockId);
    Task<Stock> CreateStockAsync(Stock stock);
    Task<Stock?> UpdateStockAsync(int id, StockRequestDto stockRequestDto);
    Task<Stock?> DeleteStockAsync(int id);
}