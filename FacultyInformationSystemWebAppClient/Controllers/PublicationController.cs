using FacultyInformationSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FacultyInformationSystemWebAppClient.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {

        [HttpGet]
        public IActionResult GetYearIndex()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetMonthIndex()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetRecordCountIndex()
        {
            return View();
        }

        [HttpGet]
        //[Route("GetPublications")]
        public async Task<IActionResult> GetPublicationsStudent(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetPublications?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Publication>>(jsonData.Result);

            if (clientData == null)
            {
                return NotFound();
            }

            return View(clientData);
        }



        [HttpPost]
        public async Task<IActionResult> PostYear(Publication publication)
        {

            int year = publication.PublicationId;
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetReportsYearly?year=" + year.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Publication>>(jsonData.Result);

            if (clientData != null)
            {
                return View("GetAdminPublications", clientData);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> PostMonth(Publication publication)
        {
            int month = publication.PublicationId;
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetReportsMonthly?month=" + month.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Publication>>(jsonData.Result);

            if (clientData != null)
            {
                return View("GetAdminPublications", clientData);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostDays(Publication publication)
        {
            int days = publication.PublicationId;
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetReportsRecent?count=" + days.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Publication>>(jsonData.Result);

            if (clientData != null)
            {
                return View("GetAdminPublications", clientData);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetFilterIndex(int? Id)
        {
            if (Id == 1)
            {
                return RedirectToAction("GetYearIndex");
            }
            else if (Id == 2)
            {
                return RedirectToAction("GetMonthIndex");
            }
            else if (Id == 3)
            {
                return RedirectToAction("GetRecordCountIndex");
            }
            return View();
        }


        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        //[Route("GetPublications")]
        public async Task<IActionResult> GetPublications(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetPublications?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Publication>>(jsonData.Result);

            if (clientData == null)
            {
                return RedirectToAction("CreatePublication");
            }

            return View(clientData);
        }



        [HttpGet]
        //[Route("CreatePublication")]
        public IActionResult CreatePublication()
        {
            return View();
        }

        [HttpPost]
        //[Route("PostPublication")]
        public async Task<IActionResult> CreatePublication(Publication publication)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                publication.FacultyId = int.Parse(claim.Value);
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(publication), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostPublications", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetPublications", new { Id = publication.FacultyId });
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePublication(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Faculty/GetPublications?FacultyId=" + UniversalId.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Publication>>(jsonData.Result);

            Publication publication = clientData.Where(m => m.PublicationId == Id).FirstOrDefault();
            return View(publication);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePublication(Publication publication)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                int UniversalId = int.Parse(claim.Value);
                publication.FacultyId = UniversalId;
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(publication), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/UpdatePublication", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

                if (clientData != null)
                {

                    return RedirectToAction("GetPublications", "Publication", new { Id = publication.FacultyId });
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeletePublication(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = string.Format($"https://localhost:44317/api/Faculty/DeletePublication?PublicationId={Id.ToString()}&FacultyId={UniversalId.ToString()}");
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetPublications", new { Id = UniversalId });
            }

            return View();
        }

    }
}
