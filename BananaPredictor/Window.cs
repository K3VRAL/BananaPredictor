using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using BananaPredictor.Osu;

namespace BananaPredictor
{
    public partial class K3Banana : Form
    {
        private readonly OpenFileDialog fileDir = new();

        public K3Banana()
        {
            InitializeComponent();
        }

        private void SubmitBeatmap_Click(object sender, EventArgs e)
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
                var aNew = new BananaSpinPredictor();
                did = aNew.SpinnerPredictor(tbBeatmap.Text);
            } else
            {
                var aNew = new BananaSpinMaker();
                did = aNew.SpinnerMaker(tbBeatmap.Text);
            }

            if (did) {
                MessageBox.Show("Successfully made conversion! Press F5 in osu and it should be there.", "Done");
                Process.Start("explorer.exe", String.Join("\\", tbBeatmap.Text.Split('\\').Reverse().Skip(1).Reverse().ToArray()));
            } else {
                MessageBox.Show("Failed!", "Error");
            }
        }

        private void BrowseBeatmap_Click(object sender, EventArgs e)
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
            else if (fileDir.ShowDialog().Equals(DialogResult.Cancel)) // TODO: FIX LATER
            {
                return;
            }
            else
            {
                MessageBox.Show("An error has occurred. Make sure you selected the correct file.", "Error"); // TODO: FIX LATER
                return;
            }
        }

        private void cbDebug_MouseHover(object sender, EventArgs e)
        {
            ttDebug.Show("Using Predictor than Maker to test and see if the code works under other specific unknown situations", cbDebug);
        }
    }
}
