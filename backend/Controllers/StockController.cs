using backend.Data;
using backend.DTOs;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IStockRepository _stockRepository;
    public StockController(ApplicationDBContext context, IStockRepository stockRepository)
    {
        _context = context;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        var stocks = await _stockRepository.GetAllStocksAsync();
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetStockByIdAsync(id);
        
        if (stock == null)
            return NotFound();
        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] StockRequestDto request)
    {
        var stockModel = request.ToStockFromCreate();
        await _stockRepository.CreateStockAsync(stockModel);
        return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockRequestDto request)
    {
        var stockModel = await _stockRepository.UpdateStockAsync(id, request);
        if (stockModel == null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        var stockModel = await _stockRepository.DeleteStockAsync(id);

        if (stockModel == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}