using Diferent_Verbs.Model;
using System.Collections.Generic;

namespace Diferent_Verbs.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long id);
        IList<Person> FindAll();
        void Delete(long id);
        Person Update(Person person);
    }
}
