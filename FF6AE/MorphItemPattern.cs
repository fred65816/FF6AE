using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFVIEditor
{
    class MorphItemPattern
    {
        public String[][] morphPatterns { get; set; }

        public MorphItemPattern()
        {
            morphPatterns = new String[32][];

            morphPatterns[0] = new String[4] { "Antidote", "Green Cherry", "Eye Drops", "Gold Needle" };
            morphPatterns[1] = new String[4] { "Tent", "Phoenix Down", "Teleport Stone", "Holy Water" };
            morphPatterns[2] = new String[4] { "Dried Meat", "Dried Meat", "Dried Meat", "Dried Meat" };
            morphPatterns[3] = new String[4] { "Remedy", "Remedy", "Remedy", "Remedy" };
            morphPatterns[4] = new String[4] { "Mythril Blade", "Mythril Helm", "Mythril Mail", "Heavy Shield" };
            morphPatterns[5] = new String[4] { "Golden Spear", "Golden Shield", "Golden Helm", "Golden Armor" };
            morphPatterns[6] = new String[4] { "Crystal Sword", "Crystal Shield", "Crystal Helm", "Crystal Mail" };
            morphPatterns[7] = new String[4] { "Reed Cloak", "Saucer", "Tortoise Shield", "Impartisan" };
            morphPatterns[8] = new String[4] { "Potion", "Potion", "Potion", "Organyx" };
            morphPatterns[9] = new String[4] { "Potion", "Potion", "Potion", "Miracle Shoes" };
            morphPatterns[10] = new String[4] { "Potion", "Potion", "Potion", "Tintinabar" };
            morphPatterns[11] = new String[4] { "Potion", "Potion", "Potion", "Megalixir" };
            morphPatterns[12] = new String[4] { "Potion", "Potion", "Potion", "X-Potion" };
            morphPatterns[13] = new String[4] { "Potion", "Potion", "Potion", "X-Ether" };
            morphPatterns[14] = new String[4] { "Potion", "Potion", "Potion", "Elixir" };
            morphPatterns[15] = new String[4] { "Potion", "Potion", "Potion", "Gauntlet" };
            morphPatterns[16] = new String[4] { "Potion", "Potion", "Potion", "Genji Glove" };
            morphPatterns[17] = new String[4] { "Potion", "Potion", "Potion", "Safety Bit" };
            morphPatterns[18] = new String[4] { "Potion", "Potion", "Potion", "Growth Egg" };
            morphPatterns[19] = new String[4] { "Potion", "Potion", "Potion", "Ribbon" };
            morphPatterns[20] = new String[4] { "Potion", "Potion", "Potion", "Flame Shield" };
            morphPatterns[21] = new String[4] { "Potion", "Potion", "Potion", "Ice Shield" };
            morphPatterns[22] = new String[4] { "Potion", "Potion", "Potion", "Thunder Shield" };
            morphPatterns[23] = new String[4] { "Cursed Ring", "Cursed Ring", "Thornlet", "Lich Ring" };
            morphPatterns[24] = new String[4] { "Angel Ring", "Angel Ring", "Safety Bit", "Guard Bracelet" };
            morphPatterns[25] = new String[4] { "Viper Darts", "Viper Darts", "Assassin's Dagger", "Ichigeki" };
            morphPatterns[26] = new String[4] { "Dagger", "Dagger", "Dagger", "Dagger" };
            morphPatterns[27] = new String[4] { "Dagger", "Dagger", "Dagger", "Dagger" };
            morphPatterns[28] = new String[4] { "Dagger", "Dagger", "Dagger", "Dagger" };
            morphPatterns[29] = new String[4] { "Dagger", "Dagger", "Dagger", "Dagger" };
            morphPatterns[30] = new String[4] { "Dagger", "Dagger", "Dagger", "Dagger" };
            morphPatterns[31] = new String[4] { "Dagger", "Dagger", "Dagger", "Dagger" };
        }
    }
}
