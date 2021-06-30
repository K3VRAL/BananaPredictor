using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using BananaPredictor.Osu;

namespace BananaPredictor
{
    public partial class WinOptions : Form
    {
        private StoringInfoDict spinnerAOR = new();

        // TODO: Remove this and replace this with an actual counting system
        private int prev = 0;

        private WinOptions()
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

            // TODO: Get saved items
            //if (win.bspr.spinnerSpecs == null) AddMore(); else spinnerAOR = win.bspr.spinnerSpecs;
            AddMore();

            tbLeftStart.Text = "0";
            tbRightStart.Text = "0";
            tbLeftEnd.Text = "1";
            tbRightEnd.Text = "1";
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
            SavingData(prev);

            for (int i = 0; i < spinnerAOR.Count; i++)
                if (spinnerAOR[i].MapRelated[0] >= spinnerAOR[i].MapRelated[1])
                {
                    Console.WriteLine("Start Time is bigger than End Time from list {0}", i);
                    MessageBox.Show("Start Time is bigger than End Time from list" + i, "Error");
                    cbList.SelectedItem = i;
                    return;
                }
                else if (spinnerAOR[i].MapRelated[2] <= 0)
                {
                    Console.WriteLine("Distance Snap from list {0} must be bigger than 0", i);
                    MessageBox.Show("Distance Snap from list " + i + " must be bigger than 0", "Error");
                    cbList.SelectedItem = i;
                    return;
                }
                else if (cbMoveTo.Checked && (spinnerAOR[i].SpinnerRelated[0] >= spinnerAOR[i].SpinnerRelated[2]
                    || spinnerAOR[i].SpinnerRelated[1] >= spinnerAOR[i].SpinnerRelated[3]))
                {
                    Console.WriteLine("Left or Right Start is bigger than Left or Right End from list {0}. Use the Invert checkBox instead", i);
                    MessageBox.Show("Left or Right Start is bigger than Left or Right End from list " + i + ". Use the Invert checkBox instead", "Error");
                    cbList.SelectedItem = i;
                    return;
                }
                else if (!cbMoveTo.Checked && spinnerAOR[i].SpinnerRelated[0] >= spinnerAOR[i].SpinnerRelated[1])
                {
                    Console.WriteLine("Left Empty Pos is bigger than Right Empty Pos from list {0}. Use the Invert checkBox instead", i);
                    MessageBox.Show("Left Empty Pos is bigger than Right Empty Pos from list " + i + ". Use the Invert checkBox instead", "Error");
                    cbList.SelectedItem = i;
                    return;
                }

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
            // TODO: Fix then remove this
            MessageBox.Show("They are in a broken state right now; under development", "Warning");
            //AddMore();
        }

        // TODO: Remove a list of spinners
        private void BMinus_Click(object sender, EventArgs e)
        {
            // TODO: Fix then remove this
            MessageBox.Show("They are in a broken state right now; under development", "Warning");
            /*int saving = (int)cbList.SelectedItem;
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
                    if (!((int)cbList.Items[i] == i))
                    {
                        //cbList.Items[i]. -= 1;
                    }
                }
            }
            else
                MessageBox.Show("You can only have a minimum amount of 1 spinner", "Error");*/
        }

        private void CbList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (spinnerAOR.Count > 0)
                SavingData(prev);

            ToTextBox((int)cbList.SelectedItem);

