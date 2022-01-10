using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFVIEditor
{
    public class TableEntry
    {
        byte hex;
        string text;

        public TableEntry(byte hex, string text)
        {
            this.hex = hex;
            this.text = text;
        }
    }
}
