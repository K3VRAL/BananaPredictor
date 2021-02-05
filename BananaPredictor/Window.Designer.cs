
namespace BananaPredictor
{
    partial class Window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.submitBeatmap = new System.Windows.Forms.Button();
            this.browseBeatmap = new System.Windows.Forms.Button();
            this.tbBeatmap = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // submitBeatmap
            // 
            this.submitBeatmap.Location = new System.Drawing.Point(321, 55);
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
            this.browseBeatmap.Location = new System.Drawing.Point(321, 16);
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
            this.tbBeatmap.Location = new System.Drawing.Point(14, 16);
            this.tbBeatmap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbBeatmap.Name = "tbBeatmap";
            this.tbBeatmap.Size = new System.Drawing.Size(300, 27);
            this.tbBeatmap.TabIndex = 2;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 105);
            this.Controls.Add(this.tbBeatmap);
            this.Controls.Add(this.browseBeatmap);
            this.Controls.Add(this.submitBeatmap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Window";
            this.Text = "Banana Predictor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submitBeatmap;
        private System.Windows.Forms.Button browseBeatmap;
        private System.Windows.Forms.TextBox tbBeatmap;
    }
}

