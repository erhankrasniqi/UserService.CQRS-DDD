namespace SharedKernel
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        public async Task NotifyEvent(INotifier notifier, string channelName)
        {
            await notifier.Notify<TKey>(this, channelName).ConfigureAwait(false);
        }
    }
}
