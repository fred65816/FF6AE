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
    public partial class Editor : Form
    {
        FF6Arom rom;
        MonsterEditor monsterEditor;
        GraphicEditor graphicEditor;
        ItemEditor itemEditor;
        SpellEditor spellEditor;
        ActorEditor actorEditor;
        About aboutWindow;

        public Editor()
        {
            InitializeComponent();
            monsterEditor = null;
            graphicEditor = null;
            itemEditor = null;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rom = new FF6Arom(dlg.FileName);

                    if (rom.complementaryTest)
                    {
                        showResults();
                    }
                    else
                    {
                        MessageBox.Show("The ROM did not pass the complementary test. The program will now close.");
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to fetch data from the ROM. The program will now close." + "\n" + "exception: " + ex.Message.ToString());
                    Application.Exit();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (monsterEditor != null)
                InstanceObjects.monsterList.writeMonsterList(rom.filePath);

            if (itemEditor != null)
                InstanceObjects.itemList.writeMonsterList(rom.filePath);

            if (spellEditor != null)
                InstanceObjects.spellList.writeSpellList(rom.filePath);

            if (actorEditor != null)
                InstanceObjects.actorList.writeActorList(rom.filePath);

            if (graphicEditor != null)
            {
                InstanceObjects.smallFont.writeFont(rom.filePath);
                InstanceObjects.largeFont.writeFont(rom.filePath);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void infosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutWindow = new About();
        }
        private string ValidateString(string stringToValidate)
        {
            if (stringToValidate.Equals("") || stringToValidate == null)
            {
                stringToValidate = "Error!";
            }
            return stringToValidate;
        }

        private void showResults()
        {
            tbRomName.Text = ValidateString(rom.romName);
            tbCountry.Text = ValidateString(rom.country);
            tbSaveType.Text = ValidateString(rom.saveType);
            tbSerial.Text = ValidateString(rom.serial.ToString());

            if ((rom.serial == "AGB-BZ6E-USA" || rom.serial == "AGB-BZ6P-EUR") && (rom.romName.CompareTo("FF6ADVANCE") == 0))
            {
                InstanceObjects.initializeSmallFont(rom.filePath, 0x161FF0);
                InstanceObjects.initializeLargeFont(rom.filePath, 0x162CCC);
                btnMonsterEditor.Enabled = true;
                btnGraphicEditor.Enabled = true;
                btnItemEditor.Enabled = true;
                btnSpellEditor.Enabled = true;
                btnActorEditor.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please load a valid Final Fantasy VI Advance ROM (USA or EUR version).");
            }
        }

        private void btnMonsterEditor_Click(object sender, EventArgs e)
        {
            if (monsterEditor == null || monsterEditor.IsDisposed)
                monsterEditor = new MonsterEditor(rom.filePath);
            else
                monsterEditor.Show();
        }

        private void btnGraphicEditor_Click(object sender, EventArgs e)
        {
            if (graphicEditor == null || graphicEditor.IsDisposed)
                graphicEditor = new GraphicEditor();
            else
                graphicEditor.Show();
        }

        private void btnItemEditor_Click(object sender, EventArgs e)
        {
            if (itemEditor == null || itemEditor.IsDisposed)
                itemEditor = new ItemEditor(rom.filePath);
            else
                itemEditor.Show();
        }

        private void btnSpellEditor_Click(object sender, EventArgs e)
        {
            if (spellEditor == null || spellEditor.IsDisposed)
                spellEditor = new SpellEditor(rom.filePath);
            else
                spellEditor.Show();
        }

        private void btnActorEditor_Click(object sender, EventArgs e)
        {
            if (actorEditor == null || actorEditor.IsDisposed)
                actorEditor = new ActorEditor(rom.filePath);
            else
                actorEditor.Show();
        }












    }
}
