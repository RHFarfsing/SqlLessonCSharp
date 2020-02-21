using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary {
    public class InstructorController {
        public static BcConnection bcConnection { get; set; }
        private static Instructor LoadInstructorInstance(SqlDataReader reader) {
            var instructor = new Instructor();
            instructor.Id = Convert.ToInt32(reader["Id"]);
            instructor.Firstname = Convert.ToString(reader["Firstname"]);
            instructor.Lastname = Convert.ToString(reader["Lastname"]);
            instructor.YearsExperience = Convert.ToInt32(reader["YearsExperience"]);
            instructor.IsTenured = Convert.ToBoolean(reader["IsTenured"]);
            return instructor;
        }
        public static List<Instructor> GetAllInstructors() {
            var sql = "Select * From Instructor;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows for GetAllInstructors!");
                return new List<Instructor>();
            }
            var instructors = new List<Instructor>();
            while (reader.Read()) {
                var instructor = LoadInstructorInstance(reader);
                //var instructor = new Instructor();
                //instructor.Id = Convert.ToInt32(reader["Id"]);
                //instructor.Firstname = Convert.ToString(reader["Firstname"]);
                //instructor.Lastname = Convert.ToString(reader["Lastname"]);
                //instructor.YearsExperience = Convert.ToInt32(reader["YearsExperience"]);
                //instructor.IsTenured = Convert.ToBoolean(reader["IsTenured"]);
                instructors.Add(instructor);
            }
            reader.Close();
            reader = null;
            return instructors;
        }
        public static Instructor GetInstructorByPk(int id) {
            var sql = "Select * from Instructor Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var instructor = LoadInstructorInstance(reader);
            reader.Close();
            reader = null;
            return instructor;
        }
        public static bool InsertInstructor(Instructor instructor) {
            var sql = "INSERT into Instructor()" +
                " VALUES(@Id, @Firstname, @Lastname, @YearsExperience, @IsTenured);";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Insert Failed!");
            }
            return true;
        }
        public static bool UpdateInstructor(Instructor instructor) {
            var sql = "UPDATE Instructor set" +
                "Firstname = @Firstname," +
                "Lastname = @Lastname," +
                "YearsExperience = @YearsExperience," +
                "InTenured = @IsTenured," +
                "Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            command.Parameters.AddWithValue("@Firstname", instructor.Firstname);
            command.Parameters.AddWithValue("@Lastname", instructor.Lastname);
            command.Parameters.AddWithValue("@YearsExperience", instructor.YearsExperience);
            command.Parameters.AddWithValue("@IsTenured", instructor.IsTenured);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Update Failed!");
            }
            return true;
        }
        public static bool DeleteIntructor(Instructor instructor) {
            var sql = "DELETE from Instructor Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", instructor.Id);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Delete Failed!");
            }
            return true;
        }
        public static bool DeleteInstructor(int id) {
            var instructor = GetInstructorByPk(id);
            if(instructor == null) {
                return false;
            }
            var success = DeleteInstructor(1);
            return true;
        }
    }
}
