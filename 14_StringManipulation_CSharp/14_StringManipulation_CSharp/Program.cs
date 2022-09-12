using System;
using System.Text;

namespace StringManipulation
{
    public static class StringAlgorithms{

        public static int NumberOfVowels(string input)
        {
            HashSet<char> vowels = new HashSet<char>() { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' };
            int count = 0;
            foreach (var letter in input)
                if (vowels.Contains(letter)) count++;
            return count;
        }

        public static string ReverseString(string input)
        {
            var output = new StringBuilder();
            for (int i = input.Length-1; i >= 0; i--)
                output.Append(input[i]);
            return output.ToString();
        }

        public static string ReverseWordOrder(string input)
        {
            var words = input.Split(' ');
            return String.Join(" ", words.Reverse());
        }

        public static bool IsRotation(string input1, string input2)
        {
            //int i2 = input2.IndexOf(input1[0]);
            //for (int i1 = 0; i1 < input2.Length; i1++)
            //{
            //    if(input1[i1] != input2[i2++]) return false;
            //    if (i2 == input2.Length) i2 = 0;
            //}
            //return true;

            return (input1.Length == input2.Length &&
                    (input1 + input2).Contains(input2));
        }

        public static string RemoveDuplicates(string input)
        {
            var set = new HashSet<char>();
            var output = new StringBuilder();
            foreach (var letter in input)
            {
                if (!set.Contains(letter))
                {
                    set.Add(letter);
                    output.Append(letter);
                }
            }
            return output.ToString();
        }

        public static char MostRepeatedChar(string input)
        {
            var occurances = new Dictionary<char, int>();
            foreach (var letter in input)
            {
                if (occurances.ContainsKey(letter))
                {
                    occurances[letter]++;
                }
                else
                {
                    occurances.Add(letter, 0);
                }
            }
            int max = 0;
            char maxChar = ' ';
            foreach (var item in occurances)
            {
                if (item.Value > max) maxChar = item.Key;
            }
            return maxChar;
        }

        public static string CapitalizeString(string input)
        {
            var words = input.Trim()
                             .Split(' ');
            var output = new StringBuilder();
            foreach (var word in words)
            {
                output.Append(Char.ToUpper(word[0]));
                output.Append(word[1..^0]);
                output.Append(" ");
            }
            return output.ToString();
        }

        public static bool IsAnagram(string input1, string input2)
        {
            if (input2.Length != input1.Length) return false;
            foreach (var letter in input1)
            {
                if (!input2.Contains(letter)) return false;
            }
            return true;
        }

        public static bool IsPalindrome(string input1)
        {
            int j = input1.Length-1;
            for (int i = 0; i < input1.Length/2; i++)
                if (input1[i] != input1[j--]) return false;
            return true;
        }
    }


public class Program
    {
        public static void Main(string[] arg)
        {
            #region String Exercises

            Console.WriteLine($"Number of vowels in 'hello' is: {StringAlgorithms.NumberOfVowels("hello")}");
            Console.WriteLine($"Reverse 'hello' is: {StringAlgorithms.ReverseString("hello")}");
            Console.WriteLine($"Reverse words 'Trees are beautiful' is: {StringAlgorithms.ReverseWordOrder("Trees are beautiful")}");
            Console.WriteLine($"Is CDAB rotation of ABDC : {StringAlgorithms.IsRotation("ABCD", "CDAB")}");
            Console.WriteLine($"Remove Duplicates of 'Helllooo' : {StringAlgorithms.RemoveDuplicates("Helllooo")}");
            Console.WriteLine($"Most Repeated Char of 'Hellooo' : {StringAlgorithms.MostRepeatedChar("Hellooo")}");
            Console.WriteLine($"Capitalize 'first letter of each word' : {StringAlgorithms.CapitalizeString("  first letter of each word")}");
            Console.WriteLine($"Is Palindrome 'abba' : {StringAlgorithms.IsPalindrome("abba")}");

            #endregion

        }
    }
}