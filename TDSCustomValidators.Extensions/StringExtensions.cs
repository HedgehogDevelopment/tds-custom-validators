
namespace TDSCustomValidators.Extensions
{
    public static class StringExtensions
    {
        public static int UpperCaseCharactersCount(this string str)
        {
            int count = 0;

            foreach (char character in str)
            {
                if (char.IsUpper(character))
                {
                    count++;
                }
            }

            return count;
        }

        public static int LowerCaseCharactersCount(this string str)
        {
            int count = 0;

            foreach (char character in str)
            {
                if (char.IsLower(character))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
