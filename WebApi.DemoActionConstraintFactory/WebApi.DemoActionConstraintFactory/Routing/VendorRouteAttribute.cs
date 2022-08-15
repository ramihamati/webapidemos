using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApi.DemoActionConstraintFactory.Routing;

namespace WebApi.DemoActionConstraintFactory;

public class VendorRouteAttribute : RouteAttribute, IActionConstraintFactory
{
    private readonly IActionConstraint _constraint;

    public VendorRouteAttribute(string template, VendorType vType)
        : base(template)
    {
        Order = -10;
        _constraint = new VendorRouteConstraint(vType);
    }

    public bool IsReusable => true;

    public IActionConstraint CreateInstance(IServiceProvider services)
    {
        return _constraint;
    }
}
