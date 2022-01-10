using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    class TextBlock
    {
        public List<TextEntry> teList{ get; set; }
        int nbElements;
        int strType;
        byte strEnd;
        byte[] tbPtr1;
        byte[] tbPtr2;
        byte[] tbPtr3;
        byte[] tbPtr4;
        byte[] ptrs;
        byte[] data;
        int offset;
        public int dataSize { get; set; }
        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;

        private const int TEXT1_OFFSET = 0x1654F8;
        private const int TEXT2_OFFSET = 0x174451;
        private const byte TEXT1_STR_END = 0x04;
        private const byte TEXT2_STR_END = 0x0E;
        private const int TEXT1_ELEMENTS = 2694;
        private const int TEXT2_ELEMENTS = 6114;

        public TextBlock(string fileName, int strType)
        {
            dataSize = 0;
            this.strType = strType;

            if (strType == 1)
            {
                strEnd = TEXT1_STR_END;
                nbElements = TEXT1_ELEMENTS;
                offset = TEXT1_OFFSET;
            }
            else if (strType == 2)
            {
                strEnd = TEXT2_STR_END;
                nbElements = TEXT2_ELEMENTS;
                offset = TEXT1_OFFSET;
            }

            teList = new List<TextEntry>();
            initializeElements(fileName, offset);
        }

        public void initializeElements(string fileName, int offset)
        {
            List<byte> tData = new List<byte>();
            int cmptr = 0;
            string tStr;
            fs = new FileStream(fileName, FileMode.Open);
            br = new BinaryReader(fs);

            fs.Seek(offset, 0);
            tbPtr1 = br.ReadBytes(0x04);
            tbPtr2 = br.ReadBytes(0x04);
            tbPtr3 = br.ReadBytes(0x04);
            tbPtr4 = br.ReadBytes(0x04);
            dataSize = (int)tbPtr4[0] + (int)tbPtr4[1]*256 + (int)tbPtr4[2]*65536;

            ptrs = br.ReadBytes(0x04 * nbElements);
            data = br.ReadBytes(dataSize);
            

            for (int i = 0; i < dataSize; i++)
            {
                if (data[i] == strEnd)
                {
                    if (strType == 1)
                        tStr = StringDecoding.decodeStringTable(tData);
                    else 
                        tStr = StringDecoding.decodeStringTable2(tData);

                    teList.Add(new TextEntry(cmptr, tData, tStr));
                    tData = new List<byte>();
                    cmptr++;
                }
                else
                    tData.Add(data[i]);
            }

            if (strType == 1)
                tStr = StringDecoding.decodeStringTable(tData);
            else
                tStr = StringDecoding.decodeStringTable2(tData);

            teList.Add(new TextEntry(cmptr, tData, tStr));
            
            br.BaseStream.Flush();
            fs.Flush();
            br.Close();
            fs.Close();
        }
    }
}
