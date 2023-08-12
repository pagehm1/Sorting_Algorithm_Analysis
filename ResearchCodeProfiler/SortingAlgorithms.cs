//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Research Paper - Code Profiling
//	File Name:		ResearchCodeProfiler
//	Description:	Sorts a list of numbers through multiple sorting algorithms
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Hunter Page, pagehm1@etsu.edu
//	Created:		Thursday, March 19, 2020
//	Copyright:		Hunter Page, 2020
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Linq;

namespace ResearchCodeProfiler
{
    /// <summary>
    /// Sorts list of from 100 - 10,000 numbers with different algorithms, depending on the Number Range
    /// </summary>
    class SortingAlgorithms
    {
        //Sets an int to use for the capacity for the list
        private static int NumberRange = 1000;

        //Initializes random number
        private static Random RunNumbers = new Random();        
        
        //creates an individual list for every algorithm and sets up the initial list
        private static List<int> BubbleSortList = new List<int>(NumberRange);
        private static List<int> OriginalList = new List<int>(NumberRange);
        private static List<int> SelectionSortList = new List<int>(NumberRange);
        private static List<int> InsertSortList = new List<int>(NumberRange);
        private static List<int> MergeSortList = new List<int>(NumberRange);
        private static List<int> QuickSortList = new List<int>(NumberRange);
        private static List<int> QuickSortMedianOfThree = new List<int>(NumberRange);
        private static List<int> ShellSortList = new List<int>(NumberRange);
        private static List<int> CountingSortList = new List<int>(NumberRange);
        private static List<int> RadixSortList = new List<int>(NumberRange);

        /// <summary>
        /// Runs a list through all of the algorithms and displays them
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            //Adds number to the original list and reverse sorts the list
            for(int i = 0; i < NumberRange; i++)
            {
                int randomNumber = RunNumbers.Next(0, 500);
                OriginalList.Add(randomNumber);
                OriginalList.Sort();
                OriginalList.Reverse();
            }
            
            //adds the components of the original list to the lists for the algorithms
            BubbleSortList.AddRange(OriginalList);
            SelectionSortList.AddRange(OriginalList);
            InsertSortList.AddRange(OriginalList);
            MergeSortList.AddRange(OriginalList);
            QuickSortList.AddRange(OriginalList);
            QuickSortMedianOfThree.AddRange(OriginalList);
            ShellSortList.AddRange(OriginalList);
            CountingSortList.AddRange(OriginalList);
            RadixSortList.AddRange(OriginalList);

            //sorts each list using a specific algorithm
            BubbleSort(BubbleSortList);
            SelectionSort(SelectionSortList, NumberRange);
            InsertionSort(InsertSortList);
            OriginalQuickSort(QuickSortList);
            MedianOfThreeQuickSortMain(QuickSortMedianOfThree);
            ShellSort(ShellSortList);
            CountingSort(CountingSortList);
            RadixTenLSDSort(RadixSortList);
            MergeSortList = MergeSort(MergeSortList); 
        }

        /// <summary>
        /// swaps numbers in list until all numbers are in order
        /// </summary>
        /// <param name="list">list of unordered numbers</param>
        private static void BubbleSort(List<int> list)
        {
            bool isSorted = false;
            int pass = 0;
            
            //run until list is ordered.
            while(!isSorted && pass < list.Count)
            {
                pass++;
                isSorted = true;
                int passDifference = NumberRange - pass;

                //passes number through the list until it finds a bigger one
                for(int i = 0; i < passDifference; i++)
                {
                    if(list[i] > list[i+1])
                    {
                        SwapNumbers(list, i, i + 1);
                        isSorted = false;
                    }
                }

            }
        }
        
        /// <summary>
        /// switch the positioning of two numbers
        /// </summary>
        /// <param name="list">list being passed</param>
        /// <param name="one">first number</param>
        /// <param name="two">second number being passed</param>
        private static void SwapNumbers(List<int> list, int one, int two)
        {
            int temporaryNumber = list[one];
            list[one] = list[two];
            list[two] = temporaryNumber;
        }

