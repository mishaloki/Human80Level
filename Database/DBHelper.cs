using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Human80Level.Database
{
    public class DBHelper
    {
        private const string ConnectionString = @"isostore:/ProfileStatistics.sdf";



        public static void CreateDatabase()
        {

            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {

                if (!context.DatabaseExists())
                {

                    // create database if it does not exist

                    context.CreateDatabase();

                }

            }

        }



        public static void DeleteDatabase()
        {

            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {

                if (context.DatabaseExists())
                {

                    // delete database if it exist

                    context.DeleteDatabase();

                }

            }

        }



        public static void AddEvent(Event employee)
        {

            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {

                if (context.DatabaseExists())
                {

                    context.Events.InsertOnSubmit(employee);

                    context.SubmitChanges();

                }

            }

        }

        public static void RemoveEvent(Event employee)
        {

            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {

                if (context.DatabaseExists())
                {
                    context.Events.Attach(employee);
                    context.Events.DeleteOnSubmit(employee);

                    context.SubmitChanges();

                }

            }

        }

        public static ObservableCollection<Event> GetEventList()
        {

            ObservableCollection<Event> employees;

            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {

                employees = new ObservableCollection<Event>((from emp in context.Events select emp).ToList());

            }

            return employees;

        }
    }
}
