using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
       static void Main(string[] args) {
            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");
            StudentController.bcConnection = (sqllib);
            //var newStudent = new Student {
            //    Id = 999,
            //    Firstname = "Lazy",
            //    Lastname = "Larry",
            //    SAT = 1600,
            //    GPA = 4.0,
            //    MajorId = 3
            //};
            //var success = StudentController.InsertStudent(newStudent);
            var student = StudentController.GetStudentByPk(888);
            if(student == null) {
                Console.WriteLine("Student not found");
            } else {
                Console.WriteLine(student);
            }
            //var studentToDelete = new Student {
            //    Id = 999
            //};
            //var success = StudentController.DeleteStudent(999);
            //student.Firstname = "Charlie";
            //student.Lastname = "Chan";
            //var success = StudentController.UpdateStudent(student);
            var students = StudentController.GetAllStudents();
            foreach(var student0 in students) {
                Console.WriteLine(student0);
            }
            sqllib.Disconnect();
        }
    }
}
