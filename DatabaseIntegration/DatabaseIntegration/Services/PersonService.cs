using DatabaseIntegration.Model;
using DatabaseIntegration.Model.Context;
using System.Linq;

namespace DatabaseIntegration.Services
{
    public class PersonService : IPersonService
    {

        private readonly CursoAspNetCoreContext _context;

        public PersonService(CursoAspNetCoreContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
            return person;
        }

        public void Delete(int id)
        {
            var personToDelete = TryGetValue(id);

            if (personToDelete.Exist)
                _context.Remove(personToDelete.Person);
            else
                throw new PersonNotFoundException();
        }

        public Person GetById(int id)
        {
            return _context.Find<Person>(id);
        }

        public void Update(Person person)
        {
            var personToUpdate = TryGetValue(person.Id);

            if (personToUpdate.Exist)
            {
                _context.Entry(personToUpdate.Person).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            else
            {
                throw new PersonNotFoundException();
            }
        }

        private (Person Person, bool Exist) TryGetValue(int id)
        {
            var person = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            return (person, (person != null));
        }
    }
}
