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
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[Route("GetNewDepartment")]
        public async Task<IActionResult> GetDepartment(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetDepartment";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Department>>(jsonData.Result);


            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpGet]
        //[Route("CreateNewDepartment")]
        public IActionResult CreateNewDepartment()
        {
            return View();
        }

        [HttpPost]
        //[Route("PostDepartment")]
        public async Task<IActionResult> CreateNewDepartment(Department department)
        {

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(department), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/PostAddDepartment", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Department>(jsonData.Result);


                if (clientData != null)
                {
                    return RedirectToAction("GetDepartment");
                }

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDepartment(int Id)
        {
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetDepartment";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Department>>(jsonData.Result);
            var departmentObj = clientData.Where(m => m.DeptId == Id).FirstOrDefault();

            return View(departmentObj);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(department), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/UpdateDepartment", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Department>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetDepartment");
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/DeleteDepartment?Id=" + Id.ToString();
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetDepartment");
            }

            return View();
        }

    }
}
