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
            if(checkBox_main.Checked) { Program.isShowMainInStart = true; }
            else { Program.isShowMainInStart = false; }
            ConfigHelper.WriteConfig("isShowMainInStart",Program.isShowMainInStart.ToString());
        }

        private void FrmNormal_Load(object sender, EventArgs e)
        {
            checkBox_main.Checked = Program.isShowMainInStart; 
        }
    }
}
