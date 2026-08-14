using backend.DTOs;
using backend.Models;

namespace backend.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto()
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
            Purchase = stockModel.Purchase,
            Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
        };
    }

    public static Stock ToStockFromCreate(this StockRequestDto request)
    {
        return new Stock()
        {
            CompanyName = request.CompanyName,
            Industry = request.Industry,
            MarketCap = request.MarketCap,
            Purchase = request.Purchase,
            LastDiv = request.LastDiv,
            Symbol = request.Symbol
        };
    }
}