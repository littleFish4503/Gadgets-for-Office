using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 进程管理
{
    public partial class FrmNormal : Form
    {
        public FrmNormal()
        {
            InitializeComponent();
        }

        private void checkBox_main_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_main.Checked) { Program.isShowMainInStart = true; }
            else { Program.isShowMainInStart = false; }
            ConfigHelper.WriteConfig("isShowMainInStart", Program.isShowMainInStart.ToString());
        }

        private void FrmNormal_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
            checkBox_main.Checked = Program.isShowMainInStart;
            numericUpDown1.Value = Convert.ToDecimal(ConfigHelper.ReadConfig("fontSize"));

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                Program.backgroundColor = Color.White;

            }
            else if (comboBox1.SelectedIndex == 1) { Program.backgroundColor = Color.Black; }
            else if (comboBox1.SelectedIndex == 2) { Program.backgroundColor = Color.Gray; }
            else { Program.backgroundColor = Color.LightSkyBlue; }
            ConfigHelper.WriteConfig("backgroundColor", Program.backgroundColor.ToString());
            frmBig.FrmBig_Load(sender, e);
        }
        FrmBig frmBig = new FrmBig();
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.fontSize = (float)numericUpDown1.Value;
            ConfigHelper.WriteConfig("fontSize", Program.fontSize.ToString());

            frmBig.FrmBig_Load(sender, e);
        }
    }
}
