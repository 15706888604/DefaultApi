using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DefaultApi.Models;

namespace DefaultApi.Controllers
{
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public HomeController(SqlDbContext context){_context = context;}

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <remarks>
        /// Home/GetUsers
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            List<Users> users = _context.Users.ToList<Users>();
            return Ok(users);
        }

        /// <summary>
        /// 根据ID查询用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SelectUser(int ID)
        {
            var user = _context.Users.Where(c => c.ID == ID);
            return Ok(user);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="Name">姓名</param>
        /// <param name="Age">年龄</param>
        [HttpPost]
        public IActionResult CreateUser(string Name, int Age)
        {
            if (string.IsNullOrEmpty(Name))
                return Ok("姓名不为空");
            Users user = new Users
            {
                Name = Name,
                Age = Age
            };
            _context.Add(user);
            return Ok(_context.SaveChanges() == 1 ? "新增成功" : "新增失败");
        }

        /// <summary>
        /// 根据ID修改用户
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        /// <param name="Age"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateUser(int ID, string Name, int Age)
        {
            var v1 = _context.Users.Where(c => c.ID == ID);
            Users user = v1.Take(1).Single();
            if (!string.IsNullOrEmpty(Name))
                user.Name = Name;
            user.Age = Age;
            return Ok(_context.SaveChanges() == 1 ? "修改成功" : "修改失败");
        }

        /// <summary>
        /// 根据ID删除用户
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteUser(int ID)
        {
            var v1 = _context.Users.Where(c => c.ID == ID);
            Users user = v1.Take(1).Single();
            _context.Users.Remove(user);
            return Ok(_context.SaveChanges() == 1 ? "删除成功" : "删除失败");
        }

    }
}
