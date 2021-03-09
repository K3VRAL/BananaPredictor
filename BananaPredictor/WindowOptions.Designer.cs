
namespace BananaPredictor
{
    partial class WindowOptions
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowOptions));
            this.bSave = new System.Windows.Forms.Button();
            this.cbDebug = new System.Windows.Forms.CheckBox();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.lStartSpin = new System.Windows.Forms.Label();
            this.lEndSpin = new System.Windows.Forms.Label();
            this.lEndPos = new System.Windows.Forms.Label();
            this.lStartPos = new System.Windows.Forms.Label();
            this.tbEndPos = new System.Windows.Forms.TextBox();
            this.tbStartPos = new System.Windows.Forms.TextBox();
            this.bConsole = new System.Windows.Forms.Button();
            this.ttDebug = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // bSave
            // 
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.ForeColor = System.Drawing.SystemColors.Control;
            this.bSave.Location = new System.Drawing.Point(9, 112);
            this.bSave.Margin = new System.Windows.Forms.Padding(2);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(70, 19);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbDebug.Location = new System.Drawing.Point(10, 8);
            this.cbDebug.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(86, 17);
            this.cbDebug.TabIndex = 4;
            this.cbDebug.Text = "Debug Mode";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.Click += new System.EventHandler(this.cbDebug_Click);
            this.cbDebug.MouseHover += new System.EventHandler(this.cbDebug_MouseHover);
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(10, 27);
            this.tbStartTime.Margin = new System.Windows.Forms.Padding(2);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(95, 21);
            this.tbStartTime.TabIndex = 5;
            this.tbStartTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartSpin_KeyPress);
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(10, 49);
            this.tbEndTime.Margin = new System.Windows.Forms.Padding(2);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(95, 21);
            this.tbEndTime.TabIndex = 6;
            this.tbEndTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndSpin_KeyPress);
            // 
            // lStartSpin
            // 
            this.lStartSpin.AutoSize = true;
            this.lStartSpin.ForeColor = System.Drawing.SystemColors.Control;
            this.lStartSpin.Location = new System.Drawing.Point(108, 29);
            this.lStartSpin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lStartSpin.Name = "lStartSpin";
            this.lStartSpin.Size = new System.Drawing.Size(90, 13);
            this.lStartSpin.TabIndex = 7;
            this.lStartSpin.Text = "Start Time Offset";
            // 
            // lEndSpin
            // 
            this.lEndSpin.AutoSize = true;
            this.lEndSpin.ForeColor = System.Drawing.SystemColors.Control;
            this.lEndSpin.Location = new System.Drawing.Point(108, 51);
            this.lEndSpin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lEndSpin.Name = "lEndSpin";
            this.lEndSpin.Size = new System.Drawing.Size(84, 13);
            this.lEndSpin.TabIndex = 8;
            this.lEndSpin.Text = "End Time Offset";
            // 
            // lEndPos
            // 
            this.lEndPos.AutoSize = true;
            this.lEndPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lEndPos.Location = new System.Drawing.Point(108, 94);
            this.lEndPos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lEndPos.Name = "lEndPos";
            this.lEndPos.Size = new System.Drawing.Size(65, 13);
            this.lEndPos.TabIndex = 12;
            this.lEndPos.Text = "End Position";
            // 
            // lStartPos
            // 
            this.lStartPos.AutoSize = true;
            this.lStartPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lStartPos.Location = new System.Drawing.Point(108, 72);
            this.lStartPos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lStartPos.Name = "lStartPos";
            this.lStartPos.Size = new System.Drawing.Size(71, 13);
            this.lStartPos.TabIndex = 11;
            this.lStartPos.Text = "Start Position";
            // 
            // tbEndPos
            // 
            this.tbEndPos.Location = new System.Drawing.Point(10, 92);
            this.tbEndPos.Margin = new System.Windows.Forms.Padding(2);
            this.tbEndPos.Name = "tbEndPos";
            this.tbEndPos.Size = new System.Drawing.Size(95, 21);
            this.tbEndPos.TabIndex = 10;
            this.tbEndPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndPos_KeyPress);
            // 
            // tbStartPos
            // 
            this.tbStartPos.Location = new System.Drawing.Point(10, 70);
            this.tbStartPos.Margin = new System.Windows.Forms.Padding(2);
            this.tbStartPos.Name = "tbStartPos";
            this.tbStartPos.Size = new System.Drawing.Size(95, 21);
            this.tbStartPos.TabIndex = 9;
            this.tbStartPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartPos_KeyPress);
            // 
            // bConsole
            // 
            this.bConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConsole.ForeColor = System.Drawing.SystemColors.Control;
            this.bConsole.Location = new System.Drawing.Point(84, 112);
            this.bConsole.Margin = new System.Windows.Forms.Padding(2);
            this.bConsole.Name = "bConsole";
            this.bConsole.Size = new System.Drawing.Size(88, 19);
            this.bConsole.TabIndex = 13;
            this.bConsole.Text = "Open Console";
            this.bConsole.UseVisualStyleBackColor = true;
            this.bConsole.Click += new System.EventHandler(this.bConsole_Click);
            // 
            // WindowOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(202, 139);
            this.Controls.Add(this.bConsole);
            this.Controls.Add(this.lEndPos);
            this.Controls.Add(this.lStartPos);
            this.Controls.Add(this.tbEndPos);
            this.Controls.Add(this.tbStartPos);
            this.Controls.Add(this.lEndSpin);
            this.Controls.Add(this.lStartSpin);
            this.Controls.Add(this.tbEndTime);
            this.Controls.Add(this.tbStartTime);
            this.Controls.Add(this.cbDebug);
            this.Controls.Add(this.bSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WindowOptions";
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.CheckBox cbDebug;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label lStartSpin;
        private System.Windows.Forms.Label lEndSpin;
        private System.Windows.Forms.Label lEndPos;
        private System.Windows.Forms.Label lStartPos;
        private System.Windows.Forms.TextBox tbEndPos;
        private System.Windows.Forms.TextBox tbStartPos;
        private System.Windows.Forms.Button bConsole;
        private System.Windows.Forms.ToolTip ttDebug;
    }
}