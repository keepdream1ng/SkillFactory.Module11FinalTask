namespace UtilityBot.Utilities
{
    public static class MultibleIntParser
    {
    /// <summary>
    /// Method returns true if all elements between spaces [or specified separator] are parsed succesfully. Parsed ints are put in out int[] argument.
    /// </summary>
        public static bool TryParse(string input, out int[] numbers, char separator = ' ')
        {
            bool noErrors = true;
            string[] items = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int length = items.Length;
            numbers = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (int.TryParse(items[i], out int num))
                {
                    numbers[i] = num;
                }
                else
                {
                    noErrors = false;
                }
            }
            return noErrors;
        }
    }
}
