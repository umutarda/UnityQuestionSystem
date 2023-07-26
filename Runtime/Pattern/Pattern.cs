using UnityEngine;

namespace MultiChoiceQuestion
{

    [System.Serializable]
    public struct Pattern : IPattern
    {
        [SerializeField] private string _SYMBOL;
        public string SYMBOL => _SYMBOL;
        [SerializeField] private string _PATTERN;
        public string PATTERN => _PATTERN;
    }

}

