using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Project.DTO;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [Authorize]
        [HttpGet("{id:int}")]
        public IActionResult GetDepartment(int id)
        {
            ITIEntity context = new ITIEntity();
            Department deptModel =context.Department.Include(d => d.Employees).FirstOrDefault(e=>e.Id==id);

            DepartmentWithEmployees deptEmps = new DepartmentWithEmployees();
            deptEmps.Id = deptModel.Id;
            deptEmps.Name = deptModel.Name;

            foreach(var item in deptModel.Employees)
            {
                deptEmps.emps.Add(new EmployeeDto() { Id = item.Id, Name = item.Name });
            }

            return Ok(deptEmps);
        }

    }
}
