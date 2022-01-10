using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;

namespace FFVIEditor
{
    public class Monster
    {
        public String monsterName { get; set; }
        public int monsterNumber { get; set; }
        public int speed { get; set; }              // Byte 1
        public int battlePower { get; set; }        // Byte 2
        public int hitRate { get; set; }            // Byte 3
        public int attackBlock { get; set; }        // Byte 4
        public int magicBlock { get; set; }         // Byte 5
        public int attackDefense { get; set; }      // Byte 6
        public int magicDefense { get; set; }       // Byte 7
        public int magicPower { get; set; }         // Byte 8
        public int hp { get; set; }                 // Byte 9-10
        public int mp { get; set; }                 // Byte 11-12
        public int experience { get; set; }         // Byte 13-14
        public int gold { get; set; }               // Byte 15-16
        public int level { get; set; }              // Byte 17
        public byte morphTemplate { get; set; }     // Byte 18
        public byte byte19 { get; set; }            // Special status 1
        public byte byte20 { get; set; }            // Special status 2
        public byte byte21 { get; set; }            // Block status 1
        public byte byte22 { get; set; }            // Block status 2
        public byte byte23 { get; set; }            // Block status 3
        public byte byte24 { get; set; }            // Element absorb
        public byte byte25 { get; set; }            // Element nullify
        public byte byte26 { get; set; }            // Element weakness
        public byte byte27 { get; set; }            // Attack type
        public byte byte28 { get; set; }            // Status 1
        public byte byte29 { get; set; }            // Status 2
        public byte byte30 { get; set; }            // Status 3
        public byte byte31 { get; set; }            // Status 4
        public byte byte32 { get; set; }            // Special attack attribute

        public Monster(int monsterNumber, String monsterName, byte[] data)
        {
            this.monsterNumber = monsterNumber;
            this.monsterName = monsterName;
            fillAttributes(data);
        }

        public void fillAttributes(byte[] data)
        {
            speed = (int)data[0];
            battlePower = (int)data[1];
            hitRate = (int)data[2];
            attackBlock = (int)data[3];
            magicBlock = (int)data[4];
            attackDefense = (int)data[5];
            magicDefense = (int)data[6];
            magicPower = (int)data[7];
            hp = (int)data[8] + (int)data[9] * 256;
            mp = (int)data[10] + (int)data[11] * 256;
            experience = (int)data[12] + (int)data[13] * 256;
            gold = (int)data[14] + (int)data[15] * 256;
            level = (int)data[16];
            morphTemplate = data[17];
            byte19 = data[18]; byte20 = data[19]; byte21 = data[20]; byte22 = data[21];
            byte23 = data[22]; byte24 = data[23]; byte25 = data[24]; byte26 = data[25];
            byte27 = data[26]; byte28 = data[27]; byte29 = data[28]; byte30 = data[29];
            byte31 = data[30]; byte32 = data[31];
        }

        public byte[] fillArray()
        {
            byte[] array = new byte[32];

            array[0] = (byte)speed;
            array[1] = (byte)battlePower;    
            array[2] = (byte)hitRate;
            array[3] = (byte)attackBlock;
            array[4] = (byte)magicBlock;
            array[5] = (byte)attackDefense;
            array[6] = (byte)magicDefense;
            array[7] = (byte)magicPower;
            array[8] = (byte)(hp % 256); 
            array[9] = (byte)((Math.Floor((double)(hp / 256))));
            array[10] = (byte)(mp % 256); 
            array[11] = (byte)((Math.Floor((double)(mp / 256))));
            array[12] = (byte)(experience % 256); 
            array[13] = (byte)((Math.Floor((double)(experience / 256))));
            array[14] = (byte)(gold % 256);
            array[15] = (byte)((Math.Floor((double)(gold / 256))));
            array[16] = (byte)level;
            array[17] = (byte)morphTemplate;
            array[18] = byte19;
            array[19] = byte20; array[20] = byte21; array[21] = byte22; array[22] = byte23; 
            array[23] = byte24; array[24] = byte25; array[25] = byte26; array[26] = byte27;
            array[27] = byte28; array[28] = byte29; array[29] = byte30; array[30] = byte31; 
            array[31] = byte32;

            return array;
        }
    }

    public class MonsterList : List<Monster>
    {
        private const int OFF_BEG_MON_NAME = 0x16B02B;
        private const int OFF_END_MON_NAME = 0x16BFD5;
        private const int OFF_BEG_MON_DATA = 0x73798A;
        private const int OFF_END_MON_DATA = 0x73AE6A;
        private const int TOTAL_MON = 423;
        private const int SIZE_OF_MON_DATA = 32;
        
        
        List<string> monsterNameList;
        private FileStream fs;

        public MonsterList(String fileName)
        {
            fs = new FileStream(fileName, FileMode.Open);
            buildMonsterNameList();
            builMonsterList();
            fs.Close();
        }

        public void builMonsterList()
        {
            Monster tempMonster = null;
            byte[] tempArray = new byte[32];
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(OFF_BEG_MON_DATA, 0);

            for (int i = 0; i < TOTAL_MON; i++)
            {
                tempArray = br.ReadBytes(SIZE_OF_MON_DATA);
                tempMonster = new Monster(i, monsterNameList[i], tempArray);
                this.Add(tempMonster);
                tempMonster = null;
            }
        }

        public void buildMonsterNameList()
        {
            fs.Seek(OFF_BEG_MON_NAME, 0);
            BinaryReader br = new BinaryReader(fs);
            byte[] namesArray = br.ReadBytes(OFF_END_MON_NAME - OFF_BEG_MON_NAME);
            List<byte> currentArray = new List<byte>();
            monsterNameList = new List<string>();
            string currentName;

            for(int i = 0; i < namesArray.Length; i++)
            {
                if(namesArray[i] == 0x04)
                {
                    currentName = StringDecoding.decodeStringTable(currentArray);
                    monsterNameList.Add(currentName);
                    currentArray = new List<byte>();
                }
                else
                {
                    currentArray.Add(namesArray[i]);
                }
            }

            currentName = StringDecoding.decodeStringTable(currentArray);
            monsterNameList.Add(currentName);
        }

        public void writeMonsterList(String fileName)
        {
            FileStream fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            byte[] tempArray;

            for (int i = 0; i < TOTAL_MON; i++)
            {
                tempArray = this.ElementAt(i).fillArray();
                bw.Seek(OFF_BEG_MON_DATA + (i * SIZE_OF_MON_DATA), 0);
                bw.Write(tempArray);
            }

            bw.BaseStream.Flush();
            fs.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
