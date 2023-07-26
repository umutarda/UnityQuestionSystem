using UnityEngine;

namespace MultiChoiceQuestion
{
    [CreateAssetMenu(fileName = "MultiChoiceQuestionData", menuName = "ScriptableObjects/MultiChoiceQuestionSO", order = 1)]
    public class MultiChoiceQuestionSO : ScriptableObject
    {
        [Header("Path Definitions")]

        public AssetFolderPath IMAGES_PATH;
        public AssetFolderPath AUDIO_PATH;
        public AssetFolderPath JSON_PATH;

        [Header("Tag Definitions")]
        public LinkPattern IMAGE_TAG;
        public LinkPattern AUDIO_TAG;
        public ConstantPattern CORRECT_ANSWER_TAG;

        [Header("Question JSON Object Variables")]
        public JSONVariablePattern DESCRIPTION_VAR;
        public JSONVariablePattern CHOICES_VAR;

        [Header("JSONs Manifest Definition")]
        public JSONsManifest JSONS_MANIFEST;

    }


}