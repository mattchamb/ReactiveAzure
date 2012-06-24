namespace ReactiveAzure
{
    public interface IQueueMessageReader<out T>
    {
        ITypedQueueMessage<T> GetMessage();
    }
}