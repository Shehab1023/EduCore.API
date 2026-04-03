using EduCore.API.Models;

namespace EduCore.API.DataSimulation
{
    public class StudentDataSimulation
    {
        public readonly static List<Student> students = new List<Student>()
        {
            new Student() { Id = 1, Name = "John" , Age = 20, Grade = 85 },
            new Student() { Id = 2, Name = "Alice", Age = 22, Grade = 90 },
            new Student() { Id = 3, Name = "Bob"  , Age = 19, Grade = 78 },
            new Student() { Id = 4, Name = "Eve"  , Age = 21, Grade = 92 },
            new Student() { Id = 5, Name = "Ali"  , Age = 19, Grade = 39 },
            new Student() { Id = 6, Name = "Ahmed", Age = 21, Grade = 51 },
            new Student() { Id = 7, Name = "Omar" , Age = 20, Grade = 50 },
            new Student() { Id = 8, Name = "Amr"  , Age = 23, Grade = 49 }
        };
    }
}
