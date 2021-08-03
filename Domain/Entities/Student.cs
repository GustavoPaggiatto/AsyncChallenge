using System;

namespace Domain.Entities
{
    public sealed class Student : BaseEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}