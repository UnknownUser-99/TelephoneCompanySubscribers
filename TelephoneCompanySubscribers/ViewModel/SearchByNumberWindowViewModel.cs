using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TelephoneCompanySubscribers.Model;

namespace TelephoneCompanySubscribers.ViewModel
{
    public class SearchByNumberWindowViewModel : ViewModelBase
    {
        public ICommand SearchByNumberCommand { get; private set; }

        public event Action<AbonentsTable> AbonentsTableUpdated;

        private AbonentsTable abonentsTable;

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public SearchByNumberWindowViewModel(AbonentsTable abonentsTable)
        {
            SearchByNumberCommand = new RelayCommand(SearchByNumber);

            this.abonentsTable = abonentsTable;
        }

        private void SearchByNumber()
        {
            bool result = abonentsTable.FindNumber(PhoneNumber);

            if (result == true)
            {
                UpdateTable();
            }
            else
            {
                MessageBox.Show($"Не удалось найти введённый номер {phoneNumber}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTable()
        {
            AbonentsTableUpdated(abonentsTable);
        }
    }
}
