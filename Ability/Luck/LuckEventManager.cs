using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Human80Level.Ability.Luck
{
    public static class LuckEventManager
    {
        public static ObservableCollection<Event> getEventList()
        {
            //ObservableCollection<Event> eventList = new ObservableCollection<Event>();
            //eventList.Add(new Event("message 1",DateTime.Now,true));
            //eventList.Add(new Event("message 2", DateTime.Now, true));
            //eventList.Add(new Event("message 3", DateTime.Now, true));
            //eventList.Add(new Event("message 4", DateTime.Now, true));
            //eventList.Add(new Event("message 5", DateTime.Now, true));
            //eventList.Add(new Event("message 6", DateTime.Now, true));
            //eventList.Add(new Event("message 7", DateTime.Now, true));
            //return eventList;
            ObservableCollection<Event> eventList = DBHelper.GetEventList();
            return eventList;
        }

        public static void AddEventMessage(Event eventMessage)
        {
            //MessageBox.Show("AddEventMessage");
            DBHelper.AddEvent(eventMessage);
        }


        public static void RemoveEventMessage (Event eventMessage)
        {
            //MessageBox.Show("RemoveEventMessage");
            DBHelper.RemoveEvent(eventMessage);
        }

        public static void UpdateEventMessage (Event eventMessage)
        {
            MessageBox.Show("UpdateEventMessage");
        }
    }
}
