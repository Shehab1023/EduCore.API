using EduCore.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EduCore.API.DataSimulation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace First.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Students")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if (StudentDataSimulation.students.Count == 0)
            {
                return NotFound("No students found.");
            }
            return Ok(StudentDataSimulation.students);
        }




        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            if (StudentDataSimulation.students.Count == 0)
            {
                return NotFound("No students found.");
            }
            var PassedStudents = StudentDataSimulation.students.Where(s => s.Grade >= 50).ToList();
            return Ok(PassedStudents);
        }




        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<double> GetAverageGrade()
        {
            if (StudentDataSimulation.students.Count == 0)
            {
                return NotFound("No students found to calculate the average grade.");
            }
            else
            {
                var AverageGrade = StudentDataSimulation.students.Average(s => s.Grade);
                return Ok(AverageGrade);
            }
        }



        [HttpGet("IdStudent/{id}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Student> GetStudentByID(int id)
        {
            if (id < 1)
            {
                return BadRequest($" The ID {id} Invalid student ID. ID must be a positive integer.");
            }
            var student = StudentDataSimulation.students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(student);
        }


        [HttpPost("Add", Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Student> AddNewStudent(Student NewSt)
        {
            if (NewSt.Id == null || string.IsNullOrEmpty(NewSt.Name) || NewSt.Age < 0 || NewSt.Grade < 0)
            {
                return BadRequest("Invalid student data ");
            }

            var newStudent = StudentDataSimulation.students.Count > 0 ?
                StudentDataSimulation.students.Max(s => s.Id) + 1 : 1;

            return CreatedAtRoute("GetStudentByID", new { Id = NewSt.Id }, newStudent);
        }


        [HttpDelete("Delete/{ID}" , Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult DeleteStudent(int ID)
        {
            if (ID < 1)
            {
                return BadRequest($"The{ID} is invalid");                
            }
            var DeleteStudent = StudentDataSimulation.students.FirstOrDefault(s => s.Id == ID);
            if(DeleteStudent is null)
            {
                return NotFound($"No Student Found with ID {ID}");

            }
            StudentDataSimulation.students.Remove(DeleteStudent);
            return Ok($"Student with ID {ID} Deleted successfully");
        }


        [HttpPut("Update/{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> UpdateStudent(int id, Student updatedStudent)
        {
            if (id < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 || updatedStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }

            var student = StudentDataSimulation.students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;

            return Ok(student);
        }

    }
}
