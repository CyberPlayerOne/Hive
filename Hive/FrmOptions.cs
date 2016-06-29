using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hive
{
    public partial class FrmOptions : Form
    {
        public FrmOptions()
        {
            InitializeComponent();
        }
//资源遵循即打开即关闭之原则，不浪费资源。（如数据库访问）

        private void FrmOptions_Load(object sender, EventArgs e)
        {
            tabControlOptions.DrawItem += new DrawItemEventHandler(tabControlOptions_DrawItem);
        }

        void tabControlOptions_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat sfForamt = new StringFormat();
            sfForamt.LineAlignment = StringAlignment.Center;
            sfForamt.Alignment = StringAlignment.Center;
            //sfForamt.FormatFlags = StringFormatFlags.DirectionVertical;
            TabControl tcTab = (TabControl)sender;
            e.Graphics.DrawString(tcTab.TabPages[e.Index].Text, SystemInformation.MenuFont, new SolidBrush(Color.Black), e.Bounds, sfForamt);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}