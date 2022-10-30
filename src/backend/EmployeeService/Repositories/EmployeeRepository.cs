using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.RepositoryInterfaces;

namespace EmployeeService.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeContext _context;

    public EmployeeRepository(EmployeeContext context)
    {
        _context = context;
    }
    public void Insert(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
        _context.SaveChanges();
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public IEnumerable<Employee> GetAll()
    {
        return _context.Employees;
    }

    public Employee Get(Guid id)
    {
        return _context.Employees.FirstOrDefault(x => x.Id == id);
    }
}