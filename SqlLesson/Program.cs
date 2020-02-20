using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
       static void Main(string[] args) {
            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");
            StudentController.bcConnection = (sqllib);
            var newStudent = new Student {
                Id = 888,
                Firstname = "Crazy",
                Lastname = "Eights",
                SAT = 600,
                GPA = 0.00,
                MajorId = null
            };
            var success = StudentController.InsertStudent(newStudent);
            var student = StudentController.GetStudentByPk(888);
            if(student == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student);
            }
            var students = StudentController.GetAllStudents();
            foreach(var student0 in students) {
                Console.WriteLine(student0);
            }
            sqllib.Disconnect();
        }
    }
}
