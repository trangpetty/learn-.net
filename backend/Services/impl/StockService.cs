using backend.DTOs;
using backend.DTOs.Comment;
using backend.Interfaces;
using backend.Mappers;

namespace backend.Services.impl;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;
    public StockService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    
    public async Task<List<StockDto>> GetAllAsync(StockQueryDto query)
    {
        var stocks = await _stockRepository.GetAllStocksAsync(query);
        var stockDtos = stocks.Select(s => s.ToStockDto());
        return stockDtos.ToList();
    }

    public async Task<StockDto?> GetByIdAsync(int id)
    {
        var stocks = await _stockRepository.GetStockByIdAsync(id);
        return stocks?.ToStockDto();
    }

    public async Task<List<CommentDto>> GetAllCommentsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<StockDto?> CreateAsync(StockRequestDto requestDto)
    {
        var stockModel = requestDto.ToStockFromCreate();
        var stock = await _stockRepository.CreateStockAsync(stockModel);
        return stock.ToStockDto();
    }

    public async Task<StockDto?> UpdateAsync(int id, StockRequestDto requestDto)
    {
        var stockModel = requestDto.ToStockFromCreate();
        var stock = await _stockRepository.UpdateStockAsync(id, stockModel);
        return stock?.ToStockDto();
    }

    public async Task DeleteAsync(int id)
    {
        await _stockRepository.DeleteStockAsync(id);
    }
}