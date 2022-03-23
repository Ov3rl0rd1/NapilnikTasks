using Devart.Data.SQLite;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace SQLTask
{
    class SQLDB
    {
        public static bool IsConnected => _isConnected;

        private static bool _isConnected;
        private static SQLiteConnection _connection;

        public static void Connect()
        {
            if (_isConnected)
                return;

            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");
            try
            {
                _connection = new SQLiteConnection(connectionString);
                _connection.Open();
                _isConnected = true;
            }
            catch (SQLiteException ex)
            {
                if ((int)ex.ErrorCode != 1)
                    return;
                throw new Exception("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
            }
        }

        public static DataTable GetDataTable(string command)
        {
            if (_isConnected == false)
                throw new InvalidOperationException();

            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(command, _connection));
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = dataTable1;
            sqLiteDataAdapter.Fill(dataTable2);
            return dataTable1;
        }

        public static void Disconnect()
        {
            _isConnected = false;
            _connection.Close();
            _connection = null;
        }
    }
}
