//Xay dung 2 api
//getallproduct => 2 param : page index (so trang) - pageSize (so phan tu tren 1 trang) => vi du nguoi dung truyen pageIndex = 0 -> pageSize = 10 => dong 0 -> 10 .Skip(0).Take(10);
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_blazor.models.EbayDB;
using webapi_blazor.Models.ViewModel;
//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EbayContext _context;
        public ProductController(EbayContext db)
        {
            _context = db;
        }
        //Xac thuc, Phan quyen
        [Authorize(Roles = "Admin")]
        [HttpGet("/Product/Getall")]
        public async Task<IActionResult> GetAll(int pageIndex = 0, int pageSize = 10)
        {
            var productList = _context.Products.Skip(pageIndex * pageSize).Take(pageSize);
            return Ok();
        }
        [HttpGet("/Product/GetProductCategory")]
        public async Task<IActionResult> GetProductListCategory()
        {
            return Ok(_context.Products.Skip(0).Take(10));
        }
        [HttpGet("/Product/GetallsqlRaw")]
        public async Task<IActionResult> GetAllSQLRaw(int pageIndex = 0, int pageSize = 10)
        {
            bool isNumber = int.TryParse(pageIndex.ToString(), out pageIndex);
            var productList = _context.Products.FromSqlRaw($"select * from products order by id offset {pageIndex} rows fetch next {pageSize} rows only;").ToList();
            return Ok(productList);
        }
        [HttpGet("/Product/GetbyId/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var productDetail = _context.Products.SingleOrDefault(prod => prod.Id == id);
            if (productDetail != null)
            {
                return Ok(productDetail);
            }
            return BadRequest("Ma Sp khong ton tai");
        }
        [HttpGet("/GetDetailProductById/{id}")]
        public async Task<ActionResult> GetDetailProductById(int id)
        {
            var result = await _context.Database.SqlQueryRaw<ProductDetailVM>($@"Select PR.Id, PR.Name, C.Name as 'Category', PR.[Description], PR.Price, PR.CreatedAt, (Select PI2.ImageUrl from ProductImages PI2 Where PI2.ProductId = PR.Id for json path) as ListImage From Products PR INNER JOIN Categories C on PR.CategoryId = C.Id INNER JOIN ProductImages PI On PI.ProductId = PR.Id Where PR.Id = {id} GROUP By pr.id, Pr.name, C.name, Pr.[Description],Pr.price,Pr.CreatedAt").ToListAsync();

            if (result.Count() == 0)
            {
                return BadRequest("id không tồn tại");
            }
            // List<ProductDetailVM> res = new List<ProductDetailVM>();
            // var resGroup = result.GroupBy(key => new
            // {
            //     Id = key.Id,
            //     Name = key.Name,
            //     Category = key.Category,
            //     Price = key
            // .Price,
            //     CreateAt = key.CreatedAt
            // }).ToList();
            // List<string> lstImg = new List<string>();
            // foreach (var item in resGroup)
            // {
            //     foreach (var img in item)
            //     {
            //         lstImg.Add(img.ListImage);
            //     }
            // }
            // ProductDetailVM resFinal = new ProductDetailVM();
            // resFinal.Id = resGroup.First().Key.Id;
            // resFinal.Name = resGroup.First().Key.Name;
            // resFinal.Price = resGroup.First().Key.Id;
            // resFinal.ListImage = lstImg.ToString();;

            return Ok(result);
        }

    }
}