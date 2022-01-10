using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    public class Item
    {
        public int itemNumber { get; set; }
        public string itemName { get; set; }
        public byte byte1 { get; set; }             // Item type (W, A, I)
        public byte byte2 { get; set; }             // Who can equip A (W, A)
        public byte byte3 { get; set; }             // Who can equip B (W, A)
        public byte byte4 { get; set; }             // Spell learn rate (W, A)
        public byte byte5 { get; set; }             // Magic spell to learn (W, A)
        public byte byte6 { get; set; }
        public byte byte7 { get; set; }
        public byte byte8 { get; set; }
        public byte byte9 { get; set; }
        public byte byte10 { get; set; }
        public byte byte11 { get; set; }
        public byte byte12 { get; set; }
        public byte byte13 { get; set; }
        public byte byte14 { get; set; }
        public byte byte15 { get; set; }
        public byte byte16 { get; set; }
        public byte byte17 { get; set; }
        public byte byte18 { get; set; }
        public byte byte19 { get; set; }
        public byte byte20 { get; set; }
        public byte byte21 { get; set; }
        public byte byte22 { get; set; }
        public byte byte23 { get; set; }
        public byte byte24 { get; set; }
        public byte byte25 { get; set; }
        public byte byte26 { get; set; }
        public byte byte27 { get; set; }
        public byte byte28 { get; set; }
        public int itemPrice { get; set; }

        public Item(int itemNumber, string itemName, byte[] data)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            fillAttributes(data);
        }

        public void fillAttributes(byte[] data)
        {
            byte1 = data[0]; byte2 = data[1]; byte3 = data[2]; byte4 = data[3];
            byte5 = data[4]; byte6 = data[5]; byte7 = data[6]; byte8 = data[7];
            byte9 = data[8]; byte10 = data[9]; byte11 = data[10]; byte12 = data[11];
            byte13 = data[12]; byte14 = data[13]; byte15 = data[14]; byte16 = data[15];
            byte17 = data[16]; byte18 = data[17]; byte19 = data[18]; byte20 = data[19];
            byte21 = data[20]; byte22 = data[21]; byte23 = data[22]; byte24 = data[23];
            byte25 = data[24]; byte26 = data[25]; byte27 = data[26]; byte28 = data[27];
            itemPrice = (int)data[28] + (int)data[29] * 256;
        }

        public byte[] fillArray()
        {
            byte[] array = new byte[30];

            array[0] = byte1; array[1] = byte2; array[2] = byte3; array[3] = byte4;
            array[4] = byte5; array[5] = byte6; array[6] = byte7; array[7] = byte8;
            array[8] = byte9; array[9] = byte10; array[10] = byte11; array[11] = byte12;
            array[12] = byte13; array[13] = byte14; array[14] = byte15; array[15] = byte16;
            array[16] = byte17; array[17] = byte18; array[18] = byte19; array[19] = byte20; 
            array[20] = byte21; array[21] = byte22; array[22] = byte23; array[23] = byte24; 
            array[24] = byte25; array[25] = byte26; array[26] = byte27; array[27] = byte28;
            array[28] = (byte)(itemPrice % 256);
            array[29] = (byte)((Math.Floor((double)(itemPrice / 256))));
             
            return array;
        }
    }

    public class ItemList : List<Item>, ICloneable
    {
        private const int OFF_BEG_ITEM_NAME = 0x1BF7D9;
        private const int OFF_END_ITEM_NAME = 0x1C0419;
        private const int OFF_BEG_ITEM_DATA = 0x62CC64;
        private const int OFF_END_ITEM_DATA = 0x62EA63;
        private const int TOTAL_ITEM = 271;
        private const int SIZE_OF_ITEM_DATA = 30;

        List<string> itemNameList;
        private FileStream fs;

        public ItemList(String fileName)
        {
            fs = new FileStream(fileName, FileMode.Open);
            buildItemNameList();
            builItemList();
            fs.Close();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void builItemList()
        {           
            Item tempItem;
            byte[] tempArray = new byte[30];
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(OFF_BEG_ITEM_DATA, 0);

            for (int i = 0; i < TOTAL_ITEM; i++)
            {
                tempArray = br.ReadBytes(SIZE_OF_ITEM_DATA);
                tempItem = new Item(i, itemNameList[i], tempArray);
                this.Add(tempItem);
                tempItem = null;
            }
        }

        public void buildItemNameList()
        {
            fs.Seek(OFF_BEG_ITEM_NAME, 0);
            BinaryReader br = new BinaryReader(fs);
            byte[] namesArray = br.ReadBytes(OFF_END_ITEM_NAME - OFF_BEG_ITEM_NAME);
            List<byte> currentArray = new List<byte>();
            itemNameList = new List<string>();
            string currentName;

            for(int i = 0; i < namesArray.Length; i++)
            {
                if(namesArray[i] == 0x0E)
                {
                    currentName = StringDecoding.decodeStringTable2(currentArray);
                    itemNameList.Add(currentName);
                    currentArray = new List<byte>();
                }
                else
                {
                    currentArray.Add(namesArray[i]);
                }
            }

            currentName = StringDecoding.decodeStringTable2(currentArray);
            itemNameList.Add(currentName);           
        }

        public void writeMonsterList(String fileName)
        {
            FileStream fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            byte[] tempArray;
            bw.Seek(OFF_BEG_ITEM_DATA, 0);

            for (int i = 0; i < TOTAL_ITEM; i++)
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
