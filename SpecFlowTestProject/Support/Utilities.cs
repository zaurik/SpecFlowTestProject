using System.Reflection;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace SpecFlowTestProject.Support
{
    public static class Utilities
    {
        public static string RemoveWhiteSpacesAndPunctuations(this string value)
        {
            value = string.Concat(value.Where(c => !char.IsWhiteSpace(c)));  // removes all whitespaces
            value = string.Concat(value.Where(c => !char.IsPunctuation(c)));    // removes punctuation marks
            return value;
        }

        public static string GetDescription(this Enum value)
        {
            Type enumType = value.GetType();
            MemberInfo[] memberInfo = enumType.GetMember(value.ToString());

            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if ((attributes != null && attributes.Count() > 0))
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }

            return value.ToString();
        }
    }
}
