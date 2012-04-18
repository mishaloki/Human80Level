using System;
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
using Human80Level.Resources;
using Human80Level.Statistics;
using Human80Level.Utils;
using Microsoft.Phone.Shell;

namespace Human80Level
{
    public class TileManager
    {
        private static readonly string tileTitle = AppResources.LiveTileLevel;

        
        public static void UpdateTile()
        {
            try
            {
                if (ProfileManager.GetProfile() == null)
                {
                    return;
                }
                ShellTile appTile = ShellTile.ActiveTiles.First();
                if (appTile != null)
                {
                    double level = IndicatorManager.GetTotalLevel();
                    StandardTileData newTileData = new StandardTileData
                    {
                        Title = tileTitle,
                        BackgroundImage = new Uri("/Images/tile.png", UriKind.Relative),
                        Count = (int)Math.Round(level)
                    };
                    appTile.Update(newTileData);
                }
            }
            catch (Exception err)
            {
                Logger.Error("UpdateTile", err.Message); 
            }

        }

        public static void UpdateTile(string ability,double level, string image)
        {
            try
            {
            ShellTile appTile = ShellTile.ActiveTiles.First();
            if (appTile != null)
            {
                //double level = IndicatorManager.GetTotalLevel();
                StandardTileData NewTileData = new StandardTileData
                                                   {
                    BackTitle = ability,
                    BackBackgroundImage = new Uri(image, UriKind.Relative),
                    BackContent = Math.Round(level).ToString()
                };
                appTile.Update(NewTileData);

            }
            }
            catch (Exception err)
            {
                Logger.Error("UpdateTile", err.Message);
            }
        }
    }
}
