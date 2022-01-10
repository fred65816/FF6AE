using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFVIEditor
{
    public partial class SpellEditor : Form
    {
        string[] extraEffects;
        string[] extraEffectsEx;
        private bool boolInit;
        private bool boolRepeat;

        public SpellEditor(string fileName)
        {
            this.Hide();
            InitializeComponent();
            boolInit = true;
            boolRepeat = true;
            initializeSpellEditorComponents(fileName);
            tabControlSpell.TabPages.Remove(tabPageSpellAnimation);
            this.Show();
            getCurrentData(0);
        }

        #region INITIALIZATION FUNCTIONS (LISTS CREATION, DATA BINDING)

        private void initializeSpellEditorComponents(string fileName)
        {
            if (InstanceObjects.spellList == null)
                InstanceObjects.initializeSpellList(fileName);

            initializeExtraEffects();
            initializeExtraEffectsExamples();
            fillListView();

            bindData(cmbSpellName, InstanceObjects.spellList, "spellName", "spellNumber");
        }

        private void initializeExtraEffects()
        {
            extraEffects = new string[]
            {
                #region EXTRA EFFECTS DESCRIPTIONS STRINGS

                 /*0x00*/ "Nothing", 
                 /*0x01*/ "Try to steal item", 
                 /*0x02*/ "0x02", 
                 /*0x03*/ "Randomly kills, doesn't work on zombies",
                 /*0x04 to 0x08*/ "0x04", "0x05", "0x06", "0x07", "0x08", 
                 /*0x09*/ "Randomly select graphic animation [Fire to Drain]", 
                 /*0x0A to 0x0F*/ "0x0A", "0x0B", "0x0C", "0x0D", "0x0E", "0x0F",
                 /*0x10*/ "See enemy's HP/MP/LV/Weaknesses",
                 /*0x11*/ "The Earth wall effect",
                 /*0x12*/ "Morph enemy",
                 /*0x13*/ "Makes target jump",
                 /*0x14*/ "0x14",
                 /*0x15*/ "Hit every target except caster; Damage/Heal points are dealt by division of the amount of targets",
                 /*0x16*/ "Remove caster from battle, fully restore target party",
                 /*0x17*/ "???",
                 /*0x18*/ "Party runs away if possible, does not matter who the target is",
                 /*0x19*/ "Does damage = to caster's HP to the caster and enemy",
                 /*0x1A*/ "Does 1000 damage",
                 /*0x1B*/ "Attack/Healing move substract/add HP according to caster's HP",
                 /*0x1C*/ "Causes spell to only target those with Reflect status",
                 /*0x1D*/ "Damages enemies whose levels are multiples of the last digit of your Gil",
                 /*0x1E*/ "Does Steps / Attack power as damage, minutes played / 30 for MP cost to characters",
                 /*0x1F*/ "Halves level of target, rounding up",
                 /*0x20*/ "Kills caster, set HP and MP to 0 and removes him/her from battle",
                 /*0x21*/ "Exchange all status aliment or effect if found",
                 /*0x22*/ "Extra damage if target enemy is the same level as caster",
                 /*0x23*/ "Cancel enemy desperation attack",
                 /*0x24*/ "???",
                 /*0x25*/ "Does not hit floating enemies, ignores unblockable status in spell",
                 /*0x26*/ "Changes weakness to one particular element, all others ineffective, exact opposite of weak element absorbed",
                 /*0x27*/ "Causes caster to run away",
                 /*0x28*/ "Hits with random checked status",
                 /*0x29*/ "Randomly misses target completely, even against Invisible targets",
                 /*0x2A*/ "Lowest target's level * spell Attack power determines total damage",
                 /*0x2B*/ "Toggles Row position of target(s)",
                 /*0x2C*/ "Spell hits 8 times, total damage dealt at spell's end [can't aim, autoaims at enemies]",
                 /*0x2D*/ "Makes target take physical blows for caster",
                 /*0x2E*/ "Paralyze and drain HP from target [Cause Grab]",
                 /*0x2F*/ "Victim can now be chosen by attacks by using aiming method 4D; effect wears off after said aiming method is employed once	",
                 /*0x30*/ "Cannot hit certain enemies",
                 /*0x31*/ "Nulls damage done by a random element",
                 /*0x32*/ "Spell hits 4 seperate times",
                 /*0x33*/ "Removes character from current party if target is a character",
                 /*0x34*/ "Causes Entice, a special ailment that acts like Confuse but can only be cured when either the original caster or the victim dies	",
                 /*0x35*/ "Restores undead target's HP to full",
                 /*0x36*/ "Combined with drain and concern MP, allows one to drain both HP and MP. Drains MP with 1/4 Attack power. Without one or the other, no visual effect and instant damage",
                 /*0x37*/ "Makes target gain Zombie instead of KO at death",
                 /*0x38*/ "Banish target from battle",
                 /*0x39*/ "Banish target from battle, if all allies hit, warp to triangle island",
                 /*0x3A*/ "Caster possesses the victim until the latter dies",
                 /*0x3B*/ "Randomly cause only one of the checked status",
                 /*0x3C*/ "No effect",
                 /*0x3D*/ "Do Maximum HP of caster minus Current HP of caster as damage",
                 /*0x3E*/ "Small HP drain, uncurable",
                 /*0x3F*/ "Ailment uses spell hit rate to determine a hit or miss",
                 /*0x40*/ "Drop Hp to 1",
                 /*0x41 to 0x42*/ "0x41", "0x42",
                 /*0x43*/ "Target gets two turns in a row without interruption, effects only player-controlled character",
                 /*0x44*/ "Lifts Grab from caster's victim",
                 /*0x45*/ "No effect",
                 /*0x46 to 0x49*/ "0x46", "0x47", "0x48", "0x49",
                 /*0x4A*/ "makes Super Ball attack out of spell",
                 /*0x4B to 0x4F*/ "0x4B", "0x4C", "0x4D", "0x4E", "0x4F",
                 /*0x50*/ "User and target dies",
                 /*0x51*/ "0x51",
                 /*0x52*/ "Steal GP",
                 /*0x53*/ "Takes control of enemy",
                 /*0x54*/ "Caster leaps onto target, battle ends, Gau leaves party to learn target's Rage",
                 /*0x55*/ "0x55",
                 /*0x56*/ "Makes opponent weak against one new element",
                 /*0x57*/ "Inflicts Heat status. Automatically misses those invulnerable to instant death",
                 /*0x58 to 0x5F*/ "0x58", "0x59", "0x5A", "0x5B", "0x5C", "0x5D", "0x5E", "0x5F",
                 /*0x60 to 0x67*/ "0x60", "0x61", "0x62", "0x63", "0x64", "0x65", "0x66", "0x67",
                 /*0x68 to 0x6F*/ "0x68", "0x69", "0x6A", "0x6B", "0x6C", "0x6D", "0x6E", "0x6F",
                 /*0x70 to 0x77*/ "0x70", "0x71", "0x72", "0x73", "0x74", "0x75", "0x76", "0x77",
                 /*0x78 to 0x7F*/ "0x78", "0x79", "0x7A", "0x7B", "0x7C", "0x7D", "0x7E", "0x7F",
                 /*0x80 to 0x87*/ "0x80", "0x81", "0x82", "0x83", "0x84", "0x85", "0x86", "0x87",
                 /*0x88 to 0x8F*/ "0x88", "0x89", "0x8A", "0x8B", "0x8C", "0x8D", "0x8E", "0x8F",
                 /*0x90 to 0x97*/ "0x90", "0x91", "0x92", "0x93", "0x94", "0x95", "0x96", "0x97",
                 /*0x98 to 0x9F*/ "0x98", "0x99", "0x9A", "0x9B", "0x9C", "0x9D", "0x9E", "0x9F",
                 /*0xA0 to 0xA7*/ "0xA0", "0xA1", "0xA2", "0xA3", "0xA4", "0xA5", "0xA6", "0xA7",
                 /*0xA8 to 0xAF*/ "0xA8", "0xA9", "0xAA", "0xAB", "0xAC", "0xAD", "0xAE", "0xAF",
                 /*0xB0 to 0xB7*/ "0xB0", "0xB1", "0xB2", "0xB3", "0xB4", "0xB5", "0xB6", "0xB7",
                 /*0xB8 to 0xBF*/ "0xB8", "0xB9", "0xBA", "0xBB", "0xBC", "0xBD", "0xBE", "0xBF",
                 /*0xC0 to 0xC7*/ "0xC0", "0xC1", "0xC2", "0xC3", "0xC4", "0xC5", "0xC6", "0xC7",
                 /*0xC8 to 0xCF*/ "0xC8", "0xC9", "0xCA", "0xCB", "0xCC", "0xCD", "0xCE", "0xCF",
                 /*0xD0 to 0xD7*/ "0xD0", "0xD1", "0xD2", "0xD3", "0xD4", "0xD5", "0xD6", "0xD7",
                 /*0xD8 to 0xDF*/ "0xD8", "0xD9", "0xDA", "0xDB", "0xDC", "0xDD", "0xDE", "0xDF",
                 /*0xE0 to 0xE7*/ "0xE0", "0xE1", "0xE2", "0xE3", "0xE4", "0xE5", "0xE6", "0xE7",
                 /*0xE8 to 0xEF*/ "0xE8", "0xE9", "0xEA", "0xEB", "0xEC", "0xED", "0xEE", "0xEF",
                 /*0xF0 to 0xF7*/ "0xF0", "0xF1", "0xF2", "0xF3", "0xF4", "0xF5", "0xF6", "0xF7",
                 /*0xF8 to 0xFE*/ "0xF8", "0xF9", "0xFA", "0xFB", "0xFC", "0xFD", "0xFE", 
                 /*0xFF*/ "Nothing"

                #endregion
            };
        }

        private void initializeExtraEffectsExamples()
        {
            extraEffectsEx = new string[]
            {
                #region EXTRA EFFECTS EXAMPLES STRINGS

                 /*0x00*/ "Raging Fist",
                 /*0x01*/ "Steal command",
                 /*0x02*/ "",
                 /*0x03*/ "Equip. Death Tarot",
                 /*0x04 to 0x08*/ "", "", "", "", "",
                 /*0x09*/ "???",
                 /*0x0A to 0x0F*/ "", "", "", "", "", "",
                 /*0x10*/ "Libra",
                 /*0x11*/ "Golem",
                 /*0x12*/ "Ragnarok",
                 /*0x13*/ "Quetzalli",
                 /*0x14*/ "",
                 /*0x15*/ "Chakra",
                 /*0x16*/ "Soul Spiral",
                 /*0x17*/ "Tapir",
                 /*0x18*/ "Teleport",
                 /*0x19*/ "Self-Destruct",
                 /*0x1A*/ "1000 Needles",
                 /*0x1B*/ "White Wind",
                 /*0x1C*/ "Reflect ???",
                 /*0x1D*/ "Lv.? Holy",
                 /*0x1E*/ "Traveler",
                 /*0x1F*/ "Dischord",
                 /*0x20*/ "Transfusion",
                 /*0x21*/ "Rippler",
                 /*0x22*/ "Stone",
                 /*0x23*/ "Odin, Raiden, Oblivion, Banish, Banisher, Snare",
                 /*0x24*/ "Crusader",
                 /*0x25*/ "Quake, Landslide, Magnitude 8, Wild Fang",
                 /*0x26*/ "BarrierChange",
                 /*0x27*/ "Flee",
                 /*0x28*/ "Mind Blast",
                 /*0x29*/ "Northern Cross",
                 /*0x2A*/ "Flare Star",
                 /*0x2B*/ "Reverse Polarity",
                 /*0x2C*/ "Launcher",
                 /*0x2D*/ "Overtune",
                 /*0x2E*/ "Grab",
                 /*0x2F*/ "Targeting",
                 /*0x30*/ "Meteor Strike",
                 /*0x31*/ "Force Field",
                 /*0x32*/ "Tempest, Flurry",
                 /*0x33*/ "Humbaba Breath",
                 /*0x34*/ "Entice",
                 /*0x35*/ "Death, enemy Roulette",
                 /*0x36*/ "Dragon",
                 /*0x37*/ "Cloudy Heaven",
                 /*0x38*/ "Snort",
                 /*0x39*/ "Inhale",
                 /*0x3A*/ "Fury",
                 /*0x3B*/ "Diabolic Whistle",
                 /*0x3C*/ "Sky",
                 /*0x3D*/ "Revenge Blast",
                 /*0x3E*/ "Phantasm",
                 /*0x3F*/ "Eclipse",
                 /*0x40*/ "Heartless Angel",
                 /*0x41 to 0x42*/ "", "",
                 /*0x43*/ "Quick",
                 /*0x44*/ "Release",
                 /*0x45*/ "Vanish",
                 /*0x46 to 0x49*/ "", "", "", "",
                 /*0x4A*/ "Super Ball",
                 /*0x4B to 0x4F*/ "", "", "", "", "",
                 /*0x50*/ "User and target dies",
                 /*0x51*/ "",
                 /*0x52*/ "Steal GP",
                 /*0x53*/ "Control",
                 /*0x54*/ "Leap",
                 /*0x55*/ "",
                 /*0x56*/ "Debilitator",
                 /*0x57*/ "Air Anchor",
                 /*0x58 to 0x5F*/ "", "", "", "", "", "", "", "",
                 /*0x60 to 0x6F*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0x70 to 0x7F*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0x80 to 0x8F*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0x90 to 0x9F*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0xA0 to 0xAF*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0xB0 to 0xBF*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0xC0 to 0xCF*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0xD0 to 0xDF*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0xE0 to 0xEF*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                 /*0xF0 to 0xFF*/ "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",

                #endregion
            };
        }

        private void bindData(ComboBox comboBox, Object list, string displayMember, string valueMember)
        {
            comboBox.DataSource = list;

            if (displayMember != null && valueMember != null)
            {
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
            }

            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void fillListView()
        {
            ListViewItem tempItem;
            ListViewItem[] array = new ListViewItem[256];

            ColumnHeader header = new ColumnHeader();
            header.Text = "Description";
            header.Name = "column1";
            header.Width = 380;
            listViewSpellByte10.Columns.Add(header);

            header = new ColumnHeader();
            header.Text = "Example(s)";
            header.Name = "column2";
            header.Width = 120;
            listViewSpellByte10.Columns.Add(header);


            for (int i = 0; i < 256; i++)
            {
                tempItem = new ListViewItem(extraEffects[i]);
                tempItem.SubItems.Add(extraEffectsEx[i]);
                array[i] = tempItem;
            }

            listViewSpellByte10.Items.AddRange(array);
        }

        #endregion

        #region GENERAL FUNCTIONS

        private bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private void ckbCheck(byte b, CheckBox ckb, int bit)
        {
            if (IsBitSet(b, bit))
                ckb.Checked = true;
            else
                ckb.Checked = false;
        }

        #endregion

        #region CLOSE BUTTON

        private void btnItemReturn1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion

        #region SPELL SELECTORS FUNCTIONS

        private void cmbSpellName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                nudSpellNumber.Value = cmbSpellName.SelectedIndex;
                getCurrentData((int)nudSpellNumber.Value);
                boolRepeat = true;
            }
        }

        private void nudSpellNumber_ValueChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                cmbSpellName.SelectedIndex = (int)nudSpellNumber.Value;
                getCurrentData(cmbSpellName.SelectedIndex);
                boolRepeat = true;
            }
        }

        private void getCurrentData(int index)
        {
            boolInit = true;

            #region BYTES 6, 7, 9 & 10 (BASIC STATS & SPECIAL EFFECT)

            nudSpellByte6.Value = (int)InstanceObjects.spellList[index].byte6;
            nudSpellByte7.Value = (int)InstanceObjects.spellList[index].byte7;
            nudSpellByte9.Value = (int)InstanceObjects.spellList[index].byte9;
            listViewSpellByte10.Items[(int)InstanceObjects.spellList[index].byte10].Selected = true;
            listViewSpellByte10.Select();
            listViewSpellByte10.EnsureVisible((int)InstanceObjects.spellList[index].byte10);

            #endregion

            #region BYTE 1 (TARGETING)

            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte1, ckbSpellByte1Bit7, 7);

            #endregion

            #region BYTE 2 (ELEMENTS)

            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte2, ckbSpellByte2Bit7, 7);

            #endregion

            #region BYTE 3 (SPECIAL 1)

            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte3, ckbSpellByte3Bit7, 7);

            #endregion

            #region BYTE 4 (DAMAGE TYPE)

            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte4, ckbSpellByte4Bit7, 7);

            #endregion

            #region BYTE 5 (SPECIAL 2)

            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte5, ckbSpellByte5Bit7, 7);

            #endregion

            #region BYTE 8 (SPECIAL 3)

            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte8, ckbSpellByte8Bit7, 7);

            #endregion

            #region BYTE 11 (STATUS 1)

            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte11, ckbSpellByte11Bit7, 7);

            #endregion

            #region BYTE 12 (STATUS 2)

            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte12, ckbSpellByte12Bit7, 7);

            #endregion

            #region BYTE 13 (STATUS 3)

            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte13, ckbSpellByte13Bit7, 7);

            #endregion

            #region BYTE 14 (STATUS 4)

            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit0, 0);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit1, 1);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit2, 2);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit3, 3);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit4, 4);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit5, 5);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit6, 6);
            ckbCheck(InstanceObjects.spellList[index].byte14, ckbSpellByte14Bit7, 7);

            #endregion

            boolInit = false;
        }

        #endregion

        #region SPELL DATA PAGE

        #region BYTES 6, 7, 9 & 10 (BASIC STATS & SPECIAL EFFECT)

        private void nudSpellByte6_Leave(object sender, EventArgs e)
        {
            InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte6 = (byte)nudSpellByte6.Value;
        }

        private void nudSpellByte7_Leave(object sender, EventArgs e)
        {
            InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte7 = (byte)nudSpellByte7.Value;
        }

        private void nudSpellByte9_Leave(object sender, EventArgs e)
        {
            InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte9 = (byte)nudSpellByte9.Value;
        }

        private void listViewSpellByte10_Leave(object sender, EventArgs e)
        {
            InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte10 = (byte)listViewSpellByte10.SelectedIndices[0];
        }

        #endregion

        #region BYTE 1 (TARGETING)

        private void ckbSpellByte1Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x01;
            }
        }

        private void ckbSpellByte1Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x02;
            }
        }

        private void ckbSpellByte1Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x04;
            }
        }

        private void ckbSpellByte1Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x08;
            }
        }

        private void ckbSpellByte1Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x10;
            }
        }

        private void ckbSpellByte1Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x20;
            }
        }

        private void ckbSpellByte1Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x40;
            }
        }

        private void ckbSpellByte1Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte1Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte1 -= 0x80;
            }
        }

        #endregion

        #region BYTE 2 (ELEMENTS)

        private void ckbSpellByte2Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x01;
            }
        }

        private void ckbSpellByte2Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x02;
            }
        }

        private void ckbSpellByte2Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x04;
            }
        }

        private void ckbSpellByte2Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x08;
            }
        }

        private void ckbSpellByte2Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x10;
            }
        }

        private void ckbSpellByte2Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x20;
            }
        }

        private void ckbSpellByte2Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x40;
            }
        }

        private void ckbSpellByte2Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte2Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte2 -= 0x80;
            }
        }

        #endregion

        #region BYTE 3 (SPECIAL 1)

        private void ckbSpellByte3Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x01;
            }
        }

        private void ckbSpellByte3Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x02;
            }
        }

        private void ckbSpellByte3Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x04;
            }
        }

        private void ckbSpellByte3Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x08;
            }
        }

        private void ckbSpellByte3Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x10;
            }
        }

        private void ckbSpellByte3Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x20;
            }
        }

        private void ckbSpellByte3Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x40;
            }
        }

        private void ckbSpellByte3Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte3Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte3 -= 0x80;
            }
        }

        #endregion

        #region BYTE 4 (DAMAGE TYPE)

        private void ckbSpellByte4Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x01;
            }
        }

        private void ckbSpellByte4Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x02;
            }
        }

        private void ckbSpellByte4Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x04;
            }
        }

        private void ckbSpellByte4Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x08;
            }
        }

        private void ckbSpellByte4Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x10;
            }
        }

        private void ckbSpellByte4Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x20;
            }
        }

        private void ckbSpellByte4Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x40;
            }
        }

        private void ckbSpellByte4Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte4Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte4 -= 0x80;
            }
        }

        #endregion

        #region BYTE 5 (SPECIAL 2)

        private void ckbSpellByte5Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x01;
            }
        }

        private void ckbSpellByte5Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x02;
            }
        }

        private void ckbSpellByte5Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x04;
            }
        }

        private void ckbSpellByte5Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x08;
            }
        }

        private void ckbSpellByte5Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x10;
            }
        }

        private void ckbSpellByte5Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x20;
            }
        }

        private void ckbSpellByte5Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x40;
            }
        }

        private void ckbSpellByte5Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte5Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte5 -= 0x80;
            }
        }

        #endregion

        #region BYTE 8 (SPECIAL 3)

        private void ckbSpellByte8Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x01;
            }
        }

        private void ckbSpellByte8Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x02;
            }
        }

        private void ckbSpellByte8Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x04;
            }
        }

        private void ckbSpellByte8Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x08;
            }
        }

        private void ckbSpellByte8Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x10;
            }
        }

        private void ckbSpellByte8Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x20;
            }
        }

        private void ckbSpellByte8Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x40;
            }
        }

        private void ckbSpellByte8Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte8Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte8 -= 0x80;
            }
        }

        #endregion

        #region BYTE 11 (STATUS 1)

        private void ckbSpellByte11Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x01;
            }
        }

        private void ckbSpellByte11Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x02;
            }
        }

        private void ckbSpellByte11Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x04;
            }
        }

        private void ckbSpellByte11Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x08;
            }
        }

        private void ckbSpellByte11Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x10;
            }
        }

        private void ckbSpellByte11Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x20;
            }
        }

        private void ckbSpellByte11Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x40;
            }
        }

        private void ckbSpellByte11Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte11Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte11 -= 0x80;
            }
        }

        #endregion

        #region BYTE 12 (STATUS 2)

        private void ckbSpellByte12Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x01;
            }
        }

        private void ckbSpellByte12Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x02;
            }
        }

        private void ckbSpellByte12Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x04;
            }
        }

        private void ckbSpellByte12Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x08;
            }
        }

        private void ckbSpellByte12Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x10;
            }
        }

        private void ckbSpellByte12Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x20;
            }
        }

        private void ckbSpellByte12Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x40;
            }
        }

        private void ckbSpellByte12Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte12Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte12 -= 0x80;
            }
        }

        #endregion

        #region BYTE 13 (STATUS 3)

        private void ckbSpellByte13Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x01;
            }
        }

        private void ckbSpellByte13Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x02;
            }
        }

        private void ckbSpellByte13Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x04;
            }
        }

        private void ckbSpellByte13Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x08;
            }
        }

        private void ckbSpellByte13Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x10;
            }
        }

        private void ckbSpellByte13Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x20;
            }
        }

        private void ckbSpellByte13Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x40;
            }
        }

        private void ckbSpellByte13Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte13Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte13 -= 0x80;
            }
        }

        private void ckbSpellByte14Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit0.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x01;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x01;
            }
        }

        #endregion

        #region BYTE 14 (STATUS 4)

        private void ckbSpellByte14Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit1.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x02;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x02;
            }
        }

        private void ckbSpellByte14Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit2.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x04;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x04;
            }
        }

        private void ckbSpellByte14Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit3.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x08;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x08;
            }
        }

        private void ckbSpellByte14Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit4.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x10;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x10;
            }
        }

        private void ckbSpellByte14Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit5.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x20;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x20;
            }
        }

        private void ckbSpellByte14Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit6.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x40;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x40;
            }
        }

        private void ckbSpellByte14Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbSpellByte14Bit7.Checked)
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 += 0x80;
                else
                    InstanceObjects.spellList[cmbSpellName.SelectedIndex].byte14 -= 0x80;
            }
        }

        #endregion

        #endregion
    }
}
