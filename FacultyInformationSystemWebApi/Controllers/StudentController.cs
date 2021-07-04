using FacultyInformationSystemEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacultyInformationSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        //[HttpPost]
        //[Route("LoginCheck")]
        //public IActionResult LoginCheck(User user)
        //{
        //    bool status;
        //    var userObj = _dbContext.Users.Where(m => m.UserName == user.UserName && m.Password == user.Password && m.Usertype == user.Usertype).FirstOrDefault();
        //    if (userObj != null)
        //        status = true;
        //    else
        //        status = false;
        //    return Ok(status);
        //}

        [HttpGet]
        [Route("GetPublications")]
        public IActionResult GetPublications(int facultyId)
        {
            var publicationObj = _dbContext.Publications.Where(m => m.FacultyId == facultyId).ToList();
            return Ok(publicationObj);
        }
    }
}
