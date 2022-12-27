using EmployeeService.Dtos;
using EmployeeService.Models;
using EmployeeService.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, IPositionRepository positionRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _logger = logger;
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is null)
                return NotFound();
            return Ok(new EmployeeGetDto() {Id = id, Lastname = employee.Lastname, Name = employee.Name, PhoneNumber = employee.PhoneNumber, PositionId = employee.PositionId});
        }

        [HttpGet("GetByStorage/{storageId:Guid}")]
        public IEnumerable<EmployeeGetDto> GetEmployeesOfStorage(Guid storageId)
        {
            var employees = _employeeRepository.GetAll();
            var result = 
                employees.Select(employee => new EmployeeGetDto()
                {
                    Id = employee.Id, 
                    Lastname = employee.Lastname,
                    Name = employee.Name, 
                    PhoneNumber = employee.PhoneNumber,
                    PositionId = employee.PositionId
                }).ToList();
            return result;
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeePostDto employeePostDto)
        {
            var position = _positionRepository.Get(employeePostDto.PositionId);
            if (position is null)
                return NotFound();

            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Lastname = employeePostDto.Lastname,
                Name = employeePostDto.Name,
                PhoneNumber = employeePostDto.PhoneNumber,
                PositionId = position.Id
            };
            _employeeRepository.Insert(employee);

            _logger.LogInformation($"Employee created. Id: {employee.Id}");

            return Created("api/employees", employeePostDto);
        }

        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] EmployeeGetDto employeePostDto)
        {
            var employee = _employeeRepository.Get(employeePostDto.Id);
            if (employee is null)
                return NotFound();
            employee.Name = employeePostDto.Name;
            employee.Lastname = employeePostDto.Lastname;
            employee.PhoneNumber = employeePostDto.PhoneNumber;
            employee.PositionId = employeePostDto.PositionId;
            _employeeRepository.Update(employee);

            _logger.LogInformation($"Employee updated. Id: {employee.Id}");

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is null)
                return NotFound();
            
            _employeeRepository.Delete(employee);

            _logger.LogInformation($"Employee deleted. Id: {employee.Id}");

            return Ok();
        }
    }
}
