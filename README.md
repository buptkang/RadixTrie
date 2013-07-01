**Radix tree version of Trie Data Structure (C#)**
===============================
Functional Operation Of Trie: Insert, Delete, LoopUp.

TrieNode Key:   List<int>  "0,1,2,4"
TrieNode Value: String,  e.g "Kang"
-------------------------------
Computational Complexity:

- Time Analysis:  Same to Trie, theta(k) where k is the length of lookup sequence

- Space Analysis: Less space to store encoding sequences in compared to Trie. 

How to: Run the Unit Test File TestRadixTrie.cs To Use It. 

Example

(List1 : 1, 2, 3, 4; "A")
(List2 : 1, 2, 3;  "B")
(List3: 2,3,4; "C")
(List4: 2,3; "D")
(List5: 3; "E")

				Trie						  RadixTrie
				
				root							  root
			  /   |    \                        /  |   \
			 1    2    (3)				  (1,2,3) (2,3) (3)							
		    /     |							  /    |
		  1,2   (2,3)				  (1,2,3,4)  (2,3,4)
		  /       |
	(1,2,3)    (2,3,4)
		/  
    (1,2,3,4)			