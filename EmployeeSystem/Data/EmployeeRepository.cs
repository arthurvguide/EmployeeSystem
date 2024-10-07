using EmployeeSystem.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeSystem.Data
{
    public class EmployeeRepository
    {
        private SqlConnection _connection;

        public EmployeeRepository()
        {
            string coonStr = "server=LAPTOP-NTBOS8PM\\SQLEXPRESS;database=EmployeeSystem;Integrated Security=true;TrustServerCertificate=true;";

            _connection = new SqlConnection(coonStr);
        }

        public List<EmployeeEntity> RetrieveAllEmployee()
        {
            List<EmployeeEntity> EmployeeListEntity = new List<EmployeeEntity>();

            SqlCommand cmd = new SqlCommand("RetrieveAllEmployees", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            dataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                AddEmployeeToList(EmployeeListEntity, dr);
            }


            return EmployeeListEntity;

            void AddEmployeeToList(List<EmployeeEntity> employeeListEntity, DataRow dr)
            {
                // Create a new EmployeeEntity object and populate its properties
                var employee = new EmployeeEntity
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FullName = dr["FullName"].ToString(),
                    JobTitle = dr["JobTitle"].ToString(),
                    Email = dr["Email"].ToString(),
                    PhoneNumber = dr["PhoneNumber"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                    DepartmentId = Convert.ToInt32(dr["Department_Id"])  // Assign DepartmentId
                };

                // Fetch the department name using the DepartmentId and assign it to the Department object
                employee.Department = new DepartmentEntity
                {
                    Name = GetDepartmentNameById(employee.DepartmentId)  // Call method to get department name
                };

                // Add employee to the list
                employeeListEntity.Add(employee);
            }
        }

        public string GetDepartmentNameById(int departmentId)
        {
            string departmentName = string.Empty;

            SqlCommand cmd = new SqlCommand("RetrieveDepartmentNameById", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DepartmentId", departmentId);

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            object result = cmd.ExecuteScalar();  // ExecuteScalar returns the first value from the result set

            if (result != null)
            {
                departmentName = result.ToString();
            }

            return departmentName;
        }

        public bool AddEmployee(EmployeeEntity Employee)
        {
            SqlCommand cmd = new SqlCommand("AddEmployee", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@FullName", Employee.FullName);
            cmd.Parameters.AddWithValue("@JobTitle", Employee.JobTitle);
            cmd.Parameters.AddWithValue("@Email", Employee.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", Employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@DateOfBirth", Employee.DateOfBirth);

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

        public EmployeeEntity RetrieveEmployeeById(int Id)
        {
            EmployeeEntity EmployeeListEntity = new EmployeeEntity();

            SqlCommand cmd = new SqlCommand("RetrieveEmployeeById", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter param;

            cmd.Parameters.Add(new SqlParameter("Id", Id));

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            dataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)

                EmployeeListEntity = new EmployeeEntity
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FullName = dr["FullName"].ToString(),
                    JobTitle = dr["JobTitle"].ToString(),
                    Email = dr["Email"].ToString(),
                    PhoneNumber = dr["PhoneNumber"].ToString(),
                    DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                };

            return EmployeeListEntity;
        }

        public bool EditEmployeeSave(int Id, EmployeeEntity Employee)
        {
            SqlCommand cmd = new SqlCommand("EditEmployeeSave", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);

            cmd.Parameters.AddWithValue("@FullName", Employee.FullName);
            cmd.Parameters.AddWithValue("@JobTitle", Employee.JobTitle);
            cmd.Parameters.AddWithValue("@Email", Employee.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", Employee.PhoneNumber);
            cmd.Parameters.AddWithValue("@DateOfBirth", Employee.DateOfBirth);

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

        public bool DeleteEmployee(int Id)
        {
            SqlCommand cmd = new SqlCommand("DeleteEmployee", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);

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
