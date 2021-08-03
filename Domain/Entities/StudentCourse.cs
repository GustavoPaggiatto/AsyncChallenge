namespace Domain.Entities
{
    public sealed class StudentCourse : BaseEntity
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}