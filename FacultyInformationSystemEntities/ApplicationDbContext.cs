
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyInformationSystemEntities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseSubject> CourseSubjects { get; set; }
        public virtual DbSet<CourseTaught> CourseTaughts { get; set; }
        public virtual DbSet<Degree> Degrees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Grant> Grants { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<WorkHistory> WorkHistories { get; set; }

    }
}