        /// <summary>
        /// sorts by finding smallest number and putting it at the beginning
        /// of the list
        /// </summary>
        /// <param name="list">list being passed</param>
        /// <param name="listCount">amount of numbers in the list</param>
        private static void SelectionSort(List<int> list, int listCount)
        {
            //if list is empty
            if(listCount <= 1)
            {
                return;
            }


            int biggestNumber = HighestNumber(list, listCount);

            //replace number at end of list with biggest number in list
            if(list[biggestNumber] != list[ listCount - 1])
            {
                SwapNumbers(list, biggestNumber, listCount - 1);
            }

            //recursive call to complete remainder of list until sorted
            SelectionSort(list, listCount - 1);
           
        }

        /// <summary>
        /// finds the biggest number in a list
        /// </summary>
        /// <param name="list">list being passed</param>
        /// <param name="listCount">count of numbers in the list</param>
        /// <returns>index of the largest number</returns>
        private static int HighestNumber(List<int> list, int listCount)
        {
            int maxNum = 0;

            for(int i = 0; i < listCount; i++)
            {
                if(list[maxNum] < list[i])
                {
                    maxNum = i;
                }
            }
            return maxNum;
        }

        #region Insertion methods
        
        /// <summary>
        /// moves numbers one at a time to beginning of the list
        /// until it becomes larger than the number before it 
        /// </summary>
        /// <param name="list">list passed through</param>
        private static void InsertionSort(List<int> list)
        {
            int j;

            for(int i = 1; i < list.Count; i++)
            {
                int index = list[i];
                
                //move through list until chosen number is smaller than number it is comparing
                for(j = i; j > 0 && index < list[j-1]; j--)
                {
                    list[j] = list[j - 1];
                }

                list[j] = index;
            }
        }

        /// <summary>
        /// overloaded Insert sort method that is provided with a beginning and end index 
        /// rather than just the beginning and end of the list
        /// </summary>
        /// <param name="list">list passed through</param>
        /// <param name="startIndex">starting position</param>
        /// <param name="endIndex">the position in list to end at</param>
        private static void InsertionSort(List<int> list, int startIndex, int endIndex)
        {
            int index, j;

            //moves through list until current index is smaller than number being compared
            for (int i = startIndex +1; i <= endIndex; i++)
            {
                index = list[i];

                for (j = i; j > startIndex && index < list[j - 1]; j--)
                {
                    list[j] = list[j - 1];
                }

                list[j] = index;
            }
        }
        #endregion

        #region MergeSort
        
        /// <summary>
        /// divides list into smaller subsets, sorts each subset, then combines them back, 
        /// sorting at the same time
        /// </summary>
        /// <param name="list">The unordered list</param>
        /// <returns>The sorted list</returns>
        private static List<int> MergeSort(List<int> list)
        {
            //return list if no numbers 
            if (list.Count <= 1)
            {
                return list;
            }

            //create lists to modify
            List<int> modifiedList = new List<int>();
            List<int> leftSide = new List<int>();
            List<int> rightSide = new List<int>();

            //find middle number of list
            int middleOfList = list.Count / 2;

            //adds left half of list
            for (int i = 0; i < middleOfList; i++)
            {
                leftSide.Add(list[i]);
            }

            //adds right side of list 
            for (int i = middleOfList; i < list.Count; i++)
            {
                rightSide.Add(list[i]);
            }

            //recursive call to method to modify each side of each sub-list until it the entire list is ordered.
            leftSide = MergeSort(leftSide);
            rightSide = MergeSort(rightSide);

            //checks if all of the left side is smaller than the numbers on the right side 
            if (leftSide[leftSide.Count - 1] <= rightSide[0])
            {
                return Append(leftSide, rightSide);
            }

            //merge the now two ordered sub-lists together to get the final list
            modifiedList = MergeList(leftSide, rightSide);

            return modifiedList;
        }

