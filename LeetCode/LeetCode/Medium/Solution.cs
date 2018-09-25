using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode.Medium
{
    public class Solution
    {
		public IList<IList<int>> ThreeSum(int[] nums)
		{
			List<IList<int>> result = new List<IList<int>>();

			Array.Sort(nums);

			for(int i = 0; i < nums.Length; i++)
			{
				if (i > 0 && nums[i] == nums[i - 1])
					continue;

				var target = nums[i] * -1;

				int s = i + 1;
				int e = nums.Length - 1;

				while (s < e)
				{
					var sum = nums[s] + nums[e];
					if (sum == target)
					{ 
						result.Add(new List<int>() { nums[i], nums[s], nums[e] });
						s++;
						while (s < e && nums[s] == nums[s - 1])
							s++;
					}
					else if (sum < target)
						s++;
					else
						e--;
				}

			}

			return result;
		}


		public IList<IList<string>> GroupAnagrams(string[] strs)
		{
			Dictionary<string,IList<string>> result = new Dictionary<string, IList<string>>();

			foreach (var s in strs)
			{
				var orderedS = new string(s.OrderBy(c => c).ToArray());
				IList<string> list;
				if (!result.TryGetValue(orderedS, out list))
					result[orderedS] = list = new List<string>();

				list.Add(s);
			}

			return result.Values.ToList();
		}

		public int LengthOfLongestSubstring(string s)
		{
			Dictionary<char, int> set = new Dictionary<char, int>();

			int start = 0;
			int i = 0;
			int maxSize = 0;

			while (start < s.Length)
			{
				//means letter does not ocurred before or occurred in a segment not being acounted right now
				while (i < s.Length && (!set.ContainsKey(s[i]) || set[s[i]] < start))
				{
					set[s[i]] = i;
					i++;
				}

				maxSize = Math.Max(maxSize, i - start);

				//means it found some repeated char
				if (i < s.Length)
				{
					start = set[s[i]] + 1;
					set[s[i]] = i;
					i++;
				}
				else
					break;
			}

			return maxSize;
		}

		public string LongestPalindrome(string s)
		{
			if (s.Length == 0)
				return "";
			else if (s.Length == 1)
				return s;

			int start = 0;
			int end = 0;
			int maxLength = 0;

			for (int i = 0; i < s.Length - 1; i++)
			{
				int len1 = expandAround(s, i, i);
				int len2 = expandAround(s, i, i + 1);
				int len = Math.Max(len1, len2);

				if (len > maxLength)
				{
					maxLength = len;
					if (len % 2 == 0)
						start = i - ((len / 2) - 1);
					else
						start = i - (len / 2);

					end = start + len - 1;
				}
			}

			return s.Substring(start, maxLength);
		}
		private int expandAround(string s, int left, int right)
		{
			while (left >= 0 && right < s.Length && s[left] == s[right])
			{
				left--;
				right++;
			}

			return right - left - 1;
		}

		public bool IncreasingTriplet(int[] nums)
		{
			int a = int.MaxValue;
			int b = int.MaxValue;
			int c = int.MaxValue;
			bool cSet = false;

			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] <= a)
					a = nums[i];
				else if (nums[i] <= b)
					b = nums[i];
				else
				{ 
					c = nums[i];
					cSet = true;
				}
			}

			return b != int.MaxValue && b > a && cSet && c > b;
		}

		public ListNode OddEvenList(ListNode head)
		{
			var oddCurrent = head;
			var evenStart = head?.next;
			var evenCurrent = head?.next;
			var current = head?.next?.next;

			while(current != null)
			{
				oddCurrent.next = current;
				oddCurrent = current;

				if (current.next != null)
				{
					evenCurrent.next = current.next;
					evenCurrent = current.next;
				}
				else
					evenCurrent.next = null;

				current = current.next?.next;
			}

			if(oddCurrent != null)
				oddCurrent.next = evenStart;

			return head;
		}

		public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
		{
			if (headA == null || headB == null)
				return null;

			var a = ReverseWalk(headA).GetEnumerator();
			var b = ReverseWalk(headB).GetEnumerator();
			ListNode result = null;
			while(a.MoveNext() && b.MoveNext() && a.Current == b.Current)
				result = a.Current;

			return result;
		}

		public IEnumerable<ListNode> ReverseWalk(ListNode head)
		{
			if (head.next != null)
				foreach (var x in ReverseWalk(head.next))
					yield return x;

			yield return head;
		}

		public IList<int> InorderTraversal(TreeNode root)
		{
			return InorderTraversalDefered(root).ToList();
		}
		public IEnumerable<int> InorderTraversalDefered(TreeNode root)
		{
			if (root == null)
				yield break;

			if (root.left != null)
				foreach (var element in InorderTraversalDefered(root.left))
					yield return element;

			yield return root.val;

			if (root.right != null)
				foreach (var element in InorderTraversalDefered(root.right))
					yield return element;

		}

		public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
		{
			if (root == null)
				return new List<IList<int>>();

			List<IList<int>> resultList = new List<IList<int>>();
			Dictionary<int,IList<int>> result = new Dictionary<int, IList<int>>();
			TraverseInorder(root, 0, result);

			int lvl = 0;
			while(result.ContainsKey(lvl))
			{
				if (lvl % 2 == 0)
					resultList.Add(result[lvl]);
				else
					resultList.Add(result[lvl].Reverse().ToList());

				lvl++;
			}

			return resultList;
		}

		private void TraverseInorder(TreeNode root, int level, Dictionary<int, IList<int>> result)
		{
			if (root.left != null)
				TraverseInorder(root.left, level + 1, result);

			IList<int> r;
			if (!result.TryGetValue(level, out r))
				result[level] = r = new List<int>();

			r.Add(root.val);

			if (root.right != null)
				TraverseInorder(root.right, level + 1, result);
		}

		public TreeNode BuildTree(int[] preorder, int[] inorder)
		{
			//preorder Root > Left > Right
			//inorder Left > Root > Right

			if (preorder.Length == 0 || inorder.Length == 0)
				return null;

			return BuildTree(new ArraySegment<int>(preorder), new ArraySegment<int>(inorder));
		}
		public TreeNode BuildTree(ArraySegment<int> preorder, ArraySegment<int> inorder)
		{
			if (preorder.Count == 0 || inorder.Count == 0)
				return null;

			//root element must be the first of preorder
			TreeNode root = new TreeNode(preorder[0]);

			int indexAtInorder = 0;
			for (int i = 0; i < inorder.Count; i++)
				if (inorder[i] == root.val)
				{ 
					indexAtInorder = i;
					break;
				}

			//in the inorder traversal, all elements from left subtree appear before the root
			//so we know that the elements up to indexAtInorder are inorder traversal for the left subtree
			//and the elements after indexAtInorder are the inorder traversal of right subtree
			ArraySegment<int> leftInOrder = inorder.Slice(0, indexAtInorder);
			ArraySegment<int> rightInOrder = inorder.Slice(indexAtInorder + 1);

			//for the preorder, we first visit the root, then left, then right
			ArraySegment<int> lefPreOrder = preorder.Slice(1, leftInOrder.Count);
			ArraySegment<int> rightPreOrder = preorder.Slice(indexAtInorder + 1);

			root.left = BuildTree(lefPreOrder, leftInOrder);
			root.right = BuildTree(rightPreOrder, rightInOrder);

			return root;
		}

		public int KthSmallest(TreeNode root, int k)
		{
			var iterator = InorderTraversalDefered(root).GetEnumerator();
			while(k > 0)
			{
				iterator.MoveNext();
				k--;
			}

			return iterator.Current;
		}

		public int NumIslands(char[,] grid)
		{
			char currentIslandId = 'a'; //arbitrary non 1 or 0 or z;

			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					if (grid[i, j] == '0' || grid[i,j] != '1')
						continue;
					else if(grid[i,j] == '1')
					{
						expandIsland(i, j, currentIslandId, grid);
						currentIslandId++;
					}

					//since we go from left to right and from up to bottom we dont check 
					//for right or down because it would never contain an identified island
				}
			}
				return currentIslandId - 'a';
		}
		private void expandIsland(int i, int j, char currentIslandId, char[,] grid)
		{
			if (grid[i, j] != '1')
				return;

			grid[i, j] = currentIslandId;

			if (j < grid.GetLength(1) - 1 && grid[i, j + 1] == '1') //right
				expandIsland(i, j + 1, currentIslandId, grid);

			if (i > 0 && grid[i - 1, j] == '1') //down
				expandIsland(i - 1, j, currentIslandId, grid);

			if (j > 0 && grid[i, j - 1] == '1') //looks to the left
				expandIsland(i, j - 1, currentIslandId, grid);

			if (i < grid.GetLength(0) - 1 && grid[i + 1, j] == '1') //down
				expandIsland(i + 1, j, currentIslandId, grid);
		}

		public IList<string> LetterCombinations(string digits)
		{
			Dictionary<char, List<string>> map = new Dictionary<char, List<string>>()
			{
				{ '2', new List<string>() { "a","b","c" } },
				{ '3', new List<string>() { "d","e","f" } },
				{ '4', new List<string>() { "g","h","i" } },
				{ '5', new List<string>() { "j","k","l" } },
				{ '6', new List<string>() { "m","n","o" } },
				{ '7', new List<string>() { "p","q","r","s" } },
				{ '8', new List<string>() { "t","u","v" } },
				{ '9', new List<string>() { "w", "x","y", "z" } },
			};

			List<string> result = new List<string>();

			List<List<string>> listsToBeJoined = new List<List<string>>();
			foreach (var c in digits)
				listsToBeJoined.Add(map[c]);

			IEnumerable<string> x = null;
			foreach (var item in listsToBeJoined)
				x = x == null ? item : x.Join(item, s => 1, s => 1, (a, b) => a + b);

			return x != null ? x.ToList() : new List<string>();
		}

		public IList<string> GenerateParenthesis(int n)
		{
			return GenerateParenthesis(n, 0, 0);
		}

		private IList<string> GenerateParenthesis(int n, int openCount, int closedCount)
		{
			List<string> result = new List<string>();

			if(openCount < n)
			{
				//can open once again
				var rOpen = GenerateParenthesis(n, openCount + 1, closedCount);
				result.AddRange(rOpen.Select(x => "(" + x));
			}

			if(openCount > 0 && closedCount < openCount)
			{
				//can close
				var rOpen = GenerateParenthesis(n, openCount, closedCount + 1);
				result.AddRange(rOpen.Select(x => ")" + x));
			}

			if (result.Count == 0)
				result.Add("");

			return result;
		}

		public IList<IList<int>> Permute(int[] nums)
		{
			HashSet<int> usedInts = new HashSet<int>();
			return Permute(nums, usedInts);
		}

		private IList<IList<int>> Permute(int[] nums, HashSet<int> usedInts)
		{
			if (usedInts.Count == nums.Length)
				return new List<IList<int>>() { new List<int>() };

			List<IList<int>> result = new List<IList<int>>();
			foreach (var num in nums)
			{
				if (usedInts.Contains(num))
					continue;

				usedInts.Add(num);

				var r = Permute(nums, usedInts);
				foreach(var l in r)
				{
					l.Add(num);
					result.Add(l);
				}

				usedInts.Remove(num);
			}

			return result;
		}

		public IList<IList<int>> Subsets(int[] nums)
		{
			List<IList<int>> result = new List<IList<int>>() { new List<int>() };

			foreach(var n in nums)
			{
				List<IList<int>> copy = new List<IList<int>>(result);
				foreach(var c in copy)
				{
					var copyToAddElement = new List<int>(c);
					copyToAddElement.Add(n);
					result.Add(copyToAddElement);
				}
			}

			return result;
		}

		public bool Exist(char[,] board, string word)
		{
			if (string.IsNullOrWhiteSpace(word))
				return true;

			List<(int, int)> initialPositions = new List<(int, int)>();

			for (int i = 0; i < board.GetLength(0); i++)
				for (int j = 0; j < board.GetLength(1); j++)
					if (board[i, j] == word[0])
						initialPositions.Add((i, j));

			if (initialPositions.Count != 0)
			{
				foreach(var pos in initialPositions)
				{
					HashSet<(int, int)> set = new HashSet<(int, int)>();
					if (findWordFromPos(word, 0, set, pos, board))
						return true;
				}
			}

			return false;
		}

		private bool findWordFromPos(string word, int v, HashSet<(int, int)> set, (int, int) pos, char[,] board)
		{
			//No more letters to check
			if (v == word.Length)
				return true;

			//out of bounds
			if (pos.Item1 < 0 || pos.Item1 == board.GetLength(0) || pos.Item2 < 0 || pos.Item2 == board.GetLength(1))
				return false;

			//not the letter we are looking for
			if (word[v] != board[pos.Item1, pos.Item2])
				return false;

			//already used this letter
			if (set.Contains(pos))
				return false;

			set.Add(pos);

			if (findWordFromPos(word, v + 1, set, (pos.Item1 + 1, pos.Item2), board))
				return true;
			if (findWordFromPos(word, v + 1, set, (pos.Item1 - 1, pos.Item2), board))
				return true;
			if (findWordFromPos(word, v + 1, set, (pos.Item1, pos.Item2 + 1), board))
				return true;
			if (findWordFromPos(word, v + 1, set, (pos.Item1, pos.Item2 - 1), board))
				return true;

			set.Remove(pos);

			return false;
		}

		public void SortColors(int[] nums)
		{
			if (nums == null || nums.Length <= 1)
				return;

			int zeroIndex = 0;
			int twoIndex = nums.Length - 1;

			for(int i = 0; i < nums.Length; i++)
			{
				if (nums[i] == 1)
					continue;
				else if(nums[i] == 0)
				{
					swap(nums, i, zeroIndex);
					zeroIndex++;
				}
				else if(nums[i] == 2 && i < twoIndex)
				{
					swap(nums, i, twoIndex);
					twoIndex--;

					if (nums[i] == 0)
					{
						swap(nums, zeroIndex, i);
						zeroIndex++;
					}
				}
			}
		}
		private void swap(int[] arr, int x, int y)
		{
			int temp = arr[x];
			arr[x] = arr[y];
			arr[y] = temp;
		}

		public IList<int> TopKFrequent(int[] nums, int k)
		{

			Dictionary<int, int> frequencyDictionary = new Dictionary<int, int>();

			int maxFrequency = 0;
			foreach (var n in nums)
			{
				if (frequencyDictionary.ContainsKey(n))
					frequencyDictionary[n]++;
				else
					frequencyDictionary[n] = 1;

				maxFrequency = Math.Max(maxFrequency, frequencyDictionary[n]);
			}

			List<int>[] buckets = new List<int>[maxFrequency + 1];

			foreach(var pair in frequencyDictionary)
			{
				if (buckets[pair.Value] == null)
					buckets[pair.Value] = new List<int>();

				buckets[pair.Value].Add(pair.Key);
			}

			List<int> result = new List<int>(k);
			while(k > 0 && maxFrequency > 0)
			{
				if(buckets[maxFrequency] != null)
				{ 
					result.AddRange(buckets[maxFrequency]);
					k -= buckets[maxFrequency].Count;
				}

				maxFrequency--;
			}

			return result;
		}
		public int FindKthLargest(int[] nums, int k)
		{
			quicksort(nums, 0 , nums.Length - 1);
			return nums[nums.Length - k];
		}
		public void quicksort(int[] nums, int start, int end)
		{
			if (start >= end)
				return;

			int pivotIndex = start;
			int nextLesserIndex = pivotIndex + 1;
			for(int i = nextLesserIndex; i <= end; i++)
			{
				if(nums[i] < nums[pivotIndex])
				{
					swap(nums, i, nextLesserIndex);
					nextLesserIndex++;
				}
			}

			swap(nums, pivotIndex, nextLesserIndex - 1);
			pivotIndex = nextLesserIndex - 1;

			quicksort(nums, start, pivotIndex - 1);
			quicksort(nums, pivotIndex + 1, end);
		}


		public int FindPeakElement(int[] nums)
		{
			return findPeakUtil(nums, 0, nums.Length - 1);
		}

		private int findPeakUtil(int[] nums, int start, int end)
		{
			int mid = (start + end) / 2;

			int? left = mid > 0 ? nums[mid - 1] : (int?)null;
			int? right = mid < nums.Length - 1 ? nums[mid + 1] : (int?)null;

			if ((!left.HasValue || nums[mid] > left) && (!right.HasValue || nums[mid] > right))
			{
				return mid;
			}
			else if (nums[mid] < left)
				return findPeakUtil(nums, start, mid - 1);
			else
				return findPeakUtil(nums, mid + 1, end);
		}
	}

	public class ListNode {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
  }

	public class TreeNode
	{
		public int val;
		public TreeNode left;
		public TreeNode right;
		public TreeNode(int x) { val = x; }
  }

}
