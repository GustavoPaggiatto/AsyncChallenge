using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public sealed class Student : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}