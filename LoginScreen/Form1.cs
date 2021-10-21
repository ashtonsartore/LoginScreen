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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        string[] index;
       public static String[] username;
       public static String[] password;

        public static DataTable dt = new DataTable();

        private void Form1_Load(object sender, EventArgs e)
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


                username = new string[dt.Rows.Count];
                password = new string[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    username[i] = dt.Rows[i].Field<string>("username");
                    password[i] = dt.Rows[i].Field<string>("password");
                }


            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        string u, p;
        int ui;

        private void label3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog(); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            u = txtUsername.Text;
            p = txtPassword.Text;

            for (int i = 0; 1 < dt.Rows.Count - 1; i++)
            {
                try
                {
                    if (!String.IsNullOrEmpty(username[i]))
                    {
                        if (username[i].Equals(u))
                        {
                            ui = i;
                        }
                    }

                }
                catch
                {

                }

                if (password[ui].Equals(p))
                {
                    MessageBox.Show("LogIn Sucessful");
                }

                else
                {
                    lblIncorrect.Visible = true;
                }

                break;

            }



        }
    }
}
