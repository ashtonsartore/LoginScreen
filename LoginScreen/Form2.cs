using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace LoginScreen
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        int records = 0;
        List<string> Username = new List<string>();
        List<string> Password = new List<string>();

        public static DataTable dt = new DataTable();


        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        string fileName = @"I:/users.csv";

        private void SaveNewUserToDatabase()
        {
            File.Delete(fileName);


            StringBuilder sb = new StringBuilder();


            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));


            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }


            File.WriteAllText(fileName, sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {


            // add to data table
            dt.Rows.Add(txtUsername.Text, txtPassword.Text);
            records++;
            Username.Add(dt.Rows[records].Field<string>("username"));
            Password.Add(dt.Rows[records].Field<string>("username"));
            SaveNewUserToDatabase();

            MessageBox.Show("Account Created");
            Close();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            string RunningPath = AppDomain.CurrentDomain.BaseDirectory;

            // read each line into a string array
            string[] lines = System.IO.File.ReadAllLines(@"I:/users.csv");


            // validate that data is present
            if (lines.Length > 0)
            {
                // the first line is considered header titles
                string firstLine = lines[0];
                string[] headerLabels = firstLine.Split(',');

                // build the columns of the dt
                foreach (string headerWord in headerLabels)
                {
                    // add the column
                    dt.Columns.Add(new DataColumn(headerWord));
                }


                // add data to each column
                for (int i = 1; i < lines.Length; i++)
                {
                    // add each word in each line to string array
                    string[] dataWords = lines[i].Split(',');


                    // create the row to put those word into
                    DataRow dr = dt.NewRow();


                    // add the data one column at a time
                    int columnIndex = 0;
                    foreach (string headerWord in headerLabels)
                    {
                        dr[headerWord] = dataWords[columnIndex++];
                    }


                    // write to the datatable
                    dt.Rows.Add(dr);
                }


                }
            }
        }
    }



