using DatabaseIntegration.Model;

namespace DatabaseIntegration.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person GetById(int id);
    }
}