using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using BananaPredictor.Osu;
using SixLabors.ImageSharp;

namespace BananaPredictor
{
    // Apologies for the terrible code/not using other libraries such as OsuPrasers to get the job done
    public partial class K3BananaWindow : Form
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
        public BananaSpinPredictor bspr;
        bool cancel = true;

        // Option Windows
        WindowOptions oWin;

        // Loading and Initializing
        public K3BananaWindow()
        {
            InitializeComponent();

            bspr = new();

            OriginalWidth = this.Width;
            OriginalHeight = this.Height;
            this.CenterToScreen();

            Console.WriteLine("Application Started Up");
        }

        // TODO: Fix issue where nothing is being written to the console
        // Output type won't let me use console application. Thanks stackoverflow for the help
        public void consoleOutput()
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
            if (!File.Exists(tbBeatmap.Text) || !Path.GetExtension(fileDir.FileName).Equals(".osu"))
            {
                MessageBox.Show("There was an error trying to get the file. Make sure it either exists or has the \".osu\" extension");
                return;
            }

            if (bspr.startTime <= 0 && bspr.endTime <= 0 && bspr.startPos <= 0 && bspr.endPos <= 0)
            {
                MessageBox.Show("Please go to options and input the neccessary values", "Error");
                return;
            }

            cancel = !cancel;

            if (cancel)
            {
                Console.WriteLine("Cancel Clicked");

                if (processingThread.IsAlive)
                {
                    bspr.Stop();
                    processingThread.Join();
                }
            } else
            {
                Console.WriteLine("Submit Clicked");

                processingThread = new(new ThreadStart(threadMethod));
                processingThread.Start();
            }
        }

        private void threadMethod()
        {
            Invoke(new Action(() =>
            {
                lStatus.Text = "Processing";
                bSubmit.Text = "Cancel";
                bSubmit.BackColor = System.Drawing.Color.Red;
            }));

            if (bspr.SpinnerPredictor(tbBeatmap.Text))
            {
                Invoke(new Action(() => { lStatus.Text = "Success"; }));
                Console.WriteLine("Program Success");
                MessageBox.Show("Successfully made conversion! Press F5 in osu and it should be there", "Done");
            }
            else if (bspr.getFlag())
            {
                Invoke(new Action(() => { lStatus.Text = "Canceled"; }));
                Console.WriteLine("Program Canceled");
                MessageBox.Show("Canceled", "Error");
            }
            else
            {
                Invoke(new Action(() => { lStatus.Text = "Failed"; }));
                Console.WriteLine("Program Failed");
                MessageBox.Show("Failed", "Error");
            }

            Invoke(new Action(() =>
            {
                Console.WriteLine("Program Ended");
                bSubmit.Text = "Submit";
                bSubmit.BackColor = System.Drawing.Color.Transparent;
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
                MessageBox.Show("Directory doesn't exist", "Error");
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Options Clicked");
            if (Application.OpenForms.OfType<WindowOptions>().Count() == 1)
                Application.OpenForms.OfType<WindowOptions>().First().Close();
            oWin = new(this);
            oWin.StartPosition = FormStartPosition.CenterScreen;
            oWin.Show();
            // Open another winform and click on avaiable features
            // Learn from https://www.youtube.com/watch?v=wgcrxUjXR-I and others

            // IDEAS:
            // + Add in final hitobject for platter reset
            // + Make list on where to place sliders (maybe as well as changing where the end slider will go)
            // + Scale from beginning to end on where the spinner will be
            // + Have the ability to change where the banana will be placed (static)
            // + Scale of randomness from 0 to specifically
            // + Debug mode
            // + Invert bananas
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
