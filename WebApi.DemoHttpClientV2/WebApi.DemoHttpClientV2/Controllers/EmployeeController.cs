using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.DemoHttpClientV2.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("employee")]
        public IActionResult GetEmployee()
        {
            return Ok(new Employee
            {
                FirstName = "John",
                LastName = "Doe"
            });
        }
    }
}
