using EmployeeService.Models;

namespace EmployeeService.Auxiliary
{
    public class SeedData
    {
        public static List<Position> Position =>
            new List<Position>()
            {
                new Position()
                {
                    Id = Guid.Parse("0d1a8e87-c8dd-465c-9d58-03de8557612f"),
                    Name = "Sitecore Consultant"
                },
                new Position()
                {
                    Id = Guid.Parse("936bfe7f-d97b-4591-abcf-08844d35e9f6"),
                    Name = "DevOps"
                }
            };

        public static List<Employee> Employees =>
            new List<Employee>()
            {
                new Employee()
                {
                    Id = Guid.Parse("54056016-dcf5-42ad-ae29-b778c063a6a2"),
                    Lastname = "Zozuluk",
                    Name = "Viktor",
                    PhoneNumber = "+380506402278",
                    PositionId = Guid.Parse("0d1a8e87-c8dd-465c-9d58-03de8557612f")
                },
                new Employee()
                {
                    Id = Guid.Parse("2b993441-418f-46f1-b3bd-6d8f3f54ab86"),
                    Lastname = "Stankov",
                    Name = "Artem",
                    PhoneNumber = "+380668896351",
                    PositionId = Guid.Parse("936bfe7f-d97b-4591-abcf-08844d35e9f6")
                }
            };
    }
}
