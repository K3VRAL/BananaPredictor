using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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
        readonly int OriginalHeight, OriginalWidth;

        // Thread
        Thread processingThread;
        BananaSpinPredictorRedo bspr;
        
        // Loading and Initializing
        public K3Banana()
        {
            InitializeComponent();
            consoleOutput();

            OriginalWidth = this.Width;
            OriginalHeight = this.Height;
            this.CenterToScreen();
            this.ActiveControl = tbBeatmap;
            this.bCancel.Hide();

            Console.WriteLine("Application Started Up");
        }

        // Output type won't let me use console application. Thanks stackoverflow for the help
        private void consoleOutput()
        {
            AllocConsole();
            Console.WriteLine("Console Window Started Up\nDon't close this window. It's more to look at how the program is working or if it is");
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        // Main Window
        private void bSubmit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Submit Clicked");

            if (!File.Exists(tbBeatmap.Text) || !Path.GetExtension(fileDir.FileName).Equals(".osu"))
            {
                MessageBox.Show("There was an error trying to get the file. Make sure it either exists or has the \".osu\" extension.");
                return;
            }

            bCancel.Show();
            bSubmit.Hide();
            bspr = new();
            processingThread = new(new ThreadStart(threadMethod));
            processingThread.Start();
        }

        private void threadMethod()
        {
            if (bspr.SpinnerPredictor(tbBeatmap.Text, cbDebug.Checked, 112, 400))
                MessageBox.Show("Successfully made conversion! Press F5 in osu and it should be there.", "Done");
            else if (bspr.getFlag())
                MessageBox.Show("Canceled", "Error");
            else
                MessageBox.Show("Failed", "Error");
            Console.WriteLine("Program Ended");

            Invoke(new MethodInvoker(() =>
            {
                bCancel.Hide();
                bSubmit.Show();
            }));
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Browse Clicked");

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
        private void bOpen_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Directory Clicked");
            String dir = String.Join("\\", tbBeatmap.Text.Split('\\').Reverse().Skip(1).Reverse().ToArray());
            if (Directory.Exists(dir))
                Process.Start("explorer.exe", String.Join("\\", tbBeatmap.Text.Split('\\').Reverse().Skip(1).Reverse().ToArray()));
            else
                MessageBox.Show("Directory doesn't exist!", "Error");
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Cancel Clicked");
            if (processingThread.IsAlive)
            {
                bspr.Stop();
                processingThread.Join();
            }
            bSubmit.Show();
            bCancel.Hide();
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
            Console.WriteLine("Minimize Clicked");
            tAnimation.Interval = 15;
            tAnimation.Start();
        }

        private void wbExit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit Clicked");
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
                this.Height = OriginalHeight;
                this.Width = OriginalWidth;
                this.WindowState = FormWindowState.Minimized;
            }
        }
    }
}
