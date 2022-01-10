using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    public class BattleCommand
    {
        public int commandNumber { get; set; }
        public string commandName { get; set; }

        public BattleCommand(int commandNumber, string commandName)
        {
            this.commandNumber = commandNumber;
            this.commandName = commandName;
        }
    }

    public class BattleCommandList : List<BattleCommand>, ICloneable
    {
        private const int OFF_BEG_COMMAND_NAME = 0x16911C;
        private const int OFF_END_COMMAND_NAME = 0x1691DB;
        private const int TOTAL_COMMAND = 30;

        List<string> commandNameList;
        private FileStream fs;

        public BattleCommandList(String fileName)
        {
            fs = new FileStream(fileName, FileMode.Open);
            buildCommandNameList();
            buildCommandList();
            fs.Close();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void buildCommandList()
        {
            BattleCommand tempCommand;

            for (int i = 0; i < TOTAL_COMMAND; i++)
            {
                tempCommand = new BattleCommand(i, commandNameList[i]);
                this.Add(tempCommand);
                tempCommand = null;
            }

            this.Add(new BattleCommand(30, "Empty"));
        }

        public void buildCommandNameList()
        {
            fs.Seek(OFF_BEG_COMMAND_NAME, 0);
            BinaryReader br = new BinaryReader(fs);
            byte[] namesArray = br.ReadBytes(OFF_END_COMMAND_NAME - OFF_BEG_COMMAND_NAME);
            List<byte> currentArray = new List<byte>();
            commandNameList = new List<string>();
            string currentName;

            for (int i = 0; i < namesArray.Length; i++)
            {
                if (namesArray[i] == 0x04)
                {
                    currentName = StringDecoding.decodeStringTable(currentArray);
                    commandNameList.Add(currentName);
                    currentArray = new List<byte>();
                }
                else
                {
                    currentArray.Add(namesArray[i]);
                }
            }

            currentName = StringDecoding.decodeStringTable(currentArray);
            commandNameList.Add(currentName);
        }
    }
}
