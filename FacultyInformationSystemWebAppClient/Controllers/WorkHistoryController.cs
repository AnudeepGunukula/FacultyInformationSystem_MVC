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
    public class WorkHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[Route("GetJobHistory")]
        public async Task<IActionResult> GetJobHistory(int Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetJobHistory?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<WorkHistory>>(jsonData.Result);

            if (clientData == null)
            {
                return RedirectToAction("CreateJobHistory");
            }

            return View(clientData);
        }

        [HttpGet]
        //[Route("CreateJobHistory")]
        public IActionResult CreateJobHistory()
        {
            return View();
        }

        [HttpPost]
        //[Route("PostJobHistory")]
        public async Task<IActionResult> CreateJobHistory(WorkHistory workHistory)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                workHistory.FacultyId = int.Parse(claim.Value);


                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(workHistory), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostJobHistory", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<WorkHistory>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetJobHistory", new { Id = workHistory.FacultyId });
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateJobHistory(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);

            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Faculty/GetJobHistory?FacultyId=" + UniversalId.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<WorkHistory>>(jsonData.Result);
            WorkHistory workHistory = clientData.Where(m => m.WorkHistoryId == Id).FirstOrDefault();

            return View(workHistory);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateJobHistory(WorkHistory workHistory)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                int UniversalId = int.Parse(claim.Value);
                workHistory.FacultyId = UniversalId;
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(workHistory), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/UpdateJobHistory", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<WorkHistory>(jsonData.Result);

                if (clientData != null)
                {

                    return RedirectToAction("GetJobHistory", "WorkHistory", new { Id = workHistory.FacultyId });
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteJobHistory(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = string.Format($"https://localhost:44317/api/Faculty/DeleteWorkHistory?WorkHistoryId={Id.ToString()}&FacultyId={UniversalId.ToString()}");
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetJobHistory", new { Id = UniversalId });
            }

            return View();
        }

    }
}
