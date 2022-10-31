namespace EmployeeService.Dtos
{
    public class EmployeePostDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public Guid PositionId { get; set; }
    }
}
