using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TelephoneCompanySubscribers.Model.Save;

namespace TelephoneCompanySubscribers.Model
{
    public class SaveCSV : ISaveFile
    {
        public void SaveFile(AbonentsTable abonentsTable)
        {
            string fileName = $"report_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.csv";

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "CSV файл (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                WriteFile(filePath, abonentsTable);
            }
        }

        private void WriteFile(string filePath, AbonentsTable abonentsTable)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                DataTable table = abonentsTable.GetTable();

                foreach (DataColumn column in table.Columns)
                {
                    writer.Write(column.ColumnName);
                    writer.Write(",");
                }

                writer.WriteLine();

                foreach (DataRow row in table.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        writer.Write(item);
                        writer.Write(",");
                    }

                    writer.WriteLine();
                }
            }
        }
    }
}
