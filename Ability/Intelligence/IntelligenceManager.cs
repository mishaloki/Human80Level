using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Human80Level.Database;
using Human80Level.Utils;

namespace Human80Level.Ability.Intelligance
{
    public class IntelligenceManager
    {
        private static Question currentQuestion;
        
        public static void ExtractRandomQuestion()
        {
            try
            {
                Random random = new Random();
                int id = random.Next(1, 6);
                currentQuestion = DBHelper.GetQuestionById(id);
            }
            catch (Exception err)
            {
                Logger.Error("ExtractRandomQuestion", err.Message);
            }
        }

        public static string GetQuestion()
        {
            return currentQuestion.QuestionRus;
        }

        public static string GetLink()
        {
            return currentQuestion.Link;
        }

        public static bool IsAnswerCorrect(string answer)
        {
            try
            {
                if (currentQuestion.AnswerRus.Trim().ToLower() == answer.Trim().ToLower())
                {
                    currentQuestion.IsAnswered = true;
                    DBHelper.UpdateQuestion(currentQuestion);
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {
                Logger.Error("IsAnswerCorrect", err.Message);
                return false;
            }

        }

        public static void AddQuestion()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Question question = new Question("q"+i.ToString(), "a"+i.ToString(), "http://google.com");
                    DBHelper.UpdateQuestion(question);
                }
                    
            }
            catch (Exception err)
            {
                Logger.Error("AddQuestion", err.Message);
            }
        }
    }
}
