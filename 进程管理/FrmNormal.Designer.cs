namespace 进程管理
{
    partial class FrmNormal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_main = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox_main
            // 
            this.checkBox_main.AutoSize = true;
            this.checkBox_main.Location = new System.Drawing.Point(26, 13);
            this.checkBox_main.Name = "checkBox_main";
            this.checkBox_main.Size = new System.Drawing.Size(132, 16);
            this.checkBox_main.TabIndex = 0;
            this.checkBox_main.Text = "启动是否显示主页？";
            this.checkBox_main.UseVisualStyleBackColor = true;
            this.checkBox_main.CheckedChanged += new System.EventHandler(this.checkBox_main_CheckedChanged);
            // 
            // FrmNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 400);
            this.Controls.Add(this.checkBox_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmNormal";
            this.Text = "FrmNormal";
            this.Load += new System.EventHandler(this.FrmNormal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_main;
    }
}