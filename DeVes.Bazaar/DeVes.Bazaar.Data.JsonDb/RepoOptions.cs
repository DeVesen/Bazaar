using JsonFlatFileDataStore;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class RepoOptions
    {
        internal DataStore DataStoreObj { get; }

        public RepoOptions(string path)
        {
            DataStoreObj = new DataStore(path);
        }
    }
}