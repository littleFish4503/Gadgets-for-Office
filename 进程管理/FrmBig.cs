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
            this.TopLevel = true;
            listBox1.Items.Clear();
            websites = WebsiteService.GetWebsites();
            foreach (Website website in websites)
            {
                if (website.Name != "")
                { listBox1.Items.Add(website.Name); }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Website website = websites[listBox1.SelectedIndex];
            foreach (var x in websites)
            {
                if(x.Name==listBox1.SelectedItem.ToString()) { website = x; }
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
                try { Process.Start(url); } catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
