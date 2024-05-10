using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using TelephoneCompanySubscribers.View;

namespace TelephoneCompanySubscribers.ViewModel
{
    public class ErrorWindowViewModel : ViewModelBase
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {

                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

            }
        }

        public ErrorWindowViewModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
