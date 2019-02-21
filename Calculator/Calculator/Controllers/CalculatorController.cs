using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {   
        // GET api/values/5
        [HttpGet("{firstNumber}/{secondNumber}")]
        public IActionResult Get(int firstNumber, int secondNumber)
        {
            return Ok(firstNumber + secondNumber);
        }
    }
}
