using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApi.DemoActionConstraintFactory.Routing;

namespace WebApi.DemoActionConstraintFactory;

public class VendorRouteConstraint : IActionConstraint
{
    private readonly VendorType _type;

    public VendorRouteConstraint(VendorType type)
    {
        _type = type;
    }

    public int Order => 0;

    public bool Accept(ActionConstraintContext context)
    {
        VendorType ventorType = context.RouteContext.HttpContext.Request.GetVendorAcceptTypeheader();

        return _type == ventorType;
    }
}
