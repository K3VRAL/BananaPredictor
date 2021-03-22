
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
            this.bSave = new System.Windows.Forms.Button();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.tbEndTime = new System.Windows.Forms.TextBox();
            this.lStartSpin = new System.Windows.Forms.Label();
            this.lEndSpin = new System.Windows.Forms.Label();
            this.lRightPos = new System.Windows.Forms.Label();
            this.lLeftPos = new System.Windows.Forms.Label();
            this.tbRightPos = new System.Windows.Forms.TextBox();
            this.tbLeftPos = new System.Windows.Forms.TextBox();
            this.bConsole = new System.Windows.Forms.Button();
            this.ttDebug = new System.Windows.Forms.ToolTip(this.components);
            this.cbList = new System.Windows.Forms.ComboBox();
            this.lList = new System.Windows.Forms.Label();
            this.bPlus = new System.Windows.Forms.Button();
            this.bMinus = new System.Windows.Forms.Button();
            this.lPosInX = new System.Windows.Forms.Label();
            this.cbMoveTo = new System.Windows.Forms.CheckBox();
            this.tbRightEnd = new System.Windows.Forms.TextBox();
            this.tbLeftEnd = new System.Windows.Forms.TextBox();
            this.tbRightStart = new System.Windows.Forms.TextBox();
            this.tbLeftStart = new System.Windows.Forms.TextBox();
            this.lLeftStart = new System.Windows.Forms.Label();
            this.lRightStart = new System.Windows.Forms.Label();
            this.lLeftEnd = new System.Windows.Forms.Label();
            this.lRightEnd = new System.Windows.Forms.Label();
            this.lRemoveLater = new System.Windows.Forms.Label();
            this.lDistance = new System.Windows.Forms.Label();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.lInvert = new System.Windows.Forms.Label();
            this.cbInvert = new System.Windows.Forms.CheckBox();
            this.lWarning = new System.Windows.Forms.Label();
            this.cbOnlySpin = new System.Windows.Forms.CheckBox();
            this.lOnlySpin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bSave
            // 
            this.bSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bSave.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.ForeColor = System.Drawing.SystemColors.Control;
            this.bSave.Location = new System.Drawing.Point(12, 172);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(93, 29);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.BSave_Click);
            // 
            // tbStartTime
            // 
            this.tbStartTime.BackColor = System.Drawing.SystemColors.Control;
            this.tbStartTime.Location = new System.Drawing.Point(13, 42);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(125, 27);
            this.tbStartTime.TabIndex = 5;
            this.tbStartTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbStartSpin_KeyPress);
            // 
            // tbEndTime
            // 
            this.tbEndTime.BackColor = System.Drawing.SystemColors.Control;
            this.tbEndTime.Location = new System.Drawing.Point(13, 75);
            this.tbEndTime.Name = "tbEndTime";
            this.tbEndTime.Size = new System.Drawing.Size(125, 27);
            this.tbEndTime.TabIndex = 6;
            this.tbEndTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbEndSpin_KeyPress);
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
            // lRightPos
            // 
            this.lRightPos.AutoSize = true;
            this.lRightPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lRightPos.Location = new System.Drawing.Point(144, 145);
            this.lRightPos.Name = "lRightPos";
            this.lRightPos.Size = new System.Drawing.Size(190, 20);
            this.lRightPos.TabIndex = 12;
            this.lRightPos.Text = "Right Empty Space Position";
            // 
            // lLeftPos
            // 
            this.lLeftPos.AutoSize = true;
            this.lLeftPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lLeftPos.Location = new System.Drawing.Point(144, 111);
            this.lLeftPos.Name = "lLeftPos";
            this.lLeftPos.Size = new System.Drawing.Size(180, 20);
            this.lLeftPos.TabIndex = 11;
            this.lLeftPos.Text = "Left Empty Space Position";
            // 
            // tbRightPos
            // 
            this.tbRightPos.BackColor = System.Drawing.SystemColors.Control;
            this.tbRightPos.Location = new System.Drawing.Point(13, 142);
            this.tbRightPos.Name = "tbRightPos";
            this.tbRightPos.Size = new System.Drawing.Size(125, 27);
            this.tbRightPos.TabIndex = 10;
            this.tbRightPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbEndPos_KeyPress);
            // 
            // tbLeftPos
            // 
            this.tbLeftPos.BackColor = System.Drawing.SystemColors.Control;
            this.tbLeftPos.Location = new System.Drawing.Point(13, 108);
            this.tbLeftPos.Name = "tbLeftPos";
            this.tbLeftPos.Size = new System.Drawing.Size(125, 27);
            this.tbLeftPos.TabIndex = 9;
            this.tbLeftPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbStartPos_KeyPress);
            // 
            // bConsole
            // 
            this.bConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bConsole.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bConsole.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConsole.ForeColor = System.Drawing.SystemColors.Control;
            this.bConsole.Location = new System.Drawing.Point(112, 172);
            this.bConsole.Name = "bConsole";
            this.bConsole.Size = new System.Drawing.Size(117, 29);
            this.bConsole.TabIndex = 13;
            this.bConsole.Text = "Open Console";
            this.bConsole.UseVisualStyleBackColor = false;
            this.bConsole.Click += new System.EventHandler(this.BConsole_Click);
            // 
            // cbList
            // 
            this.cbList.BackColor = System.Drawing.SystemColors.Control;
            this.cbList.FormattingEnabled = true;
            this.cbList.Location = new System.Drawing.Point(12, 9);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(127, 28);
            this.cbList.TabIndex = 14;
            this.cbList.SelectedValueChanged += new System.EventHandler(this.CbList_SelectedValueChanged);
            // 
            // lList
            // 
            this.lList.AutoSize = true;
            this.lList.ForeColor = System.Drawing.SystemColors.Control;
            this.lList.Location = new System.Drawing.Point(144, 12);
            this.lList.Name = "lList";
            this.lList.Size = new System.Drawing.Size(85, 20);
            this.lList.TabIndex = 15;
            this.lList.Text = "Spinner List";
            // 
            // bPlus
            // 
            this.bPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bPlus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bPlus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlus.ForeColor = System.Drawing.SystemColors.Control;
            this.bPlus.Location = new System.Drawing.Point(231, 8);
            this.bPlus.Name = "bPlus";
            this.bPlus.Size = new System.Drawing.Size(28, 29);
            this.bPlus.TabIndex = 16;
            this.bPlus.Text = "+";
            this.bPlus.UseVisualStyleBackColor = false;
            this.bPlus.Click += new System.EventHandler(this.BPlus_Click);
            // 
            // bMinus
            // 
            this.bMinus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.bMinus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.bMinus.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.bMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinus.ForeColor = System.Drawing.SystemColors.Control;
            this.bMinus.Location = new System.Drawing.Point(265, 8);
            this.bMinus.Name = "bMinus";
            this.bMinus.Size = new System.Drawing.Size(29, 29);
            this.bMinus.TabIndex = 17;
            this.bMinus.Text = "-";
            this.bMinus.UseVisualStyleBackColor = false;
            this.bMinus.Click += new System.EventHandler(this.BMinus_Click);
            // 
            // lPosInX
            // 
            this.lPosInX.AutoSize = true;
            this.lPosInX.ForeColor = System.Drawing.SystemColors.Control;
            this.lPosInX.Location = new System.Drawing.Point(236, 177);
            this.lPosInX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPosInX.Name = "lPosInX";
            this.lPosInX.Size = new System.Drawing.Size(88, 20);
            this.lPosInX.TabIndex = 18;
            this.lPosInX.Text = "Position in x";
            // 
            // cbMoveTo
            // 
            this.cbMoveTo.AutoSize = true;
            this.cbMoveTo.ForeColor = System.Drawing.SystemColors.Control;
            this.cbMoveTo.Location = new System.Drawing.Point(336, 12);
            this.cbMoveTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbMoveTo.Name = "cbMoveTo";
            this.cbMoveTo.Size = new System.Drawing.Size(148, 24);
            this.cbMoveTo.TabIndex = 19;
            this.cbMoveTo.Text = "Start from, End to";
            this.cbMoveTo.UseVisualStyleBackColor = true;
            this.cbMoveTo.CheckedChanged += new System.EventHandler(this.CbMoveTo_CheckedChanged);
            // 
            // tbRightEnd
            // 
            this.tbRightEnd.BackColor = System.Drawing.SystemColors.Control;
            this.tbRightEnd.Location = new System.Drawing.Point(336, 142);
            this.tbRightEnd.Name = "tbRightEnd";
            this.tbRightEnd.Size = new System.Drawing.Size(125, 27);
            this.tbRightEnd.TabIndex = 23;
            this.tbRightEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbRightEnd_KeyPress);
            // 
            // tbLeftEnd
            // 
            this.tbLeftEnd.BackColor = System.Drawing.SystemColors.Control;
            this.tbLeftEnd.Location = new System.Drawing.Point(336, 108);
            this.tbLeftEnd.Name = "tbLeftEnd";
            this.tbLeftEnd.Size = new System.Drawing.Size(125, 27);
            this.tbLeftEnd.TabIndex = 22;
            this.tbLeftEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbLeftEnd_KeyPress);
            // 
            // tbRightStart
            // 
            this.tbRightStart.BackColor = System.Drawing.SystemColors.Control;
            this.tbRightStart.Location = new System.Drawing.Point(336, 75);
            this.tbRightStart.Name = "tbRightStart";
            this.tbRightStart.Size = new System.Drawing.Size(125, 27);
            this.tbRightStart.TabIndex = 21;
            this.tbRightStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbRightStart_KeyPress);
            // 
            // tbLeftStart
            // 
            this.tbLeftStart.BackColor = System.Drawing.SystemColors.Control;
            this.tbLeftStart.Location = new System.Drawing.Point(336, 42);
            this.tbLeftStart.Name = "tbLeftStart";
            this.tbLeftStart.Size = new System.Drawing.Size(125, 27);
            this.tbLeftStart.TabIndex = 20;
            this.tbLeftStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbLeftStart_KeyPress);
            // 
            // lLeftStart
            // 
            this.lLeftStart.AutoSize = true;
            this.lLeftStart.ForeColor = System.Drawing.SystemColors.Control;
            this.lLeftStart.Location = new System.Drawing.Point(468, 46);
            this.lLeftStart.Name = "lLeftStart";
            this.lLeftStart.Size = new System.Drawing.Size(69, 20);
            this.lLeftStart.TabIndex = 24;
            this.lLeftStart.Text = "Left Start";
            // 
            // lRightStart
            // 
            this.lRightStart.AutoSize = true;
            this.lRightStart.ForeColor = System.Drawing.SystemColors.Control;
            this.lRightStart.Location = new System.Drawing.Point(468, 78);
            this.lRightStart.Name = "lRightStart";
            this.lRightStart.Size = new System.Drawing.Size(79, 20);
            this.lRightStart.TabIndex = 25;
            this.lRightStart.Text = "Right Start";
            // 
            // lLeftEnd
            // 
            this.lLeftEnd.AutoSize = true;
            this.lLeftEnd.ForeColor = System.Drawing.SystemColors.Control;
            this.lLeftEnd.Location = new System.Drawing.Point(468, 112);
            this.lLeftEnd.Name = "lLeftEnd";
            this.lLeftEnd.Size = new System.Drawing.Size(63, 20);
            this.lLeftEnd.TabIndex = 26;
            this.lLeftEnd.Text = "Left End";
            // 
            // lRightEnd
            // 
            this.lRightEnd.AutoSize = true;
            this.lRightEnd.ForeColor = System.Drawing.SystemColors.Control;
            this.lRightEnd.Location = new System.Drawing.Point(468, 146);
            this.lRightEnd.Name = "lRightEnd";
            this.lRightEnd.Size = new System.Drawing.Size(73, 20);
            this.lRightEnd.TabIndex = 27;
            this.lRightEnd.Text = "Right End";
            // 
            // lRemoveLater
            // 
            this.lRemoveLater.AutoSize = true;
            this.lRemoveLater.ForeColor = System.Drawing.SystemColors.Control;
            this.lRemoveLater.Location = new System.Drawing.Point(336, 177);
            this.lRemoveLater.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lRemoveLater.Name = "lRemoveLater";
            this.lRemoveLater.Size = new System.Drawing.Size(169, 20);
            this.lRemoveLater.TabIndex = 28;
            this.lRemoveLater.Text = "Don\'t use above; broken";
            // 
            // lDistance
            // 
            this.lDistance.AutoSize = true;
            this.lDistance.ForeColor = System.Drawing.SystemColors.Control;
            this.lDistance.Location = new System.Drawing.Point(684, 49);
            this.lDistance.Name = "lDistance";
            this.lDistance.Size = new System.Drawing.Size(103, 20);
            this.lDistance.TabIndex = 30;
            this.lDistance.Text = "Distance Snap";
            // 
            // tbDistance
            // 
            this.tbDistance.BackColor = System.Drawing.SystemColors.Control;
            this.tbDistance.Location = new System.Drawing.Point(552, 43);
            this.tbDistance.Name = "tbDistance";
            this.tbDistance.Size = new System.Drawing.Size(125, 27);
            this.tbDistance.TabIndex = 29;
            this.tbDistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSpawnRate_KeyPress);
            // 
            // lInvert
            // 
            this.lInvert.AutoSize = true;
            this.lInvert.ForeColor = System.Drawing.SystemColors.Control;
            this.lInvert.Location = new System.Drawing.Point(684, 15);
            this.lInvert.Name = "lInvert";
            this.lInvert.Size = new System.Drawing.Size(100, 20);
            this.lInvert.TabIndex = 32;
            this.lInvert.Text = "Invert Spinner";
            // 
            // cbInvert
            // 
            this.cbInvert.AutoSize = true;
            this.cbInvert.Location = new System.Drawing.Point(606, 15);
            this.cbInvert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbInvert.Name = "cbInvert";
            this.cbInvert.Size = new System.Drawing.Size(18, 17);
            this.cbInvert.TabIndex = 33;
            this.cbInvert.UseVisualStyleBackColor = true;
            // 
            // lWarning
            // 
            this.lWarning.AutoSize = true;
            this.lWarning.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lWarning.ForeColor = System.Drawing.SystemColors.Control;
            this.lWarning.Location = new System.Drawing.Point(552, 82);
            this.lWarning.Name = "lWarning";
            this.lWarning.Size = new System.Drawing.Size(228, 45);
            this.lWarning.TabIndex = 34;
            this.lWarning.Text = "Warning: The lower the number,\r\nthe higher the amount, the bigger the lag.\r\nUse t" +
    "he bpm\'s distance snap";
            // 
            // cbOnlySpin
            // 
            this.cbOnlySpin.AutoSize = true;
            this.cbOnlySpin.Location = new System.Drawing.Point(606, 145);
            this.cbOnlySpin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbOnlySpin.Name = "cbOnlySpin";
            this.cbOnlySpin.Size = new System.Drawing.Size(18, 17);
            this.cbOnlySpin.TabIndex = 36;
            this.cbOnlySpin.UseVisualStyleBackColor = true;
            // 
            // lOnlySpin
            // 
            this.lOnlySpin.AutoSize = true;
            this.lOnlySpin.ForeColor = System.Drawing.SystemColors.Control;
            this.lOnlySpin.Location = new System.Drawing.Point(684, 145);
            this.lOnlySpin.Name = "lOnlySpin";
            this.lOnlySpin.Size = new System.Drawing.Size(93, 20);
            this.lOnlySpin.TabIndex = 35;
            this.lOnlySpin.Text = "Only Spinner";
            // 
            // WinOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(784, 214);
            this.Controls.Add(this.cbOnlySpin);
            this.Controls.Add(this.lOnlySpin);
            this.Controls.Add(this.lWarning);
            this.Controls.Add(this.cbInvert);
            this.Controls.Add(this.lInvert);
            this.Controls.Add(this.lDistance);
            this.Controls.Add(this.tbDistance);
            this.Controls.Add(this.lRemoveLater);
            this.Controls.Add(this.lRightEnd);
            this.Controls.Add(this.lLeftEnd);
            this.Controls.Add(this.lRightStart);
            this.Controls.Add(this.lLeftStart);
            this.Controls.Add(this.tbRightEnd);
            this.Controls.Add(this.tbLeftEnd);
            this.Controls.Add(this.tbRightStart);
            this.Controls.Add(this.tbLeftStart);
            this.Controls.Add(this.cbMoveTo);
            this.Controls.Add(this.lPosInX);
            this.Controls.Add(this.bMinus);
            this.Controls.Add(this.bPlus);
            this.Controls.Add(this.lList);
            this.Controls.Add(this.cbList);
            this.Controls.Add(this.bConsole);
            this.Controls.Add(this.lRightPos);
            this.Controls.Add(this.lLeftPos);
            this.Controls.Add(this.tbRightPos);
            this.Controls.Add(this.tbLeftPos);
            this.Controls.Add(this.lEndSpin);
            this.Controls.Add(this.lStartSpin);
            this.Controls.Add(this.tbEndTime);
            this.Controls.Add(this.tbStartTime);
            this.Controls.Add(this.bSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinOptions";
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinOptions_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.TextBox tbEndTime;
        private System.Windows.Forms.Label lStartSpin;
        private System.Windows.Forms.Label lEndSpin;
        private System.Windows.Forms.Label lRightPos;
        private System.Windows.Forms.Label lLeftPos;
        private System.Windows.Forms.TextBox tbRightPos;
        private System.Windows.Forms.TextBox tbLeftPos;
        private System.Windows.Forms.Button bConsole;
        private System.Windows.Forms.ToolTip ttDebug;
        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Label lList;
        private System.Windows.Forms.Button bPlus;
        private System.Windows.Forms.Button bMinus;
        private System.Windows.Forms.Label lPosInX;
        private System.Windows.Forms.CheckBox cbMoveTo;
        private System.Windows.Forms.TextBox tbRightEnd;
        private System.Windows.Forms.TextBox tbLeftEnd;
        private System.Windows.Forms.TextBox tbRightStart;
        private System.Windows.Forms.TextBox tbLeftStart;
        private System.Windows.Forms.Label lLeftStart;
        private System.Windows.Forms.Label lRightStart;
        private System.Windows.Forms.Label lLeftEnd;
        private System.Windows.Forms.Label lRightEnd;
        private System.Windows.Forms.Label lRemoveLater;
        private System.Windows.Forms.Label lDistance;
        private System.Windows.Forms.TextBox tbDistance;
        private System.Windows.Forms.Label lInvert;
        private System.Windows.Forms.CheckBox cbInvert;
        private System.Windows.Forms.Label lWarning;
        private System.Windows.Forms.CheckBox cbOnlySpin;
        private System.Windows.Forms.Label lOnlySpin;
    }
}