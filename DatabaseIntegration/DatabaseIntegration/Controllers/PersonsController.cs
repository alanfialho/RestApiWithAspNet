using System;
using System.Collections.Generic;
using DatabaseIntegration.Model;
using DatabaseIntegration.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseIntegration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _service;

        public PersonsController(IPersonService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _service.GetById(id);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            var @new = _service.Create(person);
            return Ok(@new);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            person.Id = id;
            _service.Update(person);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}
