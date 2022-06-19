using System;

namespace SerializationTask.Models
{
    [Serializable]
    class Child
    {
        public Int32 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int64 BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}