        /// <summary>
        /// Takes the left and right sub-lists and adds them until each list is empty.
        /// </summary>
        /// <param name="leftSide">The left half of the list</param>
        /// <param name="rightSide">The right half of the list</param>
        /// <returns>list from the left and right subsets</returns>
        private static List<int> MergeList(List<int> leftSide, List<int> rightSide)
        {
            List<int> modifiedList = new List<int>();

            //compares the number at an index and adds to the modified list,
            //then removes the number from the list.
            while (leftSide.Count > 0 && rightSide.Count > 0)
            {
                if (leftSide[0] < rightSide[0])
                {
                    modifiedList.Add(leftSide[0]);
                    leftSide.RemoveAt(0);
                }
                else
                {
                    modifiedList.Add(rightSide[0]);
                    rightSide.RemoveAt(0);
                }
            }

            //one list still has one number, add to the modified list
            while (leftSide.Count > 0)
            {
                modifiedList.Add(leftSide[0]);
                leftSide.RemoveAt(0);
            }

            while(rightSide.Count > 0)
            {
                modifiedList.Add(rightSide[0]);
                rightSide.RemoveAt(0);
            }
            return modifiedList;
        }

        /// <summary>
        /// returns a modified list that contains all numbers in order
        /// </summary>
        /// <param name="leftSide">The left half of the list.</param>
        /// <param name="rightSide">The right half of the list.</param>
        /// <returns>Appended list created from the left and right side subsets</returns>
        private static List<int> Append(List<int> leftSide, List<int> rightSide)
        {
            List<int> modifiedList = new List<int>(leftSide);
            
            //combines numbers from each list together
            foreach(int num in rightSide)
            {
                modifiedList.Add(num);
            }

            return modifiedList;
        }
        #endregion

        #region QuickSort methods

        /// <summary>
        ///  Divide and Conquer algorithm that partitions array around selected pivot
        /// </summary>
        /// <param name="list">The list passed in.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        private static void QuickSort(List<int> list, int startIndex, int endIndex)
        {
            //beginning and end number for the list
            int left = startIndex;
            int right = endIndex;

            //if list is empty
            if(left >= right)
            {
                return;
            }

            
            while(left < right)
            {
                //changes number to check to the previous number on the right side of the list 
                while(list[left] <= list[right] && left < right)
                {
                    right--;
                }

                //swap the numbers if right is greater
                if(left < right)
                {
                    SwapNumbers(list, left, right);
                }

                //changes number on left side to compare
                while(list[left] <= list[right] && left < right)
                {
                    left++;
                }

                //checks new left number and swaps if necessary
                if(left < right)
                {
                    SwapNumbers(list, left, right);
                }
            }

            //recursive call to check half of each list until entire list is sorted.
            QuickSort(list, startIndex, left - 1); 
            QuickSort(list, right + 1, endIndex); 
        }

        /// <summary>
        /// Calls the quick sort method with initialized indexers
        /// </summary>
        /// <param name="list">The list that needs sorted</param>
        private static void OriginalQuickSort(List<int> list)
        {
            QuickSort(list, 0, list.Count - 1);
        }

        #endregion


        #region Median of Three

        /// <summary>
        /// Quick sort method uses first middle and last number in list as the pivot
        /// </summary>
        /// <param name="list">The unordered list</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="endIndex">The end index.</param>
        private static void MedianOfThreeQuickSort(List<int> list, int startIndex, int endIndex)
        {
            //used to switch to insertion point
            const int cutoff = 10;


            if(startIndex + cutoff > endIndex)
            {
                InsertionSort(list, startIndex, endIndex);
            }

            else
            {
                //median of list
                int middleIndex = (startIndex + endIndex) / 2;

                //swap the three pivots until they are in order
                if(list[middleIndex] < list[startIndex])
                {
                    SwapNumbers(list, startIndex, middleIndex);
                }
                if(list[endIndex] < list[startIndex])
                {
                    SwapNumbers(list, startIndex, endIndex);
                }
                if(list[endIndex] < list[middleIndex])
                {
                    SwapNumbers(list, middleIndex, endIndex);
                }

                int pivot = list[middleIndex];
                SwapNumbers(list, middleIndex, endIndex - 1);

                //partition the list
                int left, right;
                for (left = startIndex, right = endIndex - 1; ;)
                {
                    while(list[++left] < pivot)
                    {
                        ;
                    }
                    while(pivot < list[--right])
                    {
                        ;
                    }
                    if(left < right)
                    {
                        SwapNumbers(list, left, right);
                    }
                    else
                    {
                        break;
                    }
                }

                //place pivot back
                SwapNumbers(list, left, endIndex - 1);

                //recursive call the left and right half of the list
                MedianOfThreeQuickSort(list, startIndex, left - 1);
                MedianOfThreeQuickSort(list, left + 1, endIndex);
            }   
        }

