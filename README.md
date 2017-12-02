# Meraki Dashboard API .Net Client

The beginnings of a C# wrapper over the Meraki Dashboard APIs (https://dashboard.meraki.com/api_docs).

Includes the exercises found at http://developers.meraki.com/post/152434096196/dashboard-api-learning-lab. 

Based off original work by Michael Park at https://github.com/migrap/Meraki.

## Example

Create a `MerakiDashboardClient` instance using `MerakiDashboardClientFactory.Create`, supplying your API key, 
then call methods on it, such as:

``` C#
using MerakiDashboard;

 // ...

string organizationId; // my organization ID
using (MerakiDashboardClient merakiDashboardClient = MerakiDashboardClientFactory.Create(apiKey))
{
	Organization organization = await merakiDashboardClient.GetOrganizationAsync(organizationId);

	// ...
}
```