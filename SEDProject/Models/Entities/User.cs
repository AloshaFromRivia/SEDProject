namespace SEDProject.Models.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Email { get; set; }


    }
}
