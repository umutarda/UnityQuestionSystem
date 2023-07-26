using UnityEngine;
using System.Text.RegularExpressions;

namespace MultiChoiceQuestion
{
    public static class PatternExtension
    {
        private static JSONVariablePattern _LITERAL_WITHOUT_NAME = new JSONVariablePattern("");
        public static JSONVariablePattern LITERAL_WITHOUT_NAME => _LITERAL_WITHOUT_NAME;

        public static string ReplacePattern(this IPattern pattern, string input, string replacement)
        {
            return Regex.Replace(input, pattern.PATTERN, replacement);
        }

        public static string RemovePattern(this IPattern pattern, string input)
        {
            return Regex.Replace(input, pattern.PATTERN, "");
        }


        public static bool HasPattern(this IPattern pattern, string input)
        {
            return Regex.IsMatch(input, pattern.PATTERN);
        }

        public static string Match (this IPattern pattern, string input, out string symbol, out int position, int index=0, bool giveSymbolIndex = false) 
        {
            position = -1;
            symbol = null;

            Match match = Regex.Match(input, pattern.PATTERN);
            int counter = 0;

            if (match.Success && index == 0) 
            {   
                if (pattern.SYMBOL != null)
                    symbol = match.Groups[pattern.SYMBOL].Value;

                position = giveSymbolIndex ? match.Groups[pattern.SYMBOL].Index : match.Index;

                return match.Value;
            }

            else 
            {
                while (match.Success && counter < index)
                {
                    match.NextMatch();
                    counter++;
                }

                if (match.Success) 
                {   
                    if (pattern.SYMBOL != null)
                        symbol =   match.Groups[pattern.SYMBOL].Value;

                    position = giveSymbolIndex ? match.Groups[pattern.SYMBOL].Index : match.Index;
                    return match.Value;
                }

                Debug.LogError("No. " + index + " match of" + pattern.PATTERN + " does not exist!");
                return null;
            }


        }

        public static string Match (this IPattern pattern, string input, out int position, int index=0) =>
            Match ( pattern,  input, out string symbol, out position,  index);

        public static string GetSymbolFromMatch (this IPattern pattern, string input, out int position, int index=0) 
        {
            string symbol;
            Match (pattern, input, out symbol,out position,index,true);

            return symbol;
        }

        public static string GetSymbolFromMatch (this IPattern pattern, string input, int index=0) =>
            GetSymbolFromMatch ( pattern,  input, out int _position, index=0) ;

        public static string[] DivideArray (this string input, IPattern arrayElement, bool stringsAreSymbols = false) 
        {
            MatchCollection elementMatches = Regex.Matches(input, arrayElement.PATTERN);
            string[] elements = new string[elementMatches.Count];
            
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = stringsAreSymbols ? elementMatches[i].Groups[arrayElement.SYMBOL].Value : elementMatches[i].Value;
            }

            return elements;
        }

        public static string[] DivideJSONLiteralArray (this string input) 
        {
            return DivideArray(input,LITERAL_WITHOUT_NAME,true);
        }

    }

}

