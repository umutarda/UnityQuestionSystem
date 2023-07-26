using UnityEditor;

namespace MultiChoiceQuestion 
{
    public static class ImportUnityPackage 
    {
        [MenuItem("MultiChoiceQuestion/Import Unity Package")]
        private static void ImportMCQPackage()
        {
            AssetDatabase.ImportPackage("Packages/com.yayyapim.mcq/UnityPackage/MCQ_UnityPackage.unitypackage", true);
        }
        
    }
}