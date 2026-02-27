using System.Text.RegularExpressions;
using Vapour.Models.Internal;

namespace Vapour.ISteamWebAPIUtil;

public class GetSupportedAPIList
{
    public readonly static Delegate Handler = (HttpContext httpContext, IEnumerable<EndpointDataSource> endpointSources) =>
    {
        // TODO: this should also check if a key is specified and possibly add extra endpoints based on the extra permissions it provides
        var endpoints = endpointSources.SelectMany(source => source.Endpoints).OfType<RouteEndpoint>();

        var apiList = new Dictionary<string, List<APIMethod>>();

        foreach (var endpoint in endpoints)
        {
            var url = endpoint.RoutePattern.RawText;
            var metadata = endpoint.Metadata.OfType<HttpMethodMetadata>().First();

            if (url != null)
            {
                // strip out anything empty as there will be a "" at the start from before the first /
                var segments = url.Split("/").Where(segment => !string.IsNullOrEmpty(segment)).ToArray();

                if (segments.Length != 3)
                {
                    throw new Exception("Found URL with unexpected format");
                }

                // strip out the leading v and anything else
                var versionString = Regex.Replace(segments[2], "[^0-9,-]+", "");
                // convert as base 10 as there may be leading 0s (but only for some endpoints)
                var versionNumber = Convert.ToInt32(versionString, 10);

                if (apiList.ContainsKey(segments[0]))
                {
                    apiList[segments[0]].Add(new APIMethod()
                    {
                        name = segments[1],
                        version = versionNumber,
                        httpmethod = metadata.HttpMethods.First(),
                        // TODO: this should be non-empty - how do we store this against a controller?
                        parameters = []
                    });
                }
                else
                {
                    apiList.Add(segments[0], new List<APIMethod>() {
                        new APIMethod()
                        {
                            name = segments[1],
                            version = versionNumber,
                            httpmethod = metadata.HttpMethods.First(),
                            // TODO: this should be non-empty - how do we store this against a controller?
                            parameters = []
                        }
                    });
                }
            }
            else
            {
                throw new Exception("Found endpoint with no RoutePattern");
            }
        }

        var output = new List<Dictionary<string, Object>>();

        foreach (var api in apiList)
        {
            output.Add(new Dictionary<string, Object>
            {
                {
                    "name", api.Key
                },
                {
                    "methods", api.Value
                }
            });
        }

        return Results.Ok(new Dictionary<string, Dictionary<string, List<Dictionary<string, Object>>>>
        {
            {
                "apilist", new Dictionary<string, List<Dictionary<string, Object>>>
                {
                    {
                        "interfaces", output
                    }
                }
            }
        });
    };
}
