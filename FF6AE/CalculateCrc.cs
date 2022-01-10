using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFVIEditor
{
    public class CalculateCrc
    {
        private Int32[] crc32Table;

        public CalculateCrc()
        {

            crc32Table = new Int32[256];
            Int32 dwPolynomial = -306674912;
            Int32 dwCrc;

            for (int i = 0; i <= 255; i++)
            {
                dwCrc = i;
                for (int j = 8; j >= 1; j += -1)
                {
                    if ((dwCrc & 1) == 1)
                    {
                        dwCrc = ((dwCrc & -2) / 2) & 2147483647;
                        dwCrc = dwCrc ^ dwPolynomial;
                    }
                    else
                    {
                        dwCrc = ((dwCrc & -2) / 2) & 2147483647;
                    }
                }
                crc32Table[i] = dwCrc;
            }
        }

        /// <summary>
        /// Calculate the CRC32 number with the CRC32 table created in the constructor.
        /// </summary>
        /// <param name="stream">The FileStream of the ROM. It's usually the first 8192 bytes</param>
        /// <returns>The CRC32 value</returns>
        public Int32 GetCrc32(FileStream stream)
        {
            Int32 result = -1;
            byte[] buffer = new byte[1024];
            int readSize = 1024;
            int count = stream.Read(buffer, 0, readSize);
            int i;
            Int32 iLookup;

            while ((count > 0))
            {
                for (i = 0; i <= count - 1; i++)
                {
                    iLookup = (result & 255) ^ buffer[i];
                    result = ((result & -256) / 256) & 16777215;
                    result = result ^ crc32Table[iLookup];
                }
                count = stream.Read(buffer, 0, readSize);
            }
            return result;
        }
    } 
}
