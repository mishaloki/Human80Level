using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Human80Level.Database;
using Human80Level.Resources;
using Human80Level.Utils;

namespace Human80Level.Ability.Luck
{
    public static class LuckEventManager
    {
        private const string tileUri = "/Images/Ability/Luck/tile.png";
        
        #region database operations

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
                TileManager.UpdateTile(AppResources.AbListBtnLuck,GetValue(),tileUri);
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
                TileManager.UpdateTile(AppResources.AbListBtnLuck,GetValue(),tileUri);
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
                //todo check if I need this method
                MessageBox.Show("UpdateEventMessage");
            }
            catch (Exception e)
            {
                Logger.Error("UpdateEventMessage", e.Message);
            }            
        }

        #endregion

        #region statistics methods

        public static int GetValue()
        {
            int dif = GetDifference();
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
            int dif = GetDifference();
            int level = 0;
            if (dif < 10)
            {
                level = 0;
            }
            else
            {
                if (dif < 20)
                {
                    level = 1;
                }
                else
                {
                    if (dif < 35)
                    {
                        level = 2;
                    }
                    else
                    {
                        if (dif < 50)
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

        private static int GetDifference()
        {
            try
            {
                ObservableCollection<Event> events = GetEventList();
                int luckEventNumb = (from @event in events where @event.IsLuck select @event).Count();
                int dif = luckEventNumb - (events.Count - luckEventNumb);
                return dif;
            }
            catch (Exception err)
            {
                Logger.Error("GetDifference", err.Message);
                return 0;
            }

        }

        #endregion
    }
}
