namespace ReactiveAzure
{
    internal delegate void MessageReceivedHandler<TMessage>(TypedMessage<TMessage> message);
}