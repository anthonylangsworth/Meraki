[![Build status](https://ci.appveyor.com/api/projects/status/u5n74r69je9h3ha0/branch/master?svg=true)](https://ci.appveyor.com/project/anthonylangsworth/merakidashboard/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/anthonylangsworth/MerakiDashboard/badge.svg?branch=master)](https://coveralls.io/github/anthonylangsworth/MerakiDashboard?branch=master)

# Meraki Dashboard API .Net Core Client

[Meraki](http://meraki.cisco.com) is a network appliance vendor acquired by Cisco in 2012 that focuses on 
cloud management and ease of use. Their devices are increasingly popular with companies of any size looking
to simply their network management.

Meraki provides a limited web API for managing their devices called the [Dashboard API](https://dashboard.meraki.com/api_docs). 
Meraki provides wrappers in NodeJS (JavaScript), Ruby and Python but not for C#. This library is the 
beginnings of a .Net Core 2.0 wrapper. Its goals are:
1. Wrap the Meraki Dashboard APIs, making them callable with the least effort.
1. Follow C# standards and conventions as much as possible. For example, provide lists and enums with transparent conversions.
1. Allow easy mocking to make automated testing of calling code easier.
1. Allow APIs not supported by this library to be callable.
1. Provide assistance for debugging both this library and Meraki Dashboard API calls.

It is currently a work in progress. While this library is in beta, breaking changes are still possible.

Based off original work by Michael Park at https://github.com/migrap/Meraki.

## Example

Create a `MerakiDashboardClient` instance using `MerakiDashboardClientFactory.Create`, supplying your API key, 
then call methods on it, such as:

``` C#
using MerakiDashboard;

 // ...

string apiKey; // API key from the user's profile in the Meraki Dashboard
string organizationId; // my organization ID
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	try
	{
		// GET /organizations/[id] as documented at https://dashboard.meraki.com/api_docs#return-an-organization
		Organization organization = await merakiDashboardClient.GetOrganizationAsync(organizationId);

		// ...
	}
	catch(HttpRequestException ex)
	{
		// Hande errors
	}
}
```

If Meraki introduces a new Dashboard API where there is no support in the library, use the `Client` 
property to get access to the underlying `MerakiHttpApiClient` object that a `MerakiDashboardClient`
object uses to call Meraki Dashboard APIs.

For example, if Meraki introduced a `GET /networks/[id]/alerts` API to retreive the alerts
for each network (found on the Dashboard under "Network-wide" -> "Configure" -> "Alerts):

``` C#
using MerakiDashboard;

 // ...

 [DataContract]
 public class NetworkAlerts
 {
	[DataMember(Name="networkId")]
	public string NetworkId { get; set; }

	// Insert other fields
 }

string apiKey; // API key from the user's profile in the Meraki Dashboard
string networkId; // my network ID
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	NetworkAlerts networkAlerts = await merakiDashboardClient.Client.GetAsync<NetworkAlerts>("v0/network/{id}/alerts");

	// ...
}
```

## Exercise

The exercises found at http://developers.meraki.com/post/152434096196/dashboard-api-learning-lab can be found 
in the MerakiDashboard.Console project. At the time of writing, the Meraki organization and devices shown does
not match the documented exercises. However, this may still be helpful to those looking for more complex
examples.

## Contributing

As this is incomplete, pull requests are welcome. Please follow general C# programming guidelines and naming
conventions and remember to include at least one mocked unit test for each new method.

In general:
1. The `MerakiDashboardClient` class is the main interace and exposes one public method wrapping each of the Meraki Dashboard APIs.
1. Each API method should be in the form of `<Verb><MethodName>` where `<Verb>` is the HTTP verb (e.g. "Get", "Put") and `<MethodName>` is an indicative name.
1. Each API method should use the `MerakiHttpApiClient` class in the `Client` property to make the API calls. This provides authentication and a single point of mocking if needed. Add methods to `MerakiHttpApiClient` class for unsupported types of calls if needed.
1. Each API method should have one corresponding mocked test in the `TestMerakiDashboardClient` class in `Meraki.Dashboard.Test`. The aim is to check for future breaking changes.
1. Each API method should escape any URIs passed to methods on the `Client` property using `InterpolateAndEscape`.
1. For new contracts, consider providing strong typing (e.g. `enum`s, `DateTime`s, arrays) for Meraki's weakly typed fields. Provide a field with a "Raw" suffix that accepts or provides the Meraki Dashboard API value and a more strongly typed version. Contracts should be in the Meraki.Dashboard namespace to prevent the need for users to include multiple namespaces.
1. For converting Meraki API weak types to stronger types, Create a class with a `Converter` suffix to convert to and from the type. The converter class should be in the converter namespace and should have corresponding tests in the `Meraki.Dashboard.Test` assembly.

To assist in debugging API calls, instantiate a `MerakiHttpApiDebugContext` around calls to `MerakiHttpApiClient` 
methods. They will log details of data sent and received to Debug listeners, including the Debug window in 
Visual Studio. For example:

```
using (new MerakiHttpApiDebugContext())
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	// Details are written to the debug window for the following call
	Network network = await merakiDashboardClient.GetNetworkAsync("v0/network/{id}/alerts");
}
```