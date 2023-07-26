using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MultiChoiceQuestion 
{
    public class QuestionIterator 
    {
        private Question[] questions;
        
        private Queue<Question> currentQueue;
        
        public bool RandomizeQuestions;
        public bool RandomizeChoices;

        public QuestionIterator (Question[] _questions, bool _randomizeQuestions = false, bool _randomizeChoices = false) 
        {
            questions = _questions;
            RandomizeQuestions = _randomizeQuestions;
            RandomizeChoices = _randomizeChoices;

            currentQueue = new Queue<Question>();

            ResetIterator();
        }
        
        public QuestionIterator (Questions _questions, bool _randomizeQuestions = false, bool _randomizeChoices = false) 
            : this(_questions.AllQuestions, _randomizeQuestions, _randomizeChoices) {}

        
        public QuestionIterator (string _JSONString, bool _randomizeQuestions = false, bool _randomizeChoices = false)
            : this((new Questions(_JSONString)).AllQuestions, _randomizeQuestions, _randomizeChoices) {}

        public void ResetIterator (bool _randomizeQuestions, bool _randomizeChoices) 
        {
            RandomizeQuestions = _randomizeQuestions;
            RandomizeChoices = _randomizeChoices;

            Question[] questionsToQueue =  new Question[questions.Length];;
            currentQueue.Clear();

            if (RandomizeQuestions) 
            {

                for (int i=0; i<questions.Length; i++) 
                {
                    int randPlace =  UnityEngine.Random.Range(i,questions.Length);
                    
                    Question temp = questions[i].Copy();
                    questionsToQueue[i] = questions[randPlace].Copy();
                    questionsToQueue[randPlace] = temp;

                }
            }

            else 
            {
               for (int i=0; i<questions.Length; i++)
                     questionsToQueue[i] = questions[i].Copy();
            }  

        

            foreach (Question q in questionsToQueue) 
            {
                 if (RandomizeChoices)
                    q.MixChoices();

                currentQueue.Enqueue(q);
            }
                
                    
        }
        public void ResetIterator() => ResetIterator(RandomizeQuestions,RandomizeChoices);
        public Question NextQuestion => currentQueue.Dequeue();
        public bool HasNextQuestion => currentQueue.Count > 0;
        public int TotalCount => questions.Length;
        public int Index => questions.Length - currentQueue.Count - 1;
    }
}
