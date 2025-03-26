//api-controller-async
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_blazor.Filter;
using webapi_blazor.Models;
//using webapi_blazor.Models;

namespace webapi_blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("allow_Get")]
    public class DemoController : ControllerBase
    {
        private readonly StoreCybersoftContext _context = new StoreCybersoftContext();
        public DemoController()
        {
            // _context = db;
        }
        [DemoFilter]
        [HttpPost("post")]
        public async Task<IActionResult> post()
        {
            return Ok("Post method");
        }
        [HttpGet("HandleUser")]
        public async Task<IActionResult> HandleUser() {
            
            int b = 0;
            int res = 10 / b;

            return Ok(res);
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            List<User> lstUser = _context.Users.ToList();
            return Ok(lstUser);
        }
        [HttpGet("GetAllSQLRaw")]
        public async Task<ActionResult> GetAllSQLRaw()
        {
            List<User> lstUser = _context.Users.FromSqlRaw("Select * From Users").ToList();
            return Ok(lstUser);
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] User newUser)
        {
            //linq:
            _context.Users.Add(newUser);
            _context.SaveChanges(); //Luuw vao db that
            return Ok(_context.Users.ToList());
        }
        [HttpPost("AddUserRaw")]
        public async Task<ActionResult> AddUserRaw([FromBody] User newUser)
        {
            //linq:
            string sqlCommand = $"INSERT INTO Users(Name,Age,Email,Additional) values(N'{newUser.Name}','{newUser.Age}','{newUser.Email}','{newUser.Additional}')";
            //linq:
            _context.Database.ExecuteSqlRaw(sqlCommand);
           
            return Ok(_context.Users.ToList());
        }

        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute]int id)
        {
            User? usDelete = _context.Users.Find(id);
            if(usDelete != null){
                _context.Users.Remove(usDelete);
                _context.SaveChanges();
            }
            return Ok(_context.Users.ToList());
        }
        [HttpDelete("/deleteraw/{id}")]
        public async Task<ActionResult> DeleteRawUser([FromRoute]int id)
        {
            // _context.Database.BeginTransaction();
            string sqlCommand = $"DELETE FROM Users where Id={id}";
            //linq:
            _context.Database.ExecuteSqlRaw(sqlCommand);
            // _context.Database.RollbackTransaction();
            // _context.Database.CommitTransaction();

            //from: can khi tra ve du lieu
            return Ok(_context.Users.ToList());
        }
        [HttpPut("/update/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute]int id,[FromBody] User userEdit)
        {
            _context.Entry(userEdit).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(_context.Users.ToList());
        }
        [HttpPut("/update2/{id}")]
        public async Task<IActionResult> UpdateUser2([FromRoute]int id,[FromBody] User userEdit)
        {
            User? usUpdate = _context.Users.Find(id);
            if(usUpdate !=null){
                usUpdate.Name = userEdit.Name;
                usUpdate.Age = userEdit.Age;
                usUpdate.Additional = userEdit.Additional;
                usUpdate.Email = userEdit.Email;
                _context.SaveChanges();
            }
            return Ok(_context.Users.ToList());
        }
    }
}