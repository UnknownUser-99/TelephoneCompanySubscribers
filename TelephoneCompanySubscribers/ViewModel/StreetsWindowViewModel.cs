using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TelephoneCompanySubscribers.Model;

namespace TelephoneCompanySubscribers.ViewModel
{
    public class StreetsWindowViewModel : ViewModelBase
    {
        private DataTable streetsTable;

        public DataTable StreetsTable
        {
            get { return streetsTable; }
            set
            {
                streetsTable = value;
                OnPropertyChanged(nameof(streetsTable));
            }
        }

        public StreetsWindowViewModel(StreetsTable streetsTable)
        {
            LoadData(streetsTable);
        }

        private void LoadData(StreetsTable streetsTable)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                StreetsTable = streetsTable.GetTable();
            });
        }
    }
}
