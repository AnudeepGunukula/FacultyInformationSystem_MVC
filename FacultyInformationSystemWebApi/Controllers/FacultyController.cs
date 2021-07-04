using FacultyInformationSystemEntities;
using Microsoft.AspNetCore.Authorization;
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

    public class FacultyController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public FacultyController(ApplicationDbContext context)
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
        [Route("GetPersonalInfo")]
        public IActionResult GetPersonalInfo(int facultyId)
        {
            Faculty facultyData = _dbContext.Faculties.FirstOrDefault(m => m.FacultyId == facultyId);
            if (facultyData != null)
            {
                facultyData.DepartmentName = _dbContext.Departments.Where(d => d.DeptId == facultyData.DeptId).FirstOrDefault().DeptName;
                facultyData.DesignationName = _dbContext.Designations.Where(d => d.DesignationId == facultyData.DesignationId).FirstOrDefault().DesignationName;

            }


            return Ok(facultyData);
        }



        [HttpPost]
        [Route("PostFacultyInfo")]
        public IActionResult PostFacultyInfo(Faculty facultyObj)
        {
            _dbContext.Faculties.Add(facultyObj);
            _dbContext.SaveChanges();
            return Ok(facultyObj);
        }


        [HttpPost]
        [Route("UpdatePersonalInfo")]
        public IActionResult UpdatePersonalInfo(Faculty faculty)
        {
            Faculty facultyObj = _dbContext.Faculties.FirstOrDefault(m => m.FacultyId == faculty.FacultyId);
            if (facultyObj != null)
            {
                facultyObj.FirstName = faculty.FirstName;
                facultyObj.LastName = faculty.LastName;
                facultyObj.Address = faculty.Address;
                facultyObj.City = faculty.City;
                facultyObj.State = faculty.State;
                facultyObj.Pincode = faculty.Pincode;
                facultyObj.MobileNo = faculty.MobileNo;
                facultyObj.HireDate = faculty.HireDate;
                facultyObj.EmailAddress = faculty.EmailAddress;
                facultyObj.DateofBirth = faculty.DateofBirth;
                facultyObj.DeptId = faculty.DeptId;
                facultyObj.DesignationId = faculty.DesignationId;
                _dbContext.SaveChanges();

            }
            return Ok(facultyObj);

        }


        [HttpDelete]
        [Route("DeletePersonalInfo")]

        public IActionResult DeletePersonalInfo(int FacultyId)
        {
            var FacultyObj = _dbContext.Faculties.FirstOrDefault(m => m.FacultyId == FacultyId);


            _dbContext.Faculties.Remove(FacultyObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }

        [HttpGet]
        [Route("GetJobHistory")]
        public IActionResult GetJobHistory(int FacultyId)
        {
            var jobHistoryObj = _dbContext.WorkHistories.Where(m => m.FacultyId == FacultyId).ToList();
            return Ok(jobHistoryObj);
        }

        [HttpPost]
        [Route("PostJobHistory")]
        public IActionResult PostJobHistory(WorkHistory workHistory)
        {
            _dbContext.WorkHistories.Add(workHistory);
            _dbContext.SaveChanges();
            return Ok(workHistory);
        }


        [HttpPost]
        [Route("UpdateJobHistory")]
        public IActionResult UpdateJobHistory(WorkHistory workHistory)
        {
            var workHistoryObj = _dbContext.WorkHistories.Where(m => m.FacultyId == workHistory.FacultyId).FirstOrDefault();
            if (workHistoryObj != null)
            {
                workHistoryObj.Organization = workHistory.Organization;
                workHistoryObj.JobTitle = workHistory.JobTitle;
                workHistoryObj.JobBeginDate = workHistory.JobBeginDate;
                workHistoryObj.JobEndDate = workHistory.JobEndDate;
                workHistoryObj.JobResponsibilities = workHistory.JobResponsibilities;
                workHistoryObj.JobType = workHistory.JobType;
                _dbContext.SaveChanges();
            }

            return Ok(workHistoryObj);
        }

        [HttpDelete]
        [Route("DeleteWorkHistory")]

        public IActionResult DeleteWorkHistory(int FacultyId, int workHistoryId)
        {
            var workHistoryObj = _dbContext.WorkHistories.Where(m => m.FacultyId == FacultyId && m.WorkHistoryId == workHistoryId).FirstOrDefault();

            _dbContext.WorkHistories.Remove(workHistoryObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }




        [HttpGet]
        [Route("GetPublications")]
        public IActionResult GetPublications(int FacultyId)
        {
            var publicationsObj = _dbContext.Publications.Where(m => m.FacultyId == FacultyId).ToList();

            return Ok(publicationsObj);
        }

        [HttpPost]
        [Route("PostPublications")]
        public IActionResult PostPublications(Publication publication)
        {
            _dbContext.Publications.Add(publication);
            _dbContext.SaveChanges();
            return Ok(publication);
        }


        [HttpPost]
        [Route("UpdatePublication")]
        public IActionResult UpdatePublication(Publication publication)
        {
            var publicationObj = _dbContext.Publications.Where(m => m.FacultyId == publication.FacultyId).FirstOrDefault();
            if (publicationObj != null)
            {
                publicationObj.PublicationTitle = publication.PublicationTitle;
                publicationObj.ArticleName = publication.ArticleName;
                publicationObj.PublisherName = publication.PublisherName;
                publicationObj.PublisherLocation = publication.PublisherLocation;
                publicationObj.CitationDate = publication.CitationDate;
                _dbContext.SaveChanges();
            }

            return Ok(publicationObj);
        }

        [HttpDelete]
        [Route("DeletePublication")]

        public IActionResult DeletePublication(int FacultyId, int PublicationId)
        {
            var publicationObj = _dbContext.Publications.Where(m => m.FacultyId == FacultyId && m.PublicationId == PublicationId).FirstOrDefault();

            _dbContext.Publications.Remove(publicationObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }



        [HttpGet]
        [Route("GetCourseTaughts")]
        public IActionResult GetCourseTaughts(int FacultyId)
        {

            var courseTaughtObj = _dbContext.CourseTaughts.Where(m => m.FacultyId == FacultyId).Select(m => new CourseTaught
            {

                SubjectName = m.Subject.SubjectName,
                FirstDateTaught = m.FirstDateTaught,
                CourseId = m.CourseId,
                FacultyId = m.FacultyId

            }
               ).ToList();

            return Ok(courseTaughtObj);
        }




        [HttpPost]
        [Route("PostCourseTaughts")]
        public IActionResult PostCourseTaughts(CourseTaught courseTaught)
        {
            _dbContext.CourseTaughts.Add(courseTaught);
            _dbContext.SaveChanges();
            return Ok(courseTaught);
        }


        [HttpPost]
        [Route("UpdateCourseTaughts")]
        public IActionResult UpdateCourseTaughts(CourseTaught courseTaught)
        {
            var courseTaughtObj = _dbContext.CourseTaughts.Where(m => m.FacultyId == courseTaught.FacultyId).FirstOrDefault();
            if (courseTaughtObj != null)
            {
                courseTaughtObj.FirstDateTaught = courseTaught.FirstDateTaught;
                courseTaughtObj.SubjectId = courseTaught.SubjectId;
                courseTaught.CourseId = courseTaught.CourseId;
                _dbContext.SaveChanges();
            }

            return Ok(courseTaughtObj);
        }


        [HttpDelete]
        [Route("DeleteCourseTaught")]

        public IActionResult DeleteCourseTaught(int FacultyId, int courseId)
        {
            var courseTaughtObj = _dbContext.CourseTaughts.Where(m => m.FacultyId == FacultyId && m.CourseId == courseId).FirstOrDefault();

            _dbContext.CourseTaughts.Remove(courseTaughtObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }




        [HttpGet]
        [Route("GetGrants")]
        public IActionResult GetGrants(int FacultyId)
        {
            var grantsObj = _dbContext.Grants.Where(m => m.FacultyId == FacultyId).ToList();
            return Ok(grantsObj);
        }


        [HttpPost]
        [Route("PostGrants")]
        public IActionResult PostGrants(Grant grants)
        {
            _dbContext.Grants.Add(grants);
            _dbContext.SaveChanges();
            return Ok(grants);
        }


        [HttpPost]
        [Route("UpdateGrants")]
        public IActionResult UpdateGrants(Grant grants)
        {
            var grantsObj = _dbContext.Grants.Where(m => m.FacultyId == grants.FacultyId).FirstOrDefault();
            if (grantsObj != null)
            {
                grantsObj.GrantTitle = grants.GrantTitle;
                grantsObj.GrantDescription = grants.GrantDescription;


                _dbContext.SaveChanges();
            }

            return Ok(grantsObj);
        }


        [HttpDelete]
        [Route("DeleteGrants")]

        public IActionResult DeleteGrants(int FacultyId, int grantId)
        {
            var grantsObj = _dbContext.Grants.Where(m => m.FacultyId == FacultyId && m.GrantId == grantId).FirstOrDefault();

            _dbContext.Grants.Remove(grantsObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }
    }
}
