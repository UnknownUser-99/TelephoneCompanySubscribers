using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TelephoneCompanySubscribers.View;
using TelephoneCompanySubscribers.Model;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.ComponentModel;
using TelephoneCompanySubscribers.Model.Save;

namespace TelephoneCompanySubscribers.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand LoadDataCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand SortingCommand { get; private set; }
        public ICommand SearchByNumberCommand { get; private set; }
        public ICommand ShowStreetsCommand { get; private set; }
        public ICommand ExportCSVCommand { get; private set; }

        private SearchByNumberWindow searchByNumberWindow;
        private StreetsWindow streetsWindow;
        private ErrorWindow errorWindow;

        private AbonentsTable aTable;
        private StreetsTable sTable;

        private DataTable abonentsTable;

        public DataTable AbonentsTable
        {
            get { return abonentsTable; }
            set
            {
                abonentsTable = value;
                OnPropertyChanged(nameof(AbonentsTable));
            }
        }

        private string sortColumn;

        public string SortColumn
        {
            get { return sortColumn; }
            set
            {
                
                sortColumn = value;
                OnPropertyChanged(nameof(SortColumn));
                
            }
        }

        private string sortDirection;

        public string SortDirection
        {
            get { return sortDirection; }
            set
            {
                sortDirection = value;
                OnPropertyChanged(nameof(SortDirection));
            }
        }

        public MainWindowViewModel()
        {
            LoadDataCommand = new RelayCommand(LoadData);
            CancelCommand = new RelayCommand(Cancel);
            SortingCommand = new RelayCommand(Sorting);
            SearchByNumberCommand = new RelayCommand(OpenSearchByNumberWindow);
            ShowStreetsCommand = new RelayCommand(OpenStreetsWindow);
            ExportCSVCommand = new RelayCommand(ExportCSV);

            LoadData();

            SortColumn = "FullName";
            SortDirection = "ASC";
        }

        private async void LoadData()
        {
            try
            {
                Database db = new Database();

                (List<Abonent> abonents, Dictionary<int, List<Phone>> phones, Dictionary<int, Street> streets, Dictionary<int, Address> addresses) = await db.GetAllData();

                aTable = new AbonentsTable(abonents, phones, streets, addresses);
                sTable = new StreetsTable(streets, addresses);

                AbonentsTable = aTable.GetTable();
            }
            catch(Exception ex)
            {
                string errorMessage = $"Не удалось загрузить данные: {ex.Message}";

                OpenErrorWindow(errorMessage);
            }
        }

        private void OpenSearchByNumberWindow()
        {
            if (searchByNumberWindow == null)
            {
                searchByNumberWindow = new SearchByNumberWindow(new SearchByNumberWindowViewModel(aTable));
                (searchByNumberWindow.DataContext as SearchByNumberWindowViewModel).AbonentsTableUpdated += HandleAbonentsTableUpdated;
                searchByNumberWindow.Closed += (sender, args) => searchByNumberWindow = null;
                searchByNumberWindow.Show();
            }
            else
            {
                searchByNumberWindow.Activate();
            }
        }

        private void HandleAbonentsTableUpdated()
        {
            AbonentsTable = aTable.GetTable();
        }

        private void OpenStreetsWindow()
        {
            if (streetsWindow == null)
            {
                streetsWindow = new StreetsWindow(new StreetsWindowViewModel(sTable));
                streetsWindow.Closed += (sender, args) => streetsWindow = null;
                streetsWindow.Show();
            }
            else
            {
                streetsWindow.Activate();
            }
        }

        private void ExportCSV()
        {
            try
            {
                ISaveFile strategy = new SaveCSV();
                SaveFile saveFile = new SaveFile(strategy);
                saveFile.Save(aTable);
            }
            catch(Exception ex)
            {
                string errorMessage = $"Не удалось сохранить данные: {ex.Message}";

                OpenErrorWindow(errorMessage);
            }
        }

        private void Sorting()
        {
            aTable.SortTable(SortColumn, SortDirection);

            AbonentsTable = aTable.GetTable();
        }

        private void Cancel()
        {
            aTable.CancelFilter();
            aTable.SortTable(SortColumn, SortDirection);

            AbonentsTable = aTable.GetTable();
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
