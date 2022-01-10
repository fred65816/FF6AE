using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FFVIEditor
{
    public partial class MonsterEditor : Form
    {
        private MorphItemPattern morphItems;                // List of Morphing patterns
        private List<MorphHitPercent> morphHitValues;       // List of Morph Hit values
        private string[] extraEffects;                      // List of special attacks extra effects
        private bool boolInit;                              // true during initializations, false during changes made by user

        public MonsterEditor(String fileName)
        {
            this.Hide();
            InitializeComponent();
            boolInit = true;
            initializeMonsterEditorComponents(fileName);
            this.Show();
        }

        private class MorphHitPercent
        {
            public int minusByte { get; set; }              // Byte that determine the morph hit percentage
            public double percentage { get; set; }          // Morph hit percentage

            public MorphHitPercent(int minusByte, double percentage)
            {
                this.minusByte = minusByte;
                this.percentage = percentage;
            }

        }

        #region INITIALIZATION FUNCTIONS (LISTS CREATION, DATA BINDING)

        private void initializeMonsterEditorComponents(string fileName)
        {
            morphItems = new MorphItemPattern();

            initializeMorphHitValues();
            bindData(this.cmbMorphHit, morphHitValues, "percentage", "minusByte");

            initializeExtraEffects();
            bindData(this.cmbExtraEffect, extraEffects, null, null);

            if (InstanceObjects.itemList == null)
                InstanceObjects.initializeItemList(fileName);

            if (InstanceObjects.monsterList == null)
                InstanceObjects.initializeMonsterList(fileName);

            bindData(this.cmbAttackLook, InstanceObjects.itemList, "itemName", "itemNumber");
            bindData(this.cmbName, InstanceObjects.monsterList, "monsterName", "monsterNumber");
        }

        private void initializeMorphHitValues()
        {
            morphHitValues = new List<MorphHitPercent>();

            morphHitValues.Add(new MorphHitPercent(0x00, 99.60));
            morphHitValues.Add(new MorphHitPercent(0x20, 75));
            morphHitValues.Add(new MorphHitPercent(0x40, 50));
            morphHitValues.Add(new MorphHitPercent(0x60, 25));
            morphHitValues.Add(new MorphHitPercent(0x80, 12.5));
            morphHitValues.Add(new MorphHitPercent(0xA0, 6.25));
            morphHitValues.Add(new MorphHitPercent(0xC0, 3.25));
            morphHitValues.Add(new MorphHitPercent(0xE0, 0));

        }

        private void initializeExtraEffects()
        {
            extraEffects = new string[]
            {
                // 0x00 to 0x07
                "Blind", "Zombie", "Poison", "Magitek", "Clear", "Imp", "Petrify", "Dead",
                // 0x08 to 0x0F
                "Condemned", "Near Fatal", "Image", "Mute", "Berserk", "Muddle", "Seizure", "Psyche",
                // 0x10 to 0x17
                "Dance", "Regen", "Slow", "Haste", "Stop", "Shell", "Safe", "Wall",
                // 0x18 to 0x1F
                "Rage", "Frozen", "Life 3", "Esper Morph", "Magic Cast", "Disappear", "Interceptor", "Float", 
                // 0x20 to 0x25
                "Attack power 1", "Attack power 2", "Attack power 3", "Attack power 4", "Attack power 5", "Attack power 6", 
                // 0x26 to 0x2B
                "Attack power 7", "Attack power 8", "Attack power 9", "Attack power 10", "Attack power 11", "Attack power 12", 
                // 0x2C to 0x31
                "Attack power 13", "Attack power 14", "Attack power 15", "Attack power 16", "Absorb HP", "Absorb MP",
                // 0x32 to 0x37
                "Remove reflect", "Remove reflect", "Remove reflect", "Remove reflect", "Remove reflect", "Remove reflect", 
                // 0x38 to 0x3D
                "Remove reflect", "Remove reflect", "Remove reflect", "Remove reflect", "Remove reflect", "Remove reflect", 
                // 0x3E to 0x3F
                "Remove reflect", "Remove reflect"
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

        #endregion

        #region GENERAL FUNCTION

        private bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        #endregion

        #region EXIT BUTTON FUNCTION

        private void btnMonsterReturn_Click(object sender, EventArgs e)
        {
            this.Hide();           
        }

        #endregion

        #region MONSTER SELECTORS FUNCTIONS

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.nudMonsNumber.Value = this.cmbName.SelectedIndex;
            getCurrentMonsterData((int)this.nudMonsNumber.Value);
        }

        private void nudMonsNumber_ValueChanged(object sender, EventArgs e)
        {
            this.cmbName.SelectedIndex = (int)this.nudMonsNumber.Value;
            getCurrentMonsterData(this.cmbName.SelectedIndex);
        }

        /// <summary>
        /// Update all monster stats for current monster
        /// </summary>
        /// <param name="monsterNumber">The ID of the monster</param>
        private void getCurrentMonsterData(int monsterNumber)
        {
            boolInit = true;

            #region MAIN STATS

            this.textBoxExp.Text = InstanceObjects.monsterList[monsterNumber].experience.ToString();
            this.textBoxMP.Text = InstanceObjects.monsterList[monsterNumber].mp.ToString();
            this.textBoxHP.Text = InstanceObjects.monsterList[monsterNumber].hp.ToString();
            this.textBoxGold.Text = InstanceObjects.monsterList[monsterNumber].gold.ToString();
            this.nudAttackBlock.Value = InstanceObjects.monsterList[monsterNumber].attackBlock;
            this.nudAttackDefense.Value = InstanceObjects.monsterList[monsterNumber].attackDefense;
            this.nudBattlePower.Value = InstanceObjects.monsterList[monsterNumber].battlePower;
            this.nudHitRate.Value = InstanceObjects.monsterList[monsterNumber].hitRate;
            this.nudLevel.Value = InstanceObjects.monsterList[monsterNumber].level;
            this.nudMagicBlock.Value = InstanceObjects.monsterList[monsterNumber].magicBlock;
            this.nudMagicDefense.Value = InstanceObjects.monsterList[monsterNumber].magicDefense;
            this.nudMagicPower.Value = InstanceObjects.monsterList[monsterNumber].magicPower;
            this.nudSpeed.Value = InstanceObjects.monsterList[monsterNumber].speed;

            #endregion

            #region MORPH PATTERN

            int morphPattern = (int)(InstanceObjects.monsterList[monsterNumber].morphTemplate & 0x1F);
            this.nudMorphPattern.Value = morphPattern;
            this.cmbMorphHit.SelectedValue = (int)(InstanceObjects.monsterList[monsterNumber].morphTemplate & 0xE0);
            this.lblMorphItem1.Text = morphItems.morphPatterns[morphPattern][0];
            this.lblMorphItem2.Text = morphItems.morphPatterns[morphPattern][1];
            this.lblMorphItem3.Text = morphItems.morphPatterns[morphPattern][2];
            this.lblMorphItem4.Text = morphItems.morphPatterns[morphPattern][3];

            #endregion

            #region BYTE 27 (ATTACK LOOK)

            this.cmbAttackLook.SelectedIndex = (int)InstanceObjects.monsterList[monsterNumber].byte27;

            #endregion

            #region BYTE 32 (EXTRA EFFECT)

            this.cmbExtraEffect.SelectedIndex = (int)(InstanceObjects.monsterList[monsterNumber].byte32 & 0x3F);

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte32, 6))
                ckbSaPhysicalHarm.Checked = true;
            else
                ckbSaPhysicalHarm.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte32, 7))
                ckbSaDodged.Checked = true;
            else
                ckbSaDodged.Checked = false;

            #endregion

            #region BYTE 24 (ELEMENT ABSORB)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 0))
                ckbElementByte24Bit0.Checked = true;
            else
                ckbElementByte24Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 1))
                ckbElementByte24Bit1.Checked = true;
            else
                ckbElementByte24Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 2))
                ckbElementByte24Bit2.Checked = true;
            else
                ckbElementByte24Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 3))
                ckbElementByte24Bit3.Checked = true;
            else
                ckbElementByte24Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 4))
                ckbElementByte24Bit4.Checked = true;
            else
                ckbElementByte24Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 5))
                ckbElementByte24Bit5.Checked = true;
            else
                ckbElementByte24Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 6))
                ckbElementByte24Bit6.Checked = true;
            else
                ckbElementByte24Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte24, 7))
                ckbElementByte24Bit7.Checked = true;
            else
                ckbElementByte24Bit7.Checked = false;

            #endregion

            #region BYTE 25 (ELEMENT NULLIFY)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 0))
                ckbElementByte25Bit0.Checked = true;
            else
                ckbElementByte25Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 1))
                ckbElementByte25Bit1.Checked = true;
            else
                ckbElementByte25Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 2))
                ckbElementByte25Bit2.Checked = true;
            else
                ckbElementByte25Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 3))
                ckbElementByte25Bit3.Checked = true;
            else
                ckbElementByte25Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 4))
                ckbElementByte25Bit4.Checked = true;
            else
                ckbElementByte25Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 5))
                ckbElementByte25Bit5.Checked = true;
            else
                ckbElementByte25Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 6))
                ckbElementByte25Bit6.Checked = true;
            else
                ckbElementByte25Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte25, 7))
                ckbElementByte25Bit7.Checked = true;
            else
                ckbElementByte25Bit7.Checked = false;

            #endregion

            #region BYTE 26 (ELEMENT WEAKNESS)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 0))
                ckbElementByte26Bit0.Checked = true;
            else
                ckbElementByte26Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 1))
                ckbElementByte26Bit1.Checked = true;
            else
                ckbElementByte26Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 2))
                ckbElementByte26Bit2.Checked = true;
            else
                ckbElementByte26Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 3))
                ckbElementByte26Bit3.Checked = true;
            else
                ckbElementByte26Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 4))
                ckbElementByte26Bit4.Checked = true;
            else
                ckbElementByte26Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 5))
                ckbElementByte26Bit5.Checked = true;
            else
                ckbElementByte26Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 6))
                ckbElementByte26Bit6.Checked = true;
            else
                ckbElementByte26Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte26, 7))
                ckbElementByte26Bit7.Checked = true;
            else
                ckbElementByte26Bit7.Checked = false;

            #endregion

            #region BYTE 28 (STATUS START WTIH A)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 0))
                ckbStatusAByte28Bit0.Checked = true;
            else
                ckbStatusAByte28Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 1))
                ckbStatusAByte28Bit1.Checked = true;
            else
                ckbStatusAByte28Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 2))
                ckbStatusAByte28Bit2.Checked = true;
            else
                ckbStatusAByte28Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 3))
                ckbStatusAByte28Bit3.Checked = true;
            else
                ckbStatusAByte28Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 4))
                ckbStatusAByte28Bit4.Checked = true;
            else
                ckbStatusAByte28Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 5))
                ckbStatusAByte28Bit5.Checked = true;
            else
                ckbStatusAByte28Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 6))
                ckbStatusAByte28Bit6.Checked = true;
            else
                ckbStatusAByte28Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte28, 7))
                ckbStatusAByte28Bit7.Checked = true;
            else
                ckbStatusAByte28Bit7.Checked = false;

            #endregion

            #region BYTE 29 (STATUS START WTIH B)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 0))
                ckbStatusAByte29Bit0.Checked = true;
            else
                ckbStatusAByte29Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 1))
                ckbStatusAByte29Bit1.Checked = true;
            else
                ckbStatusAByte29Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 2))
                ckbStatusAByte29Bit2.Checked = true;
            else
                ckbStatusAByte29Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 3))
                ckbStatusAByte29Bit3.Checked = true;
            else
                ckbStatusAByte29Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 4))
                ckbStatusAByte29Bit4.Checked = true;
            else
                ckbStatusAByte29Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 5))
                ckbStatusAByte29Bit5.Checked = true;
            else
                ckbStatusAByte29Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 6))
                ckbStatusAByte29Bit6.Checked = true;
            else
                ckbStatusAByte29Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte29, 7))
                ckbStatusAByte29Bit7.Checked = true;
            else
                ckbStatusAByte29Bit7.Checked = false;

            #endregion

            #region BYTE 30 (STATUS START WTIH C)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 0))
                ckbStatusAByte30Bit0.Checked = true;
            else
                ckbStatusAByte30Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 1))
                ckbStatusAByte30Bit1.Checked = true;
            else
                ckbStatusAByte30Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 2))
                ckbStatusAByte30Bit2.Checked = true;
            else
                ckbStatusAByte30Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 3))
                ckbStatusAByte30Bit3.Checked = true;
            else
                ckbStatusAByte30Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 4))
                ckbStatusAByte30Bit4.Checked = true;
            else
                ckbStatusAByte30Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 5))
                ckbStatusAByte30Bit5.Checked = true;
            else
                ckbStatusAByte30Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 6))
                ckbStatusAByte30Bit6.Checked = true;
            else
                ckbStatusAByte30Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte30, 7))
                ckbStatusAByte30Bit7.Checked = true;
            else
                ckbStatusAByte30Bit7.Checked = false;

            #endregion

            #region BYTE 21 (STATUS BLOCK A)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 0))
                ckbStatusAByte21Bit0.Checked = true;
            else
                ckbStatusAByte21Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 1))
                ckbStatusAByte21Bit1.Checked = true;
            else
                ckbStatusAByte21Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 2))
                ckbStatusAByte21Bit2.Checked = true;
            else
                ckbStatusAByte21Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 3))
                ckbStatusAByte21Bit3.Checked = true;
            else
                ckbStatusAByte21Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 4))
                ckbStatusAByte21Bit4.Checked = true;
            else
                ckbStatusAByte21Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 5))
                ckbStatusAByte21Bit5.Checked = true;
            else
                ckbStatusAByte21Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 6))
                ckbStatusAByte21Bit6.Checked = true;
            else
                ckbStatusAByte21Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte21, 7))
                ckbStatusAByte21Bit7.Checked = true;
            else
                ckbStatusAByte21Bit7.Checked = false;

            #endregion

            #region BYTE 22 (STATUS BLOCK B)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 0))
                ckbStatusAByte22Bit0.Checked = true;
            else
                ckbStatusAByte22Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 1))
                ckbStatusAByte22Bit1.Checked = true;
            else
                ckbStatusAByte22Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 2))
                ckbStatusAByte22Bit2.Checked = true;
            else
                ckbStatusAByte22Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 3))
                ckbStatusAByte22Bit3.Checked = true;
            else
                ckbStatusAByte22Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 4))
                ckbStatusAByte22Bit4.Checked = true;
            else
                ckbStatusAByte22Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 5))
                ckbStatusAByte22Bit5.Checked = true;
            else
                ckbStatusAByte22Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 6))
                ckbStatusAByte22Bit6.Checked = true;
            else
                ckbStatusAByte22Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte22, 7))
                ckbStatusAByte22Bit7.Checked = true;
            else
                ckbStatusAByte22Bit7.Checked = false;

            #endregion

            #region BYTE 23 (STATUS BLOCK C)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 0))
                ckbStatusAByte23Bit0.Checked = true;
            else
                ckbStatusAByte23Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 1))
                ckbStatusAByte23Bit1.Checked = true;
            else
                ckbStatusAByte23Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 2))
                ckbStatusAByte23Bit2.Checked = true;
            else
                ckbStatusAByte23Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 3))
                ckbStatusAByte23Bit3.Checked = true;
            else
                ckbStatusAByte23Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 4))
                ckbStatusAByte23Bit4.Checked = true;
            else
                ckbStatusAByte23Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 5))
                ckbStatusAByte23Bit5.Checked = true;
            else
                ckbStatusAByte23Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 6))
                ckbStatusAByte23Bit6.Checked = true;
            else
                ckbStatusAByte23Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte23, 7))
                ckbStatusAByte23Bit7.Checked = true;
            else
                ckbStatusAByte23Bit7.Checked = false;

            #endregion

            #region BYTE 19 (SPECIAL STATUS A)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 0))
                ckbStatusBByte19Bit0.Checked = true;
            else
                ckbStatusBByte19Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 1))
                ckbStatusBByte19Bit1.Checked = true;
            else
                ckbStatusBByte19Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 2))
                ckbStatusBByte19Bit2.Checked = true;
            else
                ckbStatusBByte19Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 3))
                ckbStatusBByte19Bit3.Checked = true;
            else
                ckbStatusBByte19Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 4))
                ckbStatusBByte19Bit4.Checked = true;
            else
                ckbStatusBByte19Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 5))
                ckbStatusBByte19Bit5.Checked = true;
            else
                ckbStatusBByte19Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 6))
                ckbStatusBByte19Bit6.Checked = true;
            else
                ckbStatusBByte19Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte19, 7))
                ckbStatusBByte19Bit7.Checked = true;
            else
                ckbStatusBByte19Bit7.Checked = false;

            #endregion

            #region BYTE 20 (SPECIAL STATUS B)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 0))
                ckbStatusBByte20Bit0.Checked = true;
            else
                ckbStatusBByte20Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 1))
                ckbStatusBByte20Bit1.Checked = true;
            else
                ckbStatusBByte20Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 2))
                ckbStatusBByte20Bit2.Checked = true;
            else
                ckbStatusBByte20Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 3))
                ckbStatusBByte20Bit3.Checked = true;
            else
                ckbStatusBByte20Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 4))
                ckbStatusBByte20Bit4.Checked = true;
            else
                ckbStatusBByte20Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 5))
                ckbStatusBByte20Bit5.Checked = true;
            else
                ckbStatusBByte20Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 6))
                ckbStatusBByte20Bit6.Checked = true;
            else
                ckbStatusBByte20Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte20, 7))
                ckbStatusBByte20Bit7.Checked = true;
            else
                ckbStatusBByte20Bit7.Checked = false;

            #endregion

            #region BYTE 31 (SPECIAL STATUS C)

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 0))
                ckbStatusBByte31Bit0.Checked = true;
            else
                ckbStatusBByte31Bit0.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 1))
                ckbStatusBByte31Bit1.Checked = true;
            else
                ckbStatusBByte31Bit1.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 2))
                ckbStatusBByte31Bit2.Checked = true;
            else
                ckbStatusBByte31Bit2.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 3))
                ckbStatusBByte31Bit3.Checked = true;
            else
                ckbStatusBByte31Bit3.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 4))
                ckbStatusBByte31Bit4.Checked = true;
            else
                ckbStatusBByte31Bit4.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 5))
                ckbStatusBByte31Bit5.Checked = true;
            else
                ckbStatusBByte31Bit5.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 6))
                ckbStatusBByte31Bit6.Checked = true;
            else
                ckbStatusBByte31Bit6.Checked = false;

            if (IsBitSet(InstanceObjects.monsterList[monsterNumber].byte31, 7))
                ckbStatusBByte31Bit7.Checked = true;
            else
                ckbStatusBByte31Bit7.Checked = false;

            #endregion

            boolInit = false;
        }

        #endregion

        #region TEXTBOX FUNCTIONS (HP, MP, EXPERIENCE, GOLD)

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

        private void textBoxHP_Leave(object sender, EventArgs e)
        {
            int result = validateTextBox(textBoxHP);

            if(result != -1)
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].hp = result;
        }

        private void textBoxMP_Leave(object sender, EventArgs e)
        {
            int result = validateTextBox(textBoxMP);

            if (result != -1)
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].mp = result;
        }

        private void textBoxExp_Leave(object sender, EventArgs e)
        {
            int result = validateTextBox(textBoxExp);

            if (result != -1)
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].experience = result;
        }

        private void textBoxGold_Leave(object sender, EventArgs e)
        {
            int result = validateTextBox(textBoxGold);

            if (result != -1)
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].gold = result;
        }

        #endregion

        #region NUMERIC_UP_DOWN CONTROLS FUNCTIONS (OTHER BASIC STATS)

        private void nudMagicPower_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].magicPower = (int)this.nudMagicPower.Value;
        }

        private void nudMagicDefense_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].magicDefense = (int)this.nudMagicDefense.Value;
        }

        private void nudMagicBlock_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].magicBlock = (int)this.nudMagicBlock.Value;
        }

        private void nudBattlePower_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].battlePower = (int)this.nudBattlePower.Value;
        }

        private void nudAttackDefense_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].attackDefense = (int)this.nudAttackDefense.Value;
        }

        private void nudAttackBlock_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].attackBlock = (int)this.nudAttackBlock.Value;
        }

        private void nudSpeed_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].speed = (int)this.nudSpeed.Value;
        }

        private void nudHitRate_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].hitRate = (int)this.nudHitRate.Value;
        }

        private void nudLevel_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].level = (int)this.nudLevel.Value;
        }

        #endregion

        #region MORPH PATTERN CONTROLS FUNCTIONS

        private void nudMorphPattern_Leave(object sender, EventArgs e)
        {
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].morphTemplate = (byte)(InstanceObjects.monsterList[this.cmbName.SelectedIndex].morphTemplate & 0xE0);
            InstanceObjects.monsterList[this.cmbName.SelectedIndex].morphTemplate += (byte)this.nudMorphPattern.Value;
        }

        private void cmbMorphHit_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].morphTemplate = (byte)(InstanceObjects.monsterList[this.cmbName.SelectedIndex].morphTemplate & 0x1F);
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].morphTemplate += (byte)(this.cmbMorphHit.SelectedIndex * 0x20);
            }
        }

        private void nudMorphPattern_ValueChanged(object sender, EventArgs e)
        {
            this.lblMorphItem1.Text = morphItems.morphPatterns[(int)nudMorphPattern.Value][0];
            this.lblMorphItem2.Text = morphItems.morphPatterns[(int)nudMorphPattern.Value][1];
            this.lblMorphItem3.Text = morphItems.morphPatterns[(int)nudMorphPattern.Value][2];
            this.lblMorphItem4.Text = morphItems.morphPatterns[(int)nudMorphPattern.Value][3];
        }

        #endregion

        #region SPECIAL ATTACK CONTROLS FUNCTIONS

        private void cmbAttackLook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.cmbAttackLook.SelectedIndex > 0xFF)
                    this.cmbAttackLook.SelectedIndex = 0xFF;

                InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte27 = (byte)this.cmbAttackLook.SelectedIndex;
            }
        }

        private void cmbExtraEffect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 = (byte)(InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 & 0xC0);
                InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 += (byte)this.cmbExtraEffect.SelectedIndex;
            }
        }

        private void ckbSaPhysicalHarm_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbSaPhysicalHarm.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 -= 0x40;
            }
        }

        private void ckbSaDodged_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbSaDodged.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte32 -= 0x80;
            }
        }

        #endregion

        #region ELEMENT ABSORB CHECKBOX FUNCTIONS

        private void ckbElementByte24Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit0.Checked) 
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x01;
                else 
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x01;
            }
        }

        private void ckbElementByte24Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x02;
            }
        }

        private void ckbElementByte24Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x04;
            }
        }

        private void ckbElementByte24Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x08;
            }
        }

        private void ckbElementByte24Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x10;
            }
        }

        private void ckbElementByte24Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x20;
            }
        }

        private void ckbElementByte24Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x40;
            }
        }

        private void ckbElementByte24Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte24Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte24 -= 0x80;
            }
        }

        #endregion

        #region ELEMENT NULLIFY CHECKBOX FUNCTIONS

        private void ckbElementByte25Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x01;
            }
        }

        private void ckbElementByte25Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x02;
            }
        }

        private void ckbElementByte25Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x04;
            }
        }

        private void ckbElementByte25Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x08;
            }
        }

        private void ckbElementByte25Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x10;
            }
        }

        private void ckbElementByte25Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x20;
            }
        }

        private void ckbElementByte25Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x40;
            }
        }

        private void ckbElementByte25Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte25Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte25 -= 0x80;
            }
        }

        #endregion

        #region ELEMENT WEAKNESS CHECKBOX FUNCTIONS

        private void ckbElementByte26Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x01;
            }
        }

        private void ckbElementByte26Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x02;
            }
        }

        private void ckbElementByte26Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x04;
            }
        }

        private void ckbElementByte26Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x08;
            }
        }

        private void ckbElementByte26Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x10;
            }
        }

        private void ckbElementByte26Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x20;
            }
        }

        private void ckbElementByte26Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x40;
            }
        }

        private void ckbElementByte26Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbElementByte26Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte26 -= 0x80;
            }
        }

        #endregion

        #region STATUS START WITH CHECKBOX FUNCTIONS

        private void ckbStatusAByte28Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x01;
            }
        }

        private void ckbStatusAByte28Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x02;
            }
        }

        private void ckbStatusAByte28Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x04;
            }
        }

        private void ckbStatusAByte28Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x08;
            }
        }

        private void ckbStatusAByte28Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x10;
            }
        }

        private void ckbStatusAByte28Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x20;
            }
        }

        private void ckbStatusAByte28Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x40;
            }
        }

        private void ckbStatusAByte28Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte28Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte28 -= 0x80;
            }
        }

        private void ckbStatusAByte29Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x01;
            }
        }

        private void ckbStatusAByte29Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x02;
            }
        }

        private void ckbStatusAByte29Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x04;
            }
        }

        private void ckbStatusAByte29Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x08;
            }
        }

        private void ckbStatusAByte29Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x10;
            }
        }

        private void ckbStatusAByte29Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x20;
            }
        }

        private void ckbStatusAByte29Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x40;
            }
        }

        private void ckbStatusAByte29Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte29Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte29 -= 0x80;
            }
        }

        private void ckbStatusAByte30Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x01;
            }
        }

        private void ckbStatusAByte30Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x02;
            }
        }

        private void ckbStatusAByte30Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x04;
            }
        }

        private void ckbStatusAByte30Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x08;
            }
        }

        private void ckbStatusAByte30Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x10;
            }
        }

        private void ckbStatusAByte30Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x20;
            }
        }

        private void ckbStatusAByte30Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x40;
            }
        }

        private void ckbStatusAByte30Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte30Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte30 -= 0x80;
            }
        }

        #endregion

        #region STATUS BLOCK CHECKBOX FUNCTIONS

        private void ckbStatusAByte21Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x01;
            }
        }

        private void ckbStatusAByte21Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x02;
            }
        }

        private void ckbStatusAByte21Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x04;
            }
        }

        private void ckbStatusAByte21Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x08;
            }
        }

        private void ckbStatusAByte21Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x10;
            }
        }

        private void ckbStatusAByte21Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x20;
            }
        }

        private void ckbStatusAByte21Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x40;
            }
        }

        private void ckbStatusAByte21Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte21Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte21 -= 0x80;
            }
        }

        private void ckbStatusAByte22Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x01;
            }
        }

        private void ckbStatusAByte22Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x02;
            }
        }

        private void ckbStatusAByte22Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x04;
            }
        }

        private void ckbStatusAByte22Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x08;
            }
        }

        private void ckbStatusAByte22Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x10;
            }
        }

        private void ckbStatusAByte22Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x20;
            }
        }

        private void ckbStatusAByte22Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x40;
            }
        }

        private void ckbStatusAByte22Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte22Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte22 -= 0x80;
            }
        }

        private void ckbStatusAByte23Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x01;
            }
        }

        private void ckbStatusAByte23Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x02;
            }
        }

        private void ckbStatusAByte23Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x04;
            }
        }

        private void ckbStatusAByte23Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x08;
            }
        }

        private void ckbStatusAByte23Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x10;
            }
        }

        private void ckbStatusAByte23Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x20;
            }
        }

        private void ckbStatusAByte23Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x40;
            }
        }

        private void ckbStatusAByte23Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusAByte23Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte23 -= 0x80;
            }
        }


        #endregion

        #region SPECIAL STATUS CHECKBOX FUNCTIONS

        private void ckbStatusBByte19Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x01;
            }
        }

        private void ckbStatusBByte19Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x02;
            }
        }

        private void ckbStatusBByte19Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x04;
            }
        }

        private void ckbStatusBByte19Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x08;
            }
        }

        private void ckbStatusBByte19Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x10;
            }
        }

        private void ckbStatusBByte19Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x20;
            }
        }

        private void ckbStatusBByte19Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x40;
            }
        }

        private void ckbStatusBByte19Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte19Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte19 -= 0x80;
            }
        }

        private void ckbStatusBByte20Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x01;
            }
        }

        private void ckbStatusBByte20Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x02;
            }
        }

        private void ckbStatusBByte20Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x04;
            }
        }

        private void ckbStatusBByte20Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x08;
            }
        }

        private void ckbStatusBByte20Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x10;
            }
        }

        private void ckbStatusBByte20Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x20;
            }
        }

        private void ckbStatusBByte20Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x40;
            }
        }

        private void ckbStatusBByte20Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte20Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte20 -= 0x80;
            }
        }

        private void ckbStatusBByte31Bit0_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit0.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x01;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x01;
            }
        }

        private void ckbStatusBByte31Bit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit1.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x02;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x02;
            }
        }

        private void ckbStatusBByte31Bit2_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit2.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x04;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x04;
            }
        }

        private void ckbStatusBByte31Bit3_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit3.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x08;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x08;
            }
        }

        private void ckbStatusBByte31Bit4_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit4.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x10;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x10;
            }
        }

        private void ckbStatusBByte31Bit5_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit5.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x20;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x20;
            }
        }

        private void ckbStatusBByte31Bit6_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit6.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x40;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x40;
            }
        }

        private void ckbStatusBByte31Bit7_CheckedChanged(object sender, EventArgs e)
        {
            if (!boolInit)
            {
                if (this.ckbStatusBByte31Bit7.Checked)
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 += 0x80;
                else
                    InstanceObjects.monsterList[this.cmbName.SelectedIndex].byte31 -= 0x80;
            }
        }

        #endregion
    }
}
