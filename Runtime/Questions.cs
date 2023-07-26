using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MultiChoiceQuestion 
{
    public class Questions 
    {
                                                                                                                                      
        protected string JSONString;
        protected Question[] questions;
        
        public Question[] AllQuestions => questions;
        public int TotalCount => questions.Length;

        public Questions (string _JSONString) 
        {
            JSONString = _JSONString;

            string[] questionJSONStrings = JSONString.DivideArray(MultiChoiceQuestionUtility.QUESTION_JSON_OBJECT);
            questions = new Question [questionJSONStrings.Length];
            for (int i=0; i < questionJSONStrings.Length; i++) 
            {
                questions[i] = new Question(questionJSONStrings[i]);

            }
        }

        public Question QuestionAt (int index) => questions[index];

        public int IndexOf (Question q) 
        {
            for (int i=0; i<questions.Length; i++) 
            {
                if (q.Equals(questions[i]))
                    return i;
            }

            return -1;
        }

    }
}
