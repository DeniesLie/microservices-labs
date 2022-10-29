namespace EmployeeService.Models;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string PhoneNumber { get; set; }
    public Guid PositionId { get; set; }
    public Position Position { get; set; }
}