
namespace BananaPredictor
{
    partial class WinOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinOptions));
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lList = new System.Windows.Forms.Label();
            this.bPlus = new System.Windows.Forms.Button();
            this.bMinus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bSave
            // 
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.ForeColor = System.Drawing.SystemColors.Control;
            this.bSave.Location = new System.Drawing.Point(12, 172);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(93, 29);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // cbDebug
            // 
            this.cbDebug.AutoSize = true;
            this.cbDebug.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbDebug.Location = new System.Drawing.Point(236, 175);
            this.cbDebug.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbDebug.Name = "cbDebug";
            this.cbDebug.Size = new System.Drawing.Size(119, 24);
            this.cbDebug.TabIndex = 4;
            this.cbDebug.Text = "Debug Mode";
            this.cbDebug.UseVisualStyleBackColor = true;
            this.cbDebug.Click += new System.EventHandler(this.cbDebug_Click);
            this.cbDebug.MouseHover += new System.EventHandler(this.cbDebug_MouseHover);
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(13, 42);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(125, 27);
            this.tbStartTime.TabIndex = 5;
            this.tbStartTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartSpin_KeyPress);
            // 
            // tbEndTime
            // 
            this.tbEndTime.Location = new System.Drawing.Point(13, 75);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(125, 27);
            this.tbEndTime.TabIndex = 6;
            this.tbEndTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndSpin_KeyPress);
            // 
            // lStartSpin
            // 
            this.lStartSpin.AutoSize = true;
            this.lStartSpin.ForeColor = System.Drawing.SystemColors.Control;
            this.lStartSpin.Location = new System.Drawing.Point(144, 45);
            this.lStartSpin.Name = "lStartSpin";
            this.lStartSpin.Size = new System.Drawing.Size(121, 20);
            this.lStartSpin.TabIndex = 7;
            this.lStartSpin.Text = "Start Time Offset";
            // 
            // lEndSpin
            // 
            this.lEndSpin.AutoSize = true;
            this.lEndSpin.ForeColor = System.Drawing.SystemColors.Control;
            this.lEndSpin.Location = new System.Drawing.Point(144, 78);
            this.lEndSpin.Name = "lEndSpin";
            this.lEndSpin.Size = new System.Drawing.Size(115, 20);
            this.lEndSpin.TabIndex = 8;
            this.lEndSpin.Text = "End Time Offset";
            // 
            // lEndPos
            // 
            this.lEndPos.AutoSize = true;
            this.lEndPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lEndPos.Location = new System.Drawing.Point(144, 145);
            this.lEndPos.Name = "lEndPos";
            this.lEndPos.Size = new System.Drawing.Size(90, 20);
            this.lEndPos.TabIndex = 12;
            this.lEndPos.Text = "End Position";
            // 
            // lStartPos
            // 
            this.lStartPos.AutoSize = true;
            this.lStartPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lStartPos.Location = new System.Drawing.Point(144, 111);
            this.lStartPos.Name = "lStartPos";
            this.lStartPos.Size = new System.Drawing.Size(96, 20);
            this.lStartPos.TabIndex = 11;
            this.lStartPos.Text = "Start Position";
            // 
            // tbEndPos
            // 
            this.tbEndPos.Location = new System.Drawing.Point(13, 142);
            this.tbEndPos.Name = "tbEndPos";
            this.tbEndPos.Size = new System.Drawing.Size(125, 27);
            this.tbEndPos.TabIndex = 10;
            this.tbEndPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndPos_KeyPress);
            // 
            // tbStartPos
            // 
            this.tbStartPos.Location = new System.Drawing.Point(13, 108);
            this.tbStartPos.Name = "tbStartPos";
            this.tbStartPos.Size = new System.Drawing.Size(125, 27);
            this.tbStartPos.TabIndex = 9;
            this.tbStartPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartPos_KeyPress);
            // 
            // bConsole
            // 
            this.bConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConsole.ForeColor = System.Drawing.SystemColors.Control;
            this.bConsole.Location = new System.Drawing.Point(112, 172);
            this.bConsole.Name = "bConsole";
            this.bConsole.Size = new System.Drawing.Size(117, 29);
            this.bConsole.TabIndex = 13;
            this.bConsole.Text = "Open Console";
            this.bConsole.UseVisualStyleBackColor = true;
            this.bConsole.Click += new System.EventHandler(this.bConsole_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(126, 28);
            this.comboBox1.TabIndex = 14;
            // 
            // lList
            // 
            this.lList.AutoSize = true;
            this.lList.ForeColor = System.Drawing.SystemColors.Control;
            this.lList.Location = new System.Drawing.Point(144, 13);
            this.lList.Name = "lList";
            this.lList.Size = new System.Drawing.Size(85, 20);
            this.lList.TabIndex = 15;
            this.lList.Text = "Spinner List";
            // 
            // bPlus
            // 
            this.bPlus.Location = new System.Drawing.Point(271, 9);
            this.bPlus.Name = "bPlus";
            this.bPlus.Size = new System.Drawing.Size(28, 29);
            this.bPlus.TabIndex = 16;
            this.bPlus.Text = "+";
            this.bPlus.UseVisualStyleBackColor = true;
            this.bPlus.Click += new System.EventHandler(this.bPlus_Click);
            // 
            // bMinus
            // 
            this.bMinus.Location = new System.Drawing.Point(235, 9);
            this.bMinus.Name = "bMinus";
            this.bMinus.Size = new System.Drawing.Size(30, 29);
            this.bMinus.TabIndex = 17;
            this.bMinus.Text = "-";
            this.bMinus.UseVisualStyleBackColor = true;
            this.bMinus.Click += new System.EventHandler(this.bMinus_Click);
            // 
            // WinOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(361, 214);
            this.Controls.Add(this.bMinus);
            this.Controls.Add(this.bPlus);
            this.Controls.Add(this.lList);
            this.Controls.Add(this.comboBox1);
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
            this.Name = "WinOptions";
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinOptions_FormClosing);
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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lList;
        private System.Windows.Forms.Button bPlus;
        private System.Windows.Forms.Button bMinus;
    }
}