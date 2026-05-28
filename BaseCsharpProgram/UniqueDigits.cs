namespace BaseCsharpProgram
{
    internal class UniqueDigits
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write a positive number value: ");
            
            if(uint.TryParse(Console.ReadLine(), out uint inputValue))
            {
                Console.WriteLine($"Has Unique Digits: {AllDigitsUnique(inputValue)}");
            }
            else
            {
                Console.WriteLine("Invalid positive number value.");
            }
        }

        //
        // -- UniqueDigits using a bitmask approach --
        //
        public static bool AllDigitsUnique(uint value)
        {
            int mask = 0;

            do
            {
                int digit = (int)(value % 10);
                int bit = 1 << digit;

                if ((mask & bit) != 0)
                    return false;

                mask |= bit;
                value /= 10;
            }
            while (value > 0);

            return true;
        }

        /*
         * -- UniqueDigits that uses the List.Contains approach --
         * 
        public static bool AllDigitsUnique(uint value)
        {
            List<int> digits = new();

            do
            {
                int digit = (int)(value % 10);

                if (digits.Contains(digit))
                    return false;

                digits.Add(digit);

                value /= 10;
            }
            while (value > 0);

            return true;
        }
        */

        /*
         * -- UniqueDigits that uses the Distinct() approach --
         * 
        public static bool AllDigitsUnique(uint value)
        {
            string text = value.ToString();

            return text.Length == text.Distinct().Count();
        }
        */
    }
}
