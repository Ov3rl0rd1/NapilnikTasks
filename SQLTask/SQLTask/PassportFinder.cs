using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace SQLTask
{
    static class PassportFinder
    {
        public static string Find(string rawData)
        {
            string passport = GetPassportSeries(rawData);
            if (passport.Length < 10 || passport.Length == 0)
                return "Неверный формат серии или номера паспорта";

            DataTable dataTable = GetDataTable(passport);

            if (dataTable.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]))
                    return "По паспорту «" + passport + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                else
                    return "По паспорту «" + passport + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
            }
            else
                return "Паспорт «" + rawData + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
        }

        private static DataTable GetDataTable(string passport)
        {
            if (SQLDB.IsConnected == false)
                throw new InvalidOperationException();

            string commandText = $"select * from passports where num='{ComputeSha256Hash(passport)}' limit 1;";
            DataTable dataTable = SQLDB.GetDataTable(commandText);

            SQLDB.Disconnect();

            return dataTable;
        }

        private static string GetPassportSeries(string rawData)
        {
            return rawData.Trim().Replace(" ", string.Empty);
        }

        private static string ComputeSha256Hash(string value)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
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
