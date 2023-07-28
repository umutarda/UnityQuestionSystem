using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.IO;


namespace MultiChoiceQuestion 
{
    public class MultiChoiceQuestionUtility : MonoBehaviour
    {
        [SerializeField] private MultiChoiceQuestionSO _CONSTANTS; 
        private static MultiChoiceQuestionUtility Instance;
        public static MultiChoiceQuestionSO CONSTANTS;
        private static JSONObjectPattern _QUESTION_JSON_OBJECT;
        public static JSONObjectPattern QUESTION_JSON_OBJECT => _QUESTION_JSON_OBJECT;
        protected void Awake() 
        {
            if(Instance != null)
            {
                Destroy(gameObject);
            }

            else 
            {
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                Instance = this;
                CONSTANTS = _CONSTANTS;

                
                LoadJSONFileFromName( (string JSONsManifestContent) => 
                {
                    JSONsManifest _JSONsManifest = CONSTANTS.JSONS_MANIFEST;
                    _JSONsManifest.LoadValues(JSONsManifestContent);
                    CONSTANTS.JSONS_MANIFEST = _JSONsManifest;

                } ,CONSTANTS.JSONS_MANIFEST.FILE_NAME);

               _QUESTION_JSON_OBJECT = new JSONObjectPattern (MultiChoiceQuestionUtility.CONSTANTS.DESCRIPTION_VAR,
                                                                                       MultiChoiceQuestionUtility.CONSTANTS.CHOICES_VAR);

            }
               
        }


        public static Questions LoadQuestions (string fileName) 
        {
            Questions questions = null;
            LoadJSONFileFromName((string jsonString) => questions = new Questions(jsonString),fileName);
            return questions;
        }
        public static Questions LoadQuestionsWithKey (string fileKey) 
        {
            return LoadQuestions(CONSTANTS.JSONS_MANIFEST.GetJSONFileName(fileKey));
        }
        public static Questions LoadQuestionsAt (int index = 0) 
        {
            return LoadQuestions(CONSTANTS.JSONS_MANIFEST.GetJSONFileName(index));
        } 
        public static Questions LoadDefaultQuestions() => LoadQuestionsAt();
        public static QuestionIterator IteratorFrom (Questions questions) 
        {
            return new QuestionIterator(questions);
        }

        public static QuestionIterator LoadQuestionsAndGetIteratorFrom (string fileName) 
        {
            return IteratorFrom (LoadQuestions(fileName));
        }

        public static QuestionIterator LoadQuestionsAndGetIteratorFromWithKey (string fileKey) 
        {
            return IteratorFrom (LoadQuestionsWithKey(fileKey));
        }

        public static QuestionIterator LoadQuestionsAndGetIteratorFromWithIndex (int index = 0) 
        {
            return IteratorFrom (LoadQuestionsAt(index));
        }

        public static QuestionIterator LoadDefaultQuestionsAndGetIteratorFrom() => LoadQuestionsAndGetIteratorFromWithIndex();

        private static IEnumerator DownloadFromWeb (string path, DownloadHandler dh, System.Action<DownloadHandler> callback) 
        {
            var uwr = new UnityWebRequest(path,UnityWebRequest.kHttpVerbGET);
            uwr.downloadHandler = dh;
           
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
                Debug.LogError(uwr.error);
            else 
            {
                callback.Invoke(dh);
            }                

        }

        public static void LoadImageFromPath(System.Action<Texture2D> callback, string path) 
        {
            if (path == null) return;
                
            #if UNITY_WEBGL && !UNITY_EDITOR

                DownloadHandlerTexture dhTexture = new DownloadHandlerTexture();
                Instance.StartCoroutine(DownloadFromWeb(path,dhTexture,(DownloadHandler _dhTexture) => 
                {
                    try 
                    {
                        callback.Invoke(((DownloadHandlerTexture)_dhTexture).texture);
                    }

                    catch(System.Exception e) 
                    {
                        Debug.Log(e.Message);
                    }
                }));

            #elif UNITY_EDITOR

                callback.Invoke ((Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)));
                
