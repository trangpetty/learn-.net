using backend.DTOs.Comment;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;

namespace backend.Services.impl;

public class CommentService : ICommentService
{
    private readonly IStockRepository _stockRepository;
    private readonly ICommentRepository _commentRepository;

    public CommentService(IStockRepository stockRepository, ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        return comments.Select(c => c.ToCommentDto()).ToList();
    }

    public async Task<CommentDto?> GetByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        return comment?.ToCommentDto();
    }

    public async Task<CommentDto> CreateAsync(int stockId, CommentRequestDto requestDto)
    {
        if (!await _stockRepository.IsStockExistAsync(stockId))
        {
            throw new InvalidOperationException("Stock is not exist");
        }

        var commentModel = requestDto.ToCommentFromRequest(stockId);
        var comment = await _commentRepository.CreateAsync(commentModel);
        return comment.ToCommentDto();
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        return await _commentRepository.DeleteAsync(id);
    }

    public async Task<Comment?> UpdateAsync(int id, CommentRequestDto requestDto)
    {
        var comment = requestDto.ToCommentFromUpdateRequest();
        return await _commentRepository.UpdateAsync(id, comment);
    }
}