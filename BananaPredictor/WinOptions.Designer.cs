
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
            this.cbDebug = new System.Windows.Forms.CheckBox();
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
            this.cbDebug.Click += new System.EventHandler(this.CbDebug_Click);
            this.cbDebug.MouseHover += new System.EventHandler(this.CbDebug_MouseHover);
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(13, 42);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.Size = new System.Drawing.Size(125, 27);
            this.tbStartTime.TabIndex = 5;
            this.tbStartTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbStartSpin_KeyPress);
            // 
            // tbEndTime
            // 
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
            this.lRightPos.Size = new System.Drawing.Size(100, 20);
            this.lRightPos.TabIndex = 12;
            this.lRightPos.Text = "Right Position";
            // 
            // lLeftPos
            // 
            this.lLeftPos.AutoSize = true;
            this.lLeftPos.ForeColor = System.Drawing.SystemColors.Control;
            this.lLeftPos.Location = new System.Drawing.Point(144, 111);
            this.lLeftPos.Name = "lLeftPos";
            this.lLeftPos.Size = new System.Drawing.Size(90, 20);
            this.lLeftPos.TabIndex = 11;
            this.lLeftPos.Text = "Left Position";
            // 
            // tbRightPos
            // 
            this.tbRightPos.Location = new System.Drawing.Point(13, 142);
            this.tbRightPos.Name = "tbRightPos";
            this.tbRightPos.Size = new System.Drawing.Size(125, 27);
            this.tbRightPos.TabIndex = 10;
            this.tbRightPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbEndPos_KeyPress);
            // 
            // tbLeftPos
            // 
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
            this.bPlus.Location = new System.Drawing.Point(271, 9);
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
            this.bMinus.Location = new System.Drawing.Point(235, 9);
            this.bMinus.Name = "bMinus";
            this.bMinus.Size = new System.Drawing.Size(29, 29);
            this.bMinus.TabIndex = 17;
            this.bMinus.Text = "-";
            this.bMinus.UseVisualStyleBackColor = false;
            this.bMinus.Click += new System.EventHandler(this.BMinus_Click);
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
            this.Controls.Add(this.cbDebug);
            this.Controls.Add(this.bSave);
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
    }
}