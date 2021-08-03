namespace Domain.Entities
{
    public sealed class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public int MaxNumberOfStudents { get; set; }
    }
}