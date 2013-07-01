using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadixTrie
{
    public interface IPrefixMatcher
    {
        bool NextMatchKey(List<int> next);
        void ResetMatchKey();
        bool IsExactMatchKey();
        List<string> GetPrefixMatchValues();
        List<int> GetPrefixKeys();
        string GetExactMatchValue();
    }
}
