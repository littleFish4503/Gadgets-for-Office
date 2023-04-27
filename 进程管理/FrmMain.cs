using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using 进程管理.Properties;

namespace 进程管理
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 选择进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "请选择要打开的进程";
            openFileDialog1.Filter = "应用程序|*.exe";
            openFileDialog1.ShowDialog();
            comboBox1.Text = openFileDialog1.FileName;
            try
            {
                if (!Settings.Default.PathList.Contains(openFileDialog1.FileName))
                {

                    Settings.Default.PathList.Add(openFileDialog1.FileName);
                    Settings.Default.Save();
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 打开指定进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() == "请选择")
            {
                MessageBox.Show("请选择要打开的进程");
            }
            else
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(comboBox1.Text));
                    if (processes.Length > 0)
                    {
                        // 进程已存在，将其显示在前台
                        Process process = processes[0];
                        if (process.MainWindowHandle == IntPtr.Zero)
                        {
                            // 如果进程没有主窗口，可以考虑使用其他方法将其显示在前台
                            // 这里只是一个简单的示例
                            process.WaitForInputIdle();
                        }
                        SetForegroundWindow(process.MainWindowHandle);
                    }
                    else
                    {
                        // 进程不存在，启动一个新的进程
                        Process.Start(comboBox1.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("启动失败，提示：" + ex.Message);
                }
            }

        }
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        DataTable tbProcess = new DataTable();
        private void ShowProcess_Click(object sender, EventArgs e)
        {
            tbProcess.Clear();
            if (tbProcess.Columns.Count == 0)
            {
                tbProcess.Columns.Add("Id", typeof(int));
                tbProcess.Columns.Add("ProcessName", typeof(string));
            }
            foreach (Process p in Process.GetProcesses())
            {
                DataRow dataRow = tbProcess.NewRow();
                dataRow["ID"] = p.Id;
                dataRow["ProcessName"] = p.ProcessName;
                tbProcess.Rows.Add(dataRow);
            }
            Dgv1.DataSource = tbProcess;
            Dgv1.Columns["ID"].HeaderText = "进程ID";
            Dgv1.Columns["ProcessName"].HeaderText = "进程名称";
            Dgv1.Columns["ProcessName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            if (Dgv1.CurrentCell == null)
            {
                MessageBox.Show("请选择");
                return;
            }
            else
            {
                try
                {
                    KillProcess((int)Dgv1.CurrentRow.Cells["Id"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("结束失败，提示：" + ex.Message);
                }
            }

        }
        static void KillProcess(int ProcessId)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.Id == ProcessId)
                {
                    p.Kill();
                    p.WaitForExit();
                }
            }
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tbProcess.Rows.Count > 0)
            {
                tbProcess.DefaultView.RowFilter = String.Format(@"ProcessName like '%{0}%' ", textBox1.Text);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            UpdateComBox();
            btnWebsite_Click(sender, e);

        }
        void UpdateComBox()
        {
            this.comboBox1.Items.Clear();

            foreach (var path in Settings.Default.PathList)
            {
                this.comboBox1.Items.Add(path);
                this.comboBox1.SelectedIndex = 0;
            }
        }

        private void btnWebsite_Click(object sender, EventArgs e)
        {
            List<Website> websites = WebsiteService.GetWebsites();
            Dgv1.DataSource = websites;
            Dgv1.Columns["Url"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Dgv1.Columns["Url"].HeaderText = "路径";
            Dgv1.Columns["Name"].HeaderText = "名称";
            Dgv1.Columns["Browser"].HeaderText = "使用浏览器";
            Dgv1.AllowUserToAddRows = true;
            Dgv1.EditMode = DataGridViewEditMode.EditOnEnter;
            Dgv1.Font = new Font("宋体", 9);
            Dgv1.Columns["Name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Dgv1.Rows[0].Cells[2].Value.ToString() == "在此处进行添加信息保存") { Dgv1.Rows[0].Cells[2].Value = ""; }
            Website website = new Website(); website.Name = Dgv1.CurrentRow.Cells["Name"].Value.ToString(); website.Url = Dgv1.CurrentRow.Cells["Url"].Value.ToString(); website.Browser = Dgv1.CurrentRow.Cells["Browser"].Value.ToString();
            bool b = WebsiteService.AddInfo(website);
            if (b)
            { MessageBox.Show("成功"); }
            btnWebsite_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                WebsiteService.DeleteInfo(Dgv1.CurrentRow.Cells["Name"].Value.ToString());
            }
            btnWebsite_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = Dgv1.CurrentRow.Cells["Url"].Value.ToString();
            //浏览器不为空
            if (Dgv1.CurrentRow.Cells["Browser"].Value != null)
            {
                if (Dgv1.CurrentRow.Cells["Browser"].Value.ToString() == "IE")
                {
                    Process.Start("iexplore.exe", url);
                }
                else if (Dgv1.CurrentRow.Cells["Browser"].Value.ToString() == "Edge")
                {

                    Process.Start("microsoft-edge:+", url);

                }
                else { Process.Start("chrome.exe", url); }
            }
            else
            {
                try
                {
                    if (url.Contains(".exe"))
                    { Process.Start(url); }
                    else { Process.Start("Explorer.exe",url); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Website service = new Website() { Name = this.Dgv1.CurrentRow.Cells["Name"].Value.ToString(), Url = this.Dgv1.CurrentRow.Cells["Url"].Value.ToString(), Browser = this.Dgv1.CurrentRow.Cells["Browser"].Value.ToString() };
            WebsiteService.UpdateInfo(this.Dgv1.CurrentRow.Cells["Name"].Value.ToString(), service);
            btnWebsite_Click(sender, e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            textBox1.Font = new Font("微软雅黑", 12);
        }

        private void Dgv1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                openFileDialog1.Filter = "应用程序|*.exe";
                DialogResult result = this.openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                { Dgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = openFileDialog1.FileName; }
                else { Dgv1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ""; }
                //OpenFileDialog openFileDialog = new OpenFileDialog();
                //openFileDialog.Title = "选择文件";
                //openFileDialog.Filter = "文本文件 (*.txt)|*.exe|所有文件 (*.*)|*.*";

                //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                //folderBrowserDialog.Description = "选择文件夹";

                //DialogResult result = folderBrowserDialog.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                //    string folderPath = folderBrowserDialog.SelectedPath;

                //    result = openFileDialog.ShowDialog();
                //    if (result == DialogResult.OK)
                //    {
                //        string filePath = openFileDialog.FileName;

                //        // 处理选择的文件夹和文件
                //        Console.WriteLine("选择的文件夹路径：{0}", folderPath);
                //        Console.WriteLine("选择的文件路径：{0}", filePath);
                //    }

                //}
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            this.ShowInTaskbar = false;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            toolStripStatusLabel2.Text = dateTime.ToString() + "      ";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Minimized;

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
        }
        #region 拖动时移动
        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置

        private void Mouse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }
        private void Mouse_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }

        }

        private void mouse_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        #endregion

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = false;
                this.Show();
                this.ShowInTaskbar = false;
            }

        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(OpenFrmSetting));
            thread.Start();
        }
        private void OpenFrmSetting()
        {
            FrmSetting frmSetting = new FrmSetting();
            frmSetting.ShowDialog();
        }
    }


}
