using UnityEngine;

namespace MultiChoiceQuestion 
{
    public class Question
    {   
        protected string JSONString;
        protected QuestionField description;
        protected ChoiceField[] choices;
        protected ChoiceField answer; 

        public QuestionField Description => description;
        public ChoiceField[] Choices => choices;
        public ChoiceField Answer => answer;

        public Question (string _JSONString) 
        {
            JSONString = _JSONString;
            description = new QuestionField(MultiChoiceQuestionUtility.CONSTANTS.DESCRIPTION_VAR.GetSymbolFromMatch(JSONString));
            
            string[] choiceStrings = MultiChoiceQuestionUtility.CONSTANTS.CHOICES_VAR.GetSymbolFromMatch(JSONString).DivideJSONLiteralArray();
            choices = new ChoiceField [choiceStrings.Length];

            for (int i=0; i < choiceStrings.Length; i++) 
            {
                choices[i] = new ChoiceField(choiceStrings[i]);
            }

            UpdateAnswer();

        }

        public Question Copy() 
        {
            return new Question (JSONString);
        }
        internal void MixChoices() 
        {
            if (choices == null) 
                Debug.LogError ("Choices array is null!");

            for (int i=0; i<choices.Length; i++) 
            {
                
                int randPlace =  UnityEngine.Random.Range(i,choices.Length);
                
                ChoiceField temp = choices[i];
                choices[i] = choices[randPlace];
                choices[randPlace] = temp;

            }

            UpdateAnswer();
            

        }

        private void UpdateAnswer() 
        {
            if (choices == null) 
                    Debug.LogError ("Choices array is null!");

                answer = null;
                for (int i=0; i<choices.Length; i++)
                {
                    if (choices[i].IsCorrect) 
                    {
                        if (answer == null)
                            answer = choices[i];
                        else 
                            Debug.LogError ("More than one correct answer!");
                    }
                }

                if (answer == null)
                    Debug.LogError("No correct answer");
        }

        public bool Equals(Question other)
        {
            return JSONString == other.JSONString;
        }
        
    }
}
