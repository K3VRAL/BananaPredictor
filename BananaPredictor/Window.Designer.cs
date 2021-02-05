
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
            this.submitBeatmap = new System.Windows.Forms.Button();
            this.browseBeatmap = new System.Windows.Forms.Button();
            this.tbBeatmap = new System.Windows.Forms.TextBox();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.ttDebug = new System.Windows.Forms.ToolTip(this.components);
            this.pTop = new System.Windows.Forms.Panel();
            this.wbMini = new System.Windows.Forms.Button();
            this.wbExit = new System.Windows.Forms.Button();
            this.tAnimation = new System.Windows.Forms.Timer(this.components);
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // submitBeatmap
            // 
            this.submitBeatmap.Location = new System.Drawing.Point(321, 92);
            this.submitBeatmap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.submitBeatmap.Name = "submitBeatmap";
            this.submitBeatmap.Size = new System.Drawing.Size(86, 31);
            this.submitBeatmap.TabIndex = 0;
            this.submitBeatmap.Text = "Submit";
            this.submitBeatmap.UseVisualStyleBackColor = true;
            this.submitBeatmap.Click += new System.EventHandler(this.SubmitBeatmap_Click);
            // 
            // browseBeatmap
            // 
            this.browseBeatmap.Location = new System.Drawing.Point(321, 53);
            this.browseBeatmap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.browseBeatmap.Name = "browseBeatmap";
            this.browseBeatmap.Size = new System.Drawing.Size(86, 31);
            this.browseBeatmap.TabIndex = 1;
            this.browseBeatmap.Text = "Browse";
            this.browseBeatmap.UseVisualStyleBackColor = true;
            this.browseBeatmap.Click += new System.EventHandler(this.BrowseBeatmap_Click);
            // 
            // tbBeatmap
            // 
            this.tbBeatmap.Location = new System.Drawing.Point(12, 53);
            this.tbBeatmap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbBeatmap.Name = "tbBeatmap";
            this.tbBeatmap.Size = new System.Drawing.Size(300, 27);
            this.tbBeatmap.TabIndex = 2;
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbDebug.Location = new System.Drawing.Point(12, 96);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(119, 24);
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
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(421, 35);
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
            this.wbMini.Location = new System.Drawing.Point(305, 0);
            this.wbMini.Name = "wbMini";
            this.wbMini.Size = new System.Drawing.Size(57, 33);
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
            this.wbExit.Location = new System.Drawing.Point(362, 0);
            this.wbExit.Name = "wbExit";
            this.wbExit.Size = new System.Drawing.Size(57, 33);
            this.wbExit.TabIndex = 5;
            this.wbExit.Text = "X";
            this.wbExit.UseVisualStyleBackColor = false;
            this.wbExit.Click += new System.EventHandler(this.wbExit_Click);
            // 
            // tAnimation
            // 
            this.tAnimation.Tick += new System.EventHandler(this.tAnimation_Tick);
            // 
            // K3Banana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(421, 136);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.cbDebug);
            this.Controls.Add(this.tbBeatmap);
            this.Controls.Add(this.browseBeatmap);
            this.Controls.Add(this.submitBeatmap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "K3Banana";
            this.Text = "Banana Predictor";
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitBeatmap;
        private System.Windows.Forms.Button browseBeatmap;
        private System.Windows.Forms.TextBox tbBeatmap;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.ToolTip ttDebug;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Button wbExit;
        private System.Windows.Forms.Button wbMini;
        private System.Windows.Forms.Timer tAnimation;
    }
}

