using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 进程管理
{
    public partial class FrmBig : Form
    {
        #region 使其在alt+tab时不显示
        // 定义常量，用于设置窗口样式
        private const int WS_EX_TOOLWINDOW = 0x80;

        // 导入 Win32 API，用于设置窗口样式
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }
        #endregion
        public FrmBig()
        {
            InitializeComponent();
        }
        List<Website> websites;
        public void FrmBig_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopLevel = true;
            listBox1.Items.Clear();
            websites = WebsiteService.GetWebsites();
            foreach (Website website in websites)
            {
                if (website.Name != "")
                { listBox1.Items.Add(website.Name); }
            }

            //背景颜色更改
            string color = ConfigHelper.ReadConfig("backgroundColor");
            int startIndex = color.IndexOf("[") + 1;
            int endIndex = color.IndexOf("]");
            string colorName = color.Substring(startIndex, endIndex - startIndex);
            if (colorName == "Black") { listBox1.ForeColor = Color.White; }
            else { listBox1.ForeColor = Color.Black; }
            listBox1.BackColor = (Color)new ColorConverter().ConvertFromString(colorName);

            //字体
            Program.fontSize = float.Parse(ConfigHelper.ReadConfig("fontSize"));
            listBox1.Font = new Font("微软雅黑", Program.fontSize);
            label1.Font = new Font("微软雅黑", Program.fontSize);

            this.listBox1.SelectedIndex = 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if (index >= 0 && index < listBox1.Items.Count)
            {
                listBox1.SelectedIndex = index;
                listBox1.SetSelected(index, true);
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Website website = websites[listBox1.SelectedIndex];
                foreach (var x in websites)
                {
                    if (x.Name == listBox1.SelectedItem.ToString()) { website = x; }
                }


                // string url = websites[listBox1.SelectedIndex].Url;
                string url = website.Url;

                //浏览器不为空
                if (website.Browser != "")
                {
                    if (website.Browser == "IE")
                    {
                        Process.Start("iexplore.exe", url);
                    }
                    else if (website.Browser == "Edge")
                    {

                        Process.Start("microsoft-edge:" + url);

                    }
                    else { Process.Start("chrome.exe", url); }
                }
                else
                {
                    try
                    {
                        if (url.Contains(".exe"))
                        {
                            Process.Start(url);
                        }
                        else
                        {
                            Process.Start("Explorer.exe", url);
                        }

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                this.Hide();
            }
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            //listBox1.SelectedItems.Clear();

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if ((e.KeyCode == Keys.Down))
            {
                if (index == 0)
                {
                    listBox1.SetSelected(1, true);
                }

                else if (index!=0 && index < listBox1.Items.Count-1 )
                {
                    index += 1;
                    listBox1.SetSelected(index, true);
                }
                else
                {
                    listBox1.SetSelected(0, true);
                }

               
            }
            if ((e.KeyCode == Keys.Enter))
            {
                MouseEventArgs e1 = new MouseEventArgs(MouseButtons.Left,1,0,0,1);
                this.listBox1_MouseDown(sender, e1);
                this.Hide();
            }

        }
    }
}
