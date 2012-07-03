namespace ReactiveAzure
{
    /// <summary>
    /// Responsible for reading messages out of a queue and deserializing the resulting raw message.
    /// </summary>
    /// <typeparam name="T">The type of the message to be deserialzed from the message.</typeparam>
    public interface IQueueMessageReader<T>
    {
        /// <summary>
        /// Returns a <see cref="TypedMessage{TMessage}"/> from whatever messaging mechanism implements this interface.
        /// </summary>
        /// <returns></returns>
        TypedMessage<T> GetMessage();
    }
}