        /// <summary>
        /// calls the Median of Three Quick Sort method with the required indexes.
        /// </summary>
        /// <param name="list">The unordered list</param>
        private static void MedianOfThreeQuickSortMain(List<int> list)
        {
            MedianOfThreeQuickSort(list, 0, list.Count - 1);
        }
        #endregion

        /// <summary>
        /// Starts sorting smaller subsets of the main list, then increases the amount
        /// in each set until entire list is sorted.
        /// </summary>
        /// <param name="list">The list.</param>
        private static void ShellSort(List<int> list)
        {
            //gap of 2.2 to separate list until it reach 0 or 1
            for(int gap = list.Count / 2; gap > 0; gap = (gap == 2 ? 1 : (int)(gap/2.2)))
            {
                int temporaryNumber, j;

                //sort the subset created by insertion
                for(int i = gap; i < list.Count; i++)
                {
                    temporaryNumber = list[i];
                    for(j = i; j >= gap && temporaryNumber < list[j - gap]; j-= gap)
                    {
                        list[j] = list[j - gap];
                    }

                    list[j] = temporaryNumber;
                }
            }
        }

        /// <summary>
        /// Counts occurrences of every number in the list and sorts based off amount
        /// of times a number occurs.
        /// </summary>
        /// <param name="list">The unordered list.</param>
        /// <returns>The sorted list</returns>
        private static List<int> CountingSort(List<int> list)
        {
            //min and max number of the list
            int maximumNumber = list.Max();
            int minimumNumber = list.Min();

            //int array that will hold the number of times each element is in the array
            int[] counts = new int[maximumNumber - minimumNumber + 1];
            int counter = 0;

            //sets the elements count to 0
            for(int i = 0; i < counts.Length; i++)
            {
                counts[i] = 0;               
            }
            
            //adds to element's count if fund in the array
            for (int i = 0; i < list.Count; i++)
            {
                counts[list[i] - minimumNumber]++;
            }

            //replace the item in the list to its proper place. 
            for (int i = minimumNumber; i <= maximumNumber; i++)
            {
                while(counts[i - minimumNumber]-- > 0)
                {
                    list[counter] = i;
                    counter++;
                }
            }

            return list;
        }

        #region Radix        
        /// <summary>
        /// radix sorting using base 10 and least significant digit
        /// </summary>
        /// <param name="list">The unordered list.</param>
        /// <returns>the sorted list</returns>
        private static List<int> RadixTenLSDSort(List<int> list)
        {
            //10 bins
            List<List<int>> binHolder = new List<List<int>>(10);

            //adds to bin with new lists 
            for(int i = 0; i < 10; i++)
            {
                binHolder.Add(new List<int>(list.Count));
            }

            int numberOfDigits = list.Max().ToString().Length;

            //start with right-most digit and copy into proper bin
            for(int j = 0; j < numberOfDigits; j++)
            {
                for(int k = 0; k < list.Count; k++)
                {
                    binHolder[Digit(list[k], j)].Add(list[k]);
                }

                CopyResult(binHolder, list);

                //clears bin for the next round
                for(int i = 0; i < 10; i++)
                {
                    binHolder[i].Clear();
                }
            }

            return list;
        }

        /// <summary>
        /// Copies values from each bin back into the list, 0-9
        /// </summary>
        /// <param name="bin">list of bins</param>
        /// <param name="newResult">List of integers</param>
        private static void CopyResult(List<List<int>> bin, List<int> newResult)
        {
            newResult.Clear();

            for(int i = 0; i < 10; i++)
            {
                foreach(int j in bin[i])
                {
                    newResult.Add(j);
                }
            
            }
        }

        /// <summary>
        /// gets digit of the value in the digitLocation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="digitLocation">The digit location.</param>
        /// <returns>The digit of value</returns>
        private static int Digit(int value, int digitLocation)
        {
            return (value / (int)Math.Pow(10, digitLocation) % 10);
        }

        #endregion
    }
}
