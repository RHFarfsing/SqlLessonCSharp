using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class StudentController {
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
            reader.Close();
            reader = null;
            return students;
        }
        public static Student GetStudentByPk(int id) {
            var sql = $"SELECT * from Student Where Id = {id}";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                return null;
            }
            reader.Read();
            var student = new Student();
            student.Id = Convert.ToInt32(reader["Id"]);
            student.Firstname = reader["Firstname"].ToString();
            student.Lastname = reader["Lastname"].ToString();
            student.SAT = Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDouble(reader["GPA"]);
            //student.MajorId = Convert.ToInt32(reader["MajorId"]);
            reader.Close();
            reader = null;
            return student;
        }
        public static bool InsertStudent(Student student) {
            var majorid = "";
            if(student.MajorId == null) {
                majorid = "NULL";
            } else {
                majorid = student.MajorId.ToString();
            }
            var sql = $"INSERT into Student (Id, Firstname, Lastname, SAT, GPA, MajorId) " +
                $"VALUES ({student.Id}, '{student.Firstname}', '{student.Lastname}', {student.SAT}, {student.GPA}, {majorid});";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Insert failed!");
            }
            return true;
        }
    }
}
