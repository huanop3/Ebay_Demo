using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi_blazor.models.EbayDB;
//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbayController : ControllerBase
    {
        public readonly EbayContext _context;
        public EbayController( EbayContext db)
        {
            _context = db;
        }
        [HttpGet("/Ebay/Getall")]
        public async Task<IActionResult> GetAllProduct()
        {
            var productList = _context.EbayProducts.ToList();
            return Ok(productList);
        }
        [HttpGet("/Ebay/GetByCategory")]
        public async Task<IActionResult> GetByCategory([FromQuery] string category)
        {
            var productList = _context.EbayProducts.Where(p => p.Category == category).ToList();
            return Ok(productList);
        }
        [HttpGet("/Ebay/SearchByKeyword")]
        public async Task<IActionResult> SearchByKeyword([FromQuery] string keyword)
        {
            var productList = _context.EbayProducts.Where(p => p.ProductName.ToLower().Contains(keyword.ToLower()));
            return Ok(productList);
        }
        [HttpGet("/Ebay/SearchByKeywordAndCategory")]
        public async Task<IActionResult> SearchByKeywordAndCategory([FromQuery] string keyword, [FromQuery] string category)
        {
            var productList = _context.EbayProducts.Where(p => p.ProductName.ToLower().Contains(keyword.ToLower()) && p.Category == category);
            return Ok(productList);
        }
    }
}