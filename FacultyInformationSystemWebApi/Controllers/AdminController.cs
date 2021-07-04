using FacultyInformationSystemEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacultyInformationSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdminController(ApplicationDbContext context)
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

        //[HttpPost]
        //[Route("PostAddUser")]

        //public IActionResult PostAddUser(User user)
        //{
        //    _dbContext.Users.Add(user);
        //    _dbContext.SaveChanges();
        //    return Ok(user);
        //}

        [HttpPost]
        [Route("UpdateCourse")]
        public IActionResult UpdateCourse(Course Course)
        {
            var courseObj = _dbContext.Courses.Where(m => m.CourseId == Course.CourseId).FirstOrDefault();
            courseObj.CourseName = Course.CourseName;
            courseObj.CourseCredits = Course.CourseCredits;
            courseObj.DeptId = Course.DeptId;
            _dbContext.SaveChanges();
            return Ok(courseObj);
        }

        [HttpPost]
        [Route("UpdateSubject")]
        public IActionResult UpdateSubject(Subject subject)
        {
            var subjectObj = _dbContext.Subjects.Where(m => m.SubjectId == subject.SubjectId).FirstOrDefault();
            subjectObj.SubjectName = subject.SubjectName;

            _dbContext.SaveChanges();
            return Ok(subjectObj);
        }

        [HttpPost]
        [Route("PostAddDepartment")]

        public IActionResult PostAddDepartment(Department department)
        {
            _dbContext.Departments.Add(department);
            _dbContext.SaveChanges();
            return Ok(department);
        }

        [HttpGet]
        [Route("GetDepartment")]
        public IActionResult GetDepartment()
        {
            var departments = _dbContext.Departments.ToList();

            return Ok(departments);
        }

        [HttpPost]
        [Route("UpdateDepartment")]
        public IActionResult UpdateDepartment(Department department)
        {
            var departmentObj = _dbContext.Departments.Where(m => m.DeptId == department.DeptId).FirstOrDefault();
            departmentObj.DeptName = department.DeptName;
            _dbContext.SaveChanges();
            return Ok(departmentObj);
        }

        [HttpDelete]
        [Route("DeleteDepartment")]
        public IActionResult DeleteDepartment(int Id)
        {
            var departmentObj = _dbContext.Departments.Where(m => m.DeptId == Id).FirstOrDefault();
            _dbContext.Remove(departmentObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }

        [HttpPost]
        [Route("PostAddDesignation")]

        public IActionResult PostAddDesignation(Designation designation)
        {
            _dbContext.Designations.Add(designation);
            _dbContext.SaveChanges();
            return Ok(designation);
        }


        [HttpGet]
        [Route("GetDesignation")]
        public IActionResult GetDesignation()
        {
            var designation = _dbContext.Designations.ToList();
            return Ok(designation);
        }

        [HttpPost]
        [Route("UpdateDesignation")]
        public IActionResult UpdateDesignation(Designation designation)
        {
            var designationObj = _dbContext.Designations.Where(m => m.DesignationId == designation.DesignationId).FirstOrDefault();
            designationObj.DesignationName = designation.DesignationName;
            _dbContext.SaveChanges();
            return Ok(designationObj);
        }

        [HttpDelete]
        [Route("DeleteDesignation")]
        public IActionResult DeleteDesignation(int designationId)
        {
            var designationObj = _dbContext.Designations.Where(m => m.DesignationId == designationId).FirstOrDefault();
            _dbContext.Remove(designationObj);
            _dbContext.SaveChanges();
            return Ok("Success");
        }



        [HttpGet]
        [Route("GetFacultyInfo")]
        public IActionResult GetFacultyInfo(int facultyId)
        {

            Faculty facultyObj = _dbContext.Faculties.FirstOrDefault(m => m.FacultyId == facultyId);
            facultyObj.DepartmentName = _dbContext.Departments.Where(d => d.DeptId == facultyObj.DeptId).FirstOrDefault().DeptName;
            facultyObj.DesignationName = _dbContext.Designations.Where(d => d.DesignationId == facultyObj.DesignationId).FirstOrDefault().DesignationName;

            return Ok(facultyObj);
        }

        [HttpGet]
        [Route("GetFacultyList")]
        public IActionResult GetFacultyList(int facultyId)
        {

            var FacultiesList = _dbContext.Faculties.Select(
                m => new Faculty
                {
                    FacultyId = m.FacultyId,
                    FacultyName = m.FirstName.ToString() + " " + m.LastName.ToString()
                }

                ).ToList();

            return Ok(FacultiesList);
        }



        [HttpPost]
        [Route("UpdateFacultyJob")]
        public IActionResult UpdateFacultyJob(Faculty faculty)
        {
            var facultyObj = _dbContext.Faculties.Where(m => m.FacultyId == faculty.FacultyId).FirstOrDefault();
            facultyObj.DeptId = faculty.DeptId;
            facultyObj.DesignationId = faculty.DesignationId;

            _dbContext.SaveChanges();
            return Ok(facultyObj);
        }

        [HttpGet]
        [Route("GetReportsYearly")]
        public IActionResult GetReportsYearly(int year)
        {
            var publicationsObj = _dbContext.Publications.Where(m => m.CitationDate.Year == year).ToList();
            return Ok(publicationsObj);
        }

        [HttpGet]
        [Route("GetReportsMonthly")]
        public IActionResult GetReportsMonthly(int month)
        {
            var publicationsObj = _dbContext.Publications.Where(m => m.CitationDate.Month == month).ToList();
            return Ok(publicationsObj);
        }

        [HttpGet]
        [Route("GetReportsRecent")]
        public IActionResult GetReportsRecent(int count)
        {
            var publicationsObj = _dbContext.Publications.OrderByDescending(m => m.CitationDate).Take(count);

            return Ok(publicationsObj);
        }


        [HttpPost]
        [Route("PostAddSubjects")]
        public IActionResult PostAddSubjects(Subject subject)
        {
            _dbContext.Subjects.Add(subject);
            _dbContext.SaveChanges();
            return Ok(subject);
        }


        [HttpPost]
        [Route("PostAddCourses")]
        public IActionResult PostAddCourses(Course course)
        {
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return Ok(course);
        }

        [HttpDelete]
        [Route("DeleteCourse")]
        public IActionResult DeleteCourse(int courseId)
        {
            var courseObj = _dbContext.Courses.Where(m => m.CourseId == courseId).FirstOrDefault();
            _dbContext.Courses.Remove(courseObj);
            _dbContext.SaveChanges();
            return Ok("success");
        }

        [HttpDelete]
        [Route("DeleteSubject")]
        public IActionResult DeleteSubject(int subjectId)
        {
            var subjectObj = _dbContext.Subjects.Where(m => m.SubjectId == subjectId).FirstOrDefault();
            _dbContext.Subjects.Remove(subjectObj);
            _dbContext.SaveChanges();
            return Ok("success");
        }

        [HttpGet]
        [Route("GetSubjects")]
        public IActionResult GetSubjects()
        {
            var subjects = _dbContext.Subjects.ToList();
            return Ok(subjects);
        }

        [HttpGet]
        [Route("GetCourses")]
        public IActionResult GetCourses()
        {

            var courses = _dbContext.Courses.Select(m => new Course
            {
                CourseId = m.CourseId,
                CourseCredits = m.CourseCredits,
                CourseName = m.CourseName,
                DeptId = m.DeptId,
                DepartmentName = m.Dept.DeptName

            }
                ).ToList();
            return Ok(courses);
        }
    }
}