            #endif

        }

        public static void LoadImageWithName(System.Action<Texture2D> callback, string assetName)
        {
            LoadImageFromPath(callback, GetAssetPathFromName(CONSTANTS.IMAGES_PATH.PATH,assetName));
        }

        public static void LoadImageFromJSONString(System.Action<Texture2D> callback, string JSONString, out int positionInJSON, int index=0) 
        {
            LoadImageFromPath(callback, GetImagePathFromJSON(JSONString, out positionInJSON, index));
        }  

        public static void LoadAudioFromPath(System.Action<AudioClip> callback, string path) 
        {
            if (path == null) return;

            #if UNITY_WEBGL && !UNITY_EDITOR

                DownloadHandlerAudioClip dhAudio = new DownloadHandlerAudioClip(path,AudioType.UNKNOWN);
                Instance.StartCoroutine(DownloadFromWeb(path,dhAudio,(DownloadHandler _dhAudio) => 
                {
                    try 
                    {
                        callback.Invoke(((DownloadHandlerAudioClip)_dhAudio).audioClip);
                    }

                    catch(System.Exception e) 
                    {
                        Debug.Log(e.Message);
                    }
                }));

            #elif UNITY_EDITOR

                callback.Invoke ((AudioClip)AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)));

            #endif

        }

        public static void LoadAudioWithName(System.Action<AudioClip> callback, string assetName) 
        {
            LoadAudioFromPath(callback, GetAssetPathFromName(CONSTANTS.AUDIO_PATH.PATH,assetName));
        }

        public static void LoadAudioFromJSONString(System.Action<AudioClip> callback, string JSONString, out int positionInJSON, int index=0) 
        {
            LoadAudioFromPath(callback, GetAudioPathFromJSON(JSONString, out positionInJSON, index));
        } 

        public static void LoadJSONFileFromPath (System.Action<string> callback, string path) 
        {
            if (path == null) return;

            #if UNITY_WEBGL && !UNITY_EDITOR

                DownloadHandler dh = new DownloadHandler();

                Instance.StartCoroutine(DownloadFromWeb(path,dh,(DownloadHandler _dh) => 
                {
                    try 
                    {
                        callback.Invoke(_dh.text);
                    }

                    catch(System.Exception e) 
                    {
                        Debug.Log(e.Message);
                    }
                }));
            #elif UNITY_EDITOR

                callback.Invoke (((TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset))).text);

            #endif
        }

        public static void LoadJSONFileFromName (System.Action<string> callback, string assetName) 
        {
            LoadJSONFileFromPath(callback, GetAssetPathFromName(CONSTANTS.JSON_PATH.PATH,assetName));
        }

        public static string GetAssetNameFromJSON (string JSONString, IPattern pattern, out int positionInJSON, int index=0) =>
            pattern.GetSymbolFromMatch(JSONString,out positionInJSON,index);

        public static string GetAssetPathFromName (string relativePathToStreamingAssets, string assetName) 
        {
            #if UNITY_EDITOR
                return "Assets/MultiChoiceQuestion Assets/Editor/StreamingAssets/" +relativePathToStreamingAssets+assetName;
            #else
                return Application.streamingAssetsPath+relativePathToStreamingAssets+assetName;
            #endif
        }

        public static string GetImagePathFromJSON (string JSONString, out int positionInJSON, int index=0) 
        {
            string assetName = GetAssetNameFromJSON(JSONString,CONSTANTS.IMAGE_TAG,out positionInJSON,index);
            return (assetName != null) ? GetAssetPathFromName (CONSTANTS.IMAGES_PATH.PATH, assetName) : null;

        }

        public static string GetAudioPathFromJSON (string JSONString, out int positionInJSON, int index=0) 
        {
            string assetName = GetAssetNameFromJSON(JSONString,CONSTANTS.AUDIO_TAG,out positionInJSON,index);
            return (assetName != null) ? GetAssetPathFromName (CONSTANTS.AUDIO_PATH.PATH, assetName) : null;

        }
    
    }
}
