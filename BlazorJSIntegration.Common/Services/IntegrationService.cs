using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace BlazorJSIntegration.Common.Services;

/// <summary>
/// Integration service.
/// </summary>
public class IntegrationService : IDisposable
{
    private readonly IJSRuntime jSRuntime;
    private readonly ILogger<IntegrationService> logger;
    private readonly DotNetObjectReference<IntegrationService> objRef;
    private readonly Lazy<Task<IJSObjectReference>> jsIntegrationServiceLazy;
    private readonly CancellationTokenSource cancellationTokenSource;

    private bool disposedValue;

    private CancellationToken CancellationToken => cancellationTokenSource.Token;

    /// <summary>
    /// Constructor.
    /// </summary>
    public IntegrationService(IJSRuntime jSRuntime,
        ILogger<IntegrationService> logger)
    {
        this.jSRuntime = jSRuntime;
        this.logger = logger;

        objRef = DotNetObjectReference.Create(this);
        jsIntegrationServiceLazy = new(CreateJsIntegrationServiceAsync);
        cancellationTokenSource = new();
    }

    /// <summary>
    /// Ping.
    /// </summary>
    /// <returns></returns>
    public async Task PingAsync(SomeDto dto)
    {
        var jsIntegrationService = await jsIntegrationServiceLazy.Value;

        await jsIntegrationService.InvokeVoidAsync("ping", CancellationToken, dto);
    }

    /// <summary>
    /// Update dto.
    /// </summary>
    [JSInvokable]
    public void UpdateDto(SomeDto dto)
    {
        logger.LogWarning("New dto - {Dto}", dto);
    }

    private async Task<IJSObjectReference> CreateJsIntegrationServiceAsync()
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "blazorIntegration.createIntegrationService",
            CancellationToken,
            objRef);
    }

    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (disposedValue)
        {
            return;
        }

        if (disposing)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();

            objRef.Dispose();
        }

        disposedValue = true;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

/// <summary>
/// Some dto.
/// </summary>
/// <param name="Count">Count.</param>
/// <param name="Name">Name.</param>
/// <param name="LastName">Last name.</param>
public record struct SomeDto(int Count, string Name, string LastName);
