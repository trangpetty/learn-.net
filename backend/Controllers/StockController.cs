using backend.Data;
using backend.DTOs;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;
    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStocks([FromQuery] StockQueryDto query)
    {
        var stocks = await _stockService.GetAllAsync(query);   
        return Ok(stocks);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stock = await _stockService.GetByIdAsync(id);
        if (stock == null) return NotFound();
        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] StockRequestDto request)
    {
        var stock = await _stockService.CreateAsync(request);
        return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockRequestDto request)
    {
        var stockModel = await _stockService.UpdateAsync(id, request);
        if (stockModel == null) return NotFound();
        return Ok(stockModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        await _stockService.DeleteAsync(id);
        return NoContent();
    }
}