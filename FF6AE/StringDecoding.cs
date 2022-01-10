using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FFVIEditor
{
    public class StringDecoding
    {
        public static string[] tbl1 = 
        { 
            // 0x00 to 0x0F
            " ", "e", "a", "t", "<end>", "o", "r", "i", "n", "s", "l", "h", "d", "m", "u", "g",
            // 0x10 to 0x1F
            "c", ".", "p", "S", "w", "f", "A", "y", "C", "b", "M", "R", "T", "k", "v", "I",
            // 0x20 to 0x2F
            "O", "D", "P", "H", "B", "E", "F", "K", "N", "L", "G", "'", "U", "z", "W", "?",
            // 0x30 to 0x3F
            "Y", "V", "x", "-", "0", "Z", ":", ",", "1", "q", "J", "+", "2", "j", "Q", "5",
            // 0x40 to 0x4F
            "%", "3", "/", "4", "!", "X", "8", "7", "&", "\"", "6", "9", "A", "=", "<", "_",
            // 0x50 to 0x57
            ">", "K", "H", "O", "——", "<control pad>", "<up key>", "<right key>",
            // 0x58 to 0x5F
            "<down key>", "<left key>", "<N arrow>", "<NE arrow>", "<E arrow>", "<SE arrow>", "<S arrow>", "<SW arrow>",
            // 0x60 to 0x67
            "<W arrow>", "<NW arrow>", "<A button>", "<B button>", "<L button>", "<R button>", "ñ", " ", 
            // 0x68 to 0x6F
            "Z", "U", "I", "R", "W", "<Knife icon>", "<Sword icon>", "<Lance icon>",
            // 0x70 to 0x77
            "<Katana icon>", "<Rod icon>", "<Brush icon>", "<Star icon>", "<Special icon>", "<Gambler icon>", "<Claw icon>","<SHield icon>",
            // 0x78 to 0x7F
            "<Helmet icon>", "<Tool icon>", "<Scroll icon>", "<Relic icon>", "<Sword icon>", "<White Magic icon>", "<Black Magic icon>", "<Grey Magic icon>"
        };

        public static string[] tbl2= 
        {
            // 0x00 to 0x0F
            " ", "e", "t", "o", "a", "n", "r", "i", "s", "h", ".", "l", "u", "d", "<end>", "m",
            // 0x10 to 0x1F
            "g", "y", "w", "c", "f", "p", "!", "b", "'", ":", "k", "I", "A", "v", ",", "T",
            // 0x20 to 0x2F
            "?", "W", "S", "A", "H", "M", "G", "O", "D", "E", "B", "Y", "R", "N", "C", "L",
            // 0x30 to 0x3F
            "K", "<inverted triangle>", "F", "0", "-", "P", "U", "j", "z", "\"", "x", "*", "(", ")", "V", "q",
            // 0x40 to 0x4F
            "1", "J", "5", "Q", "Z", "2", "3", "4", "8", "6", "7", "X", "9", "=", "/", "%",
            // 0x50 to 0x57
            "&", "~", "+", "  ", "——", "<control pad>", "<up key>", "<right key>", 
            // 0x58 to 0x5F
            "<down key>", "<left key>", "<N arrow>", "<NE arrow>", "<E arrow>", "<SE arrow>", "<S arrow>", "<SW arrow>",
            // 0x60 to 0x67
            "<W arrow>", "<NW arrow>", "<A button>", "<B button>", "<L button>", "<R button>", "<Holy>", "<Lightning>",
            // 0x68 to 0x6F
            "<Wind>", "<Earth>", "<Ice>", "<Fire>", "<Water>", "<Poison>", "<musical note>", "ñ"
        };

        public static string decodeStringTable(List<byte> byteList)
        {
            string word = "";

            foreach (byte b in byteList)
            {
                word += tbl1[b];
            }

            return word;
        }

        public static string decodeStringTable2(List<byte> byteList)
        {
            string word = "";

            foreach (byte b in byteList)
            {
                word += tbl2[b];
            }

            return word;
        }
    }
}
