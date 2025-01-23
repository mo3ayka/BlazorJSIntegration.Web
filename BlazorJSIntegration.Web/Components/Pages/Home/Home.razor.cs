using BlazorJSIntegration.Common.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorJSIntegration.Web.Components.Pages.Home;

/// <summary>
/// Home page.
/// </summary>
public partial class Home
{
    /// <summary>
    /// Integration service.
    /// </summary>
    [Inject]
    private IntegrationService? IntegrationService { get; set; }

    private int currentCount = 0;

    private async Task IncrementCount()
    {
        currentCount++;

        if (IntegrationService is null)
        {
            return;
        }

        await IntegrationService.PingAsync(new(currentCount, "Some", "Yess"));
    }
}
