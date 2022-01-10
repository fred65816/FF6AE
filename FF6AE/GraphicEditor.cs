using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFVIEditor
{
    public partial class GraphicEditor : Form
    {
        private const int CELL_WIDTH = 12;                      // Grid cell width/height
        private const int PAL_CELL_WIDTH = 24;                  // Palette cell width/height

        private const int LF_GRID_SIZE = 144;                   // Small Font grid width/height
        private const int LF_DATA_SIZE = 0x282A;                // Small Font original data size

        private const int SF_GRID_WIDTH = 144;
        private const int SF_GRID_HEIGHT = 96;
        private const int SF_DATA_SIZE = 0x0CDC;

        private Color currentColor;
        private Bitmap bitmapPalWhite;
        private Bitmap bitmapPalBlack;
        private Bitmap bitmapPalGray;
        private Bitmap bitmapPalBlue;
        private Bitmap bitmapPixSelection;
        private Bitmap currentLFBitmap;
        private Bitmap currentSFBitmap;
        private Bitmap bitmapLFGrid;
        private Bitmap bitmapSFGrid;
        private bool valueChange;

        public GraphicEditor()
        {
            this.Hide();
            InitializeComponent();
            initializeBitmaps();
            initializeLargeFont();
            this.Show();
        }

        public void initializeBitmaps()
        {
            bitmapPalWhite = createBitmap(PAL_CELL_WIDTH, PAL_CELL_WIDTH, Color.White);
            bitmapPalBlack = createBitmap(PAL_CELL_WIDTH, PAL_CELL_WIDTH, Color.Black);
            bitmapPalGray = createBitmap(PAL_CELL_WIDTH, PAL_CELL_WIDTH, Color.Gray);
            bitmapPalBlue = createBitmap(PAL_CELL_WIDTH, PAL_CELL_WIDTH, Color.Blue);
            bitmapPixSelection = createGrid(CELL_WIDTH, CELL_WIDTH, CELL_WIDTH, Color.DarkGray, 1);
            bitmapLFGrid = createGrid(LF_GRID_SIZE, LF_GRID_SIZE, CELL_WIDTH, Color.DarkGray, 1);
            bitmapSFGrid = createGrid(SF_GRID_WIDTH, SF_GRID_HEIGHT, CELL_WIDTH, Color.DarkGray, 1);
        }

        public void initializeLargeFont()
        {
            fillDataGridView(dgvLargeFont, InstanceObjects.largeFont);
            pbLFWhite.Image = bitmapPalWhite;
            pbLFBlack.Image = bitmapPalBlack;
            pbLFGrey.Image = bitmapPalGray;
            pbLFBlue.Image = bitmapPalBlue;
            currentColor = Color.White;
            calculateDifference(LF_DATA_SIZE, InstanceObjects.largeFont.fontDataSize, lblLFByteLeft);
            dgvLargeFont.Rows[0].Selected = true;
            switchLFPBBitmap(0);
        }

        private void initializeSmallFont()
        {
            fillDataGridView(dgvSmallFont, InstanceObjects.smallFont);
            pbSFWhite.Image = bitmapPalWhite;
            pbSFBlack.Image = bitmapPalBlack;
            pbSFGrey.Image = bitmapPalGray;
            pbSFBlue.Image = bitmapPalBlue;
            currentColor = Color.White;
            calculateDifference(SF_DATA_SIZE, InstanceObjects.smallFont.fontDataSize, lblSFByteLeft);
            dgvSmallFont.Rows[0].Selected = true;
            switchSFPBBitmap(0);
        }

        private void fillDataGridView(DataGridView dgv, Font font)
        {
            DataGridViewRow dgvRow = new DataGridViewRow();

            dgv.Columns.Add("BitmapColumn", "Bitmap");
            dgv.Columns.Add("ColumnsColumn", "Column(s)");
            dgv.Columns.Add("PixelsColumn", "Pixels");
            dgv.Columns[0].Width = 45;
            dgv.Columns[1].Width = 60;
            dgv.Columns[2].Width = 50;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv.Columns[0].DefaultCellStyle.BackColor = Color.Blue;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < font.listCharacter.Count; i++)
            {
                dgvRow = new DataGridViewRow();
                dgvRow.Cells.Add(new DataGridViewImageCell { Value = font.listCharacter.ElementAt(i).bitmapDouble });
                dgvRow.Cells.Add(new DataGridViewTextBoxCell { Value = font.listCharacter.ElementAt(i).columns.ToString() });
                dgvRow.Cells.Add(new DataGridViewTextBoxCell { Value = font.listCharacter.ElementAt(i).pixels.ToString() });
                dgv.Rows.Add(dgvRow);
            }
            
        }

        private void calculateDifference(int dataSize, int fontSize, Label label)
        {
            int difference = dataSize - fontSize;

            if (difference == 0)
                label.ForeColor = Color.Black;
            else if (difference > 0)
                label.ForeColor = Color.Green;
            else
                label.ForeColor = Color.Red;

            label.Text = difference.ToString();
        }

        public Bitmap drawBitmapOver(Bitmap backBitmap, Bitmap frontBitmap)
        {
            Bitmap bitmap = new Bitmap(backBitmap.Width, backBitmap.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            g.DrawImage(backBitmap, new Point(0, 0));
            g.DrawImage(frontBitmap, new Point(0, 0));

            return bitmap;
        }

        public Bitmap drawBitmapOver(Bitmap backBitmap, Bitmap frontBitmap, int x, int y)
        {
            Graphics g = Graphics.FromImage(backBitmap);
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            g.DrawImage(frontBitmap, new Point(x, y));

            return backBitmap;
        }

        public Bitmap cropBitmap(Bitmap source, int width, int height)
        {
            Rectangle cropRect = new Rectangle(0, 0, width, height);
            Bitmap target = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(source, new Rectangle(0, 0, width, height), cropRect, GraphicsUnit.Pixel);
            }

            return target;
        }

        public Bitmap createBitmap(int width, int height, Color color)
        {
            Bitmap tempBitmap = new Bitmap(width, height);
            LockBitmap lockBitmap = new LockBitmap(tempBitmap);
            lockBitmap.LockBits();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    lockBitmap.SetPixel(i, j, color);
                }
            }

            lockBitmap.UnlockBits();

            return tempBitmap;
        }

        public Bitmap createGrid(int width, int height, int factor, Color color, int penWidth)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            Pen p = new Pen(color, penWidth);

            for (int i = 0; i < width; i+= factor)
            {
                g.DrawLine(p, i, 0, i, height);
            }

            for (int i = 0; i < height; i += factor)
            {
                g.DrawLine(p, 0, i, width, i);
            }

            return bitmap;
        }

        public byte[] createData(int height, int columns, Bitmap bitmap)
        {
            int arraySize = height * columns;
            byte[] tempData = new byte[arraySize];
            int totalWidth = columns * 12 * 4;
            int totalHeight = height * 12;
            int x; int y; int indice; int hex;

            for (int i = 0; i < arraySize; i++)
            {
                tempData[i] = 0x00;
            }

            LockBitmap lockBitmap = new LockBitmap(bitmap);
            lockBitmap.LockBits();

            for (int i = 6; i < totalHeight; i += 12)
            {
                y = (i - 6) / 12;

                for (int j = 6; j < totalWidth; j += 12)
                {
                    x = (j - 6) / 12;
                    indice = (int)(Math.Floor((double)(x / 4)) + y * columns);
                    hex = (int)(Math.Pow(4, x % 4));

                    if (lockBitmap.GetPixel(j, i).ToArgb() == Color.White.ToArgb())
                        tempData[indice] += (byte)(0x01 * hex);
                    else if (lockBitmap.GetPixel(j, i).ToArgb() == Color.Black.ToArgb())
                        tempData[indice] += (byte)(0x02 * hex);
                    else if (lockBitmap.GetPixel(j, i).ToArgb() == Color.Gray.ToArgb())
                        tempData[indice] += (byte)(0x03 * hex);
                }
            }

            lockBitmap.UnlockBits();

            return tempData;
        }

        private void close()
        {
            int byteLF;
            int byteSF;
            bool boolLF = int.TryParse(lblLFByteLeft.Text, out byteLF);
            bool boolSF = int.TryParse(lblSFByteLeft.Text, out byteSF);

            if (boolLF && byteLF < 0)
                MessageBox.Show("The large font data is too big (" + lblLFByteLeft.Text + " bytes). You will need to make the large font data smaller in order to be able to save the ROM.");
            else if (boolSF && byteSF < 0)
                MessageBox.Show("The small font data is too big (" + lblSFByteLeft.Text + " bytes). You will need to make the small font data smaller in order to be able to save the ROM.");
            else
                this.Hide();
        }

        private Bitmap createPBBitmap(int bitMapWidth, int bitmapHeight, int index, List<FontCharacter> list, DataGridView dgv, Bitmap grid)
        {
            Bitmap backBitmap = createBitmap(bitMapWidth, bitmapHeight, Color.Green);

            int width = list.ElementAt(index).bitmap.Width;
            int height = list.ElementAt(index).bitmap.Height;
            Bitmap frontBitmap = list.ElementAt(index).createBitmapCopy(width, height, CELL_WIDTH);

            backBitmap = drawBitmapOver(backBitmap, frontBitmap);
            backBitmap = drawBitmapOver(backBitmap, grid);

            return backBitmap;
        }

        private void switchLFPBBitmap(int index)
        {
            valueChange = false;
            int pixels = InstanceObjects.largeFont.listCharacter.ElementAt(index).pixels;
            int columns = InstanceObjects.largeFont.listCharacter.ElementAt(index).columns;
            numLFPixels.Maximum = columns * 4;
            numLFPixels.Value = pixels;
            nudLFColumns.Value = columns;

            if(dgvLargeFont.CurrentRow != null)
                dgvLargeFont.CurrentRow.Selected = true;

            currentLFBitmap = createPBBitmap(LF_GRID_SIZE, LF_GRID_SIZE, index, InstanceObjects.largeFont.listCharacter, dgvLargeFont, bitmapLFGrid);
            pbLargeFont.Image = currentLFBitmap;
            valueChange = true;
        }

        private void switchSFPBBitmap(int index)
        {
            valueChange = false;
            int pixels = InstanceObjects.smallFont.listCharacter.ElementAt(index).pixels;
            int columns = InstanceObjects.smallFont.listCharacter.ElementAt(index).columns;
            nudSFPixels.Maximum = columns * 4;
            nudSFPixels.Value = pixels;
            nudSFColumns.Value = columns;

            if (dgvSmallFont.CurrentRow != null)
                dgvSmallFont.CurrentRow.Selected = true;

            currentSFBitmap = createPBBitmap(SF_GRID_WIDTH, SF_GRID_HEIGHT, index, InstanceObjects.smallFont.listCharacter, dgvSmallFont, bitmapSFGrid);
            pbSmallFont.Image = currentSFBitmap;
            valueChange = true;
        }

        private void drawLFPixels(int limit, MouseEventArgs e)
        {
            if (e.Location.X < limit)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    int positionX = e.Location.X - (e.Location.X % CELL_WIDTH);
                    int positionY = e.Location.Y - (e.Location.Y % CELL_WIDTH);
                    Bitmap colorBitmap = createBitmap(CELL_WIDTH, CELL_WIDTH, currentColor);
                    colorBitmap = drawBitmapOver(colorBitmap, bitmapPixSelection, 0, 0);
                    currentLFBitmap = drawBitmapOver(currentLFBitmap, colorBitmap, positionX, positionY);
                    pbLargeFont.Image = currentLFBitmap;
                }
            }
        }

        private void drawSFPixels(int limit, MouseEventArgs e)
        {
            if (e.Location.X < limit)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    int positionX = e.Location.X - (e.Location.X % CELL_WIDTH);
                    int positionY = e.Location.Y - (e.Location.Y % CELL_WIDTH);
                    Bitmap colorBitmap = createBitmap(CELL_WIDTH, CELL_WIDTH, currentColor);
                    colorBitmap = drawBitmapOver(colorBitmap, bitmapPixSelection, 0, 0);
                    currentSFBitmap = drawBitmapOver(currentSFBitmap, colorBitmap, positionX, positionY);
                    pbSmallFont.Image = currentSFBitmap;
                }
            }
        }

        private void tabControlGraphics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlGraphics.SelectedIndex == 1)
            {
                if (dgvSmallFont.ColumnCount == 0)
                    initializeSmallFont();
            }
        }

        private void pbLargeFont_MouseClick(object sender, MouseEventArgs e)
        {
            int limit = (int)(nudLFColumns.Value * 4 * CELL_WIDTH);
            drawLFPixels(limit, e);
        }

        private void pbLargeFont_MouseMove(object sender, MouseEventArgs e)
        {
            int limit = (int)(nudLFColumns.Value * 4 * CELL_WIDTH);
            drawLFPixels(limit, e);
        }

        private void dgvLargeFont_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switchLFPBBitmap(dgvLargeFont.CurrentRow.Index);
        }

        private void pbLFBlack_Click(object sender, EventArgs e)
        {
            currentColor = Color.Black;
        }

        private void pbLFWhite_Click(object sender, EventArgs e)
        {
            currentColor = Color.White;
        }

        private void pbLFGrey_Click(object sender, EventArgs e)
        {
            currentColor = Color.Gray;
        }

        private void pbLFBlue_Click(object sender, EventArgs e)
        {
            currentColor = Color.Blue;
        }

        private void btnLFClose_Click(object sender, EventArgs e)
        {
            close();
        }

        private void nudLFColumns_ValueChanged(object sender, EventArgs e)
        {
            if (valueChange)
            {
                int previousValue = Convert.ToInt32(((NumericUpDown)sender).Text);
                int actualValue = (int)((NumericUpDown)sender).Value;

                if (previousValue > actualValue)
                {
                    Bitmap backBitmap = createBitmap(LF_GRID_SIZE, LF_GRID_SIZE, Color.Green);
                    Bitmap letterBitmap = cropBitmap(currentLFBitmap, actualValue * 4 * CELL_WIDTH, 144);
                    backBitmap = drawBitmapOver(backBitmap, letterBitmap);
                    currentLFBitmap = drawBitmapOver(backBitmap, bitmapLFGrid);
                    pbLargeFont.Image = currentLFBitmap;

                    numLFPixels.Maximum = actualValue * 4;
                }
                else
                {
                    Bitmap backBitmap = createBitmap(LF_GRID_SIZE, LF_GRID_SIZE, Color.Green);
                    Bitmap frontBitMap = createBitmap((int)(actualValue * 4 * 12), LF_GRID_SIZE, Color.Blue);
                    Bitmap letterBitmap = cropBitmap(currentLFBitmap, previousValue * 4 * CELL_WIDTH, 144);
                    backBitmap = drawBitmapOver(backBitmap, frontBitMap);
                    backBitmap = drawBitmapOver(backBitmap, letterBitmap);
                    currentLFBitmap = drawBitmapOver(backBitmap, bitmapLFGrid);
                    pbLargeFont.Image = currentLFBitmap;

                    if (numLFPixels.Value > actualValue * 4)
                        numLFPixels.Value = actualValue * 4;

                    numLFPixels.Maximum = actualValue * 4;
                }
            }
        }

        private void btnLFSave_Click(object sender, EventArgs e)
        {
            int columns = (int)(nudLFColumns.Value);
            int pixels = (int)(numLFPixels.Value);
            int height = InstanceObjects.largeFont.fontHeight;
            byte[] data = createData(height, columns, currentLFBitmap);

            int difference = (columns - InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).columns) * InstanceObjects.largeFont.fontHeight;
            InstanceObjects.largeFont.fontDataSize += difference;
            calculateDifference(LF_DATA_SIZE, InstanceObjects.largeFont.fontDataSize, lblLFByteLeft);

            InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).data = data;
            InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).columns = (byte)columns;
            InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).pixels = (byte)pixels;
            InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).createBitmap(height);

            int bitmapWidth = InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).bitmap.Width;
            int BitmapHeight = InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).bitmap.Height;
            Bitmap bitmapDouble = InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).createBitmapCopy(bitmapWidth, BitmapHeight, 2);
            InstanceObjects.largeFont.listCharacter.ElementAt(dgvLargeFont.CurrentRow.Index).bitmapDouble = bitmapDouble;

            dgvLargeFont.CurrentRow.Cells[0].Value = bitmapDouble;
            dgvLargeFont.CurrentRow.Cells[1].Value = columns;
            dgvLargeFont.CurrentRow.Cells[2].Value = pixels;
        }

        private void pbSmallFont_MouseClick(object sender, MouseEventArgs e)
        {
            int limit = (int)(nudSFColumns.Value * 4 * CELL_WIDTH);
            drawSFPixels(limit, e);
        }

        private void pbSmallFont_MouseMove(object sender, MouseEventArgs e)
        {
            int limit = (int)(nudSFColumns.Value * 4 * CELL_WIDTH);
            drawSFPixels(limit, e);
        }

        private void dgvSmallFont_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switchSFPBBitmap(dgvSmallFont.CurrentRow.Index);
        }

        private void pbSFBlack_Click(object sender, EventArgs e)
        {
            currentColor = Color.Black;
        }

        private void pbSFWhite_Click(object sender, EventArgs e)
        {
            currentColor = Color.White;
        }

        private void pbSFGrey_Click(object sender, EventArgs e)
        {
            currentColor = Color.Gray;
        }

        private void pbSFBlue_Click(object sender, EventArgs e)
        {
            currentColor = Color.Blue;
        }

        private void btnSFClose_Click(object sender, EventArgs e)
        {
            close();
        }

        private void nudSFColumns_ValueChanged(object sender, EventArgs e)
        {
            if (valueChange)
            {
                int previousValue = Convert.ToInt32(((NumericUpDown)sender).Text);
                int actualValue = (int)((NumericUpDown)sender).Value;

                if (previousValue > actualValue)
                {
                    Bitmap backBitmap = createBitmap(SF_GRID_WIDTH, SF_GRID_HEIGHT, Color.Green);
                    Bitmap letterBitmap = cropBitmap(currentSFBitmap, actualValue * 4 * CELL_WIDTH, 144);
                    backBitmap = drawBitmapOver(backBitmap, letterBitmap);
                    currentSFBitmap = drawBitmapOver(backBitmap, bitmapSFGrid);
                    pbSmallFont.Image = currentSFBitmap;

                    nudSFPixels.Maximum = actualValue * 4;
                }
                else
                {
                    Bitmap backBitmap = createBitmap(SF_GRID_WIDTH, SF_GRID_HEIGHT, Color.Green);
                    Bitmap frontBitMap = createBitmap((int)(actualValue * 4 * 12), SF_GRID_HEIGHT, Color.Blue);
                    Bitmap letterBitmap = cropBitmap(currentSFBitmap, previousValue * 4 * CELL_WIDTH, 144);
                    backBitmap = drawBitmapOver(backBitmap, frontBitMap);
                    backBitmap = drawBitmapOver(backBitmap, letterBitmap);
                    currentSFBitmap = drawBitmapOver(backBitmap, bitmapSFGrid);
                    pbSmallFont.Image = currentSFBitmap;

                    if (nudSFPixels.Value > actualValue * 4)
                        nudSFPixels.Value = actualValue * 4;

                    nudSFPixels.Maximum = actualValue * 4;
                }
            }
        }

        private void btnSFSave_Click(object sender, EventArgs e)
        {
            int columns = (int)(nudSFColumns.Value);
            int pixels = (int)(nudSFPixels.Value);
            int height = InstanceObjects.smallFont.fontHeight;
            byte[] data = createData(height, columns, currentSFBitmap);

            int difference = (columns - InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).columns) * InstanceObjects.smallFont.fontHeight;
            InstanceObjects.smallFont.fontDataSize += difference;
            calculateDifference(SF_DATA_SIZE, InstanceObjects.smallFont.fontDataSize, lblSFByteLeft);

            InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).data = data;
            InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).columns = (byte)columns;
            InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).pixels = (byte)pixels;
            InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).createBitmap(height);

            int bitmapWidth = InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).bitmap.Width;
            int bitmapHeight = InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).bitmap.Height;
            Bitmap bitmapDouble = InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).createBitmapCopy(bitmapWidth, bitmapHeight, 2);
            InstanceObjects.smallFont.listCharacter.ElementAt(dgvSmallFont.CurrentRow.Index).bitmapDouble = bitmapDouble;

            dgvSmallFont.CurrentRow.Cells[0].Value = bitmapDouble;
            dgvSmallFont.CurrentRow.Cells[1].Value = columns;
            dgvSmallFont.CurrentRow.Cells[2].Value = pixels;
        }
















    }
}
