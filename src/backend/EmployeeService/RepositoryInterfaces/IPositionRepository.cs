using EmployeeService.Models;

namespace EmployeeService.RepositoryInterfaces;

public interface IPositionRepository
{
    public void Insert(Position position);
    public void Delete(Position position);
    public void Update(Position position);
    public IEnumerable<Position> GetAll();
    public Position Get(Guid id);
}