            prev = (int)cbList.SelectedItem;
        }

        // Dynamic Spinner
        private void CbMoveTo_CheckedChanged(object sender, EventArgs e)
        {
            // TODO: Fix then remove this
            /*if (cbMoveTo.Checked)
                MessageBox.Show("They are in a broken state right now; under development", "Warning");
            cbMoveTo.Checked = false;*/

            if (!cbMoveTo.Checked)
            {
                spinnerAOR[(int)cbList.SelectedItem].SpinnerRelated.RemoveAt(0);
                spinnerAOR[(int)cbList.SelectedItem].SpinnerRelated.RemoveAt(0);
                tbLeftStart.BackColor = Color.DarkGray;
                tbRightStart.BackColor = Color.DarkGray;
                tbLeftEnd.BackColor = Color.DarkGray;
                tbRightEnd.BackColor = Color.DarkGray;
                tbLeftPos.BackColor = SystemColors.Control;
                tbRightPos.BackColor = SystemColors.Control;
                tbLeftPos.Text = "0";
                tbRightPos.Text = "1";
            }
            else
            {
                spinnerAOR[(int)cbList.SelectedItem].SpinnerRelated.Add(0);
                spinnerAOR[(int)cbList.SelectedItem].SpinnerRelated.Add(0);
                tbLeftStart.BackColor = SystemColors.Control;
                tbRightStart.BackColor = SystemColors.Control;
                tbLeftEnd.BackColor = SystemColors.Control;
                tbRightEnd.BackColor = SystemColors.Control;
                tbLeftPos.BackColor = Color.DarkGray;
                tbRightPos.BackColor = Color.DarkGray;
                tbLeftStart.Text = "0";
                tbRightStart.Text = "0";
                tbLeftEnd.Text = "1";
                tbRightEnd.Text = "1";
            }

            tbLeftStart.Enabled = cbMoveTo.Checked;
            tbRightStart.Enabled = cbMoveTo.Checked;
            tbLeftEnd.Enabled = cbMoveTo.Checked;
            tbRightEnd.Enabled = cbMoveTo.Checked;
            tbLeftPos.Enabled = !cbMoveTo.Checked;
            tbRightPos.Enabled = !cbMoveTo.Checked;
        }

        private void CbOnlySpin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOnlySpin.Checked)
            {
                tbLeftPos.BackColor = Color.DarkGray;
                tbRightPos.BackColor = Color.DarkGray;

                tbLeftStart.BackColor = Color.DarkGray;
                tbRightStart.BackColor = Color.DarkGray;
                tbLeftEnd.BackColor = Color.DarkGray;
                tbRightEnd.BackColor = Color.DarkGray;

                tbLeftPos.Text = "0";
                tbRightPos.Text = "1";

                tbLeftStart.Text = "0";
                tbRightStart.Text = "0";
                tbLeftEnd.Text = "1";
                tbRightEnd.Text = "1";
            }
            else
            {
                if (cbMoveTo.Checked)
                {
                    tbLeftStart.BackColor = SystemColors.Control;
                    tbRightStart.BackColor = SystemColors.Control;
                    tbLeftEnd.BackColor = SystemColors.Control;
                    tbRightEnd.BackColor = SystemColors.Control;

                    tbLeftStart.Enabled = !cbOnlySpin.Checked;
                    tbRightStart.Enabled = !cbOnlySpin.Checked;
                    tbLeftEnd.Enabled = !cbOnlySpin.Checked;
                    tbRightEnd.Enabled = !cbOnlySpin.Checked;
                }
                else
                {
                    tbLeftPos.BackColor = SystemColors.Control;
                    tbRightPos.BackColor = SystemColors.Control;

                    tbLeftPos.Enabled = !cbOnlySpin.Checked;
                    tbRightPos.Enabled = !cbOnlySpin.Checked;
                }
            }

            cbInvert.Visible = !cbOnlySpin.Checked;
            cbMoveTo.Visible = !cbOnlySpin.Checked;
        }

        // Functions to get things done easier
        private void AddMore()
        {
            int num = spinnerAOR.Count;

            cbList.Items.Add(num);
            // Instead of using int[], use List<int> to allow for more dynamic things
            // TODO: This looks weird, fix it
            List<int> listMap = new();
            List<int> listSpin = new();
            listMap.Add(0);
            listMap.Add(1);
            listMap.Add(1);
            listMap.Add(0);
            listMap.Add(0);
            listSpin.Add(0);
            listSpin.Add(1);
            spinnerAOR.Add(num, listMap, listSpin);

            cbList.SelectedItem = num;
            ToTextBox(num);
        }

        private void SavingData(int num)
        {
            try
            {
                Console.WriteLine("Saving...");

                spinnerAOR[num].MapRelated[0] = Int32.Parse(tbStartTime.Text);
                spinnerAOR[num].MapRelated[1] = Int32.Parse(tbEndTime.Text);
                spinnerAOR[num].MapRelated[2] = Int32.Parse(tbDistance.Text);
                spinnerAOR[num].MapRelated[3] = Convert.ToInt32(cbInvert.Checked);
                spinnerAOR[num].MapRelated[4] = Convert.ToInt32(cbOnlySpin.Checked);

                if (cbMoveTo.Checked)
                {
                    spinnerAOR[num].SpinnerRelated[0] = Int32.Parse(tbLeftStart.Text);
                    spinnerAOR[num].SpinnerRelated[1] = Int32.Parse(tbRightStart.Text);
                    spinnerAOR[num].SpinnerRelated[2] = Int32.Parse(tbLeftEnd.Text);
                    spinnerAOR[num].SpinnerRelated[3] = Int32.Parse(tbRightEnd.Text);
                }
                else
                {
                    spinnerAOR[num].SpinnerRelated[0] = Int32.Parse(tbLeftPos.Text);
                    spinnerAOR[num].SpinnerRelated[1] = Int32.Parse(tbRightPos.Text);
                }

                ConsoleSave();
            }
            catch (FormatException)
            {
                ExceptionMade();
                return;
            }
        }

        private void ToTextBox(int num)
        {
            tbStartTime.Text = spinnerAOR[num].MapRelated[0].ToString();
            tbEndTime.Text = spinnerAOR[num].MapRelated[1].ToString();
            tbDistance.Text = spinnerAOR[num].MapRelated[2].ToString();
            cbInvert.Checked = Convert.ToBoolean(spinnerAOR[num].MapRelated[3]);
            cbOnlySpin.Checked = Convert.ToBoolean(spinnerAOR[num].MapRelated[4]);

            if (cbMoveTo.Checked)
            {
                tbLeftStart.Text = spinnerAOR[num].SpinnerRelated[0].ToString();
                tbRightStart.Text = spinnerAOR[num].SpinnerRelated[1].ToString();
                tbLeftEnd.Text = spinnerAOR[num].SpinnerRelated[2].ToString();
                tbRightEnd.Text = spinnerAOR[num].SpinnerRelated[3].ToString();
            }
            else
            {
                tbLeftPos.Text = spinnerAOR[num].SpinnerRelated[0].ToString();
                tbRightPos.Text = spinnerAOR[num].SpinnerRelated[1].ToString();
            }
        }

        private void ConsoleSave()
        {
            foreach (KeyValuePair<int, StoringInfo> kvp in spinnerAOR)
                if (kvp.Value.SpinnerRelated.Count == 2)
                    Console.WriteLine("Index: {0}"
                        + "\nStartTime: {1},  EndTime: {2}, Distance: {3}, Invert: {4}, SpinnerOnly: {5},"
                        + "\nStartPos: {6},  EndPos: {7}",
                        kvp.Key,
                        kvp.Value.MapRelated[0], kvp.Value.MapRelated[1], kvp.Value.MapRelated[2], Convert.ToBoolean(kvp.Value.MapRelated[3]), Convert.ToBoolean(kvp.Value.MapRelated[4]),
                        kvp.Value.SpinnerRelated[0], kvp.Value.SpinnerRelated[1]);
                else if (kvp.Value.SpinnerRelated.Count == 4)
                    Console.WriteLine("Index: {0}"
                        + "\nStartTime: {1},  EndTime: {2}, Distance: {3}, Invert: {4}, SpinnerOnly: {5},"
                        + "\nLeftStartPos: {6},  RightStartPos: {7},  LeftEndPos: {8},  RightEndPos: {9}",
                        kvp.Key,
                        kvp.Value.MapRelated[0], kvp.Value.MapRelated[1], kvp.Value.MapRelated[2], Convert.ToBoolean(kvp.Value.MapRelated[3]), Convert.ToBoolean(kvp.Value.MapRelated[4]),
                        kvp.Value.SpinnerRelated[0], kvp.Value.SpinnerRelated[1], kvp.Value.SpinnerRelated[2], kvp.Value.SpinnerRelated[3]);
        }

        private void ExceptionMade()
        {
            MessageBox.Show("Please only input integers", "Error");
            tbStartTime.Text = "0";
            tbEndTime.Text = "0";
            tbDistance.Text = "1";

            if (cbMoveTo.Checked)
            {
                tbLeftStart.Text = "0";
                tbRightStart.Text = "0";
                tbLeftEnd.Text = "1";
                tbRightEnd.Text = "1";
            }
            else
            {
                tbLeftPos.Text = "0";
                tbRightPos.Text = "1";
            }
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
