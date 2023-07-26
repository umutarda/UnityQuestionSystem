using UnityEngine;

namespace MultiChoiceQuestion
{
    [System.Serializable]
    public struct LinkPattern : IPattern
    {
        private const string _SYMBOL = "LINK_VAR";
        public string SYMBOL => _SYMBOL;

        private string _PATTERN;
        public string PATTERN
        {
            get
            {
                if (_PATTERN.Length == 0)
                {
                    _PATTERN = @"[<]\s*" + _KEYWORD + @"\s*=\s*(?<" + _SYMBOL + @">[^""'\s]([^""']*[^""'\s])?)\s*[>]";
                }

                return _PATTERN;
            }
        }
        [SerializeField] private string _KEYWORD;

    }

}

