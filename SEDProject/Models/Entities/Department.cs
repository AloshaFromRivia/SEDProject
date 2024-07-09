namespace SEDProject.Models.Entities
{
    public class Department : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Department>? SubsidiaryGroups { get; set; }
    }
}
