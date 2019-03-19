using DatabaseIntegration.Model;
using DatabaseIntegration.Model.Context;

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

        public Person GetById(int id)
        {
            return _context.Find<Person>(id);
        }
    }
}
