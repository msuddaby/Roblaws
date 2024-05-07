using Hangfire;
using JWTAuthTemplate.Context;

namespace JWTAuthTemplate.Services;

public class DailyUpdateService: IHostedService {
    private readonly IServiceScopeFactory _scopeFactory;

    
    public DailyUpdateService(IServiceScopeFactory scopeFactory) {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken token) {
        Console.WriteLine("Update service started");
        RecurringJob.AddOrUpdate("DailyUpdate", () => Update(), Cron.Minutely);
        return Task.CompletedTask;
    }
    
    public Task StopAsync(CancellationToken token) {
        return Task.CompletedTask;
    }
    
    public async Task Update() {
        using var scope = _scopeFactory.CreateScope();
        var scrapingService = scope.ServiceProvider.GetRequiredService<ScrapingService>();

        await scrapingService.UpdateAll();
    }
    
}