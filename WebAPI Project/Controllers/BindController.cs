using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Project.Models;

namespace WebAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindController : ControllerBase
    {
        private readonly ITIEntity context;

        public BindController(ITIEntity context)
        {
            this.context = context;
        }

        //ITIEntity context = new ITIEntity();
        [HttpGet("{id:int}/{name:alpha}")]

        public IActionResult Get(int id, string name)
        {
            //Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            //return Ok(emp);
            return Ok();

        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            return Content("Adding");
        }

        [HttpGet("{name}/{address}/{age}/{salary}")]
        public IActionResult Get([FromRoute]Employee employee)
        {
            return Content("Reading");
        }
    }
}
