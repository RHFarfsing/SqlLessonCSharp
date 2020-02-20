using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class StudentController {
        public static BcConnection bcConnection { get; set; }
        public static List<Student> GetAllStudents() {
            var sql = "Select * From Student s " +
                " Left Join Major m on m.Id = s.MajorId";
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
                if (Convert.IsDBNull(reader["Description"])) {
                    student.Major = null;
                } else {
                    var major = new Major {
                        Description = reader["Description"].ToString(),
                        MinSAT = Convert.ToInt32(reader["MinSAT"])
                    };
                    student.Major = major;
                }
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
            if (student.MajorId == null) {
                majorid = "NULL";
            } else {
                majorid = student.MajorId.ToString();
            }
            //var sql = $"INSERT into Student (Id, Firstname, Lastname, SAT, GPA, MajorId) " +
            //    $"VALUES ({student.Id}, '{student.Firstname}', '{student.Lastname}', {student.SAT}, {student.GPA}, {majorid});";
            #region SQL with parameters
            var sqlp = $"INSERT into Student (Id, Firstname, Lastname, SAT, GPA, MajorId) " +
                $"VALUES (@Id, @Firstname, @Lastname, @SAT, @GPA, @MajorId);";
            #endregion
            var command = new SqlCommand(sqlp, bcConnection.Connection);
            #region Set parameters with data
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Firstname", student.Firstname);
            command.Parameters.AddWithValue("@Lastname", student.Lastname);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);
            #endregion
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Insert failed!");
            }
            return true;
        }
        public static bool UpdateStudent(Student student) {
            var sql = "UPDATE Student Set" +
                " Firstname = @Firstname," +
                " Lastname = @Lastname," +
                " SAT = @SAT," +
                " GPA = @GPA," +
                " MajorId = @MajorId" +
                " Where Id = @Id; ";
            var command = new SqlCommand(sql, bcConnection.Connection);
            #region Set parameters with data
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@Firstname", student.Firstname);
            command.Parameters.AddWithValue("@Lastname", student.Lastname);
            command.Parameters.AddWithValue("@SAT", student.SAT);
            command.Parameters.AddWithValue("@GPA", student.GPA);
            command.Parameters.AddWithValue("@MajorId", student.MajorId ?? Convert.DBNull);
            #endregion
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Update failed!");
            }
            return true;

        }
        public static bool DeleteStudent(Student student) {
            var sql = "DELETE from Student" +
                " Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            var recAffected = command.ExecuteNonQuery();
            if (recAffected != 1) {
                throw new Exception("Delete Failed");
            }
            return true;
        }
        public static bool DeleteStudent(int id) {
            var student = GetStudentByPk(id);
            if(student == null) {
                return false;
            }
            var success = DeleteStudent(student);
            return true;
        }
    }
}