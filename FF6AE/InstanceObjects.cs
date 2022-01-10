using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFVIEditor
{
    static class InstanceObjects
    {
        public static Font smallFont = null;
        public static Font largeFont = null;
        public static MonsterList monsterList = null;
        public static ItemList itemList = null;
        public static SpellList spellList = null;
        public static ActorList actorList = null;
        public static BattleCommandList battleCommandList = null;
        public static TextBlock text1 = null;
        public static TextBlock text2 = null;

        public static void initializeSmallFont(string fileName, int offset)
        {
            if (smallFont == null)
            {
                smallFont = new Font(fileName, offset);
            }
        }

        public static void initializeLargeFont(string fileName, int offset)
        {
            if (largeFont == null)
            {
                largeFont = new Font(fileName, offset);
            }
        }

        public static void initializeMonsterList(string fileName)
        {
            if (monsterList == null)
            {
                monsterList = new MonsterList(fileName);
            }
        }

        public static void initializeItemList(string fileName)
        {
            if (itemList == null)
            {
                itemList = new ItemList(fileName);
            }
        }

        public static void initializeSpellList(string fileName)
        {
            if (spellList == null)
            {
                spellList = new SpellList(fileName);
            }
        }

        public static void initializeActorList(string fileName)
        {
            if (actorList == null)
            {
                actorList = new ActorList(fileName);
            }
        }

        public static void initializeBattleCommandList(string fileName)
        {
            if (battleCommandList == null)
                battleCommandList = new BattleCommandList(fileName);
        }

        public static void initializeText1(string fileName)
        {
            if (text1 == null)
                text1 = new TextBlock(fileName, 1);
        }

        public static void initializeText2(string fileName)
        {
            if (text2 == null)
                text2 = new TextBlock(fileName, 2);
        }
    }
}
