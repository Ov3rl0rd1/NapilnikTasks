using System;
using System.Windows.Forms;

namespace SQLTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            FindPassport(passportTextbox.Text);
        }

        private void FindPassport(string rawData)
        {
            try
            {
                SQLDB.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            string result = PassportFinder.Find(rawData);

            textResult.Text = result;
        }
    }
}
