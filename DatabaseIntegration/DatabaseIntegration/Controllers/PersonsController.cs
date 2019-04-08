using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseIntegration.Model;
using DatabaseIntegration.Services;
using DatabaseIntegration.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DatabaseIntegration.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _service;
        private readonly IUrlHelper _urlHelper;

        public PersonsController(IPersonService service, 
            IUrlHelperFactory urlHelperFactory, 
            IActionContextAccessor actionContextAccessor)
        {
            _service = service;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }
        
        [HttpGet("{id}", Name = nameof(Get))]
        public IActionResult Get(int id)
        {
            var person = _service.GetById(id);
            return Ok(MapTo(person));
        }

        [HttpPost(Name = nameof(Post))]
        [ProducesResponseType(200, Type = typeof(PersonViewModel))]
        public IActionResult Post([FromBody, Required] Person person)
        {
            var @new = _service.Create(person);
            return Ok(MapTo(@new));
        }

        [HttpPut("{id}", Name =nameof(Put))]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            person.Id = id;
            _service.Update(person);
            return Ok();
        }

        [HttpDelete("{id}", Name =nameof(Delete))]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        private PersonViewModel MapTo(Person person)
        {
            var personId = new { id = person.Id };
            List<LinkViewModel> links = new List<LinkViewModel>();

            links.Add(new LinkViewModel(_urlHelper.Link(nameof(Get), personId),
                "self",
                "GET"));

            links.Add(new LinkViewModel(_urlHelper.RouteUrl(nameof(Post)),
                "self",
                "POST"));

            links.Add(new LinkViewModel(_urlHelper.Link(nameof(Put), personId),
                "self",
                "PUT"));

            links.Add(new LinkViewModel(_urlHelper.Link(nameof(Delete), personId),
                "self",
                "DELETE"));

            return new PersonViewModel(person.Id, person.FirstName, person.LastName, person.Address, person.Gender, links);
        }
    }
}

