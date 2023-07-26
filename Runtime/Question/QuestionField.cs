using UnityEngine;


namespace MultiChoiceQuestion
{
    /// <summary>
    /// <c>QuestionField</c> is the representation of a part in a question. It may contain text, image or audio data. 
    /// </summary>
    public class QuestionField
    {
        
        protected string JSONString;
        
        public QuestionField(string _JSONString) 
        { 
            JSONString = _JSONString;
        }


        /// <summary>
        /// This field contains only the text data related to its field, any reference to an image or audio is removed.
        /// </summary>
        public virtual string Text 
        {   get 
            {
                string val = JSONString;
                val = MultiChoiceQuestionUtility.CONSTANTS.IMAGE_TAG.RemovePattern(val);
                val = MultiChoiceQuestionUtility.CONSTANTS.AUDIO_TAG.RemovePattern(val);

                return val.Trim();
            }
        }

        public Texture2D FirstImage => Image();
        public Sprite FirstImageAsSprite => ImageAsSprite();
        public AudioClip FirstAudio => Audio();


        #region Texture2D Image
        public void LoadImage(System.Action<Texture2D> callback, out int positionInJSON, int index = 0) =>
            MultiChoiceQuestionUtility.LoadImageFromJSONString(callback,JSONString,out positionInJSON,index);

        public void LoadImage(System.Action<Texture2D> callback, int index = 0) => LoadImage(callback, out int _posInJSON, index); 

        public Texture2D Image( out int positionInJSON, int index = 0) 
        {
            Texture2D texture = null;
            LoadImage((Texture2D _texture)=>texture = _texture,out positionInJSON,index);
            return texture;
        }
        public Texture2D Image(int index = 0) => Image( out int positionInJSON, index);

        #endregion
        
        #region Sprite Image
        public void LoadImageAsSprite (System.Action<Sprite> callback, out int positionInJSON, int index = 0) =>
            
            LoadImage
            (
                (Texture2D texture) => 
                {
                    Sprite spriteFromTexture = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    spriteFromTexture.name = texture.name;
                    callback.Invoke(spriteFromTexture);
                }, 
                out positionInJSON, index
            ); 
            
        public void LoadImageAsSprite(System.Action<Sprite> callback, int index = 0) =>  LoadImageAsSprite(callback, out int _posInJSON, index);

        public Sprite ImageAsSprite( out int positionInJSON, int index = 0) 
        {
            Sprite sprite = null;
            LoadImageAsSprite((Sprite _sprite)=>sprite = _sprite,out positionInJSON,index);
            return sprite;
        }
        public Sprite ImageAsSprite(int index = 0) => ImageAsSprite( out int positionInJSON, index);


        #endregion

        #region Audio 
        public void LoadAudio(System.Action<AudioClip> callback, out int positionInJSON, int index = 0) =>  MultiChoiceQuestionUtility.LoadAudioFromJSONString (callback,JSONString,out positionInJSON,index);

        public void LoadAudio(System.Action<AudioClip> callback, int index = 0) =>  LoadAudio(callback,out int _posInJSON,index);

        public AudioClip Audio( out int positionInJSON, int index = 0) 
        {
            AudioClip audio = null;
            LoadAudio((AudioClip _audio)=>audio = _audio,out positionInJSON,index);
            return audio;
        }

        public AudioClip Audio(int index = 0)  => Audio (out int _posInJSON, index);

        #endregion


    }



}
