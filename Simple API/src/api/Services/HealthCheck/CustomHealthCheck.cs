using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Services.HealthCheck
{
    public class CustomHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.FromResult(HealthCheckResult.Healthy("HealthyCheck service its running..."));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(
                                new HealthCheckResult(
                                    context.Registration.FailureStatus, "The HealthyCheck service is down."));
            }
        }
    }
}
