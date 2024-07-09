namespace SEDProject.Models.Entities
{
    public class Participant : IEntity
    {

        public Participant()
        {
            
        }

        public Participant(User user, Department department)
        {
            Id = Guid.NewGuid();
            User = user;
            Department = department;
            StartDate = DateTime.Now;
        }
        public Guid Id { get; init; }
        public User User { get; set; }
        public Department Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
