namespace EBana.Domain.Updater
{
    public interface IUpdateSourceValidator
    {
        bool IsValid(string updateSource);
    }
}
