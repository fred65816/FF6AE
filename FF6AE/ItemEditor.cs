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
    public partial class ItemEditor : Form
    {
        private Spell[] magicList;
        private Spell[] magicList2;
        private string[] extraEffects;
        private string[] itemExtraEffects;
        private string[] evadeAnimations;
        private string[] itemKinds;
        private List<cmbStatistic> vigList;
        private List<cmbStatistic> staList;
        private List<cmbStatistic> speList;
        private List<cmbStatistic> magList;
        private List<cmbStatistic> evaList;
        private List<cmbStatistic> mBlockList;
        private bool boolInit;
        private bool boolRepeat;
        private int previousTabPageIndex;

        public ItemEditor(string fileName)
        {
            this.Hide();
            InitializeComponent();
            boolInit = true;
            boolRepeat = true;
            initializeMonsterEditorComponents(fileName);
            previousTabPageIndex = tabControlItems.SelectedIndex;
            this.Show();
            getCurrentData(0);

        }

        #region ComboBox Class

        private class cmbStatistic
        {
            public int bit { get; set; }            
            public string text { get; set; }          

            public cmbStatistic(int bit, string text)
            {
                this.bit = bit;
                this.text = text;
            }

        }

        #endregion

        #region INITIALIZATION FUNCTIONS (LISTS CREATION, DATA BINDING)

        private void initializeMonsterEditorComponents(string fileName)
        {
            if (InstanceObjects.itemList == null)
                InstanceObjects.initializeItemList(fileName);

            if (InstanceObjects.spellList == null)
                InstanceObjects.initializeSpellList(fileName);

            initializeItemKinds();
            bindData(cmbKind, itemKinds, null, null);

            initializeExtraEffects();
            bindData(cmbByte28high, extraEffects, null, null);

            initializeItemExtraEffects();
            bindData(listBoxByte28, itemExtraEffects, null, null);

            initializeEvadeAnimations();
            bindData(cmbByte28low, evadeAnimations, null, null);

            initializeVigStaList(ref vigList);
            initializeVigStaList(ref staList);
            bindData(cmbByte17low, vigList, "text", "bit");
            bindData(cmbByte18low, staList, "text", "bit");

            initializeSpeMagList(ref speList);
            initializeSpeMagList(ref magList);
            bindData(cmbByte17high, speList, "text", "bit");
            bindData(cmbByte18high, magList, "text", "bit");

            initializeEvaList();
            bindData(cmbByte27low, evaList, "text", "bit");

            initializeMblockList();
            bindData(cmbByte27high, mBlockList, "text", "bit");

            magicList = new Spell[57];

            for(int i = 0; i < magicList.Length; i++)
            {
                magicList[i] = new Spell(i, InstanceObjects.spellList[i].spellName);
            }

            magicList2 = new Spell[57];

            for (int i = 0; i < magicList.Length; i++)
            {
                magicList2[i] = new Spell(i, InstanceObjects.spellList[i].spellName);
            }

            bindData(cmbByte5, magicList, "spellName", "spellNumber");
            bindData(cmbByte19, magicList2, "spellName", "spellNumber");

            bindData(cmbItemName, InstanceObjects.itemList, "itemName", "itemNumber");
            bindData(cmbItemNameAdv, InstanceObjects.itemList, "itemName", "itemNumber");           
        }

        private void initializeVigStaList(ref List<cmbStatistic> list)
        {
            list = new List<cmbStatistic>();

            list.Add(new cmbStatistic(0x0F, " -7"));
            list.Add(new cmbStatistic(0x0E, " -6"));
            list.Add(new cmbStatistic(0x0D, " -5"));
            list.Add(new cmbStatistic(0x0C, " -4"));
            list.Add(new cmbStatistic(0x0B, " -3"));
            list.Add(new cmbStatistic(0x0A, " -2"));
            list.Add(new cmbStatistic(0x09, " -1"));
            list.Add(new cmbStatistic(0x08, " -0"));
            list.Add(new cmbStatistic(0x00, " +0"));
            list.Add(new cmbStatistic(0x01, " +1"));
            list.Add(new cmbStatistic(0x02, " +2"));
            list.Add(new cmbStatistic(0x03, " +3"));
            list.Add(new cmbStatistic(0x04, " +4"));
            list.Add(new cmbStatistic(0x05, " +5"));
            list.Add(new cmbStatistic(0x06, " +6"));
            list.Add(new cmbStatistic(0x07, " +7"));
        }

        private void initializeSpeMagList(ref List<cmbStatistic> list)
        {
            list = new List<cmbStatistic>();

            list.Add(new cmbStatistic(0xF0, " -7"));
            list.Add(new cmbStatistic(0xE0, " -6"));
            list.Add(new cmbStatistic(0xD0, " -5"));
            list.Add(new cmbStatistic(0xC0, " -4"));
            list.Add(new cmbStatistic(0xB0, " -3"));
            list.Add(new cmbStatistic(0xA0, " -2"));
            list.Add(new cmbStatistic(0x90, " -1"));
            list.Add(new cmbStatistic(0x80, " -0"));
            list.Add(new cmbStatistic(0x00, " +0"));
            list.Add(new cmbStatistic(0x10, " +1"));
            list.Add(new cmbStatistic(0x20, " +2"));
            list.Add(new cmbStatistic(0x30, " +3"));
            list.Add(new cmbStatistic(0x40, " +4"));
            list.Add(new cmbStatistic(0x50, " +5"));
            list.Add(new cmbStatistic(0x60, " +6"));
            list.Add(new cmbStatistic(0x70, " +7"));
        }

        private void initializeEvaList()
        {
            evaList = new List<cmbStatistic>();

            evaList.Add(new cmbStatistic(0x0A, " -50%"));
            evaList.Add(new cmbStatistic(0x09, " -40%"));
            evaList.Add(new cmbStatistic(0x08, " -30%"));
            evaList.Add(new cmbStatistic(0x07, " -20%"));
            evaList.Add(new cmbStatistic(0x06, " -10%"));
            evaList.Add(new cmbStatistic(0x00, "  +0%"));
            evaList.Add(new cmbStatistic(0x01, " +10%"));
            evaList.Add(new cmbStatistic(0x02, " +20%"));
            evaList.Add(new cmbStatistic(0x03, " +30%"));
            evaList.Add(new cmbStatistic(0x04, " +40%"));
            evaList.Add(new cmbStatistic(0x05, " +50%"));

        }

        private void initializeMblockList()
        {
            mBlockList = new List<cmbStatistic>();

            mBlockList.Add(new cmbStatistic(0xA0, " -50%"));
            mBlockList.Add(new cmbStatistic(0x90, " -40%"));
            mBlockList.Add(new cmbStatistic(0x80, " -30%"));
            mBlockList.Add(new cmbStatistic(0x70, " -20%"));
            mBlockList.Add(new cmbStatistic(0x60, " -10%"));
            mBlockList.Add(new cmbStatistic(0x00, "  +0%"));
            mBlockList.Add(new cmbStatistic(0x10, " +10%"));
            mBlockList.Add(new cmbStatistic(0x20, " +20%"));
            mBlockList.Add(new cmbStatistic(0x30, " +30%"));
            mBlockList.Add(new cmbStatistic(0x40, " +40%"));
            mBlockList.Add(new cmbStatistic(0x50, " +50%"));

        }

        private void initializeItemKinds()
        {
            itemKinds = new string[]
            {
                // 0x00 to 0x03
                "0x0: Tool", "0x1: Weapon", "0x2: Armor", "0x3: Shield",
                // 0x04 to 0x07
                "0x4: Hat", "0x5: Relic", "0x6: Item", "0x7: ?",
                // 0x08 to 0x0B
                "0x8: ?", "0x9: ?", "0xA: ?", "0xB: ?",
                // 0x0C to 0x0F
                "0xC: ?", "0xD: ?", "0xE: ?", "0xF: ?"
            };
        }

        private void initializeExtraEffects()
        {
            extraEffects = new string[]
            {
                // 0x00 to 0x03
                "0x0: Normal", "0x1: Randomly steal", "0x2: Atma wpn. gfx (high level)", "0x3: Randomly kill",
                // 0x04 to 0x07
                "0x4: 2x damage for humans", "0x5: Drain HP", "0x6: Drain MP", "0x7: Use MP for mortal blow",
                // 0x08 to 0x0B
                "0x8: Randomly throw weapon", "0x9: Dice throw gfx." , "0xA: Stronger on low HP", "0xB: Randomly cast Wind Slash",
                // 0x0C to 0x0F
                "0xC: Curative attributes", "0xD: Randomly slice", "0xE: ?(Ogre Nix break)", "0xF: ?"
            };
        }

        private void initializeItemExtraEffects()
        {
            itemExtraEffects = new string[]
            {
               "0x00: ?", "0x01: Magicite", "0x02: Superball", "0x03: ?", "0x04: ??? (elixir, megalixir)", "0x05: Warp", "0x06: Recruit Gau on Veldt", "0x07: ?",
               "0x08: ?", "0x09: ?", "0x0A: Steal", "0x0B: Control", "0x0C: Leap", "0x0D: ?", "0x0E: Debilitator", "0x0F: Air Anchor",
               "0x10: ?", "0x11: ?", "0x12: ?", "0x13: ?", "0x14: ?", "0x15: ?", "0x16: ?", "0x17: ?",
               "0x18: ?", "0x19: ?", "0x1A: ?", "0x1B: ?", "0x1C: ?", "0x1D: ?", "0x1E: ?", "0x1F: ?",
               "0x20: ?", "0x21: ?", "0x22: ?", "0x23: ?", "0x24: ?", "0x25: ?", "0x26: ?", "0x27: ?",
               "0x28: ?", "0x29: ?", "0x2A: ?", "0x2B: ?", "0x2C: ?", "0x2D: ?", "0x2E: ?", "0x2F: ?",
               "0x30: ?", "0x31: ?", "0x32: ?", "0x33: ?", "0x34: ?", "0x35: ?", "0x36: ?", "0x37: ?",
               "0x38: ?", "0x39: ?", "0x3A: ?", "0x3B: ?", "0x3C: ?", "0x3D: ?", "0x3E: ?", "0x3F: ?",
               "0x40: ?", "0x41: ?", "0x42: ?", "0x43: ?", "0x44: ?", "0x45: ?", "0x46: ?", "0x47: ?",
               "0x48: ?", "0x49: Golem", "0x4A: Ragnarok", "0x4B: Jump", "0x4C: ?", "0x4D: ?", "0x4E: ?", "0x4F: ?",
               "0x50: ?", "0x51: ?", "0x52: ?", "0x53: ?", "0x54: ?", "0x55: ?", "0x56: ?", "0x57: ?",
               "0x58: ?", "0x59: ?", "0x5A: ?", "0x5B: ?", "0x5C: ?", "0x5D: ?", "0x5E: Force Field", "0x5F: Run",
               "0x60: ?", "0x61: ?", "0x62: ?", "0x63: ?", "0x64: ?", "0x65: True Knight", "0x66: HP drain + regen", "0x67: ?",
               "0x68: ?", "0x69: Force Field", "0x6A: ?", "0x6B: Party member(s) leave", "0x6C: Charm", "0x6D: ?", "0x6E: No gfx/sfx", "0x6F: ?",
               "0x70: ?", "0x71: ?", "0x72: ?", "0x73: ?", "0x74: ?", "0x75: ?", "0x76: ?", "0x77: ?",
               "0x78: Left 51 HP", "0x79: Give 1/2 HP/MP", "0x7A: Give 1/4 HP/MP", "0x7B: Cast Quick", "0x7C: ?", "0x7D: ?", "0x7E: ?", "0x7F: ?",
               "0x80: ?", "0x81: ?", "0x82: ?", "0x83: ?", "0x84: ?", "0x85: ?", "0x86: ?", "0x87: ?",
               "0x88: ?", "0x89: ?", "0x8A: ?", "0x8B: ?", "0x8C: ?", "0x8D: ?", "0x8E: ?", "0x8F: ?",
               "0x90: ?", "0x91: ?", "0x92: ?", "0x93: ?", "0x94: ?", "0x95: ?", "0x96: ?", "0x97: ?",
               "0x98: ?", "0x99: ?", "0x9A: ?", "0x9B: ?", "0x9C: ?", "0x9D: ?", "0x9E: ?", "0x9F: ?",
               "0xA0: ?", "0xA1: ?", "0xA2: ?", "0xA3: ?", "0xA4: ?", "0xA5: ?", "0xA6: ?", "0xA7: ?",
               "0xA8: ?", "0xA9: ?", "0xAA: ?", "0xAB: ?", "0xAC: ?", "0xAD: ?", "0xAE: ?", "0xAF: ?",
               "0xB0: ?", "0xB1: ?", "0xB2: ?", "0xB3: ?", "0xB4: ?", "0xB5: ?", "0xB6: ?", "0xB7: ?",
               "0xB8: ?", "0xB9: ?", "0xBA: ?", "0xBB: ?", "0xBC: ?", "0xBD: ?", "0xBE: ?", "0xBF: ?",
               "0xC0: ?", "0xC1: ?", "0xC2: ?", "0xC3: ?", "0xC4: ?", "0xC5: ?", "0xC6: ?", "0xC7: ?",
               "0xC8: ?", "0xC9: ?", "0xCA: ?", "0xCB: ?", "0xCC: ?", "0xCD: ?", "0xCE: ?", "0xCF: ?",
               "0xD0: ?", "0xD1: ?", "0xD2: ?", "0xD3: ?", "0xD4: ?", "0xD5: ?", "0xD6: ?", "0xD7: ?",
               "0xD8: ?", "0xD9: ?", "0xDA: ?", "0xDB: ?", "0xDC: ?", "0xDD: ?", "0xDE: ?", "0xDF: ?",
               "0xE0: ?", "0xE1: ?", "0xE2: ?", "0xE3: ?", "0xE4: ?", "0xE5: ?", "0xE6: ?", "0xE7: ?",
               "0xE8: ?", "0xE9: ?", "0xEA: ?", "0xEB: ?", "0xEC: ?", "0xED: ?", "0xEE: ?", "0xEF: ?",
               "0xF0: ?", "0xF1: ?", "0xF2: ?", "0xF3: ?", "0xF4: ?", "0xF5: ?", "0xF6: ?", "0xF7: ?",
               "0xF8: ?", "0xF9: ?", "0xFA: ?", "0xFB: ?", "0xFC: ?", "0xFD: ?", "0xFE: ?", "0xFF: Nothing",
            };
        }

        private void initializeEvadeAnimations()
        {
            evadeAnimations = new string[]
            {
                // 0x00 to 0x03
                "0x0: Nothing", "0x1: Nothing", "0x2: Nothing", "0x3: Nothing",
                // 0x04 to 0x07
                "0x4: Knife (evasion)", "0x5: Sword (evasion)", "0x6: Buckler (evasion)", "0x7: Red cape (evasion)",
                // 0x08 to 0x0B
                "0x8: Nothing", "0x9: Nothing", "0xA: Buckler (M.evasion)", "0xB: Nothing",
                // 0x0C to 0x0F
                "0xC: Knife (evasion)", "0xD: Sword (evasion)", "0xE: Buckler (evasion & M.eva.)", "0xF: Red cape (evasion)"
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

        private void bindData(ListBox listBox, Object list, string displayMember, string valueMember)
        {
            listBox.DataSource = list;

            if (displayMember != null && valueMember != null)
            {
                listBox.DisplayMember = displayMember;
                listBox.ValueMember = valueMember;
            }
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

        private void showGroupBox(GroupBox gb)
        {
            if (!gb.Visible)
                gb.Visible = true;
        }

        private void hideGroupBox(GroupBox gb)
        {
            if (gb.Visible)
                gb.Visible = false;
        }

        private void setName(CheckBox ckb, string text)
        {
            if (!ckb.Text.Equals(text))
                ckb.Text = text;
        }

        private void setName(GroupBox gb, string text)
        {
            if (!gb.Text.Equals(text))
                gb.Text = text;
        }

        private void setName(Label lbl, string text)
        {
            if (!lbl.Text.Equals(text))
                lbl.Text = text;
        }

        #region VALIDATION FUNCTION

        private int validateTextBox(TextBox textbox)
        {
            int number;

            if (textbox.Text != "")
            {
                bool valid = int.TryParse(textbox.Text, out number);

                if (valid)
                {
                    if (number < 0 || number > 65535)
                    {
                        MessageBox.Show("The number entered must be between 0 and 65535.");
                        textbox.Focus();
                        textbox.Select(0, textbox.Text.Length);
                        return -1;
                    }
                }
                else
                {
                    MessageBox.Show("The number entered is invalid.");
                    textbox.Focus();
                    textbox.Select(0, textbox.Text.Length);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("You cannot leave this field blank!");
                textbox.Focus();
                return -1;
            }

            return number;
        }

        #endregion

        #endregion

        #region ITEM SELECTORS FUNCTIONS

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                nudItemNumber.Value = cmbItemName.SelectedIndex;
                nudItemNumberAdv.Value = cmbItemName.SelectedIndex;
                getCurrentData((int)nudItemNumber.Value);
                boolRepeat = true;
            }

        }

        private void nudItemNumber_ValueChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                cmbItemName.SelectedIndex = (int)nudItemNumber.Value;
                cmbItemNameAdv.SelectedIndex = (int)nudItemNumber.Value;
                nudItemNumberAdv.Value = (int)nudItemNumber.Value;
                getCurrentData(cmbItemName.SelectedIndex);
                boolRepeat = true;
            }
        }

        private void cmbItemNameAdv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                nudItemNumberAdv.Value = cmbItemNameAdv.SelectedIndex;
                nudItemNumber.Value = cmbItemNameAdv.SelectedIndex;
                getCurrentData((int)nudItemNumberAdv.Value);
                boolRepeat = true;
            }
        }

        private void nudItemNumberAdv_ValueChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                cmbItemNameAdv.SelectedIndex = (int)nudItemNumberAdv.Value;
                cmbItemName.SelectedIndex = (int)nudItemNumberAdv.Value;
                nudItemNumber.Value = (int)nudItemNumberAdv.Value; 
                getCurrentData(cmbItemNameAdv.SelectedIndex);
                boolRepeat = true;
            }
        }

        private void tabControlItems_Deselected(object sender, TabControlEventArgs e)
        {
            previousTabPageIndex = tabControlItems.SelectedIndex;
        }

        private void tabControlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((previousTabPageIndex == 0 && tabControlItems.SelectedIndex == 1) ||
               (previousTabPageIndex == 1 && tabControlItems.SelectedIndex == 0))
            {
                getCurrentData((int)nudItemNumber.Value);
            }
        }

        private void getCurrentData(int index)
        {
            boolInit = true;

            int itemKind = (int)(InstanceObjects.itemList[index].byte1 & 0x0F);

            if (tabControlItems.SelectedTab == tabControlItems.TabPages["tabPageItems"])
            {
                #region BYTE 1 (KIND)

                cmbKind.SelectedIndex = itemKind;
                ckbCheck(InstanceObjects.itemList[index].byte1, ckbByte0Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte1, ckbByte0Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte1, ckbByte0Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte1, ckbByte0Bit7, 7);

                #endregion

                #region BYTES 21 & 22 (BASIC STATS)

                textBoxPrice.Text = InstanceObjects.itemList[index].itemPrice.ToString();
                nudByte21.Value = (int)(InstanceObjects.itemList[index].byte22);
                nudByte22.Value = (int)(InstanceObjects.itemList[index].byte21);

                #endregion

                #region BYTE 22 (ITEM STATUS)

                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte22, ckbByte22Bit7, 7);

                #endregion

                #region BYTE 19 (MAGIC EFFECT)

                cmbByte19.SelectedIndex = (int)(InstanceObjects.itemList[index].byte19 & 0x3F);
                ckbCheck(InstanceObjects.itemList[index].byte19, ckbByte19Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte19, ckbByte19Bit7, 7);

                #endregion

                #region BYTES 4 & 5 (MAGIC LEARNT)

                cmbByte5.SelectedIndex = (int)(InstanceObjects.itemList[index].byte5);
                nudByte4.Value = (int)(InstanceObjects.itemList[index].byte4);

                #endregion

                #region BYTE 15 (TARGETING)

                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte15, ckbByte15Bit7, 7);

                #endregion

                #region BYTES 2 & 3 (EQUIPABLE ON)

                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte2, ckbByte2Bit7, 7);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte3, ckbByte3Bit7, 7);

                #endregion

                #region BYTES 17, 18 & 27 (VITAL BONUSES)

                cmbByte17low.SelectedValue = (int)(InstanceObjects.itemList[index].byte17 & 0x0F);
                cmbByte17high.SelectedValue = (int)(InstanceObjects.itemList[index].byte17 & 0xF0);
                cmbByte18low.SelectedValue = (int)(InstanceObjects.itemList[index].byte18 & 0x0F);
                cmbByte18high.SelectedValue = (int)(InstanceObjects.itemList[index].byte18 & 0xF0);
                cmbByte27low.SelectedValue = (int)(InstanceObjects.itemList[index].byte27 & 0x0F);
                cmbByte27high.SelectedValue = (int)(InstanceObjects.itemList[index].byte27 & 0xF0);

                #endregion

                #region BYTE 28 (EXTRA EFFECT & ANIMATION)

                cmbByte28low.SelectedIndex = (int)(InstanceObjects.itemList[index].byte28 & 0x0F);
                cmbByte28high.SelectedIndex = (int)((InstanceObjects.itemList[index].byte28 & 0xF0) / 16);
                listBoxByte28.SelectedIndex = (int)(InstanceObjects.itemList[index].byte28);

                #endregion

                renameElements(itemKind);
                showKindElements(itemKind);
            }
            else if (tabControlItems.SelectedTab == tabControlItems.TabPages["tabPageItemsAdvanced"])
            {
                #region BYTE 25

                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte25, ckbByte25Bit7, 7);

                #endregion

                #region BYTE 24

                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte24, ckbByte24Bit7, 7);

                #endregion

                #region BYTE 23

                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte23, ckbByte23Bit7, 7);

                #endregion

                #region BYTE 16

                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte16, ckbByte16Bit7, 7);

                #endregion

                #region BYTE 20

                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte20, ckbByte20Bit7, 7);

                #endregion

                #region BYTE 9 (STATUS 1)

                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte9, ckbByte9Bit7, 7);

                #endregion

                #region BYTE 26 (STATUS 2)

                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte26, ckbByte26Bit7, 7);

                #endregion

                #region BYTE 11 (SPECIAL 1)

                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte11, ckbByte11Bit7, 7);

                #endregion

                #region BYTE 12 (SPECIAL 2)

                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte12, ckbByte12Bit7, 7);

                #endregion

                #region BYTE 13 (SPECIAL 3)

                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte13, ckbByte13Bit7, 7);

                #endregion

                #region BYTE 14 (SPECIAL 4)

                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte14, ckbByte14Bit7, 7);

                #endregion

                #region BYTE 10 (BONUS CHECKS)

                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte10, ckbByte10Bit7, 7);

                #endregion

                #region BYTE 6 (ITEM EFFECTS)

                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte6, ckbByte6Bit7, 7);

                #endregion

                #region BYTES 7 & 8 (EFFECTS WHEN EQUIPPED)

                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte7, ckbByte7Bit7, 7);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit0, 0);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit1, 1);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit2, 2);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit3, 3);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit4, 4);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit5, 5);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit6, 6);
                ckbCheck(InstanceObjects.itemList[index].byte8, ckbByte8Bit7, 7);

                #endregion

                renameElementsAdvanced(itemKind);
                showKindElementsAdvanced(itemKind);
            }

            boolInit = false;
        }

        private void renameElements(int itemKind)
        {
            #region WEAPON

            if (itemKind == 0x01)
            {
                setName(lblByte22, "Battle power");
                setName(lblByte21, "Hit rate");
            }

            #endregion

            #region ARMOR, SHIELD, HAT, RELIC

            else if (itemKind >= 0x02 && itemKind <= 0x05)
            {
                setName(lblByte22, "Def. points");
                setName(lblByte21, "Magic def.");
            }

            #endregion

            #region ITEM, TOOL

            else if (itemKind == 0x06 || itemKind == 0x00)
            {
                setName(lblByte22, "Give/lift HP/MP");
            }

            #endregion
        }

        private void renameElementsAdvanced(int itemKind)
        {
            #region WEAPON

            if (itemKind == 0x01)
            {
                setName(groupBoxByte16, "Attack");
                setName(ckbByte16Bit0, "Fire");
                setName(ckbByte16Bit1, "Ice");
                setName(ckbByte16Bit2, "Lightning");
                setName(ckbByte16Bit3, "Poison");
                setName(ckbByte16Bit4, "Wind");
                setName(ckbByte16Bit5, "Pearl");
                setName(ckbByte16Bit6, "Earth");
                setName(ckbByte16Bit7, "Water");

                setName(groupBoxByte20, "Properties");
                setName(ckbByte20Bit0, "?");
                setName(ckbByte20Bit1, "Bushido");
                setName(ckbByte20Bit2, "?");
                setName(ckbByte20Bit3, "?");
                setName(ckbByte20Bit4, "?");
                setName(ckbByte20Bit5, "Same dam. back row");
                setName(ckbByte20Bit6, "Two hands");
                setName(ckbByte20Bit7, "Runic");
            }

            #endregion

            #region ARMOR, SHIELD, HAT, RELIC

            else if (itemKind >= 0x02 && itemKind <= 0x05)
            {
                setName(groupBoxByte16, "50% absorb");
                setName(ckbByte16Bit0, "Fire");
                setName(ckbByte16Bit1, "Ice");
                setName(ckbByte16Bit2, "Lightning");
                setName(ckbByte16Bit3, "Poison");
                setName(ckbByte16Bit4, "Wind");
                setName(ckbByte16Bit5, "Pearl");
                setName(ckbByte16Bit6, "Earth");
                setName(ckbByte16Bit7, "Water");

                setName(groupBoxByte25, "Weak");
                setName(ckbByte25Bit0, "Fire");
                setName(ckbByte25Bit1, "Ice");
                setName(ckbByte25Bit2, "Lightning");
                setName(ckbByte25Bit3, "Poison");
                setName(ckbByte25Bit4, "Wind");
                setName(ckbByte25Bit5, "Pearl");
                setName(ckbByte25Bit6, "Earth");
                setName(ckbByte25Bit7, "Water");

                setName(groupBoxByte24, "No effect");
                setName(ckbByte24Bit0, "Fire");
                setName(ckbByte24Bit1, "Ice");
                setName(ckbByte24Bit2, "Lightning");
                setName(ckbByte24Bit3, "Poison");
                setName(ckbByte24Bit4, "Wind");
                setName(ckbByte24Bit5, "Pearl");
                setName(ckbByte24Bit6, "Earth");
                setName(ckbByte24Bit7, "Water");

                setName(groupBoxByte23, "HP absorb");
                setName(ckbByte23Bit0, "Fire");
                setName(ckbByte23Bit1, "Ice");
                setName(ckbByte23Bit2, "Lightning");
                setName(ckbByte23Bit3, "Poison");
                setName(ckbByte23Bit4, "Wind");
                setName(ckbByte23Bit5, "Pearl");
                setName(ckbByte23Bit6, "Earth");
                setName(ckbByte23Bit7, "Water");
            }

            #endregion

            #region ITEM

            else if (itemKind == 0x06)
            {
                setName(groupBoxByte16, "Sp.status 2");
                setName(ckbByte16Bit0, "?");
                setName(ckbByte16Bit1, "?");
                setName(ckbByte16Bit2, "?");
                setName(ckbByte16Bit3, "?");
                setName(ckbByte16Bit4, "?");
                setName(ckbByte16Bit5, "?");
                setName(ckbByte16Bit6, "?");
                setName(ckbByte16Bit7, "?");

                setName(groupBoxByte25, "Sp.status 1");
                setName(ckbByte25Bit0, "Rage");
                setName(ckbByte25Bit1, "Freeze");
                setName(ckbByte25Bit2, "Reraise");
                setName(ckbByte25Bit3, "Morph");
                setName(ckbByte25Bit4, "Chant!");
                setName(ckbByte25Bit5, "Invisible");
                setName(ckbByte25Bit6, "Dog block");
                setName(ckbByte25Bit7, "Float");

                setName(groupBoxByte24, "Sp.status 3");
                setName(ckbByte24Bit0, "Dance");
                setName(ckbByte24Bit1, "Regen");
                setName(ckbByte24Bit2, "Slow");
                setName(ckbByte24Bit3, "Haste");
                setName(ckbByte24Bit4, "Stop");
                setName(ckbByte24Bit5, "Shell");
                setName(ckbByte24Bit6, "Protect");
                setName(ckbByte24Bit7, "Reflect");

                setName(groupBoxByte23, "Btl. status");
                setName(ckbByte23Bit0, "Doom");
                setName(ckbByte23Bit1, "Critical");
                setName(ckbByte23Bit2, "Image");
                setName(ckbByte23Bit3, "Silence");
                setName(ckbByte23Bit4, "Berserk");
                setName(ckbByte23Bit5, "Confuse");
                setName(ckbByte23Bit6, "Sap");
                setName(ckbByte23Bit7, "Sleep");

                setName(groupBoxByte20, "Attributes");
                setName(ckbByte20Bit0, "?");
                setName(ckbByte20Bit1, "Reverse dam. on undead");
                setName(ckbByte20Bit2, "?");
                setName(ckbByte20Bit3, "Affects HP");
                setName(ckbByte20Bit4, "Affects MP");
                setName(ckbByte20Bit5, "Remove status");
                setName(ckbByte20Bit6, "???");
                setName(ckbByte20Bit7, "Max out");
            }

            #endregion

            #region TOOL

            else if (itemKind == 0x00)
            {
                setName(groupBoxByte16, "Sp.status 2");
                setName(ckbByte16Bit0, "?");
                setName(ckbByte16Bit1, "?");
                setName(ckbByte16Bit2, "?");
                setName(ckbByte16Bit3, "?");
                setName(ckbByte16Bit4, "?");
                setName(ckbByte16Bit5, "?");
                setName(ckbByte16Bit6, "?");
                setName(ckbByte16Bit7, "Curative");

                setName(groupBoxByte25, "Sp.status 1");
                setName(ckbByte25Bit0, "?");
                setName(ckbByte25Bit1, "?");
                setName(ckbByte25Bit2, "?");
                setName(ckbByte25Bit3, "?");
                setName(ckbByte25Bit4, "???");
                setName(ckbByte25Bit5, "?");
                setName(ckbByte25Bit6, "?");
                setName(ckbByte25Bit7, "?");

                setName(groupBoxByte24, "Sp.status 3");
                setName(ckbByte24Bit0, "?");
                setName(ckbByte24Bit1, "?");
                setName(ckbByte24Bit2, "?");
                setName(ckbByte24Bit3, "?");
                setName(ckbByte24Bit4, "?");
                setName(ckbByte24Bit5, "?");
                setName(ckbByte24Bit6, "?");
                setName(ckbByte24Bit7, "???");

                setName(groupBoxByte23, "Sp.status 4");
                setName(ckbByte23Bit0, "?");
                setName(ckbByte23Bit1, "?");
                setName(ckbByte23Bit2, "???");
                setName(ckbByte23Bit3, "?");
                setName(ckbByte23Bit4, "?");
                setName(ckbByte23Bit5, "?");
                setName(ckbByte23Bit6, "?");
                setName(ckbByte23Bit7, "?");

                setName(groupBoxByte20, "Sp.status 5");
                setName(ckbByte20Bit0, "?");
                setName(ckbByte20Bit1, "?");
                setName(ckbByte20Bit2, "?");
                setName(ckbByte20Bit3, "?");
                setName(ckbByte20Bit4, "?");
                setName(ckbByte20Bit5, "???");
                setName(ckbByte20Bit6, "?");
                setName(ckbByte20Bit7, "?");
            }

            #endregion
        }

        private void showKindElements(int itemKind)
        {
            #region WEAPON, ARMOR, SHIELD, HAT, RELIC

            if (itemKind >= 0x01 && itemKind <= 0x05)
            {
                lblByte21.Show();
                nudByte21.Show();
                hideGroupBox(groupBoxByte22);
                showGroupBox(groupBoxByte19);
                showGroupBox(groupBoxByte4_5);
                showGroupBox(groupBoxEquipable);
                showGroupBox(groupBoxVitalBonuses);
                showGroupBox(groupBoxExtraEffect);
                showGroupBox(groupBoxEvadeAnimation);
                hideGroupBox(groupBoxExtraEffects);
            }

            #endregion

            #region ITEM

            else if (itemKind == 0x06)
            {
                lblByte21.Hide();
                nudByte21.Hide();
                showGroupBox(groupBoxByte22);
                hideGroupBox(groupBoxByte19);
                hideGroupBox(groupBoxByte4_5);
                hideGroupBox(groupBoxEquipable);
                hideGroupBox(groupBoxVitalBonuses);
                hideGroupBox(groupBoxExtraEffect);
                hideGroupBox(groupBoxEvadeAnimation);
                showGroupBox(groupBoxExtraEffects);
            }

            #endregion

            #region TOOL

            else if (itemKind == 0x00)
            {
                lblByte21.Show();
                nudByte21.Show();
                hideGroupBox(groupBoxByte22);
                hideGroupBox(groupBoxByte19);
                hideGroupBox(groupBoxByte4_5);
                hideGroupBox(groupBoxEquipable);
                hideGroupBox(groupBoxVitalBonuses);
                hideGroupBox(groupBoxExtraEffect);
                hideGroupBox(groupBoxEvadeAnimation);
                hideGroupBox(groupBoxExtraEffects);
            }

            #endregion

            #region 0x07 to 0x0F

            else if (itemKind >= 0x07 && itemKind <= 0x0F)
            {
                lblByte21.Show();
                nudByte21.Show();
                showGroupBox(groupBoxByte22);
                showGroupBox(groupBoxByte19);
                showGroupBox(groupBoxByte4_5);
                showGroupBox(groupBoxEquipable);
                showGroupBox(groupBoxVitalBonuses);
                showGroupBox(groupBoxExtraEffect);
                showGroupBox(groupBoxEvadeAnimation);
                showGroupBox(groupBoxExtraEffects);
            }

            #endregion
        }

        private void showKindElementsAdvanced(int itemKind)
        {
            #region WEAPON

            if (itemKind == 0x01)
            {
                hideGroupBox(groupBoxByte25);
                hideGroupBox(groupBoxByte24);
                hideGroupBox(groupBoxByte23);
                showGroupBox(groupBoxByte16);
                showGroupBox(groupBoxByte20);
                showGroupBox(groupBoxByte9);
                showGroupBox(groupBoxByte26);
                showGroupBox(groupBoxByte11);
                showGroupBox(groupBoxByte12);
                showGroupBox(groupBoxByte13);
                showGroupBox(groupBoxByte14);
                showGroupBox(groupBoxByte10);
                showGroupBox(groupBoxByte6);
                showGroupBox(groupBoxByte7_8);
            }

            #endregion

            #region ARMOR, SHIELD, HAT, RELIC

            else if (itemKind >= 0x02 && itemKind <= 0x05)
            {
                showGroupBox(groupBoxByte25);
                showGroupBox(groupBoxByte24);
                showGroupBox(groupBoxByte23);
                showGroupBox(groupBoxByte16);
                hideGroupBox(groupBoxByte20);
                showGroupBox(groupBoxByte9);
                showGroupBox(groupBoxByte26);
                showGroupBox(groupBoxByte11);
                showGroupBox(groupBoxByte12);
                showGroupBox(groupBoxByte13);
                showGroupBox(groupBoxByte14);
                showGroupBox(groupBoxByte10);
                showGroupBox(groupBoxByte6);
                showGroupBox(groupBoxByte7_8);
            }

            #endregion

            #region ITEM, TOOL

            else if (itemKind == 0x06 || itemKind == 0x00)
            {
                showGroupBox(groupBoxByte25);
                showGroupBox(groupBoxByte24);
                showGroupBox(groupBoxByte23);
                showGroupBox(groupBoxByte16);
                showGroupBox(groupBoxByte20);
                hideGroupBox(groupBoxByte9);
                hideGroupBox(groupBoxByte26);
                hideGroupBox(groupBoxByte11);
                hideGroupBox(groupBoxByte12);
                hideGroupBox(groupBoxByte13);
                hideGroupBox(groupBoxByte14);
                hideGroupBox(groupBoxByte10);
                hideGroupBox(groupBoxByte6);
                hideGroupBox(groupBoxByte7_8);
            }

            #endregion
        }

        #endregion

        #region CLOSE BUTTONS

        private void btnItemReturn1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnItemReturn2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion

        #region ITEM PAGE

        #region PRICE, BYTE 22 & 21

        private void textBoxPrice_Leave(object sender, EventArgs e)
        {
            int result = validateTextBox(textBoxPrice);

            if (result != -1)
                InstanceObjects.itemList[cmbItemName.SelectedIndex].itemPrice = result;
        }

        private void nudByte22_Leave(object sender, EventArgs e)
        {
            InstanceObjects.itemList[cmbItemName.SelectedIndex].byte21 = (byte)nudByte22.Value;
        }

        private void nudByte21_Leave(object sender, EventArgs e)
        {
            InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 = (byte)nudByte21.Value;
        }

        #endregion

        #region BYTE 1

        private void cmbKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 & 0xF0);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 += (byte)(cmbKind.SelectedIndex);
                renameElements(cmbKind.SelectedIndex);
                showKindElements(cmbKind.SelectedIndex);
            }
        }

        private void ckbByte0Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte0Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 -= 0x10;
            }
        }

        private void ckbByte0Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte0Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 -= 0x20;
            }
        }

        private void ckbByte0Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte0Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 -= 0x40;
            }
        }

        private void ckbByte0Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte0Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte1 -= 0x80;
            }
        }

        #endregion

        #region BYTE 19

        private void cmbByte19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 & 0xC0);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 += (byte)cmbByte19.SelectedIndex;
            }
        }

        private void ckbByte19Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte19Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 -= 0x40;
            }
        }

        private void ckbByte19Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte19Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte19 -= 0x80;
            }
        }

        #endregion

        #region BYTE 5 & 4

        private void cmbByte5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte5 = (byte)cmbByte5.SelectedIndex;
            }
        }

        private void nudByte4_Leave(object sender, EventArgs e)
        {
            InstanceObjects.itemList[cmbItemName.SelectedIndex].byte4 = (byte)nudByte4.Value;
        }

        #endregion

        #region BYTE 15

        private void ckbByte15Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x01;
            }
        }

        private void ckbByte15Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x02;
            }
        }

        private void ckbByte15Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x04;
            }
        }

        private void ckbByte15Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x08;
            }
        }

        private void ckbByte15Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x10;
            }
        }

        private void ckbByte15Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x20;
            }
        }

        private void ckbByte15Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x40;
            }
        }

        private void ckbByte15Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte15Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte15 -= 0x80;
            }
        }

        #endregion

        #region BYTE 2

        private void ckbByte2Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x01;
            }
        }

        private void ckbByte2Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x02;
            }
        }

        private void ckbByte2Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x04;
            }
        }

        private void ckbByte2Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x08;
            }
        }

        private void ckbByte2Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x10;
            }
        }

        private void ckbByte2Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x20;
            }
        }

        private void ckbByte2Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x40;
            }
        }

        private void ckbByte2Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte2Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte2 -= 0x80;
            }
        }

        #endregion

        #region BYTE 3

        private void ckbByte3Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x01;
            }
        }

        private void ckbByte3Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x02;
            }
        }

        private void ckbByte3Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x04;
            }
        }

        private void ckbByte3Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x08;
            }
        }

        private void ckbByte3Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x10;
            }
        }

        private void ckbByte3Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x20;
            }
        }

        private void ckbByte3Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x40;
            }
        }

        private void ckbByte3Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte3Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte3 -= 0x80;
            }
        }

        #endregion

        #region BYTE 17, 18, 27 & 28

        private void cmbByte17low_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte17 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte17 & 0xF0);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte17 += (byte)((int)cmbByte17low.SelectedValue);
            }
        }

        private void cmbByte17high_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte17 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte17 & 0x0F);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte17 += (byte)((int)cmbByte17high.SelectedValue);
            }
        }

        private void cmbByte18low_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte18 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte18 & 0xF0);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte18 += (byte)((int)cmbByte18low.SelectedValue);
            }
        }

        private void cmbByte18high_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte18 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte18 & 0x0F);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte18 += (byte)((int)cmbByte18high.SelectedValue);
            }
        }

        private void cmbByte27low_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte27 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte27 & 0xF0);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte27 += (byte)((int)cmbByte27low.SelectedValue);
            }
        }

        private void cmbByte27high_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte27 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte27 & 0x0F);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte27 += (byte)((int)cmbByte27high.SelectedValue);
            }
        }

        private void cmbByte28low_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 & 0xF0);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 += (byte)cmbByte28low.SelectedIndex;
            }
        }

        private void cmbByte28high_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 = (byte)(InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 & 0x0F);
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 += (byte)(cmbByte28high.SelectedIndex * 0x10);
            }
        }

        private void listBoxByte28_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.itemList[cmbItemName.SelectedIndex].byte28 = (byte)listBoxByte28.SelectedIndex;
            }
        }

        #endregion

        #region BYTE 22

        private void ckbByte22Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x01;
            }
        }

        private void ckbByte22Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x02;
            }
        }

        private void ckbByte22Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x04;
            }
        }

        private void ckbByte22Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x08;
            }
        }

        private void ckbByte22Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x10;
            }
        }

        private void ckbByte22Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x20;
            }
        }

        private void ckbByte22Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x40;
            }
        }

        private void ckbByte22Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte22Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte22 -= 0x80;
            }
        }

        #endregion

        #endregion

        #region ITEM ADVANCED PAGE

        #region BYTE 25

        private void ckbByte25Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x01;
            }
        }

        private void ckbByte25Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x02;
            }
        }

        private void ckbByte25Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x04;
            }
        }

        private void ckbByte25Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x08;
            }
        }

        private void ckbByte25Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x10;
            }
        }

        private void ckbByte25Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x20;
            }
        }

        private void ckbByte25Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x40;
            }
        }

        private void ckbByte25Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte25Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte25 -= 0x80;
            }
        }

        #endregion

        #region BYTE 24

        private void ckbByte24Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x01;
            }
        }

        private void ckbByte24Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x02;
            }
        }

        private void ckbByte24Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x04;
            }
        }

        private void ckbByte24Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x08;
            }
        }

        private void ckbByte24Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x10;
            }
        }

        private void ckbByte24Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x20;
            }
        }

        private void ckbByte24Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x40;
            }
        }

        private void ckbByte24Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte24Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte24 -= 0x80;
            }
        }

        #endregion

        #region BYTE 23

        private void ckbByte23Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x01;
            }
        }

        private void ckbByte23Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x02;
            }
        }

        private void ckbByte23Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x04;
            }
        }

        private void ckbByte23Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x08;
            }
        }

        private void ckbByte23Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x10;
            }
        }

        private void ckbByte23Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x20;
            }
        }

        private void ckbByte23Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x40;
            }
        }

        private void ckbByte23Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte23Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte23 -= 0x80;
            }
        }

        #endregion

        #region BYTE 16

        private void ckbByte16Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x01;
            }
        }

        private void ckbByte16Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x02;
            }
        }

        private void ckbByte16Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x04;
            }
        }

        private void ckbByte16Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x08;
            }
        }

        private void ckbByte16Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x10;
            }
        }

        private void ckbByte16Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x20;
            }
        }

        private void ckbByte16Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x40;
            }
        }

        private void ckbByte16Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte16Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte16 -= 0x80;
            }
        }

        #endregion

        #region BYTE 20

        private void ckbByte20Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x01;
            }
        }

        private void ckbByte20Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x02;
            }
        }

        private void ckbByte20Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x04;
            }
        }

        private void ckbByte20Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x08;
            }
        }

        private void ckbByte20Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x10;
            }
        }

        private void ckbByte20Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x20;
            }
        }

        private void ckbByte20Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x40;
            }
        }

        private void ckbByte20Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte20Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte20 -= 0x80;
            }
        }

        #endregion

        #region BYTE 9

        private void ckbByte9Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x01;
            }
        }

        private void ckbByte9Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x02;
            }
        }

        private void ckbByte9Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x04;
            }
        }

        private void ckbByte9Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x08;
            }
        }

        private void ckbByte9Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x10;
            }
        }

        private void ckbByte9Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x20;
            }
        }

        private void ckbByte9Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x40;
            }
        }

        private void ckbByte9Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte9Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte9 -= 0x80;
            }
        }

        #endregion

        #region BYTE 26

        private void ckbByte26Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x01;
            }
        }

        private void ckbByte26Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x02;
            }
        }

        private void ckbByte26Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x04;
            }
        }

        private void ckbByte26Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x08;
            }
        }

        private void ckbByte26Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x10;
            }
        }

        private void ckbByte26Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x20;
            }
        }

        private void ckbByte26Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x40;
            }
        }

        private void ckbByte26Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte26Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte26 -= 0x80;
            }
        }

        #endregion

        #region Byte 11
        
        private void ckbByte11Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x01;
            }
        }

        private void ckbByte11Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x02;
            }
        }

        private void ckbByte11Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x04;
            }
        }

        private void ckbByte11Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x08;
            }
        }

        private void ckbByte11Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x10;
            }
        }

        private void ckbByte11Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x20;
            }
        }

        private void ckbByte11Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x40;
            }
        }

        private void ckbByte11Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte11Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte11 -= 0x80;
            }
        }

        #endregion

        #region BYTE 12

        private void ckbByte12Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x01;
            }
        }

        private void ckbByte12Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x02;
            }
        }

        private void ckbByte12Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x04;
            }
        }

        private void ckbByte12Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x08;
            }
        }

        private void ckbByte12Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x10;
            }
        }

        private void ckbByte12Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x20;
            }
        }

        private void ckbByte12Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x40;
            }
        }

        private void ckbByte12Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte12Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte12 -= 0x80;
            }
        }

        #endregion

        #region BYTE 13

        private void ckbByte13Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x01;
            }
        }

        private void ckbByte13Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x02;
            }
        }

        private void ckbByte13Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x04;
            }
        }

        private void ckbByte13Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x08;
            }
        }

        private void ckbByte13Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x10;
            }
        }

        private void ckbByte13Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x20;
            }
        }

        private void ckbByte13Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x40;
            }
        }

        private void ckbByte13Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte13Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte13 -= 0x80;
            }
        }

        #endregion

        #region BYTE 14

        private void ckbByte14Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x01;
            }
        }

        private void ckbByte14Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x02;
            }
        }

        private void ckbByte14Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x04;
            }
        }

        private void ckbByte14Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x08;
            }
        }

        private void ckbByte14Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x10;
            }
        }

        private void ckbByte14Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x20;
            }
        }

        private void ckbByte14Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x40;
            }
        }

        private void ckbByte14Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte14Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte14 -= 0x80;
            }
        }

        #endregion

        #region BYTE 10

        private void ckbByte10Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x01;
            }
        }

        private void ckbByte10Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x02;
            }
        }

        private void ckbByte10Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x04;
            }
        }

        private void ckbByte10Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x08;
            }
        }

        private void ckbByte10Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x10;
            }
        }

        private void ckbByte10Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x20;
            }
        }

        private void ckbByte10Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x40;
            }
        }

        private void ckbByte10Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte10Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte10 -= 0x80;
            }
        }

        #endregion

        #region BYTE 6

        private void ckbByte6Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x01;
            }
        }

        private void ckbByte6Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x02;
            }
        }

        private void ckbByte6Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x04;
            }
        }

        private void ckbByte6Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x08;
            }
        }

        private void ckbByte6Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x10;
            }
        }

        private void ckbByte6Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x20;
            }
        }

        private void ckbByte6Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x40;
            }
        }

        private void ckbByte6Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte6Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte6 -= 0x80;
            }
        }

        #endregion

        #region BYTE 7

        private void ckbByte7Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x01;
            }
        }

        private void ckbByte7Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x02;
            }
        }

        private void ckbByte7Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x04;
            }
        }

        private void ckbByte7Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x08;
            }
        }

        private void ckbByte7Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x10;
            }
        }

        private void ckbByte7Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x20;
            }
        }

        private void ckbByte7Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x40;
            }
        }

        private void ckbByte7Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte7Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte7 -= 0x80;
            }
        }

        #endregion

        #region BYTE 8

        private void ckbByte8Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit0.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x01;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x01;
            }
        }

        private void ckbByte8Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit1.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x02;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x02;
            }
        }

        private void ckbByte8Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit2.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x04;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x04;
            }
        }

        private void ckbByte8Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit3.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x08;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x08;
            }
        }

        private void ckbByte8Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit4.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x10;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x10;
            }
        }

        private void ckbByte8Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit5.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x20;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x20;
            }
        }

        private void ckbByte8Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit6.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x40;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x40;
            }
        }

        private void ckbByte8Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbByte8Bit7.Checked)
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 += 0x80;
                else
                    InstanceObjects.itemList[cmbItemName.SelectedIndex].byte8 -= 0x80;
            }
        }
        
        #endregion

        #endregion
    }
}
