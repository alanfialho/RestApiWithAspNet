using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseIntegration.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseIntegration.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        //colocar no DI
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var allBooks = _bookService.GetAll();
            return Ok(allBooks);
        }
    }
}
