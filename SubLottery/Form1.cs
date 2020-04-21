using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubLottery
{
    public partial class Form1 : Form
    {
        private readonly static string path = "./subs.bin";

        // List with subscriber data
        private static BindingList<Subscriber> subs = new BindingList<Subscriber>();

        // Total subs given
        private static int subCount = 0;

        private static bool tableLoaded = false;

        public Form1()
        {
            InitializeComponent();
        }

        // Initial setup
        private void Form1_Load(object sender, EventArgs e)
        {
            // Read data from file into List
            subs = ReadData(path);

            countSubs();

            // Set up the DataGridView (the table)
            SubsTable.DataSource = subs;

            SubsTable.RowHeadersVisible = false;
            //SubsTable.Columns["Chance"].Visible = false;
            SubsTable.Columns["Name"].HeaderText = "Jméno";
            SubsTable.Columns["Name"].ReadOnly = true;

            SubsTable.Columns["Subs"].HeaderText = "#";
            SubsTable.Columns["Subs"].Width = 33;

            SubsTable.Columns["Enabled"].HeaderText = "Aktivní";
            SubsTable.Columns["Enabled"].Width = 50;
            SubsTable.Columns["Enabled"].ReadOnly = true;

            tableLoaded = true;
        }

        // DOC MISSING
        private void SubCountText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                insertSubscriber();
            }
        }

        // DOC MISSING
        private void InsertButton_Click(object sender, EventArgs e)
        {
            insertSubscriber();
        }

        // Clicking on this button starts the lottery
        private void DrawButton_Click(object sender, EventArgs e)
        {
            if (subs.Count == 0 || subCount == 0)
            {
                return;
            }

            WinnerLabel.Visible = true;
            WinnerLabel.Text = "3";

            // Start a 3 sec timer and then display winner name
            timer1.Start();
        }

        // Lottery timer event
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int timeLeft = int.Parse(WinnerLabel.Text);
            timeLeft -= 1;

            // When time reaches 0, stop the timer and display winner name
            if (timeLeft == 0)
            {
                timer1.Stop();
                WinnerLabel.Text = getWinnerName();
            }
            else
            {
                WinnerLabel.Text = timeLeft.ToString();
            }
        }

        // Insert a new subscriber or update an existing one
        private void insertSubscriber()
        {
            string name = nameText.Text;

            // Check if Name TextBox is empty
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            // Check if Subs checkbox is a number
            if (!int.TryParse(subCountText.Text, out int value) && !string.IsNullOrEmpty(name))
            {
                InsertButton.Text = "ERROR: Sub count must be a number!";
                return;
            }
            else
            {
                InsertButton.Text = "Přidat";
            }

            // Check if name is already in the table
            if (isUnique(name)) // Insert new subscriber
            {
                if (value <= 0)
                {
                    InsertButton.Text = "ERROR: Sub count must be greater than 1 when adding new sub!";
                    return;
                }

                // Add new subsriber to the table
                subCount += value;
                subs.Add(new Subscriber(name, value, subCount == 0 ? 1.0d : Math.Round((double)value / subCount, 8)));
            }
            else // Sub already in a table - increment subcount and update chances
            {
                if (value == 0)
                {
                    InsertButton.Text = "ERROR: Sub count must be other than 0 when adding subs!";
                    return;
                }

                // Iterate over all subs
                foreach (Subscriber s in subs)
                {
                    // sub found
                    if (s.Name == name)
                    {
                        // Removing sub
                        if (s.Subs + value == 0)
                        {
                            subs.Remove(s);
                            
                            if (s.Enabled == "Ano")
                            {
                                subCount += value;
                            }
                         
                            break;
                        }
                        // error
                        else if (s.Subs + value < 0)
                        {
                            InsertButton.Text = "ERROR: Subscriber count would go below or to 0!";
                            return;
                        }
                        // updating sub
                        else
                        {
                            s.Subs += value;

                            if (s.Enabled == "Ano")
                            {
                                // Sub ACTIVE => adding only new subs to total subCount
                                subCount += value;
                            }
                            else
                            {
                                // Sub INACTIVE => adding all it's subs to total subCount
                                subCount += s.Subs;
                            }

                            s.Enabled = "Ano";
                        }
                    }
                }
            }

            InsertButton.Text = "Přidat";

            nameText.Text = "";
            subCountText.Text = "";

            // Update winning chances of every ACTIVE subscriber based on the change in total subs
            updateChances();

            // Update subscriber table
            subs = new BindingList<Subscriber>(subs);
            SubsTable.DataSource = subs;
        }

        // Returns name of a winner when running the lottery
        // DOC MISSING
        private string getWinnerName()
        {
            Random rand = new Random();

            double value = rand.NextDouble(),
                   cumulative = 0.0;

            List<Subscriber> onlyEnabledSubs = new List<Subscriber>();

            foreach (Subscriber s in subs)
            {
                if (s.Enabled == "Ano")
                {
                    onlyEnabledSubs.Add(s);
                }
            }

            List<Subscriber> sortedByChance = onlyEnabledSubs.OrderBy(o => o.Chance).ToList();

            for (int i = 0; i < sortedByChance.Count - 1; i++)
            {
                if (cumulative <= value && value < (cumulative += sortedByChance[i].Chance))
                {
                    return sortedByChance[i].Name;
                }
            }

            return sortedByChance.Last<Subscriber>().Name;
        }

        // TRUE if name is not an existing subscriber, FALSE if name found
        private bool isUnique(string name)
        {
            foreach (Subscriber s in subs)
            {
                if (s.Name == name)
                {
                    return false;
                }
            }

            return true;
        }

        // Counts subs given by ACTIVE subscribers (in total)
        private void countSubs()
        {
            subCount = 0;

            foreach (Subscriber s in subs)
            {
                if (s.Enabled == "Ano")
                {
                    subCount += s.Subs;

                }
            }
        }
        
        // Updates winning chances of all ACTIVE subscribers
        private void updateChances()
        {
            foreach (Subscriber s in subs)
            {
                if (s.Enabled == "Ano")
                {
                    s.Chance = (subCount == 0 ? 0.0 : Math.Round((double)s.Subs / subCount, 8));
                }
                else
                {
                    // INACTIVE subsriber has no chance of winning
                    s.Chance = 0.0;
                }
            }
        }

        // Save subscriber data to drive
        private void WriteData(string filePath, BindingList<Subscriber> subs)
        {
            using (Stream stream = File.Open(filePath, FileMode.Truncate))
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                formatter.Serialize(stream, subs);
            }
        }

        // Load subscriber data from drive
        private BindingList<Subscriber> ReadData(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (stream.Length != 0)
                {
                    return new BindingList<Subscriber>((BindingList<Subscriber>)formatter.Deserialize(stream));
                }
                else
                {
                    return new BindingList<Subscriber>();
                }
            }
        }

        // DOC MISSING
        private void SubsTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (tableLoaded)
            {
                if (int.Parse(SubsTable.CurrentCell.Value.ToString()) <= 0)
                {
                    SubsTable.CurrentCell.Value = 1;
                }
 
                countSubs();
                updateChances();
            }
        }

        // This is executed when program is terminated
        // Writes all subscriber data to drive
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteData(path, subs);
        }

        // DOC MISSING
        private void SubsTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            SubsTable.CurrentCell.Value = 1;

            countSubs();
            updateChances();
        }

        // Change Enabled status of all subscribers based on argument e
        // For simpler implementation Enabled is a string with possible values { "Ano", "Ne" }
        private void ChangeEnabledAll(string e)
        {
            foreach (Subscriber s in subs)
            {
                s.Enabled = e;
            }
        }

        // Clicking on this button makes all subscribers ACTIVE
        // Active subscribers have a chance of winning based on the amount of subs given
        private void EnableAllButton_Click(object sender, EventArgs e)
        {
            // Confirmation dialog
            var confirmed = MessageBox.Show("Určitě chceš všechny subsribery aktivovat?", "Aktivovat všechny", MessageBoxButtons.YesNo);

            if (confirmed == DialogResult.Yes)
            {
                // Change active status of all subsribers to ACTIVE
                ChangeEnabledAll("Ano");

                // Set subCount to the amount of all subscribers and update chances
                countSubs();

                // Update winning chances
                // All winning chances should be appropriately set after the update statement is executed
                updateChances();

                // Update table
                subs = new BindingList<Subscriber>(subs);
                SubsTable.DataSource = subs;
            }
        }

        // Clicking on this button makes all subscribers INACTIVE
        // Inactive subscibers have zero winning chances
        private void DisableAllButton_Click(object sender, EventArgs e)
        {
            // Confirmation dialog
            var confirmed = MessageBox.Show("Určitě chceš všechny subsribery deaktivovat?", "Deaktivovat všechny", MessageBoxButtons.YesNo);
            
            if (confirmed == DialogResult.Yes)
            {
                // Change active status of all subsribers to INACTIVE
                ChangeEnabledAll("Ne");

                // Set subCount to 0 (making all inactive)
                subCount = 0;

                // Update winning chances
                // All winning chances should be zero after the update statement is executed
                updateChances();

                // Update table
                subs = new BindingList<Subscriber>(subs);
                SubsTable.DataSource = subs;
            }
        }
    }
}
