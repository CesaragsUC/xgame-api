using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Services.HealthCheck
{
    public class DbHealthCheck : IHealthCheck
    {
        private readonly XGamesContext _context;
        public DbHealthCheck(XGamesContext context)
        {
            _context = context;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            //Create a Database Health Check based in my  XgameDb context
            try
            {
                if(!await _context.Database.CanConnectAsync(cancellationToken))
                    return await Task.FromResult(HealthCheckResult.Unhealthy("The database is down."));

                await _context.Database.CloseConnectionAsync();
                return await Task.FromResult(HealthCheckResult.Healthy("HealthyCheck: The Database is up and running..."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "The database is down."));
            }

        }
    }
}
