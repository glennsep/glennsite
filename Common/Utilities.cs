/************************************************************************************************************
 * Program Id:      Utilities                                                                               *
 * Author:          Glenn Seplowitz                                                                         *
 * Purpose:         Common utility functions that can be used throughout the web site.                      *
 * Written:         October 9, 2013                                                                         *
 *                                                                                                          *
 * Modifications:                                                                                           *
 ***********************************************************************************************************/   

// import namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// define class namespace
namespace Common
{
    // define the class
    public static class Utilities
    {
        #region Members

        /// <summary>
        /// This will return an array of integers that are either odd or even
        /// </summary>
        /// <param name="lower">the lower limit of the range of numbers</param>
        /// <param name="upper">the upper limit of the range of numbers</param>
        /// <param name="isOdd">True if odd numbers are desired, false if even numbers are desired</param>
        /// <returns></returns>
        public static int[] GetArrayofRandomNumbers(int lower, int upper, bool isOdd, Random rand)
        {
            // declare variables
            List<int> numbers = new List<int>();
            int[] numberList;
            int[] numberRand;

            try
            {
                // first build a list of numbers that are either odd or even
                for (int count = lower; count <= upper; count++)
                {
                    if (isOdd && count % 2 != 0)
                        numbers.Add(count);
                    else if (!isOdd && count % 2 == 0)
                        numbers.Add(count);
                }

                // convert this list to an array
                numberList = numbers.ToArray();

                // generate an array of random numbers.  this will be used to randomize the numberList array
                numberRand = Enumerable.Range(0, numberList.GetUpperBound(0)+1).OrderBy(x => rand.Next()).ToArray();

                // clear the list of numbers
                numbers.Clear();

                // now create the array of random numbers
                for (int count = numberRand.GetLowerBound(0); count <= numberRand.GetUpperBound(0); count++)
                {
                    numbers.Add(numberList[numberRand[count]]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // return the array of numbers
            return numbers.ToArray();
        }

        /// <summary>
        /// This will return an array of integers in a specified range in random order
        /// </summary>
        /// <param name="lower">The lower limit</param>
        /// <param name="upper">The upper limit</param>
        /// <returns>an array of random integers</returns>
        public static int[] GetArrayofRandomNumbers(int lower, int upper, Random rand)
        {
            // declare variables
            List<int> numbers = new List<int>();
            int[] numberList;
            int[] numberRand;

            try
            {
                // first build a list of numbers 
                for (int count = lower; count <= upper; count++)
                    numbers.Add(count);

                // convert this list to an array
                numberList = numbers.ToArray();

                // generate an array of random numbers.  this will be used to randomize the numberList array
                numberRand = Enumerable.Range(0, numberList.GetUpperBound(0) + 1).OrderBy(x => rand.Next()).ToArray();

                // clear the list of numbers
                numbers.Clear();

                // now create the array of random numbers
                for (int count = numberRand.GetLowerBound(0); count <= numberRand.GetUpperBound(0); count++)
                {
                    numbers.Add(numberList[numberRand[count]]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // return the array of numbers
            return numbers.ToArray();
        }

        /// <summary>
        /// This will remove the selected number from an array, if that number is found
        /// </summary>
        /// <param name="numberArray">the array of numbers</param>
        /// <param name="numberRemove">the number to remove</param>
        /// <returns>array of integers</returns>
        public static int[] RemoveNumberFromArray(int[] numberArray, int numberRemove)
        {
            // declare variables
            List<int> numberList = numberArray.ToList();

            try
            {
                // remove the suspect number from the list
                numberList.Remove(numberRemove);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // return the array with the suspect removed
            return numberList.ToArray();
        }

        /// <summary>
        /// Determines if number is odd or even
        /// </summary>
        /// <param name="number">the number to check</param>
        /// <returns>true if odd, false if even</returns>
        public static bool IsOdd(int number)
        {
            try
            {
                return (number % 2 != 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
