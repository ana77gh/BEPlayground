using System;
using System.Threading;
using System.Threading.Tasks;
using BEPlayground.Application.Common.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BEPlayground.Infrastructure.Background
{
    public class QueuedBackgroundService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<QueuedBackgroundService> _logger;

        public QueuedBackgroundService(IBackgroundTaskQueue taskQueue, ILogger<QueuedBackgroundService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Background Service is running.");
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing background task.");
                }
            }

        }
    }
}
