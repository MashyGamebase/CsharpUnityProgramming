namespace BaseCsharpProgram.StringSort
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class StringSort
    {
        static void Main(string[] args)
        {
            Console.Write("Enter input string: ");
            string input = Console.ReadLine()!;

            Console.Write("Enter sort order: ");
            string sortOrder = Console.ReadLine()!;

            string result = SortLetters(input, sortOrder);

            Console.WriteLine($"Sorted string: {result}");
        }

        // -- String Sort Order --
        // Direct instruction used
        // Added fallback sequences : edge case scenario and unknown sort order
        // Either way it will try to append keys not listed in the sort order or if null it will just sort alphabetically
        public static string SortLetters(string input, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            // Fallback: alphabetical sort
            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                char[] chars = input.ToCharArray();

                Array.Sort(chars);

                return new string(chars);
            }

            Dictionary<char, int> counts = new(sortOrder.Length);

            foreach (char c in sortOrder)
            {
                if (!counts.ContainsKey(c))
                    counts[c] = 0;
            }

            List<char> unknownCharacters = new();

            foreach (char c in input)
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
                else
                {
                    unknownCharacters.Add(c);
                }
            }

            StringBuilder result = new(input.Length);

            // Append characters in custom order
            foreach (char c in sortOrder)
            {
                int count = counts[c];

                for (int i = 0; i < count; i++)
                {
                    result.Append(c);
                }
            }

            // Append unknown characters alphabetically
            if (unknownCharacters.Count > 0)
            {
                unknownCharacters.Sort();

                foreach (char c in unknownCharacters)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
    }
}