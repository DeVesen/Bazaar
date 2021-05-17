using System.Collections.Generic;
using DeVes.Bazaar.Contracts.Logic;

namespace DeVes.Bazaar.Contracts
{
    public class TempDataMemory : ITempDataMemory
    {
        private readonly Dictionary<string, object> _valueDict = new Dictionary<string, object>();


        public object Get(string    key)                   => TryGet(key, out var value) ? value : null;
        public bool   TryGet(string key, out object value) => _valueDict.TryGetValue(key, out value);

        public void   Set(string    key, object     value)
        {
            _valueDict[key] = value;
        }
    }
}
