
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
            this.cbOpen = new System.Windows.Forms.CheckBox();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSubmit
            // 
            this.bSubmit.Location = new System.Drawing.Point(281, 69);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(75, 23);
            this.bSubmit.TabIndex = 0;
            this.bSubmit.Text = "Submit";
            this.bSubmit.UseVisualStyleBackColor = true;
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(281, 40);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 1;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbBeatmap
            // 
            this.tbBeatmap.Location = new System.Drawing.Point(10, 40);
            this.tbBeatmap.Name = "tbBeatmap";
            this.tbBeatmap.Size = new System.Drawing.Size(263, 23);
            this.tbBeatmap.TabIndex = 2;
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbDebug.Location = new System.Drawing.Point(10, 72);
            this.cbDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(95, 19);
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
            this.pTop.Size = new System.Drawing.Size(368, 27);
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
            this.wbMini.Location = new System.Drawing.Point(266, 0);
            this.wbMini.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.wbMini.Name = "wbMini";
            this.wbMini.Size = new System.Drawing.Size(50, 25);
            this.wbMini.TabIndex = 6;
            this.wbMini.Text = "_";
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
            this.wbExit.Location = new System.Drawing.Point(316, 0);
            this.wbExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.wbExit.Name = "wbExit";
            this.wbExit.Size = new System.Drawing.Size(50, 25);
            this.wbExit.TabIndex = 5;
            this.wbExit.Text = "X";
            this.wbExit.UseVisualStyleBackColor = false;
            this.wbExit.Click += new System.EventHandler(this.wbExit_Click);
            // 
            // tAnimation
            // 
            this.tAnimation.Tick += new System.EventHandler(this.tAnimation_Tick);
            // 
            // cbOpen
            // 
            this.cbOpen.AutoSize = true;
            this.cbOpen.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbOpen.Location = new System.Drawing.Point(112, 72);
            this.cbOpen.Name = "cbOpen";
            this.cbOpen.Size = new System.Drawing.Size(91, 19);
            this.cbOpen.TabIndex = 5;
            this.cbOpen.Text = "Open Folder";
            this.cbOpen.UseVisualStyleBackColor = true;
            // 
            // K3Banana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(368, 102);
            this.Controls.Add(this.cbOpen);
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
        private System.Windows.Forms.CheckBox cbOpen;
    }
}

