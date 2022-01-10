using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace FFVIEditor
{
    public class Font
    {
        public List<FontCharacter> listCharacter { get; set; }
        int numberOfElements;
        public int fontHeight { get; private set; }
        byte[] header;
        byte[] pointers;
        int offset;
        public int fontDataSize { get; set; }
        private FileStream fs;
        private BinaryReader br;
        private BinaryWriter bw;

        public Font(string fileName, int offset)
        {
            fontDataSize = 0;
            this.offset = offset;
            listCharacter = new List<FontCharacter>();
            initializeElements(fileName, offset);
            fillList(fileName, offset);
        }

        public void initializeElements(string fileName, int offset)
        {
            fs = new FileStream(fileName, FileMode.Open);
            br = new BinaryReader(fs);

            fs.Seek(offset, 0);
            header = br.ReadBytes(0x010C);
            fontDataSize += 0x010C;
            fontHeight = (int)header[8];
            numberOfElements = (int)header[10] + (int)header[11] * 256;

            br.BaseStream.Flush();
            fs.Flush();
            br.Close();
            fs.Close();
        }

        public void fillList(string fileName, int offset)
        {
            fs = new FileStream(fileName, FileMode.Open);
            br = new BinaryReader(fs);

            byte[] tempPointer;
            byte tempPixel;
            byte tempColumn;
            byte[] tempData;

            for (int i = 0; i < numberOfElements; i++)
            {
                fs.Seek(offset + (4 * i) + 0x010C, 0);
                tempPointer = br.ReadBytes(0x04);
                fs.Seek(offset + (int)tempPointer[0] + (int)tempPointer[1] * 256, 0);
                tempPixel = br.ReadByte();
                tempColumn = br.ReadByte();
                tempData = br.ReadBytes(tempColumn * fontHeight);
                listCharacter.Add(new FontCharacter(tempPointer, tempData, tempPixel, tempColumn, fontHeight));
                fontDataSize += (tempData.Length + 6);
                tempData = null;
            }

            br.BaseStream.Flush();
            fs.Flush();
            br.Close();
            fs.Close();
        }

        public void fillPointers()
        {
            pointers = new byte[numberOfElements * 4];
            int value = header.Length + pointers.Length;

            for (int i = 0; i < numberOfElements; i++)
            {
                pointers[i * 4] = (byte)(value % 256);
                pointers[i * 4 + 1] = (byte)(Math.Floor((double)(value / 256)));
                pointers[i * 4 + 2] = 0x00;
                pointers[i * 4 + 3] = 0x00;

                value += (listCharacter[i].data.Length + 2);
            }
        }

        public void writeFont(String fileName)
        {
            fillPointers();

            fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Write);
            bw = new BinaryWriter(fs);

            bw.Seek(offset, 0);
            bw.Write(header);
            bw.Write(pointers);

            for (int i = 0; i < numberOfElements; i++)
            {
                bw.Write(listCharacter[i].pixels);
                bw.Write(listCharacter[i].columns);
                bw.Write(listCharacter[i].data);
            }

            bw.BaseStream.Flush();
            fs.Flush();
            bw.Close();
            fs.Close();
        }
    }
}
