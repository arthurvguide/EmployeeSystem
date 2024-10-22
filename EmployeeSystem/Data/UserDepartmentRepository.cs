using EmployeeSystem.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeSystem.Data
{
    public class UserDepartmentRepository
    {
        private SqlConnection _connection;

        public UserDepartmentRepository()
        {
            string coonStr = "server=LAPTOP-NTBOS8PM\\SQLEXPRESS;database=EmployeeSystem;Integrated Security=true;TrustServerCertificate=true;";

            _connection = new SqlConnection(coonStr);
        }

        public bool AddUserDepartment(string userId, int departmentId)
        {
            SqlCommand cmd = new SqlCommand("AddUserDepartment", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

            _connection.Open();

            int i = cmd.ExecuteNonQuery();
            _connection.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
