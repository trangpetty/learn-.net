using backend.Data;
using backend.DTOs.Comment;
using backend.Interfaces;
using backend.Mappers;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCommentsAsync()
    {
        var comments = await _commentService.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCommentByIdAsync([FromRoute] int id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        if (comment == null) return NotFound();
        return Ok(comment);
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> CreateCommentAsync([FromRoute] int stockId, [FromBody] CommentRequestDto requestDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var comment = await _commentService.CreateAsync(stockId, requestDto);
            return Ok(comment);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await  _commentService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCommentAsync([FromRoute] int id, [FromBody] CommentRequestDto requestDto)
    {
        var comment = await _commentService.UpdateAsync(id, requestDto);
        if (comment == null) return  NotFound();
        return Ok(comment.ToCommentDto());
    }
}