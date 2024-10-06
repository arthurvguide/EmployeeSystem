using EmployeeSystem.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeSystem.Data
{
    public class DepartmentRepository
    {
        private SqlConnection _connection;

        public DepartmentRepository()
        {
            string coonStr = "server=LAPTOP-NTBOS8PM\\SQLEXPRESS;database=EmployeeSystem;Integrated Security=true;TrustServerCertificate=true;";

            _connection = new SqlConnection(coonStr);
        }

        public List<DepartmentEntity> RetrieveAllDepartments()
        {
            List<DepartmentEntity> DepartmentListEntity = new List<DepartmentEntity>();

            SqlCommand cmd = new SqlCommand("RetrieveAllDepartments", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            dataAdapter.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                AddDepartmentToList(DepartmentListEntity, dr);
            }

            return DepartmentListEntity;

            static void AddDepartmentToList(List<DepartmentEntity> DepartmentListEntity, DataRow dr)
            {
                DepartmentListEntity.Add(
                    new DepartmentEntity
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                    });
            }
        }

    }
}
