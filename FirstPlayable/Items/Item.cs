using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    public abstract class Item
    {
        public string name;
        public int value;
        public bool sold;

        internal abstract void Use(Player player);

        //Method foor buying an item
        internal bool Buy(Player player)
        {
            if (player.currentSeeds >= value)
            {
                player.UpdateLiveLog(BuyMessage());
                player.hud.RedrawLiveLog();

                //getting input
                ConsoleKeyInfo input = Console.ReadKey(true);
                string useMessage;

                //yes will buy item
                if (input.Key == ConsoleKey.Y)
                {
                    player.currentSeeds -= value;
                    useMessage = "You bought the " + name + " for " + value + " seeds.";
                    player.UpdateLiveLog(useMessage);
                    return true;
                }
                //no won't buy item
                else if (input.Key == ConsoleKey.N)
                {
                    useMessage = "You chose not to buy the " + name + ".";
                    player.UpdateLiveLog(useMessage);
                    return false;
                }
                //default
                else
                {
                    useMessage = "Invalid input, transaction canelled";
                    return false;
                }
            }
            else
            {
                player.UpdateLiveLog("This " + name + " is too expensive! It costs " + value + " seeds to buy.");
                return false;
            }
        }
        
        public string BuyMessage()
        {
            string message = "Buy " + name + " for " + value + " seeds? \n(Y)es or (N)o";
            return message;
        }

    }
}
