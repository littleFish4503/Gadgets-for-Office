using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 进程管理
{
    public partial class FrmMiniFloat : Form
    {
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

        public FrmMiniFloat()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
        }

        private void FrmMiniFloat_Load(object sender, EventArgs e)
        {
            //FrmBig frmBig=new FrmBig();
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
            this.Width = 50; this.Height = 50;
            this.TopMost = true;
            this.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - this.Width - 20,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }
        #region 拖动小球
        private bool mouseDown;
        private Point lastLocation;
        FrmBig frmBig = new FrmBig();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
           //  bigLocation = this.Location;
           // if (this.Location.X > Screen.PrimaryScreen.WorkingArea.Width - this.Width - frmBig.Width) { bigLocation.X = this.Location.X - frmBig.Width; }
           // else { bigLocation.X = this.Location.X + this.Width; }
           // if (this.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - this.Height - frmBig.Height) { bigLocation.Y = this.Location.Y - frmBig.Height; }
           // else { bigLocation.Y = this.Location.Y; }
           // frmBig.Location = bigLocation;
           // if (frmBig == null)
           // {
           //     frmBig = new FrmBig();
           // }
           //else frmBig.Show();


            

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            frmBig.Hide();
            timer1.Enabled = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //// 获取 PictureBox 控件的 Graphics 对象
            //Graphics g = pictureBox1.CreateGraphics();

            //// 设置绘图的填充颜色和线条颜色
            //SolidBrush brush = new SolidBrush(Color.Red);
            //Pen pen = new Pen(Color.Red);

            //// 获取 PictureBox 控件的宽度和高度
            //int width = pictureBox1.Width;
            //int height = pictureBox1.Height;

            //// 计算圆形的半径和圆心位置
            //int radius = Math.Min(width, height) / 2;
            //int centerX = width / 2;
            //int centerY = height / 2;

            //// 绘制圆形
            //g.FillEllipse(brush, centerX - radius, centerY - radius, 2 * radius, 2 * radius);
            //g.DrawEllipse(pen, centerX - radius, centerY - radius, 2 * radius, 2 * radius);

            //// 释放资源
            //brush.Dispose();
            //pen.Dispose();
            //g.Dispose();
        }
        Point bigLocation;
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
             bigLocation = this.Location;
            if (this.Location.X > Screen.PrimaryScreen.WorkingArea.Width - this.Width - frmBig.Width) { bigLocation.X = this.Location.X - frmBig.Width; }
            else { bigLocation.X = this.Location.X + this.Width; }
            if (this.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - this.Height - frmBig.Height) { bigLocation.Y = this.Location.Y - frmBig.Height; }
            else { bigLocation.Y = this.Location.Y; }
            frmBig.Location = bigLocation;
            if (frmBig == null)
            {
                frmBig = new FrmBig();
                frmBig.Location = bigLocation;
            }
            else
            {
                frmBig.FrmBig_Load(null, null);
            }
            frmBig.Show();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = this.Location;
            if (this.Location.X > Screen.PrimaryScreen.WorkingArea.Width - this.Width - contextMenuStrip1.Width) { point.X = this.Location.X - contextMenuStrip1.Width; }
            else { point.X = this.Location.X -contextMenuStrip1.Width; }
            if (this.Location.Y > Screen.PrimaryScreen.WorkingArea.Height - this.Height - frmBig.Height) { point.Y = this.Location.Y - contextMenuStrip1.Height; }
            else { point.Y = this.Location.Y-contextMenuStrip1.Height; }
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(point);
            }
        }

        private void 重启应用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            FrmMain frmMain = new FrmMain();
            frmMain.Show();
        }
    }
}
