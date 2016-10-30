namespace MvcFlash.AspNetCore
{
    public interface IFlash
    {
        Message Pop();
        void Push<TMessage>(TMessage message) where TMessage: Message;

        int Count();
        void Clear();
    }
}