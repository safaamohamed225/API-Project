using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Project.DTO;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Route("api/[controller]")] //URL : api/Employee
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITIEntity context;

        public EmployeeController(ITIEntity context)
        {
            this.context = context;
        }

        //CRUD Operations

        [HttpGet]
        public IActionResult GetEmployees()
        {
            List<Employee> emps=context.Employees.ToList();
            return Ok(emps);
        }
        [HttpGet("{id:int}" ,Name ="EmpUrlRoute")]
        //[Route("{id}")]
        public IActionResult GetEmployeeById([FromRoute]int id)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            return Ok(emp);
        }

        [HttpGet("{name:alpha}")]
        public IActionResult GetEmployeeByName(string name)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Name == name);
            return Ok(emp);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee([FromRoute]int id,[FromBody]Employee newEmployee)
        {
           

            if (ModelState.IsValid)
            {
                Employee oldEmployee = context.Employees.FirstOrDefault(e => e.Id == id);
                oldEmployee.Name = newEmployee.Name;
                oldEmployee.Age = newEmployee.Age;
                oldEmployee.Address = newEmployee.Address;
                oldEmployee.Salary = newEmployee.Salary;
                context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveEmployee(int id)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            context.Employees.Remove(emp);
            context.SaveChanges();
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee newEmp)
        {
            if (ModelState.IsValid)
            {

                context.Employees.Add(newEmp);
                context.SaveChanges();
                string url = Url.Link("EmpUrlRoute", new { id = newEmp.Id });
                return Created(url, newEmp);
            }
            return BadRequest();
        }

        [HttpGet("dept/{id}")]
        public IActionResult GetEmpWithDept(int id)
        {
            Employee emp = context.Employees.Include(d => d.Department).FirstOrDefault(e => e.Id == id);
            return Ok(emp);
        }


        [HttpGet("dto/{id}")]
        public IActionResult GetEmpWithDeptDTO(int id)
        {
            Employee emp =
                context.Employees
                .Include(d => d.Department)
                .FirstOrDefault(e => e.Id == id);

            EmployeeNameWithDepartmentNameDTO empDTO = new EmployeeNameWithDepartmentNameDTO();
            empDTO.EmpID = emp.Id;
            empDTO.EmpName = emp.Name;
            empDTO.DeptName = emp.Department.Name;
            empDTO.ManagerName = emp.Department.ManagerName;

            //DepartmentWithEmployees deptEmps = new DepartmentWithEmployees();
            //deptEmps.Id = emp.Department.Id;
            //deptEmps.Name = emp.Department.Name;

            //EmployeeDto eDTO = new EmployeeDto();
            //eDTO.Id = emp.Id;
            //eDTO.Name = emp.Name;


            return Ok(empDTO);
        }
    }
}
