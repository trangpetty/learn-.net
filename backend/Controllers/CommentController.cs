using backend.Data;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly ICommentRepository _commentRepository;

    public CommentController(ApplicationDBContext context, ICommentRepository commentRepository)
    {
        _context = context;
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentDtos = comments.Select(s => s.ToCommentDto());
        return Ok(commentDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        
        return Ok(comment.ToCommentDto());
    }
}