using SEDProject.Models.Dtos;
using SEDProject.Models.Entities;

namespace SEDProject.Models.Extentions
{
    public static class DtoExtentions
    {
        public static User ToUser(this UserDto dto) => new User 
        {
            Id = dto.id == Guid.Empty ? Guid.NewGuid() : dto.id, 
            FirstName = dto.firstName, 
            LastName = dto.lastName,
            Email = dto.email, 
            BirthDate= new DateOnly(dto.year, dto.month, dto.day)
        };
        public static Department ToDepartment(this DepartmentDto dto) => new Department 
        {
            Id = dto.id == Guid.Empty ? Guid.NewGuid() : dto.id,
            Name = dto.name, 
            Description = dto.description
        };
        public static Participant ToParticipant(this ParticipantDto dto, User user, Department department) => new Participant()
        {
            Id = dto.id == Guid.Empty ? Guid.NewGuid() : dto.id,
            StartDate = DateTime.Now,
            User = user,
            Department = department,
        };
    }
}
