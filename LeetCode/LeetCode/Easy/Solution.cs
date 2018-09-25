using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LeetCode.Easy
{
	public class Solution
	{
		bool IsBadVersion(int n)
		{
			return n >= 2;
		}
		public int FirstBadVersion(int n)
		{
			int start = 1;
			int end = n;

			while (start < end)
			{
				int middle = start + (end - start) / 2;
				if (IsBadVersion(middle))
				{
					end = middle;
				}
				else
				{
					start = middle + 1;
				}
			}

			return start;
		}

		public void Merge(int[] nums1, int m, int[] nums2, int n)
		{
			m = m - 1;
			n = n - 1;
			int currentIdx = nums1.Length - 1;

			while(m >= 0 || n >= 0)
			{
				if(n < 0)
					nums1[currentIdx--] = nums1[m--];
				else if(m < 0)
					nums1[currentIdx--] = nums2[n--];
				else
				{
					if(nums1[m] > nums2[n])
						nums1[currentIdx--] = nums1[m--];
					else
						nums1[currentIdx--] = nums2[n--];
				}
			}
		}

		public class TreeNode
		{
			public int val;
			public TreeNode left;
			public TreeNode right;
			public TreeNode(int x) { val = x; }
		}

		public TreeNode SortedArrayToBST(int[] nums)
		{
			return sortedArray(nums, 0, nums.Length);
		}

		public TreeNode sortedArray(int[] nums, int lowInclusive, int highExclusive)
		{
			if (lowInclusive >= highExclusive)
				return null;

			int mid = (lowInclusive + highExclusive) / 2;

			TreeNode root = new TreeNode(nums[mid]);
			root.left = sortedArray(nums, lowInclusive, mid);
			root.right = sortedArray(nums, mid + 1, highExclusive);

			return root;
		}


		public IList<IList<int>> LevelOrder(TreeNode root)
		{
			List<int>[] result = new List<int>[MaxDepth(root)];
			LevelOrder(root, result);

			return result.ToList<IList<int>>();
		}

		private void LevelOrder(TreeNode root, IList<int>[] result, int n = 0)
		{
			if (root == null)
				return;

			if (result[n] == null)
				result[n] = new List<int>();

			LevelOrder(root.left, result, n + 1);
			result[n].Add(root.val);
			LevelOrder(root.right, result, n + 1);
		}


		public bool IsSymmetric(TreeNode root)
		{
			var left = root;
			var right = root;

			Queue<(TreeNode, TreeNode)> q = new Queue<(TreeNode, TreeNode)>();
			q.Enqueue((left, right));

			while(q.Count > 0)
			{
				var x = q.Dequeue();

				if (x.Item1 == null && x.Item2 == null)
					continue;
				else if (x.Item1 == null || x.Item2 == null || x.Item1.val != x.Item2.val)
					return false;
				else
				{
					q.Enqueue((x.Item1.left, x.Item2.right));
					q.Enqueue((x.Item1.right, x.Item2.left));
				}
			}

			return true;
		}

		public bool IsValidBST(TreeNode root)
		{
			if (root == null)
				return true;

			return isValidBST(root, int.MaxValue, int.MinValue);
		}

		private bool isValidBST(TreeNode root, int? upperLimit, int? lowerLimit)
		{
			bool valid = true;

			if ((upperLimit.HasValue && root.val >= upperLimit) || (lowerLimit.HasValue && root.val <= lowerLimit))
				return false;

			if (root.left != null)
			{
				if (root.left.val >= root.val)
					return false;

				valid &= isValidBST(root.left, Math.Min(root.val, upperLimit.GetValueOrDefault(int.MaxValue)), lowerLimit);
			}

			if (root.right != null)
			{
				if (root.right.val <= root.val)
					return false;

				valid &= isValidBST(root.right, upperLimit, Math.Max(root.val, lowerLimit.GetValueOrDefault(int.MinValue)));
			}

			return valid;
		}

		public int MaxDepth(TreeNode root)
		{
			if (root == null)
				return 0;
			else
				return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
		}

		public class ListNode
		{
			public int val;
			public ListNode next;
			 public ListNode(int x) { val = x; }
		}

		public bool HasCycle(ListNode head)
		{
			ListNode slow = head, fast = head;
			while(slow != null && fast.next != null)
			{
				if (slow == fast)
					return true;
				else
				{
					slow = slow.next;
					fast = fast.next.next;
				}
			}

			return false;
		}

		public bool IsPalindrome(ListNode head)
		{
			Stack<ListNode> s = new Stack<ListNode>();
			var node = head;
			while (node != null)
			{ 
				s.Push(node);
				node = node.next;
			}

			int maxI = s.Count / 2;
			node = head;
			for (int i = 0; i < maxI; i++)
				if (s.Pop().val != node.val)
					return false;
				else
					node = node.next;

			return true;
		}

		public ListNode MergeTwoLists(ListNode l1, ListNode l2)
		{
			if (l1 == null && l2 == null)
				return null;

			if (l2 == null)
				return l1;

			if (l1 == null)
				return l2;

			var c1 = l1;
			var c2 = l2;

			ListNode head = null;
			if(c1.val < c2.val)
			{
				head = c1;
				c1 = c1.next;
			}
			else
			{
				head = c2;
				c2 = c2.next;
			}

			var current = head;
			while(c1 != null || c2 != null)
			{
				if(c1 == null)
				{
					current.next = c2;
					break;
				}
				else if(c2 == null)
				{
					current.next = c1;
					break;
				}
				else
				{
					if(c1.val < c2.val)
					{
						current.next = c1;
						current = c1;
						c1 = c1.next;
					}
					else
					{
						current.next = c2;
						current = c2;
						c2 = c2.next;
					}
				}
			}

			return head;
		}

		public ListNode ReverseList(ListNode head)
		{
			ListNode newHead;
			ListNode x = getReversed(head, out newHead);

			if(x != null)
				x.next = null;

			return newHead;
		}

		private ListNode getReversed(ListNode current, out ListNode newHead)
		{
			newHead = null;

			if (current == null)
				return null;

			ListNode lastItem = null;
			if(current.next == null)
			{
				newHead = current;
				lastItem = newHead;
			}
			else
			{
				var x = getReversed(current.next, out newHead);
				x.next = current;
				lastItem = current;
			}

			return lastItem;
		}

		public ListNode RemoveNthFromEnd(ListNode head, int n)
		{
			Stack<ListNode> s = new Stack<ListNode>();

			var node = head;
			while(node != null)
			{
				s.Push(node);
				node = node.next;
			}

			while (n > 0)
			{ 
				node = s.Pop();
				n--;
			}

			if (s.Count > 0)
			{ 
				s.Pop().next = node.next;
				return head;
			}
			else
				return node.next;

		}

		public string LongestCommonPrefix(string[] strs)
		{
			if (strs == null || strs.Length == 0)
				return "";

			string commonPrefix = "";
			int idx = 0;

			START:

			char currentChar = ' ';
			foreach (var s in strs)
			{
				if (idx < s.Length)
				{
					if (currentChar == ' ')
						currentChar = s[idx];
					else if (currentChar != s[idx])
						goto END;
					else
						continue;
				}
				else
					goto END;
			}

			commonPrefix += strs[0][idx];
			idx++;

			goto START;

			END:

			return commonPrefix;
		}

		public string CountAndSay(int n)
		{
			if (n == 1)
				return "1";

			int i = 1;
			string current = "1";
			while(i <= n)
			{
				current = expand(current);
				i++;
			}

			return current;
		}
		private string expand(string s)
		{
			StringBuilder b = new StringBuilder();

			char current = ' ';
			int count = 0;
			int idx = 0;

			while(idx < s.Length)
			{
				if (current == ' ')
				{
					current = s[idx];
					count++;
				}
				else if (current == s[idx])
					count++;
				else
				{
					b.Append(count);
					b.Append(current);

					current = s[idx];
					count = 1;
				}
			}

			b.Append(count);
			b.Append(current);

			return b.ToString();
		}

		public int StrStr(string haystack, string needle)
		{
			return haystack.IndexOf(needle);
		}

		public int MyAtoi(string str)
		{
			str = str.TrimStart();

			bool isNegative = false;
			if (str.Length == 0)
				return 0;

			int idx = 0;
			if (str[idx] == '-' || str[idx] == '+')
			{ 
				isNegative = str[idx] == '-';
				idx++;
			}

			if (idx < str.Length && char.IsDigit(str[idx]))
			{
				StringBuilder b = new StringBuilder();
				if (isNegative)
					b.Append('-');
				while (idx < str.Length && char.IsDigit(str[idx]))
					b.Append(str[idx++]);

				int value;
				if (int.TryParse(b.ToString(), out value))
					return value;
				else
					return isNegative ? int.MinValue : int.MaxValue;
			}
			else
				return 0;
		}

		public bool IsPalindrome(string s)
		{
			int low = 0;
			int high = s.Length - 1;

			while(low < high)
			{
				while (low < high && !char.IsLetterOrDigit(s[low]))
					low++;

				while (high > low && !char.IsLetterOrDigit(s[high]))
					high--;

				if (char.ToLower(s[low]) != char.ToLower(s[high]))
					return false;

				low++;
				high--;
			}

			return true;
		}

		public bool IsAnagram(string s, string t)
		{
			if (s.Length != t.Length)
				return false;

			HashSet<char> set = new HashSet<char>();
			Dictionary<char, int> sDic = new Dictionary<char, int>();
			Dictionary<char, int> tDic = new Dictionary<char, int>();

			foreach (var c in s)
			{
				set.Add(c);
				sDic[c] = sDic.ContainsKey(c) ? sDic[c] + 1 : 1;
			}

			foreach (var c in t)
			{
				set.Add(c);
				tDic[c] = tDic.ContainsKey(c) ? tDic[c] + 1 : 1;
			}

			foreach(char c in set)
			{
				if (!sDic.ContainsKey(c) || !tDic.ContainsKey(c))
					return false;
				else if (sDic[c] != tDic[c])
					return false;
			}

			return true;
		}

		public int FirstUniqChar(string s)
		{
			System.Collections.Specialized.OrderedDictionary d = new System.Collections.Specialized.OrderedDictionary();

			for(int i = 0; i < s.Length; i++)
			{
				if (d.Contains(s[i]))
				{
					Tuple<int,int> t = (Tuple<int, int>)(d[s[i]]);
					d[(object)s[i]] = Tuple.Create(t.Item1 + 1, t.Item2);
				}
				else
					d[(object)s[i]] = Tuple.Create(1,i);
			}

			for (int i = 0; i < d.Count; i++)
			{
				Tuple<int, int> t = (Tuple<int,int>)d[i];
				if (t.Item1 == 1)
					return t.Item2;
			}

			return -1;
		}

		public int Reverse(int x)
		{
			var s = x.ToString();
			bool isNegative = false;

			StringBuilder b = new StringBuilder(s.Length);
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (s[i] == '-')
				{
					isNegative = true;
					continue;
				}
				else
					b.Append(s[i]);
			}

			try
			{
				int value = int.Parse(b.ToString()) * (isNegative ? -1 : 1);
				return value;
			}
			catch
			{
				return 0;
			}

		}
		public string ReverseString(string s)
		{
			StringBuilder b = new StringBuilder();
			foreach (char c in s.Reverse())
				b.Append(c);

			return b.ToString();
		}

		public void Rotate(int[,] matrix)
		{
			System.Threading.Tasks.Parallel.For(0, matrix.GetLength(0) / 2, (i) => rotateOuterSquares(matrix, i));
		}
		private void rotateOuterSquares(int[,] matrix, int diagIndex)
		{
			int size = matrix.GetLength(0) - (2 * diagIndex);

			if (size == 1 || size == 0)
				return;

			for(int i = 0; i < size-1; i++)
			{
				//performs swaps top => right => bottom => left
				var topIndex = (diagIndex + 0, diagIndex + i);
				var rightIndex = (diagIndex + i, diagIndex + size - 1);
				var botIndex = (diagIndex + size - 1, diagIndex + size - 1 - i);
				var leftIndex = (diagIndex + size - 1 - i, diagIndex + 0);

				var topNewValue = matrix[leftIndex.Item1, leftIndex.Item2];

				matrix[leftIndex.Item1, leftIndex.Item2] = matrix[botIndex.Item1, botIndex.Item2];
				matrix[botIndex.Item1, botIndex.Item2] = matrix[rightIndex.Item1, rightIndex.Item2];
				matrix[rightIndex.Item1, rightIndex.Item2] = matrix[topIndex.Item1, topIndex.Item2];
				matrix[topIndex.Item1, topIndex.Item2] = topNewValue;
			}
		}

		public bool IsValidSudoku(char[,] board)
		{
			HashSet<char>[] columnsValidation = new HashSet<char>[9];
			HashSet<char>[] squaresValidation = new HashSet<char>[9];

			for (int i = 0; i < board.GetLength(0); i++)
			{
				HashSet<char> rowValidation = new HashSet<char>();
				for (int j = 0; j < board.GetLength(1); j++)
				{
					if (board[i, j] == '.')
						continue;

					//validates row
					if(!rowValidation.Add(board[i, j]))
						return false;

					//Validates column
					if (columnsValidation[j] == null)
						columnsValidation[j] = new HashSet<char>();

					if (!columnsValidation[j].Add(board[i, j]))
						return false;

					//validates Square
					int squareNumber = getSquareNumber(i, j);

					if (squaresValidation[squareNumber] == null)
						squaresValidation[squareNumber] = new HashSet<char>();

					if (!squaresValidation[squareNumber].Add(board[i, j]))
						return false;
				}
			}
			return true;
		}
		private int getSquareNumber(int i, int j)
		{
			var iDiv = i / 3;
			var jDiv = j / 3;

			return (3 * iDiv) + jDiv;
		}

		public int[] TwoSum(int[] nums, int target)
		{
			//key is complement to a existing number. for a number X, key is target - X. value is index of X.
			Dictionary<int, int> checker = new Dictionary<int, int>();

			for (int i = 0; i < nums.Length; i++)
			{
				int idx;
				if (checker.TryGetValue(nums[i], out idx))
				{
					return new int[] { idx, i };
				}
				else
				{
					checker[target - nums[i]] = i;
				}
			}

			return null;
		}

		public void MoveZeroes(int[] nums)
		{
			int iNextNonZeroNumber = 0;
			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] != 0)
				{
					nums[iNextNonZeroNumber] = nums[i];
					iNextNonZeroNumber++;
				}
			}

			for (int i = iNextNonZeroNumber; i < nums.Length; i++)
				nums[i] = 0;
		}

		public int[] Intersect(int[] nums1, int[] nums2)
		{
			Dictionary<int, (int,int)> x = new Dictionary<int, (int, int)>();

			foreach (var i in nums1)
				if (!x.ContainsKey(i))
					x.Add(i, (1,0));
				else
					x[i] = (x[i].Item1 + 1,0);

			foreach (var i in nums2)
				if (x.ContainsKey(i))
					x[i] = (x[i].Item1, x[i].Item2 + 1);

			List<int> result = new List<int>();
			foreach(var kp in x)
			{
				var i1 = kp.Value.Item1;
				var i2 = kp.Value.Item2;

				if (i2 > 0)
					result.AddRange(Enumerable.Repeat(kp.Key, Math.Min(i1, i2)));
			}

			return result.ToArray();
		}

		public int SingleNumber(int[] nums)
		{
			HashSet<int> s = new HashSet<int>();
			foreach(var x  in nums)
			{
				if (!s.Add(x))
					s.Remove(x);
			}

			return s.Single();
		}

		public bool ContainsDuplicate(int[] nums)
		{
			HashSet<int> s = new HashSet<int>();

			foreach (var x in nums)
				if (s.Contains(x))
					return true;
				else
					s.Add(x);

			return false;
		}

		public int RemoveDuplicates(int[] nums)
		{
			if (nums == null || nums.Length == 0)
				return 0;

			int cursor = 1;
			int currentNumber = nums[0];

			for(int i = 1; i < nums.Length; i++)
			{
				if(nums[i] != currentNumber)
				{
					nums[cursor] = currentNumber = nums[i];
					cursor++;
				}
			}

			return cursor;
		}

		public int MaxProfit2(int[] prices)
		{
			int maxProfix = 0;

			for (int i = 0; i < prices.Length - 1; i++)
				maxProfix += Math.Max(0, prices[i + 1] - prices[i]);

			return maxProfix;
		}
		public int MaxProfit(int[] prices)
		{
			int?[] partialResult = new int?[prices.Length];
			return maxProfit(prices, 0, partialResult);
		}
		private int maxProfit(int[] prices, int startIndex, int?[] partialResult)
		{
			if (startIndex >= prices.Length - 1)
				return 0;

			if (partialResult[startIndex] != null)
				return partialResult[startIndex].Value;

			var startElement = prices[startIndex];
			var max = 0;
			int? earliestIndexWihthoutBuy = null;
			for(int i = startIndex + 1; i < prices.Length; i++)
			{
				if(prices[i] > startElement)
				{
					var profit = prices[i] - startElement + maxProfit(prices, i + 1, partialResult);
					max = Math.Max(profit, max);
				}
				else if(!earliestIndexWihthoutBuy.HasValue)
				{
					earliestIndexWihthoutBuy = i;
					var profit = maxProfit(prices, i, partialResult);
					max = Math.Max(profit, max);
				}
			}

			partialResult[startIndex] = max;
			return max;
		}

		public void Rotate(int[] nums, int k)
		{
			if ((k = (k % nums.Length)) == 0)
				return;

			var gdc = CalculateGDC(nums.Length, k);
			for(int i = 0; i < gdc; i++)
			{
				var nextPosition = i + k;
				var element = nums[i];

				while(nextPosition != i)
				{
					var previous = nums[nextPosition];
					nums[nextPosition] = element;
					element = previous;

					nextPosition = (nextPosition + k) % nums.Length;
				}
				nums[i] = element;
				}
		}

		private int CalculateGDC(int a, int b)
		{
			if (a % b == 0)
				return b;
			else
				return CalculateGDC(b, a % b);

		}
	}
}
