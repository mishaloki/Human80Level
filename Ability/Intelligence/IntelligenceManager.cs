using System;
using System.IO.IsolatedStorage;
using Human80Level.Database;
using Human80Level.Resources;
using Human80Level.Utils;

namespace Human80Level.Ability.Intelligance
{
    public class IntelligenceManager
    {
        #region private fields

        private const string AppSettingAlreadyAnswered = "AlreadyAnswered";

        private const string tileUri = "/Images/Ability/Intelligence/tile.png";

        private static Question currentQuestion;

        #endregion


        #region adding questions to database
        
        public static void AddQuestions()
        {
            //todo implemented correct logic
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

        #endregion

        #region getting question

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
                bool isCorrect = false;
                string correctAnswer = (CultureManager.IsRus())?currentQuestion.AnswerRus:currentQuestion.AnswerEng;
                if (correctAnswer.Trim().ToLower() == answer.Trim().ToLower())
                {
                    currentQuestion.IsAnswered = true;
                    DBHelper.UpdateQuestion(currentQuestion);
                    isCorrect = true;
                }
                TileManager.UpdateTile(AppResources.AbListBtnIntel,GetValue(),tileUri);
                return isCorrect;
            }
            catch (Exception err)
            {
                Logger.Error("IsAnswerCorrect", err.Message);
                return false;
            }
        }

        #endregion

        #region getting/setting if already asked

        public static bool IsAlreadyAsked()
        {
            try
            {
                //todo implemented logic
                IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                if (settings.Contains(AppSettingAlreadyAnswered))
                {
                    if (settings[AppSettingAlreadyAnswered].ToString() == DateTime.Now.ToShortDateString())
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
                if (settings.Contains(AppSettingAlreadyAnswered))
                {
                    settings.Remove(AppSettingAlreadyAnswered);
                }
                settings[AppSettingAlreadyAnswered] = DateTime.Now.ToShortDateString();
                settings.Save();
            }
            catch (Exception err)
            {
                Logger.Error("ExtractProfileFromSettings", err.Message);
            }
        }

        #endregion

        #region statistics methods

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
            int level;
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

        #endregion
    }
}
