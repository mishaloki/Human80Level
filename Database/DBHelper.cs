using System;
using System.Collections.Generic;
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



        public static void AddEmployee(Event employee)
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



        public static IList<Event> GetEmployees()
        {

            IList<Event> employees;

            using (var context = new ProfileStatisticsDataContext(ConnectionString))
            {

                employees = (from emp in context.Events select emp).ToList();

            }



            return employees;

        }
    }
}
