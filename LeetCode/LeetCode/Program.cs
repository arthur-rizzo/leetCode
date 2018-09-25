using System;
using LeetCode.Medium;
using System.Linq;
namespace LeetCode
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Solution s = new Solution();

			TreeNode t = new TreeNode(3)
			{
				left = new TreeNode(9),
				right = new TreeNode(20)
				{
					left = new TreeNode(15),
					right = new TreeNode(7)
				}
			};
			 s.SortColors(new int[] { 0,1,0});
		}

		//public static Solution.ListNode parse(string s)
		//{
		//	Solution.ListNode head = null;
		//	Solution.ListNode current = null;
		//	foreach (var x in s.Split(','))
		//	{
		//		if (head == null)
		//			head = current = new Solution.ListNode(int.Parse(x));
		//		else
		//			current.next = current = new Solution.ListNode(int.Parse(x));
		//	}
		//	return head;
		//}

	}
}
