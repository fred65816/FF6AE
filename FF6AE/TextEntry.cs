using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFVIEditor
{
    class TextEntry
    {
        int nbr;
        List<byte> data;
        string str;

        public TextEntry(int nbr, List<byte> data, string str)
        {
            this.nbr = nbr;
            this.data = data;
            this.str = str;
        }
    }
}
