using EmployeeService.Models;

namespace EmployeeService.RepositoryInterfaces;

public interface IEmployeeRepository
{
    public void Insert (Employee employee);
    public void Delete (Employee employee);
    public void Update (Employee employee);
    public IEnumerable<Employee> GetAll ();
    public Employee Get (Guid id);

}