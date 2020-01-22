namespace EBana.Domain.Updater
{
    public class SimpleUpdateSourceValidator 
        : IUpdateSourceValidator
    {
        public bool IsValid(string updateSource)
        {
            if ((updateSource ?? string.Empty) != string.Empty)
                return true;
            return false;
        }
    }
}
