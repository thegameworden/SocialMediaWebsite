using System.ComponentModel.DataAnnotations;

namespace Worden_SocialMediaSite.CustomValidations
{
    public class InappropriateLanguage:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            
            if(value == null) return true;

            string valString = value.ToString();
            foreach (string word in BannedWords)
            {
                if (valString.Contains(word, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }


        List<string> BannedWords = new List<string>() { "shit", "bad", "ass" };
    }
}
