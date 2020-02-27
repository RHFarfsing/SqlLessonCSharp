using System;
using SqlLibrary;

namespace SqlLesson {
    class Program {
        static void Main(string[] args) {
            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress", "EdDb", "trusted_connection=true");
            StudentController.bcConnection = sqllib;
            MajorController.bcConnection = sqllib;
            InstructorController.bcConnection = sqllib;
            #region Instructor methods
            var instructors = InstructorController.GetAllInstructors();
            foreach (var i in instructors) {
                Console.WriteLine(i);
            }
            var instructor = InstructorController.GetInstructorByPk(10);
            if(instructor == null) {
                Console.WriteLine("Instructor not found!");
            } else {
                Console.WriteLine(instructor);
            }
            #endregion
            #region Major Controller methods
            var major = MajorController.GetMajorByPK(1);
            if (major == null) {
                Console.WriteLine("Major not found!");
            } else {
                Console.WriteLine(major);
            }
            var majors = MajorController.GetAllMajors();
            foreach (var m in majors) {
                Console.WriteLine(m);
            }
            #endregion
            #region Student Controller methods
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
            if (student == null) {
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
            foreach (var student0 in students) {
                Console.WriteLine(student0);
            }
            #endregion
            sqllib.Disconnect();

        }
    }
}
