using backend.DTOs.Comment;
using backend.Models;

namespace backend.Services;

public interface ICommentService
{
    Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(int id);
    Task<CommentDto> CreateAsync(int stockId, CommentRequestDto requestDto);
    Task<Comment?> DeleteAsync(int id);
    Task<Comment?> UpdateAsync(int id, CommentRequestDto requestDto);
}