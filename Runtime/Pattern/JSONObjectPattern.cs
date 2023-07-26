using UnityEngine;

namespace MultiChoiceQuestion
{
    [System.Serializable]
    public struct JSONObjectPattern : IPattern
    {
        [SerializeField] private string _NAME;
        public string SYMBOL => null;
        private string _PATTERN;
        public string PATTERN =>  _PATTERN;

        public JSONObjectPattern (string _NAME_, params JSONVariablePattern[] varList) 
        {
            
            _NAME = _NAME_;
            _PATTERN =  _NAME.Length > 0 ? @"[""]"+_NAME+@"[""]\s*:\s*" : "";


            _PATTERN += @"{\s*";
            for (int i = 0; i < varList.Length; i++)
            {
                if (i != 0)
                    _PATTERN += @",\s*";

                _PATTERN += varList[i].PATTERN_WITHOUT_SYMBOL;
            }

            _PATTERN += @"}";


        }

        public JSONObjectPattern (params JSONVariablePattern[] varList): this ("",varList) {}
    }

}

