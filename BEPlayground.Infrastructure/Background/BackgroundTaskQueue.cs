using System.Threading.Channels;
using BEPlayground.Application.Common.Interfaces;

namespace BEPlayground.Infrastructure.Background
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<CancellationToken, ValueTask>> _queue;
        public BackgroundTaskQueue()
        {
            _queue = Channel.CreateUnbounded<Func<CancellationToken, ValueTask>>();
        }
        public async ValueTask QueueAsync(Func<CancellationToken, ValueTask> workItem)
        {
            ArgumentNullException.ThrowIfNull(workItem);
            await _queue.Writer.WriteAsync(workItem);
        }
        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}
