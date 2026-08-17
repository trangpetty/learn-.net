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
    public async Task<List<Stock>> GetAllStocksAsync(StockQueryDto query)
    {
        
        var stocks = _context.Stock.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }
        
        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy) && query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
        {
            stocks = query.Descending
                ? stocks.OrderByDescending(s => s.Symbol).ThenBy(s => s.Id)
                : stocks.OrderBy(s => s.Symbol).ThenBy(s => s.Id);
        }
        else if (!string.IsNullOrWhiteSpace(query.SortBy) && query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
        {
            stocks = query.Descending
                ? stocks.OrderByDescending(s => s.CompanyName).ThenBy(s => s.Id)
                : stocks.OrderBy(s => s.CompanyName).ThenBy(s => s.Id);
        }
        else
        {
            stocks = query.Descending ?  stocks.OrderByDescending(a => a.Id) : stocks.OrderBy(a => a.Id);
        }
        
        var skipNumber = (query.PageNumber - 1) * query.PageSize;
        
        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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

    public async Task<Stock?> UpdateStockAsync(int id, Stock stock)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(stock => stock.Id == id);

        if (stockModel == null)
        {
            return null;
        }
        
        stockModel.CompanyName = stock.CompanyName;
        stockModel.Industry = stock.Industry;
        stockModel.LastDiv = stock.LastDiv;
        stockModel.MarketCap = stock.MarketCap;
        stockModel.Symbol = stock.Symbol;
        stockModel.Purchase = stock.Purchase;
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

    public async Task<bool> IsStockExistAsync(int stockId)
    {
        return await _context.Stock.AnyAsync(s => s.Id == stockId);
    }
}