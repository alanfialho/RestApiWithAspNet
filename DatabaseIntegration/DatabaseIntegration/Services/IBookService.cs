using DatabaseIntegration.Model;
using System.Collections.Generic;

namespace DatabaseIntegration.Services
{
    public interface IBookService
    {
        IList<Book> GetAll();
    }
}
