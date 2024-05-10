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
using TelephoneCompanySubscribers.View;

namespace TelephoneCompanySubscribers.ViewModel
{
    public class SearchByNumberWindowViewModel : ViewModelBase
    {
        public ICommand SearchByNumberCommand { get; private set; }

        private ErrorWindow errorWindow;

        public event Action AbonentsTableUpdated;

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
                string errorMessage = $"Не удалось найти введённый номер {phoneNumber}";

                OpenErrorWindow(errorMessage);
            }
        }

        private void UpdateTable()
        {
            AbonentsTableUpdated();
        }

        private void OpenErrorWindow(string errorMessage)
        {
            if (errorWindow == null)
            {
                errorWindow = new ErrorWindow(new ErrorWindowViewModel(errorMessage));
                errorWindow.Closed += (sender, args) => errorWindow = null;
                errorWindow.Show();
            }
            else
            {
                errorWindow.Activate();
            }
        }
    }
}
