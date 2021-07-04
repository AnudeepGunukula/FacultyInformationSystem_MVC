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
    public class FacultyController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [HttpGet]
        //[Route("GetPersonalInfo")]
        public async Task<IActionResult> GetPersonalInfo(int? Id)
        {


            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetPersonalInfo?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);
            if (clientData == null)
            {
                return RedirectToAction("CreateFacultyInfo");
            }

            return View(clientData);

        }


        [HttpGet]
        //[Route("CreateFacultyInfo")]
        public async Task<IActionResult> CreateFacultyInfo()
        {

            HttpClient httpClient = new HttpClient();
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


            return View();
        }

        [HttpPost]
        //[Route("PostFacultyInfo")]
        public async Task<IActionResult> CreateFacultyInfo(Faculty faculty)
        {
            if (ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                faculty.FacultyId = int.Parse(claim.Value);
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(faculty), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostFacultyInfo", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);


                if (clientData != null)
                {
                    return RedirectToAction("GetPersonalInfo", new { Id = faculty.FacultyId });
                }

            }
            HttpClient httpClient = new HttpClient();
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


            return View();


        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {


            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Faculty/GetPersonalInfo?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);
            return View(clientData);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(faculty), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/UpdatePersonalInfo", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Faculty>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetPersonalInfo", "Faculty", new { Id = faculty.FacultyId });
                }
            }
            return View();




        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/DeletePersonalInfo?FacultyId=" + Id.ToString();
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Index(int? Id)
        {

            return View();
        }



        [HttpGet]
        //[Route("GetCourseTaughts")]
        public async Task<IActionResult> GetCourseTaughts(int? Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetCourseTaughts?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<CourseTaught>>(jsonData.Result);

            if (clientData != null)
            {
                return View(clientData);
            }

            return NotFound();
        }

        [HttpGet]
        //[Route("CreatePublication")]
        public async Task<IActionResult> CreateCourseTaughts()
        {

            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetSubjects";
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var subjectData = JsonConvert.DeserializeObject<List<Subject>>(jsonData.Result);
            ViewBag.SubjectList = subjectData;


            return View();
        }

        [HttpPost]
        //[Route("PostPublication")]
        public async Task<IActionResult> CreateCourseTaughts(CourseTaught courseTaught)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                courseTaught.FacultyId = int.Parse(claim.Value);
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(courseTaught), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostCourseTaughts", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<CourseTaught>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetCourseTaughts", new { Id = courseTaught.FacultyId });
                }
            }
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Admin/GetSubjects";
            var result = await httpClient.GetAsync(Url);
            var jsonnData = result.Content.ReadAsStringAsync();

            var subjectData = JsonConvert.DeserializeObject<List<Subject>>(jsonnData.Result);
            ViewBag.SubjectList = subjectData;


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourseTaughts(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Faculty/GetCourseTaughts?FacultyId=" + UniversalId.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<CourseTaught>>(jsonData.Result);

            var courseData = clientData.Where(m => m.CourseId == Id).FirstOrDefault();


            //StringContent content = new StringContent(); // Post
            string subjectUrl = "https://localhost:44317/api/Admin/GetSubjects";
            var subjectresult = await httpClient.GetAsync(subjectUrl);
            var jsonDataSubject = subjectresult.Content.ReadAsStringAsync();

            var subjectData = JsonConvert.DeserializeObject<List<Subject>>(jsonDataSubject.Result);
            ViewBag.SubjectList = subjectData;

            return View(courseData);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourseTaughts(CourseTaught courseTaught)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                int UniversalId = int.Parse(claim.Value);
                courseTaught.FacultyId = UniversalId;
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(courseTaught), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/UpdateCourseTaughts", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<CourseTaught>(jsonData.Result);

                if (clientData != null)
                {

                    return RedirectToAction("GetCourseTaughts", new { Id = UniversalId });
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteCourseTaught(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);
            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = string.Format($"https://localhost:44317/api/Faculty/DeleteCourseTaught?CourseId={Id.ToString()}&FacultyId={UniversalId.ToString()}");
            var result = await httpClient.DeleteAsync(Url);


            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetCourseTaughts", new { Id = UniversalId });
            }

            return View();
        }
        [HttpGet]
        //[Route("GetGrants")]
        public async Task<IActionResult> GetGrants(int Id)
        {

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = "https://localhost:44317/api/Faculty/GetGrants?FacultyId=" + Id.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Grant>>(jsonData.Result);

            if (clientData == null)
            {
                return RedirectToAction("CreateGrants");

            }

            return View(clientData);

        }

        [HttpGet]
        //[Route("CreateGrants")]
        public IActionResult CreateGrants()
        {
            return View();
        }

        [HttpPost]
        //[Route("PostGrants")]
        public async Task<IActionResult> CreateGrants(Grant grants)
        {
            if (ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                grants.FacultyId = int.Parse(claim.Value);
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(grants), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/PostGrants", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Grant>(jsonData.Result);


                if (clientData != null)
                {
                    return RedirectToAction("GetGrants", new { Id = grants.FacultyId });
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGrants(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);
            HttpClient httpClient = new HttpClient();
            string Url = "https://localhost:44317/api/Faculty/GetGrants?FacultyId=" + UniversalId.ToString();
            var result = await httpClient.GetAsync(Url);
            var jsonData = result.Content.ReadAsStringAsync();

            var clientData = JsonConvert.DeserializeObject<List<Grant>>(jsonData.Result);
            Grant grantObj = clientData.Where(m => m.GrantId == Id).FirstOrDefault();
            return View(grantObj);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateGrants(Grant grants)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                int UniversalId = int.Parse(claim.Value);
                grants.FacultyId = UniversalId;
                HttpClient client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(grants), null, "application/json");
                var response = await client.PostAsync("https://localhost:44317/api/Faculty/UpdateGrants", content);

                var jsonData = response.Content.ReadAsStringAsync();

                var clientData = JsonConvert.DeserializeObject<Grant>(jsonData.Result);

                if (clientData != null)
                {
                    return RedirectToAction("GetGrants", "Faculty", new { Id = grants.FacultyId });
                }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> DeleteGrants(int Id)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            int UniversalId = int.Parse(claim.Value);

            //int facultyId = (int)TempData["facultyId"];
            //TempData["facultyId"] = facultyId;
            HttpClient httpClient = new HttpClient();
            //StringContent content = new StringContent(); // Post
            string Url = string.Format($"https://localhost:44317/api/Faculty/DeleteGrants?GrantId={Id.ToString()}&FacultyId={UniversalId.ToString()}");
            var result = await httpClient.DeleteAsync(Url);

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("GetGrants", new { Id = UniversalId });
            }
            return View();
        }

    }
}
