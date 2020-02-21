using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary {
    public class MajorController {
        public static BcConnection bcConnection { get; set; }
        private static Major LoadMajorInstance(SqlDataReader reader) {
            var major = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            major.Description = reader["Description"].ToString();
            major.MinSAT = Convert.ToInt32(reader["MinSAT"]);
            return major;
        }
        public static List<Major> GetAllMajors() {
            var sql = "SELECT * From Major;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows for GetAllMajors");
                return new List<Major>();
            }
            var majors = new List<Major>();
            while (reader.Read()) {
                var major = LoadMajorInstance(reader);
                //var major = new Major();
                //major.Id = Convert.ToInt32(reader["id"]);
                //major.Description = reader["Description"].ToString();
                //major.MinSAT = Convert.ToInt32(reader["MinSAT"]);
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }
        public static Major GetMajorByPK(int id) {
            var sql = $" SELECT * From Major Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
                var major = LoadMajorInstance(reader);
            //var major = new Major();
            //major.Id = Convert.ToInt32(reader["Id"]);
            //major.Description = reader["Description"].ToString();
            //major.MinSAT = Convert.ToInt32(reader["MinSAT"]);
            reader.Close();
            reader = null;
            return major;

        }
        public static bool InsertMajor(Major major) {
            var sql = $"INSERT into Major " +
                $" VALUES(@ID, @Description, @MinSAT;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Insert Failed!");
            }
            return true;
        }
        public static bool UpdateMajor(Major major) {
            var sql = $"UPDATE Major set" +
                $" Description = @Description," +
                $" MinSAT = @MinSAT," +
                $" Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            command.Parameters.AddWithValue("@Description", major.Description);
            command.Parameters.AddWithValue("MinSAT", major.MinSAT);
            var recsAffectted = command.ExecuteNonQuery();
            if(recsAffectted != 1) {
                throw new Exception("Update failed!");
            }
            return true;
        }        
        public static bool DeleteMajor(Major major) {
            var sql = $"DELETE from Major" +
                $"Where Id = @Id;";
            var command = new SqlCommand(sql, bcConnection.Connection);
            command.Parameters.AddWithValue("@Id", major.Id);
            var recsAffected = command.ExecuteNonQuery();
            if(recsAffected != 1) {
                throw new Exception("Delete Failed!");
            }
            return true;
        }
        public static bool DeleteMajor(int id) {
            var major = GetMajorByPK(id);
            if(major == null) {
                return false;
            }
            var success = DeleteMajor(major);
            return true;
        }
    }
}
