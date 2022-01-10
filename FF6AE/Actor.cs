using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    public class Actor
    {
        public int actorNumber { get; set; }
        public string actorName { get; set; }
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
        public byte byte15 { get; set; }
        public byte byte16 { get; set; }
        public byte byte17 { get; set; }
        public byte byte18 { get; set; }
        public byte byte19 { get; set; }
        public byte byte20 { get; set; }
        public byte byte21 { get; set; }
        public byte byte22 { get; set; }

        public Actor(int actorNumber, string actorName, byte[] data)
        {
            this.actorNumber =actorNumber;
            this.actorName = actorName;
            fillAttributes(data);
        }

        public void fillAttributes(byte[] data)
        {
            byte1 = data[0]; byte2 = data[1]; byte3 = data[2]; byte4 = data[3];
            byte5 = data[4]; byte6 = data[5]; byte7 = data[6]; byte8 = data[7];
            byte9 = data[8]; byte10 = data[9]; byte11 = data[10]; byte12 = data[11];
            byte13 = data[12]; byte14 = data[13]; byte15 = data[14]; byte16 = data[15];
            byte17 = data[16]; byte18 = data[17]; byte19 = data[18]; byte20 = data[19];
            byte21 = data[20]; byte22 = data[21]; 
        }

        public byte[] fillArray()
        {
            byte[] array = new byte[22];

            array[0] = byte1; array[1] = byte2; array[2] = byte3; array[3] = byte4;
            array[4] = byte5; array[5] = byte6; array[6] = byte7; array[7] = byte8;
            array[8] = byte9; array[9] = byte10; array[10] = byte11; array[11] = byte12;
            array[12] = byte13; array[13] = byte14; array[14] = byte15; array[15] = byte16;
            array[16] = byte17; array[17] = byte18; array[18] = byte19; array[19] = byte20;
            array[20] = byte21; array[21] = byte22; 

            return array;
        }
    }

    public class ActorList : List<Actor>
    {
        private const int OFF_BEG_ACTOR_NAME = 0x168FBF;
        private const int OFF_END_ACTOR_NAME = 0x16911B;
        private const int OFF_BEG_ACTOR_DATA = 0x62ACAC;
        private const int OFF_END_ACTOR_DATA = 0x62B22A;
        private const int TOTAL_ACTOR = 64;
        private const int SIZE_OF_ACTOR_DATA = 22;

        List<string> actorNameList;
        private FileStream fs;

        public ActorList(String fileName)
        {
            fs = new FileStream(fileName, FileMode.Open);
            buildActorNameList();
            builActorList();
            fs.Close();
        }

        public void builActorList()
        {
            Actor tempActor;
            byte[] tempArray = new byte[22];
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(OFF_BEG_ACTOR_DATA, 0);

            for (int i = 0; i < TOTAL_ACTOR; i++)
            {
                tempArray = br.ReadBytes(SIZE_OF_ACTOR_DATA);
                tempActor = new Actor(i, actorNameList[i], tempArray);
                this.Add(tempActor);
                tempActor = null;
            }
        }

        public void buildActorNameList()
        {
            fs.Seek(OFF_BEG_ACTOR_NAME, 0);
            BinaryReader br = new BinaryReader(fs);
            byte[] namesArray = br.ReadBytes(OFF_END_ACTOR_NAME - OFF_BEG_ACTOR_NAME);
            List<byte> currentArray = new List<byte>();
            actorNameList = new List<string>();
            string currentName;

            for (int i = 0; i < namesArray.Length; i++)
            {
                if (namesArray[i] == 0x04)
                {
                    currentName = StringDecoding.decodeStringTable(currentArray);
                    actorNameList.Add(currentName);
                    currentArray = new List<byte>();
                }
                else
                {
                    currentArray.Add(namesArray[i]);
                }
            }

            currentName = StringDecoding.decodeStringTable(currentArray);
            actorNameList.Add(currentName);
        }

        public void writeActorList(String fileName)
        {
            FileStream fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            byte[] tempArray;
            bw.Seek(OFF_BEG_ACTOR_DATA, 0);

            for (int i = 0; i < TOTAL_ACTOR; i++)
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
