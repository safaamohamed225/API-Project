using Microsoft.Identity.Client;

namespace WebAPI_Project.DTO
{
    public class EmployeeNameWithDepartmentNameDTO
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
        public string ManagerName { get; set; }
    }

  
}
