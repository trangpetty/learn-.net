using backend.DTOs;
using backend.DTOs.Comment;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services;

public interface IStockService
{
    Task<List<StockDto>> GetAllAsync(StockQueryDto query);
    Task<StockDto?> GetByIdAsync(int id);
    Task<List<CommentDto>> GetAllCommentsAsync(int id);
    Task<StockDto?> CreateAsync(StockRequestDto requestDto);
    Task<StockDto?> UpdateAsync(int id, StockRequestDto requestDto);
    Task DeleteAsync(int id);
}