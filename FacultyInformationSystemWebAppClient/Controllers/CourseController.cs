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
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //[Route("GetNewCourse")]
        public async Task<IActionResult> GetCourse(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetCourses";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Course>>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpGet]
        //[Route("CreateNewCourse")]
        public async Task<IActionResult> CreateNewCourse()
        {
            HttpClient httpClient = new HttpClient();
            string deptUrl = "https://localhost:44317/api/Admin/GetDepartment";
            var resultUrl = await httpClient.GetAsync(deptUrl);
            var jsonDataResult = resultUrl.Content.ReadAsStringAsync();

            var departmentData = JsonConvert.DeserializeObject<List<Department>>(jsonDataResult.Result);

            ViewBag.DepartmentList = departmentData.ToList();
            return View();
        }

        [HttpPost]
        //[Route("PostCourse")]
        public async Task<IActionResult> CreateNewCourse(Course course)
        {

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(course), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/PostAddCourses", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Course>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetCourse");
                }
            }
            HttpClient httpClient = new HttpClient();
            string deptUrl = "https://localhost:44317/api/Admin/GetDepartment";
            var resultUrl = await httpClient.GetAsync(deptUrl);
            var jsonDataResult = resultUrl.Content.ReadAsStringAsync();

            var departmentData = JsonConvert.DeserializeObject<List<Department>>(jsonDataResult.Result);

            ViewBag.DepartmentList = departmentData.ToList();

            return View();
        }



        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int Id)
        {
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetCourses";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Course>>(jsonData.Result);

            var courseObj = clientData.Where(m => m.CourseId == Id).FirstOrDefault();

            string deptUrl = "https://localhost:44317/api/Admin/GetDepartment";
            var resultUrl = await httpClient.GetAsync(deptUrl);
            var jsonDataResult = resultUrl.Content.ReadAsStringAsync();

            var departmentData = JsonConvert.DeserializeObject<List<Department>>(jsonDataResult.Result);

            ViewBag.DepartmentList = departmentData.ToList();

            return View(courseObj);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(course), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/UpdateCourse", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Course>(jsonData.Result);

                if (clientData != null)
                {

                    return RedirectToAction("GetCourse");
                }
            }
            HttpClient httpClient = new HttpClient();
            string deptUrl = "https://localhost:44317/api/Admin/GetDepartment";
            var resultUrl = await httpClient.GetAsync(deptUrl);
            var jsonDataResult = resultUrl.Content.ReadAsStringAsync();

            var departmentData = JsonConvert.DeserializeObject<List<Department>>(jsonDataResult.Result);

            ViewBag.DepartmentList = departmentData.ToList();
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteCourse(int Id)
        {
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/DeleteCourse?CourseId=" + Id.ToString();
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetCourse");
            }

            return View();
        }
    }
}
