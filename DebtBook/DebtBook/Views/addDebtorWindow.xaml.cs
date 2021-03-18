using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DebtBook.ViewModels;

namespace DebtBook
{
    /// <summary>
    /// Interaction logic for addDebtorWindow.xaml
    /// </summary>
    public partial class addDebtorWindow : Window
    {
       //private AddDebtorViewModel AddVM;
        public addDebtorWindow()
        {
            InitializeComponent();
            //AddVM = new AddDebtorViewModel();
            //var datacontext = App.Current.MainWindow.DataContext;
        }

        //private void saveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DialogResult = true;
        //}

        //private void cancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DialogResult = false;
        //}
    }
}
