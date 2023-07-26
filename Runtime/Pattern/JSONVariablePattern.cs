using UnityEngine;

namespace MultiChoiceQuestion
{
    [System.Serializable]
    public struct JSONVariablePattern : IPattern
    {
        [SerializeField] private string _NAME;
        [SerializeField] private bool _IS_LITERAL;

        private const string _SYMBOL = "JSON_VAR";
        public string SYMBOL => _SYMBOL;

        private string _PATTERN;
        public string PATTERN
        {
            get
            {
                if (_PATTERN.Length == 0)
                {
                    _PATTERN = @"[""]"+_NAME+@"[""]\s*:\s*" + (_IS_LITERAL ? @"[""'](?<"+ _SYMBOL +@">[^""']*)[""']": @"(?<"+ _SYMBOL +@">[^\s](.*[^\s])?)") + @"\s*";
                }

                return _PATTERN;
            }
        }

        private string _PATTERN_WITHOUT_SYMBOL;
        public string PATTERN_WITHOUT_SYMBOL
        {
            get
            {
                if (_PATTERN_WITHOUT_SYMBOL.Length == 0)
                {
                    _PATTERN_WITHOUT_SYMBOL =  @"[""]"+_NAME+@"[""]\s*:\s*" + (_IS_LITERAL ? @"[""'][^""']*[""']": @"[^\s](.*[^\s])?") + @"\s*";
                }

                return _PATTERN_WITHOUT_SYMBOL;
            }
        }

        public JSONVariablePattern (string _NAME_, bool _IS_LITERAL_ = true) 
        {
            _IS_LITERAL = _IS_LITERAL_;
            _NAME = _NAME_;
            _PATTERN =(_NAME.Length > 0 ? @"[""]"+_NAME+@"[""]\s*:\s*" : "" ) + (_IS_LITERAL ? @"[""'](?<"+ _SYMBOL +@">[^""']*)[""']": @"(?<"+ _SYMBOL +@">[^\s](.*[^\s])?)") + @"\s*";
            _PATTERN_WITHOUT_SYMBOL =  (_NAME.Length > 0 ? @"[""]"+_NAME+@"[""]\s*:\s*" : "" ) + (_IS_LITERAL ? @"[""'][^""']*[""']": @"[^\s](.*[^\s])?") + @"\s*";
        }

        
    }

}

