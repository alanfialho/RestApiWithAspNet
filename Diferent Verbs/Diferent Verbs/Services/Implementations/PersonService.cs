using System.Collections.Generic;
using Diferent_Verbs.Model;

namespace Diferent_Verbs.Services.Implementations
{
    public class PersonService : IPersonService
    {
        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
            
        }

        public IList<Person> FindAll()
        {
            List<Person> persons = new List<Person>();

            for( int i = 0; i <= 8; i++)
            {
                persons.Add(FindById(i));
            }

            return persons;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                ID = id,
                FirstName = "FistName: " + id,
                LastName = "LastName: " + id,
                Gender = "Gender: " + id,
                Address = "Address: " + id
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}
