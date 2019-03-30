using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseIntegration.Model;
using DatabaseIntegration.Model.Context;

namespace DatabaseIntegration.Services
{
    public class BookService : IBookService
    {
        private readonly CursoAspNetCoreContext _context;

        public BookService(CursoAspNetCoreContext context)
        {
            _context = context;
        }

        public IList<Book> GetAll()
        {
            return _context.Books.ToList();
        }
    }
}
