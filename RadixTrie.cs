using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadixTrie
{
    public class RadixTrie
    {
        public void Delete(List<int> lst)
        {
            if (_root.Children.Count != 0)
            {
                foreach (KeyValuePair<List<int>, TrieNode> pair in _root.Children)
                {
                    List<int> tempkey = pair.Key;
                    TrieNode tempnode = pair.Value;
                    int key1 = tempkey[0];
                    int input1 = lst[0];
                    if (key1 == input1)
                    {
                        Delete(tempnode, lst, 1);
                        break;
                    }
                }
            }
        }

        private void Delete(TrieNode node, List<int> lst, int lstIndex)
        {
            List<int> currentNodeKey = node.Key;

            if (lstIndex == lst.Count && lstIndex == currentNodeKey.Count)
            {
                //Delete the node(including the sub nodes)
                TrieNode parent = node.Parent;

                KeyValuePair<List<int>, TrieNode> tmp;
                foreach (KeyValuePair<List<int>, TrieNode> pair in parent.Children)
                {
                    if (node == pair.Value)
                    {
                        parent.Children.Remove(pair);
                        break;
                    }
                }
                node = null;
                return;
            }

            if (lstIndex < currentNodeKey.Count)
            {
                int tmpKey = currentNodeKey[lstIndex];
                int tmpInput = lst[lstIndex];

                if (tmpKey == tmpInput)
                {
                    Delete(node, lst, lstIndex + 1);
                }
            }
            else
            {
                foreach (KeyValuePair<List<int>, TrieNode> pair in node.Children)
                {
                    List<int> tempkey = pair.Key;
                    TrieNode tempnode = pair.Value;
                    int key1 = tempkey[0];
                    int input1 = lst[lstIndex];
                    if (key1 == input1)
                    {
                        Delete(tempnode, lst, lstIndex + 1);
                        break;
                    }
                }
            }
        }

        public void Insert(List<int> lst, string value)
        {
            bool matchPrefix = false;
            if (_root.Children.Count != 0)
            {
                foreach (KeyValuePair<List<int>, TrieNode> pair in _root.Children)
                {
                    List<int> tempkey = pair.Key;
                    TrieNode tempnode = pair.Value;
                    int key1 = tempkey[0];
                    int input1 = lst[0];
                    if (key1 == input1)
                    {
                        matchPrefix = true;
                        Insert(tempnode, lst, 1, value);
                        break;
                    }
                }
            }
            if (!matchPrefix)
            {
                var newNode = new TrieNode(lst, value);
                newNode.Parent = _root;
                _root.Children.Add(lst, newNode);
            }
        }

        private void Insert(TrieNode node, List<int> lst, int lstIndex, string value)
        {
            List<int> currentNodeKey = node.Key;

            if (lstIndex == lst.Count)
            {
                if (lstIndex < currentNodeKey.Count)
                {
                    var newNode = new TrieNode(lst, value);
                    TrieNode parent = node.Parent;
                    newNode.Parent = parent;
                    List<int> diff = currentNodeKey.GetRange(lstIndex, currentNodeKey.Count - lstIndex);
                    newNode.Children.Add(diff, node);
                    node.Parent = newNode;
                    parent.Children.Remove(currentNodeKey);
                    parent.Children.Add(lst, newNode);
                }
                return;
            }

            if (lstIndex < currentNodeKey.Count)
            {
                int tmpKey = currentNodeKey[lstIndex];
                int tmpInput = lst[lstIndex];

                if (tmpKey == tmpInput)
                {
                    Insert(node, lst, lstIndex + 1, value);
                }
                else
                {
                    List<int> commonPrefixKey = lst.GetRange(0, lstIndex);

                    var newInternalNode = new TrieNode(commonPrefixKey);
                    TrieNode parent = node.Parent;
                    newInternalNode.Parent = parent;

                    var newNode = new TrieNode(lst, value);
                    List<int> lstDiff = lst.GetRange(lstIndex, lst.Count - lstIndex);
                    newInternalNode.Children.Add(lstDiff, newNode);
                    newNode.Parent = newInternalNode;
                    node.Parent = newInternalNode;
                    List<int> currentNodeDiff = currentNodeKey.GetRange(lstIndex, currentNodeKey.Count - lstIndex);
                    newInternalNode.Children.Add(currentNodeDiff, node);

                    parent.Children.Remove(currentNodeKey);
                    parent.Children.Add(commonPrefixKey, newInternalNode);
                }
            }
            else
            {
                bool matchPrefix = false;

                foreach (KeyValuePair<List<int>, TrieNode> pair in node.Children)
                {
                    List<int> tempkey = pair.Key;
                    TrieNode tempnode = pair.Value;
                    int key1 = tempkey[0];
                    int input1 = lst[lstIndex];
                    if (key1 == input1)
                    {
                        matchPrefix = true;
                        Insert(tempnode, lst, lstIndex + 1, value);
                        break;
                    }
                }

                if (!matchPrefix)
                {
                    var newNode = new TrieNode(lst, value);
                    newNode.Parent = node;
                    node.Children.Add(lst.GetRange(lstIndex, lst.Count - lstIndex), newNode);
                }
            }

        }

        private void ComputeNumberOfNode(TrieNode node, ref int sum)
        {
            if (node.Children.Count != 0)
            {
                sum += node.Children.Count;
                foreach (KeyValuePair<List<int>, TrieNode> pair in node.Children)
                {
                    ComputeNumberOfNode(pair.Value, ref sum);
                }
            }
        }

        public RadixTrie()
        {
            this._root = new TrieNode();
            this.Matcher = new PrefixMatcher(this._root);
        }

        private TrieNode _root;
        public IPrefixMatcher Matcher { get; private set; }
        public int NodeCount
        {
            get
            {
                int sum = 1;
                ComputeNumberOfNode(_root, ref sum);
                return sum;
            }
        }
    }
}
