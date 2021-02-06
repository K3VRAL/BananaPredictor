using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using BananaPredictor.Osu;

namespace BananaPredictor
{
    // Apologies for the terrible code/not using other libraries such as OsuPrasers to get the job done
    public partial class K3Banana : Form
    {
        // For Dialog Box
        private readonly OpenFileDialog fileDir = new();
        
        // For Window
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        // Window Related Animations
        int OriginalHeight, OriginalWidth;
        int ScreenHeight = Screen.PrimaryScreen.Bounds.Height, ScreenWidth = Screen.PrimaryScreen.Bounds.Width;

        public K3Banana()
        {
            InitializeComponent();
            OriginalWidth = this.Width;
            OriginalHeight = this.Height;
        }

        // Main Window
        private void bSubmit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Submission Clicked");

            if (!File.Exists(tbBeatmap.Text) || !Path.GetExtension(fileDir.FileName).Equals(".osu"))
            {
                MessageBox.Show("There was an error trying to get the file. Make sure it either exists or has the \".osu\" extension.");
                return;
            }

            bool did;
            if (cbDebug.Checked)
            {
                BananaSpinPredictor aNew = new();
                did = aNew.SpinnerPredictor(tbBeatmap.Text);
            } else
            {
                BananaSpinMaker aNew = new();
                did = aNew.SpinnerMaker(tbBeatmap.Text);
                MessageBox.Show("Currently under development.");
            }

            if (did) {
                MessageBox.Show("Successfully made conversion! Press F5 in osu and it should be there.", "Done");
                Process.Start("explorer.exe", String.Join("\\", tbBeatmap.Text.Split('\\').Reverse().Skip(1).Reverse().ToArray()));
            } else {
                MessageBox.Show("Failed!", "Error");
            }
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Browse Clicked");

            fileDir.Filter = "osu Files (*.osu)|*.osu";
            fileDir.FilterIndex = 0;
            fileDir.RestoreDirectory = true;
            fileDir.Multiselect = false;

            if (fileDir.ShowDialog().Equals(DialogResult.OK))
            {
                tbBeatmap.Text = fileDir.FileName;
                return;
            }
        }

        private void cbDebug_MouseHover(object sender, EventArgs e)
        {
            ttDebug.Show("Using Predictor than Maker to test and see if the code works under other specific unknown situations", cbDebug);
        }

        // Window Panel/Top Bar
        private void tPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void wbMini_Click(object sender, EventArgs e)
        {
            tAnimation.Interval = 15;
            tAnimation.Start();
        }

        private void wbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tAnimation_Tick(object sender, EventArgs e)
        {
            if (this.Height > 5 && this.Width > 5)
            {
                this.Height -= OriginalHeight / 10;
                this.Width -= OriginalHeight / 10;
            }
            else
            {
                tAnimation.Stop();
                this.WindowState = FormWindowState.Minimized;
                this.Height = OriginalHeight;
                this.Width = OriginalWidth;
            }
        }
    }
}
