using EmployeeService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public EmployeeGetDto Get(int id)
        {
            return new EmployeeGetDto() {Id = id, Lastname = "Daun", Name = "Artem", PhoneNumber = "123456789"};
        }

        [HttpGet("GetByStorage/{storageId:int}")]
        public IEnumerable<EmployeeGetDto> GetEmployeesOfStorage(int storageId)
        {
            return new List<EmployeeGetDto>()
            {
                new() {Id = 12, Lastname = "Daun", Name = "Artem", PhoneNumber = "123456789"}
            };
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeePostDto employeePostDto)
        {
            return Created("api/employees", employeePostDto);
        }
    }
}
