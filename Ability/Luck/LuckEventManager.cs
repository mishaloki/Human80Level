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
using Human80Level.Utils;

namespace Human80Level.Ability.Luck
{
    public static class LuckEventManager
    {
        public static ObservableCollection<Event> GetEventList()
        {
            try
            {
                ObservableCollection<Event> eventList = DBHelper.GetEventList();
                return eventList;
            }
            catch (Exception e)
            {
                Logger.Error("GetEventList", e.Message);
                return null;
            }
            
        }

        public static void AddEventMessage(Event eventMessage)
        {
            try
            {
                DBHelper.AddEvent(eventMessage);
            }
            catch (Exception e)
            {
                Logger.Error("AddEventMessage", e.Message);
            }
            
        }


        public static void RemoveEventMessage (Event eventMessage)
        {
            try
            {
                DBHelper.RemoveEvent(eventMessage);
            }
            catch (Exception e)
            {
                Logger.Error("RemoveEventMessage", e.Message);
            }
            
        }

        public static void UpdateEventMessage (Event eventMessage)
        {
            try
            {
                MessageBox.Show("UpdateEventMessage");
            }
            catch (Exception e)
            {
                Logger.Error("UpdateEventMessage", e.Message);
            }
            
        }
    }
}
