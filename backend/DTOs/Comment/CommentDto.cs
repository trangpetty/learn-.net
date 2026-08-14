namespace backend.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? StockId { get; set; }
}