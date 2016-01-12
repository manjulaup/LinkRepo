using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccountERP
    {
    public partial class blankForm : Form
        {
        public blankForm()
            {
            InitializeComponent();
            }

        private void blankForm_Paint(object sender, PaintEventArgs e)
            {
            panel1.Left = (this.Width - panel1.Width) / 2;
            panel1.Top = (this.Height - panel1.Height) / 2; 
            }
        }
    }
