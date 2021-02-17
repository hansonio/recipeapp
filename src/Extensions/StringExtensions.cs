

namespace Hanson.RecipeApp.Extensions{

    public static class StringExtensions{

        public static string Capitalize(this string s){
            if(string.IsNullOrWhiteSpace(s)){
                return s;
            }

            if(s.Length == 1){
                return  char.ToUpper(s[0]).ToString();
            }

            return char.ToUpper(s[0]) + s[1..];
        }
    }
}