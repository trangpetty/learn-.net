using backend.Data;
using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;
    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    public Task<List<Stock>> GetAllStocksAsync()
    {
        
        return _context.Stock.Include(c => c.Comments).ToListAsync();
    }

    public async Task<Stock> GetStockByIdAsync(int stockId)
    {
        var stockModel = await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == stockId);

        if (stockModel == null)
        {
            return null;
        }
        
        return stockModel;
    }

    public async Task<Stock> CreateStockAsync(Stock stock)
    {
        await _context.Stock.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id, StockRequestDto request)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);

        if (stockModel == null)
        {
            return null;
        }
        
        stockModel.CompanyName = request.CompanyName;
        stockModel.Industry = request.Industry;
        stockModel.LastDiv = request.LastDiv;
        stockModel.MarketCap = request.MarketCap;
        stockModel.Symbol = request.Symbol;
        stockModel.Purchase = request.Purchase;
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteStockAsync(int id)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);

        if (stockModel == null)
        {
            return null;
        }
        
        _context.Stock.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }
}