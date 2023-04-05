using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 进程管理
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
      
        static void Main()
        {
           
            // 获取当前进程的名称
            string procName = Process.GetCurrentProcess().ProcessName;

            // 查找与当前进程名称相同的进程
            Process[] processes = Process.GetProcessesByName(procName);

            // 如果找到了多个相同的进程，则关闭当前进程
            if (processes.Length > 1)
            {
                FrmMain frmMain = new FrmMain();
                frmMain.Show();
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMiniFloat());


            //Process[] ps=Process.GetProcesses();
            //// 创建 frmMiniFloat 对象
            //FrmMiniFloat frmMiniFloat = new FrmMiniFloat();


            //// 检查进程列表
            //foreach (Process p in ps)
            //{
            //    if (p.ProcessName == "小助手")
            //    {
            //        frmMiniFloat.Show();

            //    }
            //}

            //// 如果没有找到 "小助手" 进程，则运行 frmMiniFloat 窗体
            //Application.Run(frmMiniFloat);
        }
        
    }
}
