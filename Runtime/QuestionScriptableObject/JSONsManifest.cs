using UnityEngine;

namespace MultiChoiceQuestion
{
    [System.Serializable]
    public struct JSONsManifest 
    {
        [SerializeField] private string _FILE_NAME;
        public string FILE_NAME => _FILE_NAME;

        [SerializeField] private string[] _KEYS;
        public string[] KEYS => _KEYS;
        public int KEY_COUNT => _KEYS.Length;

        private string[] _VALUES;
        public string[] VALUES => _VALUES;
        public string FirstJSONFileName => GetJSONFileName();

        internal void LoadValues (string JSONString) 
        {
            _VALUES = new string [_KEYS.Length];
            for (int i=0; i<_KEYS.Length; i++) 
            {
                _VALUES[i] = new JSONVariablePattern(_KEYS[i]).GetSymbolFromMatch(JSONString);

            }

        }
        public string GetJSONFileName (string key) 
        {
            if (_VALUES != null) 
            {
                for (int i=0; i<_KEYS.Length; i++)
                {
                    
                    if (_KEYS[i].Equals(key))
                        return _VALUES[i];
                }
            }
            

            return null;
            
        }
        public string GetJSONFileName (int index = 0) 
        {
            
            if (_VALUES != null) 
                return _VALUES[index];

            return null;
        }

    }


}