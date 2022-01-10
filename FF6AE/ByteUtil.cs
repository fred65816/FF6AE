using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    static class ByteUtil
    {
        public static byte[] ReadOffsetsDifference(FileStream fs, int startingOffset, int finishingOffset)
        {
            BinaryReader br = new BinaryReader(fs);
            byte[] bytesArray = br.ReadBytes(finishingOffset - startingOffset);
            
            return bytesArray;
        }
    }
}
