using UnityEditor;
using UnityEngine;
using System.IO;

namespace MultiChoiceQuestion
{
    public static class ImportUnityPackage
    {
        [InitializeOnLoadMethod]
        private static void ShowImporter()
        {
            string packageJSONPath = Path.Combine(Application.dataPath, "../Packages/com.yayyapim.mcq/package.json");
            string packageJSONString = File.ReadAllText(packageJSONPath);

            int position;
            if (!bool.Parse(new JSONVariablePattern("importer_showed", false).GetSymbolFromMatch(packageJSONString, out position)))
            {
                ImportMCQPackage();
                File.WriteAllText(packageJSONPath, packageJSONString.Substring(0, position) + "true" + packageJSONString.Substring(position + "false".Length));

            }

        }

        [MenuItem("MultiChoiceQuestion/Import Unity Package")]
        private static void ImportMCQPackage()
        {
            AssetDatabase.ImportPackage("Packages/com.yayyapim.mcq/UnityPackage/MCQ_UnityPackage.unitypackage", true);
        }


    }
}