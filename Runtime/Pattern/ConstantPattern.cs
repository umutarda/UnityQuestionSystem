using UnityEngine;

namespace MultiChoiceQuestion
{
    [System.Serializable]
    public struct ConstantPattern : IPattern
    {
        public string SYMBOL => null;
        private string _PATTERN;
        public string PATTERN
        {
            get
            {
                if (_PATTERN.Length == 0)
                {
                    _PATTERN = @"[<]\s*" + _KEYWORD + @"\s*[>]";
                }

                return _PATTERN;
            }
        }

        [SerializeField] private string _KEYWORD;
    }

}

