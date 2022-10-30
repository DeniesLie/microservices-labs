using EmployeeService.Data;
using EmployeeService.Models;
using EmployeeService.RepositoryInterfaces;

namespace EmployeeService.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly EmployeeContext _context;

    public PositionRepository(EmployeeContext context)
    {
        _context = context;
    }

    public void Insert(Position position)
    {
        _context.Positions.Add(position);
        _context.SaveChanges();
    }

    public void Delete(Position position)
    {
        _context.Positions.Remove(position);
        _context.SaveChanges();
    }

    public void Update(Position position)
    {
        _context.Positions.Update(position);
        _context.SaveChanges();
    }

    public IEnumerable<Position> GetAll()
    {
        return _context.Positions;
    }

    public Position Get(Guid id)
    {
        return _context.Positions.FirstOrDefault(x => x.Id == id);
    }
}