namespace ReactiveAzure
{
    public interface IQueueMessageReader<T>
    {
        ITypedQueueMessage<T> GetMessage();
    }
}