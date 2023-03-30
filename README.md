# 进程管理
***
## -目的
本项目是一个简单的C#应用程序，该程序的实现的功能：
1. 选择路径打开程序(目前支持exe)
2. 展示目前的进程，模糊查询进程并进行Kill
3. **显示常用的网址和应用，一键打开，网址支持指定浏览器打开，可以实现增删改查**
***
## -后期工作
需要完善的功能（欢迎指导）

- [x] 安装包功能
- [ ] 写完设置
- [ ] 做成悬浮球
- [ ] 报错少
- [ ] 优化代码加注释等
- [ ] 总结项目经验


***
## 下载地址
1. Beta版 V0.1 [点此下载](https://objects.githubusercontent.com/github-production-release-asset-2e65be/621276522/4f6c96e9-418f-491a-8a6c-4979b79ccf32?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAIWNJYAX4CSVEH53A%2F20230330%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20230330T130559Z&X-Amz-Expires=300&X-Amz-Signature=3fde374a914b5256a9dd2f2fc83731df2b5854f1161328ff39fc1610e2abcd65&X-Amz-SignedHeaders=host&actor_id=120644103&key_id=0&repo_id=621276522&response-content-disposition=attachment%3B%20filename%3DSetup.v0.1.msi&response-content-type=application%2Foctet-stream)
***
## 代码来源
1. 自我学习
  ```c#
   private void textBox1_Enter(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            textBox1.Font = new Font("微软雅黑", 12);
        }
  ```
2. 前期项目积累
   
  ```c#
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
  ```
   
3. 人工智能[Chat-GPT](chat.openai.com)
 ```c#
    void UpdateComBox()
        {
            this.comboBox1.Items.Clear();

            foreach (var path in Settings.Default.PathList)
            {
                this.comboBox1.Items.Add(path);
                this.comboBox1.SelectedIndex = 0;
            }
        }
  ```

