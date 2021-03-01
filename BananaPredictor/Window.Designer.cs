
namespace BananaPredictor
{
    partial class K3Banana
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K3Banana));
            this.bSubmit = new System.Windows.Forms.Button();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tbBeatmap = new System.Windows.Forms.TextBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.ttDebug = new System.Windows.Forms.ToolTip(this.components);
            this.pTop = new System.Windows.Forms.Panel();
            this.wbMini = new System.Windows.Forms.Button();
            this.wbExit = new System.Windows.Forms.Button();
            this.tAnimation = new System.Windows.Forms.Timer(this.components);
            this.bOpen = new System.Windows.Forms.Button();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSubmit
            // 
            this.bSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bSubmit.ForeColor = System.Drawing.SystemColors.Control;
            this.bSubmit.Location = new System.Drawing.Point(241, 60);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(64, 20);
            this.bSubmit.TabIndex = 0;
            this.bSubmit.Text = "Submit";
            this.bSubmit.UseVisualStyleBackColor = false;
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // bBrowse
            // 
            this.bBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bBrowse.ForeColor = System.Drawing.SystemColors.Control;
            this.bBrowse.Location = new System.Drawing.Point(241, 35);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(64, 20);
            this.bBrowse.TabIndex = 1;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = false;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbBeatmap
            // 
            this.tbBeatmap.Location = new System.Drawing.Point(9, 35);
            this.tbBeatmap.Name = "tbBeatmap";
            this.tbBeatmap.Size = new System.Drawing.Size(226, 21);
            this.tbBeatmap.TabIndex = 2;
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbDebug.Location = new System.Drawing.Point(9, 62);
            this.cbDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(86, 17);
            this.cbDebug.TabIndex = 3;
            this.cbDebug.Text = "Debug Mode";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.MouseHover += new System.EventHandler(this.cbDebug_MouseHover);
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTop.Controls.Add(this.wbMini);
            this.pTop.Controls.Add(this.wbExit);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(315, 24);
            this.pTop.TabIndex = 4;
            this.pTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tPanel_MouseDown);
            // 
            // wbMini
            // 
            this.wbMini.BackColor = System.Drawing.Color.Transparent;
            this.wbMini.Dock = System.Windows.Forms.DockStyle.Right;
            this.wbMini.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.wbMini.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.wbMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wbMini.ForeColor = System.Drawing.SystemColors.Control;
            this.wbMini.Location = new System.Drawing.Point(227, 0);
            this.wbMini.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.wbMini.Name = "wbMini";
            this.wbMini.Size = new System.Drawing.Size(43, 22);
            this.wbMini.TabIndex = 6;
            this.wbMini.Text = "-";
            this.wbMini.UseVisualStyleBackColor = false;
            this.wbMini.Click += new System.EventHandler(this.wbMini_Click);
            // 
            // wbExit
            // 
            this.wbExit.BackColor = System.Drawing.Color.Transparent;
            this.wbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.wbExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.wbExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.wbExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wbExit.ForeColor = System.Drawing.SystemColors.Control;
            this.wbExit.Location = new System.Drawing.Point(270, 0);
            this.wbExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.wbExit.Name = "wbExit";
            this.wbExit.Size = new System.Drawing.Size(43, 22);
            this.wbExit.TabIndex = 5;
            this.wbExit.Text = "X";
            this.wbExit.UseVisualStyleBackColor = false;
            this.wbExit.Click += new System.EventHandler(this.wbExit_Click);
            // 
            // tAnimation
            // 
            this.tAnimation.Tick += new System.EventHandler(this.tAnimation_Tick);
            // 
            // bOpen
            // 
            this.bOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bOpen.ForeColor = System.Drawing.SystemColors.Control;
            this.bOpen.Location = new System.Drawing.Point(95, 60);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(140, 20);
            this.bOpen.TabIndex = 6;
            this.bOpen.Text = "Open Folder";
            this.bOpen.UseVisualStyleBackColor = false;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // K3Banana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(315, 88);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.cbDebug);
            this.Controls.Add(this.tbBeatmap);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.bSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "K3Banana";
            this.Text = "Banana Predictor";
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSubmit;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.TextBox tbBeatmap;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.ToolTip ttDebug;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Button wbExit;
        private System.Windows.Forms.Button wbMini;
        private System.Windows.Forms.Timer tAnimation;
        private System.Windows.Forms.Button bOpen;
    }
}

