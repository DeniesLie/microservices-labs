namespace EmployeeService.Models;

public class Position
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}