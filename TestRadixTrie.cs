using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace RadixTrie
{
    [TestFixture]
    public class TestRadixTrie
    {
        [Test]
        public void RadixTrie()
        {
            var radixTrie = new RadixTrie();

            var lst1 = new List<int>() { 1, 2, 3, 4 };
            var lst2 = new List<int>() { 1, 2, 3 };
            var lst3 = new List<int>() { 2, 3, 4 };
            var lst4 = new List<int>() { 2, 3 };
            var lst5 = new List<int>() { 3 };

            //Insert Test
            Assert.AreEqual(1, radixTrie.NodeCount);
            radixTrie.Insert(lst1, "A");
            Assert.AreEqual(2, radixTrie.NodeCount);
            radixTrie.Insert(lst2, "B");
            Assert.AreEqual(3, radixTrie.NodeCount);
            radixTrie.Insert(lst3, "C");
            Assert.AreEqual(4, radixTrie.NodeCount);
            radixTrie.Insert(lst4, "D");
            Assert.AreEqual(5, radixTrie.NodeCount);
            radixTrie.Insert(lst5, "E");
            Assert.AreEqual(6, radixTrie.NodeCount);

            //Index Search Test
            Assert.True(radixTrie.Matcher.NextMatchKey(new List<int>() { 1 }));
            List<string> lst = radixTrie.Matcher.GetPrefixMatchValues(); // "A" , "B"
            Assert.True(lst.Count == 2);

            Assert.False(radixTrie.Matcher.IsExactMatchKey());
            Assert.True(radixTrie.Matcher.NextMatchKey(new List<int>() { 2, 3 }));
            Assert.True(radixTrie.Matcher.IsExactMatchKey());
            Assert.True("B" == radixTrie.Matcher.GetExactMatchValue());

            Assert.False(radixTrie.Matcher.NextMatchKey(new List<int>() { 5 }));
            Assert.True(radixTrie.Matcher.NextMatchKey(new List<int>() { 4 }));

            radixTrie.Matcher.ResetMatchKey();
            Assert.True(radixTrie.Matcher.NextMatchKey(new List<int>() { 3 }));

            //Delete Test
            radixTrie.Delete(lst5);
            Assert.AreEqual(5, radixTrie.NodeCount);
            radixTrie.Delete(lst4);
            Assert.AreEqual(3, radixTrie.NodeCount);
            radixTrie.Delete(lst3);
            Assert.AreEqual(3, radixTrie.NodeCount);
            radixTrie.Delete(lst1);
            Assert.AreEqual(2, radixTrie.NodeCount);
            radixTrie.Delete(lst2);
            Assert.AreEqual(1, radixTrie.NodeCount);

            radixTrie.Matcher.ResetMatchKey();
            Assert.False(radixTrie.Matcher.NextMatchKey(new List<int>() { 1 }));
        }
    }
}
