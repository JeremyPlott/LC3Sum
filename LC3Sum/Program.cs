using System;
using System.Collections.Generic;
using System.Linq;

namespace LC3Sum
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] nums = new int[] { -1, 0, 1, 2, -1, -4 };

            IList<IList<int>> neutralSets = ThreeSum(nums);

            foreach (List<int> set in neutralSets)
            {
                foreach (int num in set)
                {
                    Console.Write($"{num}, ");
                }
                Console.WriteLine("");
            }

            IList<IList<int>> ThreeSum(int[] nums)
            {
                List<IList<int>> zeroSumSets = new List<IList<int>>();

                Array.Sort(nums);

                var sortedArray = nums.ToArray();

                for (int targetIndex = 0; targetIndex < sortedArray.Length - 2; targetIndex++)
                {
                    if (targetIndex > 0 && sortedArray[targetIndex] == sortedArray[targetIndex - 1]) continue;

                    var rightPointer = sortedArray.Length - 1;                
                    var leftPointer = targetIndex + 1;

                    var negatingValue = sortedArray[targetIndex] * -1;

                    while (leftPointer < rightPointer)
                    {
                        var pointerSum = sortedArray[leftPointer] + sortedArray[rightPointer];

                        if (pointerSum > negatingValue) rightPointer--;
                        if (pointerSum < negatingValue) leftPointer++;

                        if (pointerSum == negatingValue)
                        {
                            List<int> neutralArray = new List<int>() { sortedArray[targetIndex], sortedArray[leftPointer], sortedArray[rightPointer] };

                            zeroSumSets.Add(neutralArray);

                            while (leftPointer < rightPointer && sortedArray[leftPointer] == sortedArray[leftPointer + 1]) leftPointer++;
                            while (leftPointer < rightPointer && sortedArray[rightPointer] == sortedArray[rightPointer - 1]) rightPointer--;

                            rightPointer--;
                            leftPointer++;
                        }

                    }
                }                                

                return zeroSumSets;
            }
        }
    }
}