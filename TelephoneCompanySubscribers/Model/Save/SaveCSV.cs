using System;
using System.Collections.Generic;
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
            MessageBox.Show($"Реализована стратегия CSV", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
