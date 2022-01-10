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
    public partial class ActorEditor : Form
    {
        private bool boolInit;
        private bool boolRepeat;
        private List<cmbStatistic> levelList;
        private BattleCommandList bcList1;
        private BattleCommandList bcList2;
        private BattleCommandList bcList3;
        private BattleCommandList bcList4;
        private ItemList iList1;
        private ItemList iList2;
        private ItemList iList3;
        private ItemList iList4;
        private ItemList iList5;
        private ItemList iList6;


        public ActorEditor(string fileName)
        {
            this.Hide();
            InitializeComponent();
            boolInit = true;
            boolRepeat = false;
            initializeActorEditorComponents(fileName);
            tabControlActor.TabPages.Remove(tabPage2);
            this.Show();
            getCurrentData(0);
        }

        #region Level average Class

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

        private void initializeActorEditorComponents(string fileName)
        {
            if (InstanceObjects.actorList == null)
                InstanceObjects.initializeActorList(fileName);

            if (InstanceObjects.itemList == null)
                InstanceObjects.initializeItemList(fileName);

            if (InstanceObjects.battleCommandList == null)
                InstanceObjects.initializeBattleCommandList(fileName);

            bindData(cmbActorName, InstanceObjects.actorList, "actorName", "actorNumber");

            initializeLevelList(ref levelList);
            bindData(cmbActorByte22Bit_2_3, levelList, "text", "bit");

            bcList1 = (BattleCommandList)InstanceObjects.battleCommandList.Clone();
            bcList2 = (BattleCommandList)InstanceObjects.battleCommandList.Clone();
            bcList3 = (BattleCommandList)InstanceObjects.battleCommandList.Clone();
            bcList4 = (BattleCommandList)InstanceObjects.battleCommandList.Clone();
            bindData(cmbActorByte3, bcList1, "commandName", "commandNumber");
            bindData(cmbActorByte4, bcList2, "commandName", "commandNumber");
            bindData(cmbActorByte5, bcList3, "commandName", "commandNumber");
            bindData(cmbActorByte6, bcList4, "commandName", "commandNumber");

            iList1 = (ItemList)InstanceObjects.itemList.Clone();
            iList2 = (ItemList)InstanceObjects.itemList.Clone();
            iList3 = (ItemList)InstanceObjects.itemList.Clone();
            iList4 = (ItemList)InstanceObjects.itemList.Clone();
            iList5 = (ItemList)InstanceObjects.itemList.Clone();
            iList6 = (ItemList)InstanceObjects.itemList.Clone();
            bindData(cmbActorByte16, iList1, "itemName", "itemNumber");
            bindData(cmbActorByte17, iList2, "itemName", "itemNumber");
            bindData(cmbActorByte18, iList3, "itemName", "itemNumber");
            bindData(cmbActorByte19, iList4, "itemName", "itemNumber");
            bindData(cmbActorByte20, iList5, "itemName", "itemNumber");
            bindData(cmbActorByte21, iList6, "itemName", "itemNumber");
            
            boolRepeat = true;
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

        private void initializeLevelList(ref List<cmbStatistic> list)
        {
            list = new List<cmbStatistic>();

            list.Add(new cmbStatistic(0x00, "Add 0 on average"));
            list.Add(new cmbStatistic(0x04, "Add 2 on average"));
            list.Add(new cmbStatistic(0x08, "Add 5 on average"));
            list.Add(new cmbStatistic(0x0C, "Substract 3 on average"));
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

        private void cmbIndexChange(ref ComboBox cmb, byte byteToCheck, byte hex1, byte hex2)
        {
            if (byteToCheck == hex1)
                cmb.SelectedIndex = (int)hex2;
            else
                cmb.SelectedIndex = (int)byteToCheck;
        }

        #endregion

        #region CLOSE BUTTON

        private void btnItemReturn1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion

        #region ACTORS SELECTORS FUNCTIONS

        private void cmbActorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                nudActorNumber.Value = cmbActorName.SelectedIndex;
                getCurrentData((int)nudActorNumber.Value);
                boolRepeat = true;
            }
        }

        private void nudActorNumber_ValueChanged(object sender, EventArgs e)
        {
            if (boolRepeat)
            {
                boolRepeat = false;
                cmbActorName.SelectedIndex = (int)nudActorNumber.Value;
                getCurrentData(cmbActorName.SelectedIndex);
                boolRepeat = true;
            }
        }

        private void getCurrentData(int index)
        {
            boolInit = true;

            #region BYTES 3, 4, 5, 6 (BATTLE COMMANDS)

            cmbIndexChange(ref cmbActorByte3, InstanceObjects.actorList[index].byte3, 0xFF, 30);
            cmbIndexChange(ref cmbActorByte4, InstanceObjects.actorList[index].byte4, 0xFF, 30);
            cmbIndexChange(ref cmbActorByte5, InstanceObjects.actorList[index].byte5, 0xFF, 30);
            cmbIndexChange(ref cmbActorByte6, InstanceObjects.actorList[index].byte6, 0xFF, 30);

            #endregion

            #region BYTES 16 TO 21 (EQUIPMENTS & RELICS)

            cmbActorByte16.SelectedIndex = (int)InstanceObjects.actorList[index].byte16;
            cmbActorByte17.SelectedIndex = (int)InstanceObjects.actorList[index].byte17;
            cmbActorByte18.SelectedIndex = (int)InstanceObjects.actorList[index].byte18;
            cmbActorByte19.SelectedIndex = (int)InstanceObjects.actorList[index].byte19;
            cmbActorByte20.SelectedIndex = (int)InstanceObjects.actorList[index].byte20;
            cmbActorByte21.SelectedIndex = (int)InstanceObjects.actorList[index].byte21;

            #endregion

            #region BYTES 7 TO 15, 1 & 2 (ATTRIBUTES)

            nudActorByte7.Value = (int)InstanceObjects.actorList[index].byte7;
            nudActorByte8.Value = (int)InstanceObjects.actorList[index].byte8;
            nudActorByte9.Value = (int)InstanceObjects.actorList[index].byte9;
            nudActorByte10.Value = (int)InstanceObjects.actorList[index].byte10;
            nudActorByte11.Value = (int)InstanceObjects.actorList[index].byte11;
            nudActorByte12.Value = (int)InstanceObjects.actorList[index].byte12;
            nudActorByte13.Value = (int)InstanceObjects.actorList[index].byte13;
            nudActorByte14.Value = (int)InstanceObjects.actorList[index].byte14;
            nudActorByte15.Value = (int)InstanceObjects.actorList[index].byte15;
            nudActorByte1.Value = (int)InstanceObjects.actorList[index].byte1;
            nudActorByte2.Value = (int)InstanceObjects.actorList[index].byte2;

            #endregion

            #region BYTE 22 (RE-EQUIP, RUN SUCCESS & LEVEL FACTOR)

            ckbCheck(InstanceObjects.actorList[index].byte22, ckbActorByte22Bit4, 4);
            nudActorByte22Bit0_1.Value = 5 - (int)(InstanceObjects.actorList[index].byte22 & 0x03);
            cmbActorByte22Bit_2_3.SelectedValue = (int)(InstanceObjects.actorList[index].byte22 & 0x0C);

            #endregion

            boolInit = false;
        }

        #endregion

        #region BASE STATS PAGE

        private void cmbActorByte3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte3.SelectedIndex == 30)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte3 = 0xFF;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte3 = (byte)cmbActorByte3.SelectedIndex;
            }
        }

        private void cmbActorByte4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte4.SelectedIndex == 30)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte4 = 0xFF;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte4 = (byte)cmbActorByte4.SelectedIndex;
            }
        }

        private void cmbActorByte5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte5.SelectedIndex == 30)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte5 = 0xFF;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte5 = (byte)cmbActorByte5.SelectedIndex;
            }
        }

        private void cmbActorByte6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte6.SelectedIndex == 30)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte6 = 0xFF;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte6 = (byte)cmbActorByte6.SelectedIndex;
            }
        }

        private void cmbActorByte16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte16.SelectedIndex > 0xFF)
                    cmbActorByte16.SelectedIndex = 0xFF;

                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte16 = (byte)cmbActorByte16.SelectedIndex;
            }
        }

        private void cmbActorByte17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte17.SelectedIndex > 0xFF)
                    cmbActorByte17.SelectedIndex = 0xFF;

                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte17 = (byte)cmbActorByte17.SelectedIndex;
            }
        }

        private void cmbActorByte18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte18.SelectedIndex > 0xFF)
                    cmbActorByte18.SelectedIndex = 0xFF;

                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte18 = (byte)cmbActorByte18.SelectedIndex;
            }
        }

        private void cmbActorByte19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte19.SelectedIndex > 0xFF)
                    cmbActorByte19.SelectedIndex = 0xFF;

                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte19 = (byte)cmbActorByte19.SelectedIndex;
            }
        }

        private void cmbActorByte20_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte20.SelectedIndex > 0xFF)
                    cmbActorByte20.SelectedIndex = 0xFF;

                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte20 = (byte)cmbActorByte20.SelectedIndex;
            }
        }

        private void cmbActorByte21_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (cmbActorByte21.SelectedIndex > 0xFF)
                    cmbActorByte21.SelectedIndex = 0xFF;

                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte21 = (byte)cmbActorByte21.SelectedIndex;
            }
        }

        private void nudActorByte7_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte7 = (byte)nudActorByte7.Value;
        }

        private void nudActorByte8_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte8 = (byte)nudActorByte8.Value;
        }

        private void nudActorByte9_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte9 = (byte)nudActorByte9.Value;
        }

        private void nudActorByte10_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte10 = (byte)nudActorByte10.Value;
        }

        private void nudActorByte11_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte11 = (byte)nudActorByte11.Value;
        }

        private void nudActorByte12_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte12 = (byte)nudActorByte12.Value;
        }

        private void nudActorByte13_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte13 = (byte)nudActorByte13.Value;
        }

        private void nudActorByte14_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte14 = (byte)nudActorByte14.Value;
        }

        private void nudActorByte15_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte15 = (byte)nudActorByte15.Value;
        }

        private void nudActorByte22Bit0_1_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 &= 0xFC;
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 += (byte)(5 - nudActorByte22Bit0_1.Value);
        }

        private void nudActorByte1_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte1 = (byte)nudActorByte1.Value;
        }

        private void nudActorByte2_Leave(object sender, EventArgs e)
        {
            InstanceObjects.actorList[cmbActorName.SelectedIndex].byte2 = (byte)nudActorByte2.Value;
        }

        private void cmbActorByte22Bit_2_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 &= 0xF3;
                InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 += (byte)((int)cmbActorByte22Bit_2_3.SelectedValue);
            }
        }

        private void ckbActorByte22Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbActorByte22Bit4.Checked)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 += 0x10;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 -= 0x10;
            }
        }

        private void ckbActorByte22Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbActorByte22Bit4.Checked)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 += 0x20;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 -= 0x20;
            }
        }

        private void ckbActorByte22Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbActorByte22Bit4.Checked)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 += 0x40;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 -= 0x40;
            }
        }

        private void ckbActorByte22Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (ckbActorByte22Bit4.Checked)
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 += 0x80;
                else
                    InstanceObjects.actorList[cmbActorName.SelectedIndex].byte22 -= 0x80;
            }
        }

        #endregion

    }
}
