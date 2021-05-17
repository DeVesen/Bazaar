namespace DeVes.Bazaar.Contracts.Logic
{
    public interface ITempDataMemory
    {
        object Get(string    key);
        bool   TryGet(string key, out object value);


        void Set(string key, object value);
    }
}