using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using DebtBook.Models;
using Prism.Commands;
using Prism.Mvvm;
//Inspireret af AgentAssignment5 udleveret af Poul


namespace DebtBook.ViewModels
{
    public class DebtorWindowViewModel : BindableBase
    {
        #region Properties

        public Debtor currentDebtor;

        public Debtor CurrentDebtor
        {
            get => currentDebtor;
            set => SetProperty(ref currentDebtor, value);
        }

        #endregion

        public DebtorWindowViewModel(Debtor debtor)
        {
            CurrentDebtor = debtor;
        }


        #region Commands

        


        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


        #endregion
    }
}
