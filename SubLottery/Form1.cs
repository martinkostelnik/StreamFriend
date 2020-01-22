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
        // List with subsriber data
        private static List<Subscriber> subs = new List<Subscriber>();

        // Helper List to bind Data to Table
        private static BindingList<Subscriber> subsBL = new BindingList<Subscriber>();

        // Total subs given
        private int subCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Read data from file into List
            subs = ReadData("./subs.bin");

            countSubs();

            // Create BindingList based on the Data List
            subsBL = new BindingList<Subscriber>(subs);

            // Set up the DataGridView (the table)
            SubsTable.DataSource = subsBL;

            SubsTable.RowHeadersVisible = false;
            SubsTable.Columns["Chance"].Visible = false;
            SubsTable.Columns["Name"].HeaderText = "Jméno";
            SubsTable.Columns["Subs"].HeaderText = "Počet subů";
        }

        private void SubCountText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                insertSubscriber();
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            insertSubscriber();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            if (subs.Count == 0)
            {
                return;
            }

            WinnerLabel.Visible = true;
            WinnerLabel.Text = "3";

            timer1.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int timeLeft = int.Parse(WinnerLabel.Text);
            timeLeft -= 1;

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
                subs.Add(new Subscriber(name, value, subCount == 0 ? 1.0d : Math.Round((double)value / subCount, 4)));
            }
            else // Sub already in a table - increment subcount and update chances
            {
                if (value == 0)
                {
                    InsertButton.Text = "ERROR: Sub count must be greater than 1 when adding subs!";
                    return;
                }

                foreach (Subscriber s in subs)
                {
                    if (s.Name == name)
                    {
                        if (s.Subs + value <= 0 || subCount + value <= 0)
                        {
                            InsertButton.Text = "ERROR: Subscriber count would go below or to 0!";
                            return;
                        }

                        s.Subs += value;

                        subCount += value;
                    }
                }
            }

            InsertButton.Text = "Přidat";

            nameText.Text = "";
            subCountText.Text = "";

            // Update winning chances of every subscriber based on the increment of subs
            updateChances();

            // Update subscriber table
            subsBL = new BindingList<Subscriber>(subs);
            SubsTable.DataSource = subsBL;

            // Write data to file
            WriteData("./subs.bin", subs);
        }

        private string getWinnerName()
        {
            Random rand = new Random();
            double value = rand.NextDouble();
            Console.WriteLine(value.ToString());
            double cumulative = 0.0;
      
            List<Subscriber> sortedByChance = subs.OrderBy(o => o.Chance).ToList();

            for (int i = 0; i < sortedByChance.Count - 1; i++)
            {
                if (cumulative <= value && value < (cumulative += sortedByChance[i].Chance))
                {
                    return sortedByChance[i].Name;
                }
            }

            return sortedByChance.Last<Subscriber>().Name;
        }

        // TRUE if name is not among the subscribers, FALSE if name found
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

        // Counts subs given by subscribers (in total)
        private void countSubs()
        {
            foreach (Subscriber s in subs)
            {
                subCount += s.Subs;
            }
        }
        
        // Updates winning chances of all subscribers
        private void updateChances()
        {
            foreach (Subscriber s in subs)
            {
                s.Chance = Math.Round((double)s.Subs / subCount, 4);
            }
        }

        // Saves subscriber data to drive
        private void WriteData(string filePath, List<Subscriber> subs)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                formatter.Serialize(stream, subs);
            }
        }

        // Loads subscriber data from drive
        private List<Subscriber> ReadData(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                if (stream.Length != 0)
                {
                    return (List<Subscriber>)formatter.Deserialize(stream);
                }
                else
                {
                    return new List<Subscriber>();
                }
            }
        }
    }
}
