using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Comment;

public class CommentRequestDto
{
    [Required]
    [MinLength(5,  ErrorMessage = "The field must contain at least 5 characters")]
    public string Title { get; set; }
    public string Content { get; set; }
}