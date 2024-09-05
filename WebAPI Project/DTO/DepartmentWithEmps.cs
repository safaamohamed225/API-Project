namespace WebAPI_Project.DTO
{
    public class DepartmentWithEmployees
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<EmployeeDto> emps { get; set; } = new List<EmployeeDto>();
    }

    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
