using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadixTrie
{
    /// <summary>
    /// TrieNode Class
    /// </summary>
    public class TrieNode
    {
        public List<string> GetPrefixMatchValues()
        {
            if (IsLeaf())
            {
                if (IsTerminal())
                {
                    return new List<string>() { Value };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var values = new List<string>();
                foreach (TrieNode node in Children.Values)
                {
                    values.AddRange(node.GetPrefixMatchValues());
                }

                if (IsTerminal())
                {
                    values.Add(Value);
                }

                return values;
            }
        }

        public bool IsSubList(List<int> input, int index, ref bool isMatch)
        {
            int i = 0;

            for (i = 0; i < input.Count; i++)
            {
                if (input[i] != Key[i + index])
                {
                    return false;
                }
            }

            isMatch = ((i + index) == Key.Count);

            return true;
        }

        public bool IsLeaf()
        {
            return Children.Count == 0;
        }

        public bool IsTerminal()
        {
            return Value != null;
        }

        public TrieNode()
        {
            Parent = null;
            Children = new Dictionary<List<int>, TrieNode>();
        }

        public TrieNode(List<int> key)
            : this()
        {
            this.Key = key;
        }

        public TrieNode(List<int> key, string value)
            : this()
        {
            this.Key = key;
            this.Value = value;
        }

        public List<int> Key { get; private set; }
        public string Value { get; private set; }
        public TrieNode Parent { get; set; }
        public IDictionary<List<int>, TrieNode> Children { get; private set; } 
    }
}
