using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {   
        // GET api/values/5
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(int firstNumber, int secondNumber)
        {
            return Ok(firstNumber + secondNumber);
        }

        [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(int firstNumber, int secondNumber)
        {
            return Ok(firstNumber - secondNumber);
        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(int firstNumber, int secondNumber)
        {
            return Ok(firstNumber * secondNumber);
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(int firstNumber, int secondNumber)
        {
            return Ok(firstNumber / secondNumber);
        }
    }
}
