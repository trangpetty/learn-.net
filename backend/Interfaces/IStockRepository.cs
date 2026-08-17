using backend.DTOs;
using backend.Models;

namespace backend.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllStocksAsync(StockQueryDto query);
    Task<Stock> GetStockByIdAsync(int stockId);
    Task<Stock> CreateStockAsync(Stock stock);
    Task<Stock?> UpdateStockAsync(int id, Stock stock);
    Task<Stock?> DeleteStockAsync(int id);
    Task<bool> IsStockExistAsync(int stockId);
}