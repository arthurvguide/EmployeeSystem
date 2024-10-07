namespace EmployeeSystem.Models
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }

        // Foreign Key reference to Department
        public int DepartmentId { get; set; }  // Include this line

        // You can also include the DepartmentEntity for convenience, if needed
        // Make Department optional by making it nullable (?)
        public DepartmentEntity? Department { get; set; }  // This can be used to store department details
    }
}