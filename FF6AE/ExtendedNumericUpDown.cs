using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FFVIEditor
{
    public partial class ExtendedNumericUpDown : NumericUpDown
    {
        public ExtendedNumericUpDown()
        {
            InitializeComponent();
        }

        public ExtendedNumericUpDown(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override void UpButton()
        {
            if (this.Value == this.Maximum)
                this.Value = this.Minimum;
            else
                base.UpButton();

            

        }

        public override void DownButton()
        {
            if (this.Value == this.Minimum)
                this.Value = this.Maximum;
            else
                base.DownButton();

            
        }
    }
}
