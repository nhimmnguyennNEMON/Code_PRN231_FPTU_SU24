using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Q1.DTOs;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        DUONGPV14_PRNContext _context;

        public StudentController(DUONGPV14_PRNContext context)
        {
            _context = context;
        }

        [HttpGet("List")]
        [EnableQuery]
        public IActionResult Get()
        {
            var studs = _context.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                FullName = s.FullName,
                Gender = s.Male == true ? "Male" : "Female",
                Dob = s.Dob.ToString("MM/dd/yyyy"),
                LecturerName = s.Lecture.FullName,
                Age = 2024 - s.Dob.Year
            }).ToList();

            return Ok(studs);
        }

        [HttpGet("Detail")]
        public IActionResult GetDetail(int id)
        {
            var studs = _context.Students.Select(s => new 
            {
                Id = s.Id,
                FullName = s.FullName,
                Gender = s.Male == true ? "Male" : "Female",
                Dob = s.Dob.ToString("MM/dd/yyyy"),
                LecturerName = s.Lecture.FullName,
                Age = 2024 - s.Dob.Year,
                Classes = s.Classes.Select(c => c.ClassName.Trim()).ToList(),
                Subjects = s.StudentSubjects.Select(s => s.Subject.Subject1.Trim()).ToList(),
                CPA = s.StudentSubjects.Sum(s => s.Grade) / s.StudentSubjects.Count()
            }).FirstOrDefault(s => s.Id == id);

            return Ok(studs);
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var studs = _context.Students.Include(s => s.StudentSubjects).Include(s => s.Classes).FirstOrDefault(s => s.Id == id);
            if (studs == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    var a = studs.Classes.Count();
                    var b = studs.StudentSubjects.Count();
                    studs.StudentSubjects.Clear();
                    studs.Classes.Clear();
                    _context.Remove(studs);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        numberOfRelatedClasses = a,
                        numberOfRelatedSubjects = b
                    });
                }catch(Exception ex)
                {
                    return Conflict("There are .....");
                }
            }
        }

    }
}
