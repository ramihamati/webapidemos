namespace WebApi.DemoActionConstraintFactory.Routing;

public static class HttpRequestExtensions
{
    private static readonly IReadOnlyDictionary<string, VendorType> Map = new Dictionary<string, VendorType>
    {
        [InputAcceptTypes.VndMyAppAccounting] = VendorType.Accounting,
        [InputAcceptTypes.VndMyAppHR] = VendorType.HR
    };

    public static VendorType GetVendorAcceptTypeheader(this HttpRequest request)
    {
        var mediaType = HttpMethods.IsGet(request.Method)
            ? request.GetTypedHeaders()?.Accept?.FirstOrDefault()?.MediaType.Value
            : null;

        if (mediaType == null)
        {
            return VendorType.Unknown;
        }

        return !Map.TryGetValue(mediaType, out var type)
            ? VendorType.Unknown
            : type;
    }
}
