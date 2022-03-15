using System;
using System.Data;
using System.Windows.Forms;
using Devart.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace SQLTask
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            if (passportTextbox.Text.Trim() == "")
            {
                MessageBox.Show("Введите серию и номер паспорта");
            }
            else
            {
                string rawData = passportTextbox.Text.Trim().Replace(" ", string.Empty);
                if (rawData.Length < 10)
                {
                    textResult.Text = "Неверный формат серии или номера паспорта";
                }
                else
                {
                    string commandText = string.Format("select * from passports where num='{0}' limit 1;", ComputeSha256Hash(rawData));
                    string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
                    try
                    {
                        SQLiteConnection connection = new SQLiteConnection(connectionString);
                        connection.Open();
                        SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
                        DataTable dataTable1 = new DataTable();
                        DataTable dataTable2 = dataTable1;
                        sqLiteDataAdapter.Fill(dataTable2);
                        if (dataTable1.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
                                textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                            else
                                textResult.Text = "По паспорту «" + passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
                        }
                        else
                            textResult.Text = "Паспорт «" + passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
                        connection.Close();
                    }
                    catch (SQLiteException ex)
                    {
                        if ((int)ex.ErrorCode != 1)
                            return;
                        MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
                    }
                }
            }
        }

        public static string ComputeSha256Hash(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
