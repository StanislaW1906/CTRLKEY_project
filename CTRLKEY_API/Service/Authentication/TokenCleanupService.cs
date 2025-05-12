using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CTRLKEY_API.Service.Authentication;

public class TokenCleanupService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(1);
    
    public TokenCleanupService(IServiceProvider services)
    {
        _services = services;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _services.CreateScope())
            {
                var authService = scope.ServiceProvider.GetRequiredService<AuthService>();
                await authService.CleanupExpiredTokens();
            }

            await Task.Delay(_cleanupInterval, stoppingToken);
        }
    }
}