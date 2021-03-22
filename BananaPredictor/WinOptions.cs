using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace BananaPredictor
{
    public partial class WinOptions : Form
    {
        // TODO: Don't use this, use the SpinnerPredictor's list instead
        private Dictionary<int, List<int>> spinnerAOR = new();

        // TODO: Remove this and replace this with an actual counting system
        private int prev = 0;

        public WinOptions()
        {
            InitializeComponent();

            // Using this since CheckedState doesn't do anything if you just call it as false (since it already is)
            tbLeftStart.Enabled = cbMoveTo.Checked;
            tbRightStart.Enabled = cbMoveTo.Checked;
            tbLeftEnd.Enabled = cbMoveTo.Checked;
            tbRightEnd.Enabled = cbMoveTo.Checked;
            tbLeftPos.Enabled = !cbMoveTo.Checked;
            tbRightPos.Enabled = !cbMoveTo.Checked;
            tbLeftStart.BackColor = Color.DarkGray;
            tbRightStart.BackColor = Color.DarkGray;
            tbLeftEnd.BackColor = Color.DarkGray;
            tbRightEnd.BackColor = Color.DarkGray;
            tbLeftPos.BackColor = SystemColors.Control;
            tbRightPos.BackColor = SystemColors.Control;

            AddMore();
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
                && (String.IsNullOrEmpty(tbDistance.Text) || String.IsNullOrWhiteSpace(tbDistance.Text))
                && (String.IsNullOrEmpty(tbLeftPos.Text) || String.IsNullOrWhiteSpace(tbLeftPos.Text))
                && (String.IsNullOrEmpty(tbRightPos.Text) || String.IsNullOrWhiteSpace(tbRightPos.Text)))
            {
                Console.WriteLine("One, few or all lines are missing mandatory input");
                MessageBox.Show("Input integers into the textboxes", "Error");
                return;
            }

            try
            {
                if ((Int32.Parse(tbStartTime.Text) >= Int32.Parse(tbEndTime.Text))
                    || (Int32.Parse(tbLeftPos.Text) >= Int32.Parse(tbRightPos.Text)))
                {
                    Console.WriteLine("Start Time or Pos is bigger than End Time or Pos");
                    MessageBox.Show("Start Time or Pos must be bigger than End Time or Poss", "Error");
                    return;
                }
            } catch (FormatException)
            {
                ExceptionMade();
                return;
            }

            SavingData(prev);

            win.bspr.spinnerSpecs = spinnerAOR;
            win.lStatus.Text = "Values saved";

            MessageBox.Show("Values inputted", "Success");
            win.Show();
            win.Focus();
            Close();
        }

        // Dynamic spinner list
        // TODO: Replace this where it compliments the combobox
        private void BPlus_Click(object sender, EventArgs e)
        {
            AddMore();
        }

        // TODO: Remove a list of spinners
        private void BMinus_Click(object sender, EventArgs e)
        {
            int saving = (int)cbList.SelectedItem;
            if (cbList.Items.Count > 1)
            {
                if ((int)cbList.SelectedIndex == 0)
                    cbList.SelectedIndex++;
                else
                    cbList.SelectedIndex--;

                spinnerAOR.Remove(saving);
                cbList.Items.Remove(saving);

                // TODO: Change this so that if the index doesn't match up the index of cbList, then reduce it by 1
                for (int i = 0; i < cbList.Items.Count; i++)
                {
                    if (!cbList.Items[i].Equals(i))
                    {
                        //cbList.Items[i]. -= 1;
                    }
                }
            }
            else
                MessageBox.Show("You can only have a minimum amount of 1 spinner", "Error");
        }

        private void CbList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (spinnerAOR.Count > 0)
                SavingData(prev);

            tbStartTime.Text = spinnerAOR[(int)cbList.SelectedItem][0].ToString();
            tbEndTime.Text = spinnerAOR[(int)cbList.SelectedItem][1].ToString();

            tbDistance.Text = spinnerAOR[(int)cbList.SelectedIndex][2].ToString();
            cbInvert.Checked = Convert.ToBoolean(spinnerAOR[(int)cbList.SelectedIndex][3]);

            tbLeftPos.Text = spinnerAOR[(int)cbList.SelectedItem][4].ToString();
            tbRightPos.Text = spinnerAOR[(int)cbList.SelectedItem][5].ToString();

            prev = (int)cbList.SelectedItem;
        }

        // Dynamic Spinner
        private void CbMoveTo_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: Fix then remove this
            if (cbMoveTo.Checked)
                MessageBox.Show("They are in a broken state right now; under development", "Warning");
            cbMoveTo.Checked = false;

            /*if (!cbMoveTo.Checked)
            {
                tbLeftStart.BackColor = Color.DarkGray;
                tbRightStart.BackColor = Color.DarkGray;
                tbLeftEnd.BackColor = Color.DarkGray;
                tbRightEnd.BackColor = Color.DarkGray;
                tbLeftPos.BackColor = SystemColors.Control;
                tbRightPos.BackColor = SystemColors.Control;
            }
            else
            {
                tbLeftStart.BackColor = SystemColors.Control;
                tbRightStart.BackColor = SystemColors.Control;
                tbLeftEnd.BackColor = SystemColors.Control;
                tbRightEnd.BackColor = SystemColors.Control;
                tbLeftPos.BackColor = Color.DarkGray;
                tbRightPos.BackColor = Color.DarkGray;
            }

            tbLeftStart.Enabled = cbMoveTo.Checked;
            tbRightStart.Enabled = cbMoveTo.Checked;
            tbLeftEnd.Enabled = cbMoveTo.Checked;
            tbRightEnd.Enabled = cbMoveTo.Checked;
            tbLeftPos.Enabled = !cbMoveTo.Checked;
            tbRightPos.Enabled = !cbMoveTo.Checked;*/
        }

        // Functions to get things done easier
        public void AddMore()
        {
            int num = spinnerAOR.Count == 0 ? 0 : spinnerAOR.Count;

            cbList.Items.Add(num);
            // Instead of using int[], use List<int>() to allow for more dynamic things
            //spinnerAOR.Add(num, new int[] { 0, 0, 0, 0 });
            // TODO: This looks weird, fix it
            List<int> list = new();
            list.Add(0);
            list.Add(0);
            list.Add(1);
            list.Add(0);
            list.Add(0);
            list.Add(0);
            spinnerAOR.Add(num, list);

            cbList.SelectedItem = num;
            tbStartTime.Text = spinnerAOR[num][0].ToString();
            tbEndTime.Text = spinnerAOR[num][1].ToString();

            tbDistance.Text = spinnerAOR[num][2].ToString();
            cbInvert.Checked = Convert.ToBoolean(spinnerAOR[num][3]);

            tbLeftPos.Text = spinnerAOR[num][4].ToString();
            tbRightPos.Text = spinnerAOR[num][5].ToString();
        }

        public void SavingData(int num)
        {
            try
            {
                Console.WriteLine("Saving...");
                /*if (cbMoveTo.Checked)
                {
                    spinnerAOR[num][0] = Int32.Parse(tbStartTime.Text);
                    spinnerAOR[num][1] = Int32.Parse(tbEndTime.Text);
                
                    spinnerAOR[num][2] = Int32.Parse(tbEndTime.Text);
                    spinnerAOR[num][3] = Convert.ToInt32(cbInvert.Checked);

                    spinnerAOR[num][4] = Int32.Parse(tbLeftStart.Text);
                    spinnerAOR[num][5] = Int32.Parse(tbRightStart.Text);
                    spinnerAOR[num][6] = Int32.Parse(tbLeftEnd.Text);
                    spinnerAOR[num][7] = Int32.Parse(tbRightEnd.Text);
                }
                else
                {*/
                spinnerAOR[num][0] = Int32.Parse(tbStartTime.Text);
                spinnerAOR[num][1] = Int32.Parse(tbEndTime.Text);

                spinnerAOR[num][2] = Int32.Parse(tbDistance.Text);
                spinnerAOR[num][3] = Convert.ToInt32(cbInvert.Checked);

                spinnerAOR[num][4] = Int32.Parse(tbLeftPos.Text);
                spinnerAOR[num][5] = Int32.Parse(tbRightPos.Text);

                ConsoleSave();
            }
            catch (FormatException)
            {
                ExceptionMade();
                return;
            }
        }

        public void ConsoleSave()
        {
            foreach (KeyValuePair<int, List<int>> kvp in spinnerAOR)
                Console.WriteLine("Index: {0}, StartTime: {1},  EndTime: {2}, Distance: {3}, Invert: {4},  StartPos: {5},  EndPos: {6}",
                    kvp.Key, kvp.Value[0], kvp.Value[1], kvp.Value[2], kvp.Value[3], kvp.Value[4], kvp.Value[5]);
        }

        public void ExceptionMade()
        {
            MessageBox.Show("Please only input integers", "Error");
            tbStartTime.Text = "0";
            tbEndTime.Text = "0";

            tbDistance.Text = "1";
            cbInvert.Checked = false;

            tbLeftPos.Text = "0";
            tbRightPos.Text = "0";
        }

        // Inputting integers only in TextBox
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

        private void TbLeftStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbRightStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbLeftEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbRightEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TbSpawnRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        // Focus on window, not console
        private void WinOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            win.Show();
            win.Focus();
        }
    }
}
