using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace DebtBook.ViewModels
{
   public class AddDebtorViewModel 
    {
        private DelegateCommand saveCommand;

        public DelegateCommand SaveCommand
        {
            get { return saveCommand ?? (saveCommand = new DelegateCommand(SaveCommandHandler)); }
        }

        void SaveCommandHandler()
        {
            MessageBox.Show("Herligt du trykkede gem");
           // MWVM = true;
        }


        private DelegateCommand cancelCommand;

        public DelegateCommand CancelCommand
        {
            get { return cancelCommand ?? (cancelCommand = new DelegateCommand(CancelCommandHandler)); }
        }

        void CancelCommandHandler()
        {
            MessageBox.Show("Her");
            App.Current.MainWindow.DialogResult = false;
        }
    }
}
