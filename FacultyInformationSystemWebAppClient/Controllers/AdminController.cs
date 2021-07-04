using FacultyInformationSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacultyInformationSystemWebAppClient.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //private object httpClient;

        public IActionResult Index()
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

        [HttpGet]
        public async Task<IActionResult> FacultyIndex()
        {
            HttpClient httpClient = new HttpClient();

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
        public IActionResult GetFacultyInfo(Faculty faculty)
        {
            return RedirectToAction("GetFacultyInfo", "Admin", new { Id = faculty.FacultyId });
        }

        [HttpGet]
        //[Route("GetPersonalInfo")]
        public async Task<IActionResult> GetFacultyInfo(int? Id)
        {


            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetFacultyInfo?facultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);
            if (clientData == null)
            {
                return NotFound();
            }

            return View(clientData);

        }

        [HttpGet]
        public async Task<IActionResult> EditJob(int? Id)
        {
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetFacultyInfo?facultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);


            string deptUrl = "https://localhost:44317/api/Admin/GetDepartment";
            var resultUrl = await httpClient.GetAsync(deptUrl);
            var jsonDataResult = resultUrl.Content.ReadAsStringAsync();

            var departmentData = JsonConvert.DeserializeObject<List<Department>>(jsonDataResult.Result);

            ViewBag.DepartmentList = departmentData.ToList();


            string designationUrl = "https://localhost:44317/api/Admin/GetDesignation";
            var DesignationUrl = await httpClient.GetAsync(designationUrl);
            var jsonDataDesignation = DesignationUrl.Content.ReadAsStringAsync();

            var designationData = JsonConvert.DeserializeObject<List<Designation>>(jsonDataDesignation.Result);

            ViewBag.DesignationList = designationData.ToList();

            if (clientData == null)
            {
                return NotFound();
            }

            return View(clientData);

        }


        [HttpPost]
        public async Task<IActionResult> EditJob(Faculty faculty)
        {

            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(faculty), null, "application/json");
            var response = await client.PostAsync("https://localhost:44317/api/Admin/UpdateFacultyJob", content);

            var jsonData = response.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);


            if (clientData != null)
            {
                return RedirectToAction("GetFacultyInfo", "Admin", new { Id = faculty.FacultyId });
            }
            else
            {
                return NotFound();
            }
            //return View();


        }



        // Returns login view.


        //[HttpGet]
        ////[Route("GetPersonalInfo")]
        //public async Task<IActionResult> GetNewUser(int UserId)
        //{

        //    //int facultyId = (int)TempData["facultyId"];
        //    //TempData["facultyId"] = facultyId;
        //    HttpClient httpClient = new HttpClient();
        //    //StringContent content = new StringContent(); // Post
        //    string Url = "https://localhost:44317/api/Faculty/GetNewUser?FacultyId=" + UserId.ToString();
        //    var result = await httpClient.GetAsync(Url);
        //    var jsonData = result.Content.ReadAsStringAsync();

        //    var clientData = JsonConvert.DeserializeObject<User>(jsonData.Result);

        //    if (clientData != null)
        //    {
        //        return View(clientData);
        //    }

        //    return NotFound();
        //}


        //[HttpGet]
        ////[Route("CreateNewUser")]
        //public IActionResult CreateNewUser()
        //{
        //    return View();
        //}

        //[HttpPost]
        ////[Route("PostNewUser")]
        //public async Task<IActionResult> PostNewUser(User user)
        //{

        //    HttpClient client = new HttpClient();
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(user), null, "application/json");
        //    var response = await client.PostAsync("https://localhost:44317/api/Admin/PostNewUser", content);

        //    var jsonData = response.Content.ReadAsStringAsync();

        //    var clientData = JsonConvert.DeserializeObject<User>(jsonData.Result);

        //    ViewBag.facultyId = user.UserId;               // Change facultyId
        //    if (clientData != null)
        //    {
        //        return RedirectToAction("GetNewUser");
        //    }

        //    return View();
        //}



        //[HttpGet]
        //public async Task<IActionResult> DeleteUser(int UserId)
        //{
        //    //int facultyId = (int)TempData["facultyId"];
        //    //TempData["facultyId"] = facultyId;
        //    HttpClient httpClient = new HttpClient();
        //    //StringContent content = new StringContent(); // Post
        //    string Url = "https://localhost:44317/api/Admin/DeleteUser?UserId=" + UserId.ToString();
        //    var result = await httpClient.DeleteAsync(Url);


        //    if (result.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}



        [HttpGet]
        //[Route("GetReportsYearly")]
        public async Task<IActionResult> GetReportsYearly(int year)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetReportsYearly?FacultyId=" + year.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpPost]
        //[Route("PostPublication")]
        public async Task<IActionResult> GetReportsYearly(Publication publication)
        {

            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(publication), null, "application/json");
            var response = await client.PostAsync("https://localhost:44317/api/Faculty/GetReportsYearly", content);

            var jsonData = response.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

            ViewBag.facultyId = publication.FacultyId;
            if (clientData != null)
            {
                return RedirectToAction("GetReportsYearly");
            }

            return View();
        }

        [HttpGet]
        //[Route("GetReportsYearly")]
        public async Task<IActionResult> GetReportsMonthly(int month)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetReportsMonthly?FacultyId=" + month.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpPost]
        //[Route("PostPublication")]
        public async Task<IActionResult> PostReportsMonthly(Publication publication)
        {

            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(publication), null, "application/json");
            var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostReportsMonthly", content);

            var jsonData = response.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

            ViewBag.facultyId = publication.FacultyId;
            if (clientData != null)
            {
                return RedirectToAction("GetReportsMonthly");
            }

            return View();
        }

        [HttpGet]
        //[Route("GetReportsYearly")]
        public async Task<IActionResult> GetReportsRecent(int count)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetReportsRecent?FacultyId=" + count.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpPost]
        //[Route("PostPublication")]
        public async Task<IActionResult> PostReportsRecent(Publication publication)
        {

            HttpClient client = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(publication), null, "application/json");
            var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostReportsRecent", content);

            var jsonData = response.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Publication>(jsonData.Result);

            ViewBag.facultyId = publication.FacultyId;
            if (clientData != null)
            {
                return RedirectToAction("GetReportsRecent");
            }

            return View();
        }



        [HttpGet]
        //[Route("GetNewSubject")]
        public async Task<IActionResult> GetSubject(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetSubjects";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Subject>>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpGet]
        //[Route("CreateNewSubject")]
        public IActionResult CreateNewSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSubject(Subject subject)
        {

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(subject), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/PostAddSubjects", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Subject>(jsonData.Result);


                if (clientData != null)
                {
                    return RedirectToAction("GetSubject");
                }

            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubject(int Id)
        {
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetSubjects";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Subject>>(jsonData.Result);
            var subjectObj = clientData.Where(m => m.SubjectId == Id).FirstOrDefault();

            return View(subjectObj);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubject(Subject subject)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(subject), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/UpdateSubject", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Subject>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetSubject");
                }

            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteSubject(int Id)
        {
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/DeleteSubject?SubjectId=" + Id.ToString();
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetSubject");
            }

            return View();
        }


        [HttpGet]
        //[Route("GetNewDesignation")]
        public async Task<IActionResult> GetDesignation(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetDesignation";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Designation>>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpGet]
        //[Route("CreateNewDesignation")]
        public IActionResult CreateNewDesignation()
        {
            return View();
        }

        [HttpPost]
        //[Route("PostDesignation")]
        public async Task<IActionResult> CreateNewDesignation(Designation designation)
        {
            if (ModelState.IsValid)
            {

                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(designation), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/PostAddDesignation", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Designation>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetDesignation");
                }
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateDesignation(int Id)
        {
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Admin/GetDesignation";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Designation>>(jsonData.Result);
            var designationObj = clientData.Where(m => m.DesignationId == Id).FirstOrDefault();

            return View(designationObj);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateDesignation(Designation designation)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(designation), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Admin/UpdateDesignation", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Designation>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetDesignation");
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteDesignation(int Id)
        {
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/DeleteDesignation?DesignationId=" + Id.ToString();
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetDesignation");
            }

            return View();
        }
    }
}
