using System;
using System.Globalization;
using System.IO.IsolatedStorage;
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

        public static string AlreadyAnsweredSetting = "AlreadyAnswered";
        
        public static void ExtractRandomQuestion()
        {
            try
            {
                currentQuestion = DBHelper.GetRandomQuestion();
            }
            catch (Exception err)
            {
                Logger.Error("ExtractRandomQuestion", err.Message);
            }
        }

        public static string GetQuestion()
        {
            if (CultureManager.IsRus())
            {
                return currentQuestion.QuestionRus;
            }
            return currentQuestion.QuestionEng;
        }

        public static string GetLink()
        {
            return currentQuestion.Link;
        }

        public static bool IsAnswerCorrect(string answer)
        {
            try
            {
                string correctAnswer = (CultureManager.IsRus())?currentQuestion.AnswerRus:currentQuestion.AnswerEng;
                if (correctAnswer.Trim().ToLower() == answer.Trim().ToLower())
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

        public static void AddQuestions()
        {
            try
            {
                if (DBHelper.GetAnswersCount() < 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Question question = new Question("q" + i.ToString(), "a" + i.ToString(), "http://google.com");
                        DBHelper.AddQuestion(question);
                    }
                }                    
            }
            catch (Exception err)
            {
                Logger.Error("AddQuestion", err.Message);
            }
        }

        public static bool IsAlreadyAsked()
        {
            try
            {
                //todo implemented logic
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(AlreadyAnsweredSetting))
                {
                    if (settings[AlreadyAnsweredSetting].ToString() == DateTime.Now.ToShortDateString())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception err)
            {
                Logger.Error("ExtractProfileFromSettings", err.Message);
                return false;
            }
        }

        public static void SetAlreadyAsked()
        {
            try
            {
                //todo implemented logic
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(AlreadyAnsweredSetting))
                {
                    settings.Remove(AlreadyAnsweredSetting);
                }
                settings[AlreadyAnsweredSetting] = DateTime.Now.ToShortDateString();
            }
            catch (Exception err)
            {
                Logger.Error("ExtractProfileFromSettings", err.Message);
            }
        }

        public static int GetValue()
        {
            int dif = GetCorrectAnswersCoutn();
            if (dif < 0)
            {
                dif = 0;
            }
            else
            {
                if (dif > 100)
                {
                    dif = 100;
                }
            }
            return dif;
        }

        public static int GetLevel()
        {
            int dif = GetCorrectAnswersCoutn();
            int level = 0;
            //todo replace
            if (dif < 20)
            {
                level = 0;
            }
            else
            {
                if (dif < 40)
                {
                    level = 1;
                }
                else
                {
                    if (dif < 60)
                    {
                        level = 2;
                    }
                    else
                    {
                        if (dif < 80)
                        {
                            level = 3;
                        }
                        else
                        {
                            level = 4;
                        }
                    }
                }
            }

            return level;
        }

        private static int GetCorrectAnswersCoutn()
        {
            return DBHelper.GetCorrectAnswersCount();
        }

    }
}
