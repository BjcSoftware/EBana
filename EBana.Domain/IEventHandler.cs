namespace EBana.Domain
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent e);
    }
}
