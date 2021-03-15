using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BananaPredictor
{
    public partial class WinOptions : Form
    {
        private Dictionary<int, int[]> spinnerAOR = new();
        public WinOptions()
        {
            InitializeComponent();

            cbList.Items.Add(0);
            spinnerAOR.Add(0, new int[] { 0, 0, 0, 0 });
            cbList.SelectedItem = 0;
            tbStartTime.Text = spinnerAOR[(int)cbList.SelectedItem][0].ToString();
            tbEndTime.Text = spinnerAOR[(int)cbList.SelectedItem][1].ToString();
            tbStartPos.Text = spinnerAOR[(int)cbList.SelectedItem][2].ToString();
            tbEndPos.Text = spinnerAOR[(int)cbList.SelectedItem][3].ToString();
        }

        private K3BananaWindow win = null;
        public WinOptions(Form calling) : this()
        {
            win = calling as K3BananaWindow;
        }

        private void BConsole_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you close the console, you would have to restart the whole program", "Warning");
            WinConsole.Initialize();
            Console.WriteLine("Opened Console Window");
            win.Focus();
            Focus();
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            if ((String.IsNullOrEmpty(tbStartTime.Text) || String.IsNullOrWhiteSpace(tbStartTime.Text))
                && (String.IsNullOrEmpty(tbEndTime.Text) || String.IsNullOrWhiteSpace(tbEndTime.Text))
                && (String.IsNullOrEmpty(tbStartPos.Text) || String.IsNullOrWhiteSpace(tbStartPos.Text))
                && (String.IsNullOrEmpty(tbEndPos.Text) || String.IsNullOrWhiteSpace(tbEndPos.Text)))
            {
                Console.WriteLine("One, few or all lines are missing mandatory input");
                MessageBox.Show("Input integers into the textboxes", "Error");
                return;
            }
            if ((Int32.Parse(tbStartTime.Text) >= Int32.Parse(tbEndTime.Text)) 
                || (Int32.Parse(tbStartPos.Text) >= Int32.Parse(tbEndPos.Text)))
            {
                Console.WriteLine("Start Time or Pos is bigger than End Time or Pos");
                MessageBox.Show("Start Time or Pos must be bigger than End Time or Poss", "Error");
                return;
            }

            Console.WriteLine("Saving...");
            spinnerAOR[(int)cbList.SelectedItem][0] = Int32.Parse(tbStartTime.Text);
            spinnerAOR[(int)cbList.SelectedItem][1] = Int32.Parse(tbEndTime.Text);
            spinnerAOR[(int)cbList.SelectedItem][2] = Int32.Parse(tbStartPos.Text);
            spinnerAOR[(int)cbList.SelectedItem][3] = Int32.Parse(tbEndPos.Text);
            
            foreach (KeyValuePair<int, int[]> kvp in spinnerAOR)
                Console.WriteLine("Index: {0}, StartTime: {1},  EndTime: {2},  StartPos: {3},  EndPos: {4}",
                    kvp.Key, kvp.Value[0], kvp.Value[1], kvp.Value[2], kvp.Value[3]);

            win.bspr.spinnerSpecs = spinnerAOR;
            win.lStatus.Text = "Values saved";

            MessageBox.Show("Values inputted", "Success");
            win.Show();
            win.Focus();
            Close();
        }

        // Dynamic spinner list
        // TODO: Add in lists of spinners
        private void BMinus_Click(object sender, EventArgs e)
        {
            /*
            spinnerAOR.Remove((int)cbList.SelectedItem);
            cbList.Items.Remove(cbList.SelectedItem);
            */
        }

        private void BPlus_Click(object sender, EventArgs e)
        {
            /*
            cbList.Items.Add((int)cbList.SelectedItem + 1);
            spinnerAOR.Add((int)cbList.SelectedItem + 1, new int[] { 0, 0, 0, 0 });
            cbList.SelectedIndex = (int)cbList.SelectedItem + 1;
            tbStartTime.Text = 0.ToString();
            tbEndTime.Text = 0.ToString();
            tbStartPos.Text = 0.ToString();
            tbEndPos.Text = 0.ToString();
            addingToSpinner();
            */
        }

        /*private void addingToSpinner()
        {
            if (String.IsNullOrEmpty(tbStartTime.Text) || String.IsNullOrWhiteSpace(tbStartTime.Text))
                tbStartTime.Text = 0.ToString();
            if (String.IsNullOrEmpty(tbEndTime.Text) || String.IsNullOrWhiteSpace(tbEndTime.Text))
                tbEndTime.Text = 0.ToString();
            if (String.IsNullOrEmpty(tbStartPos.Text) || String.IsNullOrWhiteSpace(tbStartPos.Text))
                tbStartPos.Text = 0.ToString();
            if (String.IsNullOrEmpty(tbEndPos.Text) || String.IsNullOrWhiteSpace(tbEndPos.Text))
                tbEndPos.Text = 0.ToString();
            spinnerAOR[(int)cbList.SelectedItem][0] = Int32.Parse(tbStartTime.Text);
            spinnerAOR[(int)cbList.SelectedItem][1] = Int32.Parse(tbEndTime.Text);
            spinnerAOR[(int)cbList.SelectedItem][2] = Int32.Parse(tbStartPos.Text);
            spinnerAOR[(int)cbList.SelectedItem][3] = Int32.Parse(tbEndPos.Text);

            foreach (KeyValuePair<int, int[]> kvp in spinnerAOR)
                Console.WriteLine(String.Format("Key = {0}, [0] = {1},  [1] = {2},  [2] = {3},  [3] = {4}, ", kvp.Key, kvp.Value[0], kvp.Value[1], kvp.Value[2], kvp.Value[3]));
        }*/

        private void CbList_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            tbStartTime.Text = spinnerAOR[(int)cbList.SelectedItem][0].ToString();
            tbEndTime.Text = spinnerAOR[(int)cbList.SelectedItem][1].ToString();
            tbStartPos.Text = spinnerAOR[(int)cbList.SelectedItem][2].ToString();
            tbEndPos.Text = spinnerAOR[(int)cbList.SelectedItem][3].ToString();
            */
        }

        // Inputting integers only
        private void TbStartSpin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbEndSpin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbStartPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbEndPos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // Debug checkbox
        private void CbDebug_MouseHover(object sender, EventArgs e)
        {
            ttDebug.Show("Debug Mode is used to be able to place the bananas as beats and show where they will be placed", cbDebug);
        }

        private void CbDebug_Click(object sender, EventArgs e)
        {
            win.bspr.debug = cbDebug.Checked;
        }

        // Focus on window, not console
        private void WinOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            win.Show();
            win.Focus();
        }
    }
}
