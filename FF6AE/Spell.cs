using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    public class Spell
    {
        public int spellNumber { get; set; }
        public string spellName { get; set; }
        public byte byte1 { get; set; }             
        public byte byte2 { get; set; }             
        public byte byte3 { get; set; }             
        public byte byte4 { get; set; }             
        public byte byte5 { get; set; }             
        public byte byte6 { get; set; }
        public byte byte7 { get; set; }
        public byte byte8 { get; set; }
        public byte byte9 { get; set; }
        public byte byte10 { get; set; }
        public byte byte11 { get; set; }
        public byte byte12 { get; set; }
        public byte byte13 { get; set; }
        public byte byte14 { get; set; }

        public Spell(int itemNumber, string itemName, byte[] data)
        {
            this.spellNumber = itemNumber;
            this.spellName = itemName;
            fillAttributes(data);
        }

        public Spell(int itemNumber, string itemName)
        {
            this.spellNumber = itemNumber;
            this.spellName = itemName;
        }

        public void fillAttributes(byte[] data)
        {
            byte1 = data[0]; byte2 = data[1]; byte3 = data[2]; byte4 = data[3]; byte5 = data[4]; 
            byte6 = data[5]; byte7 = data[6]; byte8 = data[7]; byte9 = data[8]; byte10 = data[9]; 
            byte11 = data[10]; byte12 = data[11]; byte13 = data[12]; byte14 = data[13]; 
        }

        public byte[] fillArray()
        {
            byte[] array = new byte[14];

            array[0] = byte1; array[1] = byte2; array[2] = byte3; array[3] = byte4;
            array[4] = byte5; array[5] = byte6; array[6] = byte7; array[7] = byte8;
            array[8] = byte9; array[9] = byte10; array[10] = byte11; array[11] = byte12;
            array[12] = byte13; array[13] = byte14; 

            return array;
        }
    }

    public class SpellList : List<Spell>
    {
        private const int OFF_BEG_SPELL_NAME = 0x1C0A80;
        private const int OFF_END_SPELL_NAME = 0x1C1617;
        private const int OFF_BEG_SPELL_DATA = 0x62BC34;
        private const int OFF_END_SPELL_DATA = 0x62CC1D;
        private const int TOTAL_SPELL = 291;
        private const int SIZE_OF_SPELL_DATA = 14;

        List<string> spellNameList;
        private FileStream fs;

        public SpellList(String fileName)
        {
            fs = new FileStream(fileName, FileMode.Open);
            buildSpellNameList();
            buildSpellList();
            fs.Close();
        }

        public void buildSpellList()
        {
            Spell tempSpell;
            byte[] tempArray = new byte[14];
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(OFF_BEG_SPELL_DATA, 0);

            for (int i = 0; i < TOTAL_SPELL; i++)
            { 
                tempArray = br.ReadBytes(SIZE_OF_SPELL_DATA);
                tempSpell = new Spell(i, spellNameList[i], tempArray);
                this.Add(tempSpell);
                tempSpell = null;
            }

            tempSpell = null;
        }

        public void buildSpellNameList()
        {
            fs.Seek(OFF_BEG_SPELL_NAME, 0);
            BinaryReader br = new BinaryReader(fs);
            byte[] namesArray = br.ReadBytes(OFF_END_SPELL_NAME - OFF_BEG_SPELL_NAME);
            List<byte> currentArray = new List<byte>();
            spellNameList = new List<string>();
            string currentName;

            for (int i = 0; i < namesArray.Length; i++)
            {
                if (namesArray[i] == 0x0E)
                {
                    currentName = StringDecoding.decodeStringTable2(currentArray);
                    spellNameList.Add(currentName);
                    currentArray = new List<byte>();
                }
                else
                {
                    currentArray.Add(namesArray[i]);
                }
            }

            currentName = StringDecoding.decodeStringTable2(currentArray);
            spellNameList.Add(currentName);

            for (int i = 1; i <= 8; i++)
            {
                spellNameList.RemoveAt(283);
            }
        }

        public void writeSpellList(String fileName)
        {
            fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            byte[] tempArray;
            bw.Seek(OFF_BEG_SPELL_DATA, 0);

            for (int i = 0; i < TOTAL_SPELL; i++)
            {
                tempArray = this.ElementAt(i).fillArray();
                bw.Write(tempArray);
            }

            bw.BaseStream.Flush();
            fs.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
