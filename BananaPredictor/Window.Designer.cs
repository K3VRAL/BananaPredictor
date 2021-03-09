
namespace BananaPredictor
{
    partial class K3BananaWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K3BananaWindow));
            this.bSubmit = new System.Windows.Forms.Button();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tbBeatmap = new System.Windows.Forms.TextBox();
            this.pTop = new System.Windows.Forms.Panel();
            this.wbMini = new System.Windows.Forms.Button();
            this.wbExit = new System.Windows.Forms.Button();
            this.tAnimation = new System.Windows.Forms.Timer(this.components);
            this.bOpen = new System.Windows.Forms.Button();
            this.bOptions = new System.Windows.Forms.Button();
            this.lStatus = new System.Windows.Forms.Label();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSubmit
            // 
            this.bSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSubmit.ForeColor = System.Drawing.SystemColors.Control;
            this.bSubmit.Location = new System.Drawing.Point(321, 92);
            this.bSubmit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.Size = new System.Drawing.Size(85, 31);
            this.bSubmit.TabIndex = 0;
            this.bSubmit.Text = "Submit";
            this.bSubmit.UseVisualStyleBackColor = false;
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // bBrowse
            // 
            this.bBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowse.ForeColor = System.Drawing.SystemColors.Control;
            this.bBrowse.Location = new System.Drawing.Point(321, 54);
            this.bBrowse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(85, 31);
            this.bBrowse.TabIndex = 1;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = false;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbBeatmap
            // 
            this.tbBeatmap.Location = new System.Drawing.Point(12, 54);
            this.tbBeatmap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbBeatmap.Name = "tbBeatmap";
            this.tbBeatmap.Size = new System.Drawing.Size(300, 27);
            this.tbBeatmap.TabIndex = 2;
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pTop.Controls.Add(this.wbMini);
            this.pTop.Controls.Add(this.wbExit);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(420, 36);
            this.pTop.TabIndex = 4;
            this.pTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tPanel_MouseDown);
            // 
            // wbMini
            // 
            this.wbMini.BackColor = System.Drawing.Color.Transparent;
            this.wbMini.Dock = System.Windows.Forms.DockStyle.Right;
            this.wbMini.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.wbMini.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.wbMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wbMini.ForeColor = System.Drawing.SystemColors.Control;
            this.wbMini.Location = new System.Drawing.Point(304, 0);
            this.wbMini.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.wbMini.Name = "wbMini";
            this.wbMini.Size = new System.Drawing.Size(57, 34);
            this.wbMini.TabIndex = 6;
            this.wbMini.Text = "-";
            this.wbMini.UseVisualStyleBackColor = false;
            this.wbMini.Click += new System.EventHandler(this.wbMini_Click);
            // 
            // wbExit
            // 
            this.wbExit.BackColor = System.Drawing.Color.Transparent;
            this.wbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.wbExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.wbExit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.wbExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.wbExit.ForeColor = System.Drawing.SystemColors.Control;
            this.wbExit.Location = new System.Drawing.Point(361, 0);
            this.wbExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.wbExit.Name = "wbExit";
            this.wbExit.Size = new System.Drawing.Size(57, 34);
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
            this.bOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bOpen.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpen.ForeColor = System.Drawing.SystemColors.Control;
            this.bOpen.Location = new System.Drawing.Point(127, 92);
            this.bOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(104, 31);
            this.bOpen.TabIndex = 6;
            this.bOpen.Text = "Open Folder";
            this.bOpen.UseVisualStyleBackColor = false;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bOptions
            // 
            this.bOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bOptions.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOptions.ForeColor = System.Drawing.SystemColors.Control;
            this.bOptions.Location = new System.Drawing.Point(239, 92);
            this.bOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bOptions.Name = "bOptions";
            this.bOptions.Size = new System.Drawing.Size(73, 31);
            this.bOptions.TabIndex = 9;
            this.bOptions.Text = "Options";
            this.bOptions.UseVisualStyleBackColor = false;
            this.bOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.ForeColor = System.Drawing.SystemColors.Control;
            this.lStatus.Location = new System.Drawing.Point(12, 97);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(67, 20);
            this.lStatus.TabIndex = 10;
            this.lStatus.Text = "No input";
            // 
            // K3BananaWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(420, 135);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.bOptions);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.tbBeatmap);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.bSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "K3BananaWindow";
            this.Text = "Banana Predictor";
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSubmit;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.TextBox tbBeatmap;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Button wbExit;
        private System.Windows.Forms.Button wbMini;
        private System.Windows.Forms.Timer tAnimation;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bOptions;
        public System.Windows.Forms.Label lStatus;
    }
}

