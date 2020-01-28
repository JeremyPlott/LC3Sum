using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
	public IList<IList<int>> ThreeSum(int[] nums)
	{

		//we're going to store each unique solution inside this list
		List<IList<int>> zeroSumSets = new List<IList<int>>();

		//part of optimization is sorting the array which lets us use logic to skip wrong answers faster.
		//this is important so that a double-pointer can be used instead of a triple-for loop.
		Array.Sort(nums);

		var sortedArray = nums.ToArray();

		//Here's where your brain will start to hurt while figuring out the strategy.
		//We're using the leftmost value as a target value, and looking for two numbers
		//larger than it that will neutralize it. Think of balancing a set of scales.
		//We're going to iterate through our possible target values, stopping 2 before the end because
		//solution sets contain three numbers.
		//Take the array [-5, -2, -1, 2, 3, 7]. 
		//The requirement for unique sets means that we don't have to consider the 3 or 7 at the top end of the array
		//because when solving for -5, the 7 was accounted for, and 3 was accounted for with the -2 solution.

		//increment our number to neutralize, stopping 2 before the end because solutions are 3 numbers
		for (int targetIndex = 0; targetIndex < sortedArray.Length - 2; targetIndex++)
		{
			//if it isn't the first check and this number is the same as the previous number, move on.
			//this is important for optimization. (target > 0) is there to avoid out of range while checking (target - 1).
			if (targetIndex > 0 && sortedArray[targetIndex] == sortedArray[targetIndex - 1]) continue;

			//we are going to use a pointer at each end to home in on potential solutions
			var rightPointer = sortedArray.Length - 1;
			var leftPointer = targetIndex + 1;

			//value we're looking for. I need two numbers that add up to 7 to negate -7.
			var negatingValue = sortedArray[targetIndex] * -1;

			//do this until the two pointers would overlap
			while (leftPointer < rightPointer)
			{
				//this is what we are checking the negatingValue against
				var pointerSum = sortedArray[leftPointer] + sortedArray[rightPointer];

				//another important optimization. If the sum of the two pointers is greater than the value
				//we are looking for, we need to make the sum smaller. To do that, we move the right
				//pointer more to the left. Here's where sorting the array lets us be more efficient.
				if (pointerSum > negatingValue) rightPointer--;
				if (pointerSum < negatingValue) leftPointer++;

				if (pointerSum == negatingValue)
				{
					//when we have found a solution, make a new list...
					List<int> neutralArray = new List<int>() { sortedArray[targetIndex], sortedArray[leftPointer], sortedArray[rightPointer] };

					//and add it to the empty structure we made at the start
					zeroSumSets.Add(neutralArray);

					//this is another important optimization.
					//while the pointers haven't crossed yet, and the next number is the same as the current number
					//move to the next.
					//checking the next number instead of previous lets us avoid out of range exceptions.
					//there's no risk while checking next because we're moving towards center,
					//and stopping if they would cross
					while (leftPointer < rightPointer && sortedArray[leftPointer] == sortedArray[leftPointer + 1]) leftPointer++;
					while (leftPointer < rightPointer && sortedArray[rightPointer] == sortedArray[rightPointer - 1]) rightPointer--;

					//we just took care of duplicate numbers, so the next numbers will be new.
					//now we have to increment both pointers to those new numbers or we end up
					//stuck in an infinite loop.
					rightPointer--;
					leftPointer++;
				}
			}
		}

		return zeroSumSets;
	}
}