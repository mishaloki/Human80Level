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

namespace Human80Level.Ability.Luck
{
    public static class LuckEventManager
    {
        public static ObservableCollection<LuckEventMessage> getEventList()
        {
            ObservableCollection<LuckEventMessage> eventList = new ObservableCollection<LuckEventMessage>();
            eventList.Add(new LuckEventMessage("message 1",DateTime.Now,true));
            eventList.Add(new LuckEventMessage("message 2", DateTime.Now, true));
            eventList.Add(new LuckEventMessage("message 3", DateTime.Now, true));
            eventList.Add(new LuckEventMessage("message 4", DateTime.Now, true));
            eventList.Add(new LuckEventMessage("message 5", DateTime.Now, true));
            eventList.Add(new LuckEventMessage("message 6", DateTime.Now, true));
            eventList.Add(new LuckEventMessage("message 7", DateTime.Now, true));
            return eventList;
        }

        public static void AddEventMessage(LuckEventMessage eventMessage)
        {
            MessageBox.Show("AddEventMessage");
        }

        public static void RemoveEventMessage (LuckEventMessage eventMessage)
        {
            MessageBox.Show("RemoveEventMessage");
        }

        public static void UpdateEventMessage (LuckEventMessage eventMessage)
        {
            MessageBox.Show("UpdateEventMessage");
        }
    }
}
