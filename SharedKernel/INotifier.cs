namespace SharedKernel
{
    public interface INotifier
    {
        Task Notify<TKey>(IAggregateRoot<TKey> notificationItem, string channelName);
    }
}
