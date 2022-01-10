using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FFVIEditor
{
    public class FontCharacter
    {

        public byte[] pointer { get; set; }
        public byte[] data { get; set; }
        public byte pixels { get; set; }
        public byte columns { get; set; }
        public Bitmap bitmap { get; set; }
        public Bitmap bitmapDouble { get; set; }
        LockBitmap lockBitmap;

        public FontCharacter(byte[] pointer, byte[] data, byte pixel, byte columns, int fontHeight)
        {
            this.pointer = pointer;
            this.data = data;
            this.pixels = pixel;
            this.columns = columns;
            createBitmap(fontHeight);
            bitmapDouble = createBitmapCopy(bitmap.Width, bitmap.Height, 2);
        }

        private bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public void createBitmap(int fontHeight)
        {
            bitmap = new Bitmap(columns * 4, fontHeight);
            lockBitmap = new LockBitmap(bitmap);
            lockBitmap.LockBits();

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!IsBitSet(data[i], j * 2) && !IsBitSet(data[i], j * 2 + 1))
                        lockBitmap.SetPixel((int)(j + (i % columns) * 4), (int)(Math.Floor((double)(i / columns))), Color.Blue);
                    else if (IsBitSet(data[i], j * 2) && !IsBitSet(data[i], j * 2 + 1))
                        lockBitmap.SetPixel((int)(j + (i % columns) * 4), (int)(Math.Floor((double)(i / columns))), Color.White);
                    else if (!IsBitSet(data[i], j * 2) && IsBitSet(data[i], j * 2 + 1))
                        lockBitmap.SetPixel((int)(j + (i % columns) * 4), (int)(Math.Floor((double)(i / columns))), Color.Black);
                    else if (IsBitSet(data[i], j * 2) && IsBitSet(data[i], j * 2 + 1))
                        lockBitmap.SetPixel((int)(j + (i % columns) * 4), (int)(Math.Floor((double)(i / columns))), Color.Gray);
                }
            }

            lockBitmap.UnlockBits();
        }

        public Bitmap createBitmapCopy(int width, int height, int factor)
        {
            Bitmap tempBitmap = new Bitmap(width * factor, height * factor);
            lockBitmap = new LockBitmap(tempBitmap);
            lockBitmap.LockBits();

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    for (int k = 0; k < factor; k++)
                    {
                        for (int l = 0; l < factor; l++)
                        {
                            lockBitmap.SetPixel(i * factor + l, j * factor + k, bitmap.GetPixel(i, j));
                        }
                    }
                }
            }

            lockBitmap.UnlockBits();

            return tempBitmap;
        }



    }
}
