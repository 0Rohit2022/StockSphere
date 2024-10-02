using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllStocks")]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockResponseDto());
            return Ok(stocks);
        }

        [HttpGet("GetStockById/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockResponseDto());
        }

        [HttpPost("CreateStock")]
        public IActionResult CreateStock([FromBody] StockCreateRequest create)
        {
            var stockModel = create.ToStockFromCreateRequest();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockResponseDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStockById([FromRoute] int id, [FromBody] StockUpdateRequest updateRequest)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel = updateRequest.ToStockFromUpdateRequest(stockModel);
            _context.SaveChanges();

            return Ok(stockModel.ToStockResponseDto());
        }

    }
}