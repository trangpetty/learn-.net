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
}