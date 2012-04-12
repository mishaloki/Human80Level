using System;
using System.Collections.ObjectModel;
using System.Linq;
using Human80Level.Utils;

namespace Human80Level.Database
{
    public class DBHelper
    {
        private const string ConnectionString = @"isostore:/ProfileStatistics.sdf";

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

        public static void UpdateQuestion(Question question)
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
                Logger.Error("UpdateQuestion", err.Message);
            }

        }

        public static Question GetQuestionById(int id)
        {
            try
            {
                using (var context = new ProfileStatisticsDataContext(ConnectionString))
                {
                    var questions = from question1 in context.Questions select question1;
  
                    Question question = (from q in questions where (q.Id == id) && (q.IsAnswered != true) select q).First();
                    return question;
                }
            }
            catch (Exception err)
            {
                Logger.Error("GetQuestionById", err.Message);
                return null;
            }

        }

    }
}
