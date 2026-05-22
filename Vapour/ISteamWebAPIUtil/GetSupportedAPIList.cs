using System.Text.RegularExpressions;
using Vapour.Models.Internal;

namespace Vapour.ISteamWebAPIUtil;

/// <summary>
/// See https://api.steampowered.com/ISteamWebAPIUtil/GetSupportedAPIList/v0001/
/// </summary>
public class GetSupportedAPIList
{
    public readonly static Delegate Handler = (HttpContext httpContext, IEnumerable<EndpointDataSource> endpointSources) =>
    {
        // TODO: should this also check if a key is specified and possibly add extra endpoints based on the extra permissions it provides?
        var endpoints = endpointSources.SelectMany(source => source.Endpoints).OfType<RouteEndpoint>();

        var apiList = new Dictionary<string, List<APIMethod>>();

        foreach (var endpoint in endpoints)
        {
            var url = endpoint.RoutePattern.RawText;
            var metadata = endpoint.Metadata.OfType<HttpMethodMetadata>().First();
            var parameters = endpoint.RequestDelegate?.Method.GetParameters();

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

                var parameterList = new List<APIParameter>();

                if (parameters != null)
                {
                    // TODO: figure out why this is only returning the HttpContext parameter and nothing else
                    foreach (var parameter in parameters)
                    {
                        if (parameter != null)
                        {
                            // most or all controller methods will have a HttpContext parameter that isn't part of the API
                            if (parameter.ParameterType != typeof(HttpContext))
                            {
                                parameterList.Add(new APIParameter()
                                {
                                    name = (parameter.Name != null) ? parameter.Name : "",
                                    // TODO: in some cases string is specified rather than int so that the correct error message can be returned - maybe change the parameters to a custom type that can be special-cased here
                                    type = "",
                                    // TODO: determining nullability seems to depend on whether the parameter is a value or reference type
                                    optional = false,
                                    // TODO: how can we attach extra metadata to a parameter to store this?
                                    description = ""
                                });
                            }
                        }
                    }
                }

                var apiMethod = new APIMethod()
                {
                    name = segments[1],
                    version = versionNumber,
                    httpmethod = metadata.HttpMethods.First(),
                    parameters = parameterList.ToArray()
                };

                if (apiList.ContainsKey(segments[0]))
                {
                    apiList[segments[0]].Add(apiMethod);
                }
                else
                {
                    apiList.Add(segments[0], new List<APIMethod>() {
                        apiMethod
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
