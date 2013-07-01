using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadixTrie
{
    public class PrefixMatcher : IPrefixMatcher
    {
        public bool NextMatchKey(List<int> next)
        {
            while (_currMatch != null)
            {
                List<int> currentNodeKey = _currMatch.Key;

                if (currentNodeKey == null) //Root
                {
                    bool isMatch = false;
                    foreach (KeyValuePair<List<int>, TrieNode> pair in _currMatch.Children)
                    {
                        List<int> tempKey = pair.Key;
                        TrieNode tempNode = pair.Value;
                        int key1 = tempKey[0];
                        int input1 = next[0];

                        if (key1 == input1)
                        {
                            _currMatch = tempNode;
                            isMatch = true;
                            break;
                        }
                    }
                    if (!isMatch)
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }

                int keyLength = currentNodeKey.Count;
                int inputLength = next.Count;

                if (_prefixMatched.Count == keyLength)
                {
                    bool isMatchPrefx = false;

                    foreach (KeyValuePair<List<int>, TrieNode> pair in _currMatch.Children)
                    {
                        List<int> tempKey = pair.Key;
                        TrieNode tempNode = pair.Value;
                        int key1 = tempKey[0];
                        int input1 = next[0];

                        if (key1 == input1)
                        {
                            isMatchPrefx = true;
                            _currMatch = tempNode;
                        }
                    }

                    if (!isMatchPrefx)
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (inputLength <= keyLength)
                {
                    bool isMatch = false;
                    if (_currMatch.IsSubList(next, _prefixMatched.Count, ref isMatch))
                    {
                        foreach (int tmp in next)
                        {
                            _prefixMatched.Add(tmp);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    for (int i = 0; i < keyLength; i++)
                    {
                        if (next[i] != currentNodeKey[i])
                        {
                            return false;
                        }
                    }

                    List<int> diff = next.GetRange(keyLength, inputLength - keyLength);

                    if (_currMatch.Children.Count != 0)
                    {
                        bool isMatchPrefx = false;

                        foreach (KeyValuePair<List<int>, TrieNode> pair in _currMatch.Children)
                        {
                            List<int> tempKey = pair.Key;
                            TrieNode tempNode = pair.Value;
                            int key1 = tempKey[0];
                            int input1 = next[keyLength];

                            if (key1 == input1)
                            {
                                isMatchPrefx = true;
                                _currMatch = tempNode;
                            }
                        }

                        if (!isMatchPrefx)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void ResetMatchKey()
        {
            _currMatch = _root;
            _prefixMatched = new List<int>();
        }

        public bool IsExactMatchKey()
        {
            List<int> key = _currMatch.Key;

            if (_prefixMatched.Count == key.Count && _currMatch.IsTerminal())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GetPrefixMatchValues()
        {
            return _currMatch.GetPrefixMatchValues();
        }

        public List<int> GetPrefixKeys()
        {
            return _prefixMatched;
        }

        public string GetExactMatchValue()
        {
            return IsExactMatchKey() ? _currMatch.Value : null;
        }

        public PrefixMatcher(TrieNode root)
        {
            _root = _currMatch = root;
            _prefixMatched = new List<int>();
        }

        private TrieNode _root;
        private TrieNode _currMatch;
        private List<int> _prefixMatched;
    }
}
