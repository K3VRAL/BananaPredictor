using System;
using System.Windows.Forms;

namespace BananaPredictor
{
    public partial class WindowOptions : Form
    {
        public WindowOptions()
        {
            InitializeComponent();
        }

        private K3BananaWindow win = null;
        public WindowOptions(Form calling) : this()
        {
            win = calling as K3BananaWindow;
        }

        private void bConsole_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you close the console, you would have to restart the whole program", "Warning");
            win.consoleOutput();
            win.Focus();
            this.Focus();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            if ((String.IsNullOrEmpty(tbStartTime.Text) || String.IsNullOrWhiteSpace(tbStartTime.Text))
                && (String.IsNullOrEmpty(tbEndTime.Text) || String.IsNullOrWhiteSpace(tbEndTime.Text))
                && (String.IsNullOrEmpty(tbStartPos.Text) || String.IsNullOrWhiteSpace(tbStartPos.Text))
                && (String.IsNullOrEmpty(tbEndPos.Text) || String.IsNullOrWhiteSpace(tbEndPos.Text)))
            {
                Console.WriteLine("One, few or all lines are missing mandatory input");
                MessageBox.Show("Input integers into the textboxes", "Error");
                return;
            } else if ((Int32.Parse(tbStartTime.Text) > Int32.Parse(tbEndTime.Text)) 
                || (Int32.Parse(tbStartPos.Text) > Int32.Parse(tbEndPos.Text)))
            {
                Console.WriteLine("Start Time or Pos is bigger than End Time or Pos");
                MessageBox.Show("Start Time or Pos must be bigger than End Time or Poss", "Error");
                return;
            }

            win.bspr.startTime = Int32.Parse(tbStartTime.Text);
            win.bspr.endTime = Int32.Parse(tbEndTime.Text);
            win.bspr.startPos = Int32.Parse(tbStartPos.Text);
            win.bspr.endPos = Int32.Parse(tbEndPos.Text);
            win.bspr.newSpinner = cbNewSpin.Checked;
            win.lStatus.Text = "Values inputted";

            // TODO: Fix this
            MessageBox.Show("Values inputted", "Success");
            win.Show();
            win.Focus();
            this.Close();
        }

        private void tbStartSpin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void tbEndSpin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void tbStartPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void tbEndPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void cbDebug_MouseHover(object sender, EventArgs e)
        {
            ttDebug.Show("Debug Mode is used to be able to place the bananas as beats and show where they will be placed", cbDebug);
        }

        private void cbDebug_Click(object sender, EventArgs e)
        {
            win.bspr.debug = cbDebug.Checked;
        }
    }
}
