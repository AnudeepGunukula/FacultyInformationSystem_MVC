using FacultyInformationSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacultyInformationSystemWebAppClient.Controllers
{

    [Authorize]
    public class StudentController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> GetFacultyIndex()
        {
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetFacultyList";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Faculty>>(jsonData.Result);
            if (clientData == null)
            {
                return NotFound();
            }

            ViewBag.FacultyList = clientData;

            return View();
        }


        [HttpPost]
        //[Route("GetPublications")]
        public IActionResult PostPublications(Faculty faculty)
        {

            if (faculty != null)
            {
                return RedirectToAction("GetPublicationsStudent", "Publication", new { Id = faculty.FacultyId });
            }
            return View();

        }


        public IActionResult Index()
        {

            return View();
        }
    }
}
