[![Build status](https://ci.appveyor.com/api/projects/status/u5n74r69je9h3ha0/branch/master?svg=true)](https://ci.appveyor.com/project/anthonylangsworth/merakidashboard/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/anthonylangsworth/MerakiDashboard/badge.svg?branch=master)](https://coveralls.io/github/anthonylangsworth/MerakiDashboard?branch=master)

# Meraki Dashboard API .Net Client

[Meraki](http://meraki.cisco.com) is a network appliance vendor acquired by Cisco in 2012 that focuses on 
cloud management and ease of use. Their devices are increasingly popular with companies of any size looking
to simply their network management.

Meraki provides a limited web API for managing their devices called the [Dashboard API](https://dashboard.meraki.com/api_docs). 
Meraki provides wrappers in NodeJS (JavaScript), Ruby and Python but not for C#. This library is the 
beginnings of a C# wrapper. Its goals are:
1. Wrap the Meraki Dashboard APIs, making them callable with the least effort.
1. Follow C# standards and conventions as much as possible. For example, provide lists and enums with transparent conversions.
1. Allow easy mocking to make automated testing of calling code easier.
1. Allow APIs not supported by this library to be callable.
1. Provide assistance for debugging both this library and Meraki Dashboard API calls.

It is currently a work in progress.

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
	// GET /organizations/[id] as documented at https://dashboard.meraki.com/api_docs#return-an-organization
	Organization organization = await merakiDashboardClient.GetOrganizationAsync(organizationId);

	// ...
}
```

If Meraki introduces a new Dashboard API where there is no support in the library, use the `Client` 
property to get access to the underlying `MerakiHttpApiClient` object that the `MerakiDashboardClient`
uses to call APIs.

For example, if Meraki introduced a "GET /networks/[id]/alerts" method to retreive the alerts
for each network.

``` C#
using MerakiDashboard;

 // ...

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
