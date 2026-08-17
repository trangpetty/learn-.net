using backend.DTOs.Comment;
using backend.Models;

namespace backend.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto()
        {
            Id = comment.Id,
            StockId = comment.StockId,
            CreatedAt = comment.CreatedAt,
            Title = comment.Title,
            Content = comment.Content
        };
    }

    public static Comment ToCommentFromRequest(this CommentRequestDto request, int stockId)
    {
        return new Comment()
        {
            Title = request.Title,
            CreatedAt = DateTime.UtcNow,
            StockId = stockId,
            Content = request.Content
        };
    }

    public static Comment ToCommentFromUpdateRequest(this CommentRequestDto request)
    {
        return new Comment()
        {
            Title = request.Title,
            Content = request.Content
        };
    }
}