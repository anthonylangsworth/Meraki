# Meraki Dashboard API .Net Client

The beginnings of a C# wrapper over the Meraki Dashboard APIs (https://dashboard.meraki.com/api_docs).

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

## Exercise

The exercises found at http://developers.meraki.com/post/152434096196/dashboard-api-learning-lab can be found 
in the MerakiDashboard.Console project. At the time of writing, the Meraki organization and devices shown does
not match the documented exercises. However, this may still be helpful to those looking for more complex
examples.

