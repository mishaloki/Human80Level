using System;
using System.Collections.ObjectModel;
using System.Linq;
using Human80Level.Utils;

namespace Human80Level.Database
{
    public class DBHelper
    {
        private const string ConnectionString = @"isostore:/ProfileStatistics.sdf";

        #region create/delete database

        /// <summary>
        /// Creates database in Isolated storage
        /// </summary>
        public static void CreateDatabase()
        {
            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {
                if (!context.DatabaseExists())
                {
                    context.CreateDatabase();
                }
            }
        }

        /// <summary>
        /// Removes database from Isolated storage
        /// </summary>
        public static void DeleteDatabase()
        {
            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {
                if (context.DatabaseExists())
                {
                    context.DeleteDatabase();
                }
            }
        }

        #endregion

        #region Luck ability methods

        /// <summary>
        /// Adds event to database
        /// </summary>
        /// <param name="eventMessage"></param>
        public static void AddEvent(Event eventMessage)
        {
            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {
                if (context.DatabaseExists())
                {
                    context.Events.InsertOnSubmit(eventMessage);
                    context.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Removes event from database
        /// </summary>
        /// <param name="eventMessage"></param>
        public static void RemoveEvent(Event eventMessage)
        {
            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {
                if (context.DatabaseExists())
                {
                    context.Events.Attach(eventMessage);
                    context.Events.DeleteOnSubmit(eventMessage);

                    context.SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Gets event list from database
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Event> GetEventList()
        {
            try
            {
                ObservableCollection<Event> events;

                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    events = new ObservableCollection<Event>((from emp in context.Events select emp).ToList());
                }
                return events;
            }
            catch (Exception err)
            {
                Logger.Error("GetEventList",err.Message); 
                return null;
            }
        }

        #endregion

        #region Intelligence ability methods

        public static void AddQuestion(Question question)
        {
            try
            {
                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    if (context.DatabaseExists())
                    {
                        context.Questions.InsertOnSubmit(question);
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception err)
            {
                Logger.Error("AddQuestion", err.Message);
            }

        }

        public static void UpdateQuestion(Question question)
        {
            try
            {
                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    if (context.DatabaseExists())
                    {
                        var qToUpdate = (from qu in context.Questions where qu.Id == question.Id select qu).First();
                        qToUpdate.IsAnswered = question.IsAnswered;
                        context.SubmitChanges();
                    }
                }
            }
            catch (Exception err)
            {
                Logger.Error("UpdateQuestion", err.Message);
            }
        }

        public static Question GetRandomQuestion()
        {
            try
            {
                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    var questions = from q in context.Questions where q.IsAnswered != true select q;
                    int count = questions.Count();
                    int index = new Random().Next(count);
                    Question question = questions.Skip(index).FirstOrDefault();
                    return question;
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetQuestionById", err.Message);
                return null;
            }

        }

        public static int GetCorrectAnswersCount()
        {
            try
            {
                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    var questions = from q in context.Questions where q.IsAnswered select q;
                    return questions.Count();
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetCorrectAnswersCount", err.Message);
                return 0;
            }

        }

        public static int GetAnswersCount()
        {
            try
            {
                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    var questions = from q in context.Questions select q;
                    return questions.Count();
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetAnswersCount", err.Message);
                return 0;
            }
        }

        #endregion

    }
}
