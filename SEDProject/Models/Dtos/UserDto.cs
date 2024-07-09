namespace SEDProject.Models.Dtos
{
    public record UserDto(Guid id,string firstName, string lastName, string email, int year, int month, int day);
}
