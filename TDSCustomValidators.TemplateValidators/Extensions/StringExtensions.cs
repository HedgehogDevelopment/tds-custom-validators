﻿
namespace TDSCustomValidators.TemplateValidators.Extensions
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
    }
}
