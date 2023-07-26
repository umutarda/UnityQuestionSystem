using UnityEditor;
using UnityEngine;
using System.IO;

namespace MultiChoiceQuestion
{
    public static class ImportUnityPackage
    {
        private static readonly string ASSETS_IMPORTER_JSON_PATH = Path.Combine(Application.dataPath, "../Packages/com.yayyapim.mcq/assets_importer.json");
        
        [InitializeOnLoadMethod]
        private static void ShowImporter()
        {
            string packageJSONString = File.ReadAllText(ASSETS_IMPORTER_JSON_PATH);

            int position;
            if (!bool.Parse(new JSONVariablePattern("importer_showed", false).GetSymbolFromMatch(packageJSONString, out position)))
            {
                ImportMCQPackage();
                File.WriteAllText(ASSETS_IMPORTER_JSON_PATH, packageJSONString.Substring(0, position) + "true" + packageJSONString.Substring(position + "false".Length));

            }

        }

        [MenuItem("MultiChoiceQuestion/Import Unity Package")]
        private static void ImportMCQPackage()
        {
            AssetDatabase.ImportPackage("Packages/com.yayyapim.mcq/UnityPackage/MCQ_UnityPackage.unitypackage", true);
        }


    }
}