 

namespace SharedKernel
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        Task NotifyEvent(INotifier notifier, string channelName);
    }
}
