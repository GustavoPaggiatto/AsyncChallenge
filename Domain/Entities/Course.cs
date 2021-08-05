using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public sealed class Course : BaseEntity, ICloneable
    {
        public string Name { get; set; }
        public string Lecturer { get; set; }
        public int MaxNumberOfStudents { get; set; }
        public IEnumerable<Student> Students { get; set; }

        public object Clone()
        {
            var students = new List<Student>();

            if (this.Students != null && this.Students.Count() > 0)
                students.AddRange(this.Students);

            return new Course()
            {
                Id = this.Id,
                Lecturer = this.Lecturer,
                MaxNumberOfStudents = this.MaxNumberOfStudents,
                Name = this.Name,
                Students = students
            };
        }
    }
}