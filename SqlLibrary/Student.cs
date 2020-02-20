using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace SqlLibrary {
    public class Student {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int SAT { get; set; }
        public double GPA { get; set; }
        public int MajorId { get; set; }
        public static BcConnection bcConnection { get; set; }
        public static List<Student> GetAllStudents() {
            var sql = "Select * From Student";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows from GetAllStudents()");
                return new List<Student>();
            }
            var students = new List<Student>();
            while (reader.Read()) {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = reader["Lastname"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDouble(reader["GPA"]);
                //student.MajorId = Convert.ToInt32(reader["MajorId"]);
                students.Add(student);
            }
            return students;
        }
        public Student() { }
        public Student(BcConnection connection) {
            bcConnection = connection;
        }
    }
}
