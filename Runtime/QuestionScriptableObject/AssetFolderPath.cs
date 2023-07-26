using UnityEngine;

namespace MultiChoiceQuestion
{
    [System.Serializable]
    public struct AssetFolderPath 
    {
        [SerializeField] private string _FOLDER_NAME;
        public string PATH => "/" + _FOLDER_NAME + "/";
        public string PATH_ABSOLUTE =>
            
            #if UNITY_EDITOR
                "Assets/MultiChoiceQuestion Assets/Editor/StreamingAssets/" + _FOLDER_NAME + "/";
            #else
                Application.streamingAssetsPath + "/" + _FOLDER_NAME + "/";
            #endif
        
        
    }


}