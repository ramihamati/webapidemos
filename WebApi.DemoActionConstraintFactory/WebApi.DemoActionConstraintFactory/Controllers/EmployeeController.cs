using Microsoft.AspNetCore.Mvc;
using WebApi.DemoActionConstraintFactory.Routing;

namespace WebApi.DemoActionConstraintFactory.Controllers;

[ApiController]
[Route("api/employee")]

public class EmployeeHRController : ControllerBase
{
    [HttpGet]
    [VendorRoute("Get", VendorType.HR)]
    public IActionResult Get()
    {
        return Ok(new EmployeeHR
        {
            FirstName = "John",
            LastName = "Doe",
            Onboarded = new DateOnly(2000, 1, 1)
        });
    }
}

[ApiController]
[Route("api/employee")]

public class EmployeeAccountingController : ControllerBase
{
    [HttpGet]
    [VendorRoute("Get", VendorType.Accounting)]
    public IActionResult Get()
    {
        return Ok(new EmployeeAccounting
        {
            FirstName = "John",
            LastName = "Doe",
            BilledHours = 100
        });
    }
}