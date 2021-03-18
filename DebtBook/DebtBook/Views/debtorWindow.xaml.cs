using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DebtBook.Models;

namespace DebtBook.Views
{
    /// <summary>
    /// Interaction logic for debtorWindow.xaml
    /// </summary>
    public partial class debtorWindow : Window
    {
        public debtorWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindowViewModel context =
                    System.Windows.Application.Current.MainWindow.DataContext as MainWindowViewModel;
                //List<Debt> data = DataGridWithDebts.ItemsSource as List<Debt>;
                DateTime now = DateTime.Today;
                //data.Add(new Debt(now.ToString(),int.Parse(valueBox.Text)));
                context.CurrentDebtor.AddDebt(new Debt(now.ToString(), int.Parse(valueBox.Text)));
                DataGridWithDebts.Items.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Invalid value. Use numbers");
            }
        }
    }
}
