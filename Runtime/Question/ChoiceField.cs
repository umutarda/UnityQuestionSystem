namespace MultiChoiceQuestion 
{
    public class ChoiceField : QuestionField
    {
        public ChoiceField (string _JSONString) : base (_JSONString) {}

        public override string Text =>  MultiChoiceQuestionUtility.CONSTANTS.CORRECT_ANSWER_TAG.RemovePattern(base.Text).Trim();
        public bool IsCorrect => MultiChoiceQuestionUtility.CONSTANTS.CORRECT_ANSWER_TAG.HasPattern(JSONString);

       
        public bool Equals(ChoiceField other)
        {
            return JSONString == other.JSONString;
        }
        

    }
}
