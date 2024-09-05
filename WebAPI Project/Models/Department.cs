using System.Text.Json.Serialization;

namespace WebAPI_Project.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }

        [JsonIgnore]
        public virtual List<Employee>? Employees { get; set; }
      
    }
}
