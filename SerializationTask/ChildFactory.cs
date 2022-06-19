using SerializationTask.Models;

namespace SerializationTask
{
    static class ChildFactory
    {
        public static Child Create(Person parent)
        {
            var child = new Child
            {
                Id = DataSource.NextId(),
                Gender = DataSource.NextGender(),
                BirthDate = DataSource.NextChildBirthdate(parent.BirthDate)
            };
            
            child.FirstName = DataSource.NextFirstName(child.Gender);
            child.LastName = DataSource.NextChildLastName(parent, child);
            
            return child;
        }
        
        public static Child[] CreateArray(Person parent, int length)
        {
            var children = new Child[length];
            
            for (var i = 0; i < children.Length; i++)
            {
                children[i] = Create(parent);
            }
            
            return children;
        }
    }
}