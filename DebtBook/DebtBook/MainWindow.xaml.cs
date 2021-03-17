using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using DebtBook.Models;

namespace DebtBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MWVM = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MWVM;
        }

        private void addDebtorButton_Click_1(object sender, RoutedEventArgs e)
        {
            addDebtorWindow window = new addDebtorWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (window.ShowDialog() == true)//Åben tilføj debtor vindue
            {
                double amount;
                try
                {
                    amount = double.Parse(window.ValueBox.Text);
                }
                catch ( Exception FormatException)//Bruger har ikke indtastet initial amount
                {
                    amount = 0;
                }
               
                
                MWVM.debtors.Add(new Debtor(window.NameBox.Text,amount));
            }
        }

        private void SaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (var debtor in MWVM.debtors)
            {
                XmlSerializer x = new XmlSerializer(typeof(List<Debt>));
                TextWriter writer = new StreamWriter(@"DebtorSaveFile" + i+".xml");
                x.Serialize(writer, debtor.debts);
                i++;
            }
            MessageBox.Show("Data is saved in DebtorSaveFileN.xml");
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            MWVM.debtors.Clear();
            bool fileExists = File.Exists("DebtorSaveFile0.xml");
            int i = 0;
            while (fileExists)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Debt>));

                FileStream fs = new FileStream(@"DebtorSaveFile" + i + ".xml", FileMode.Open);
                MWVM.debtors.Add(new Debtor());
                MWVM.debtors[i].debts = (List<Debt>)serializer.Deserialize(fs);
                i++;
                fileExists = File.Exists("DebtorSaveFile"+i+".xml");
            }

            

        }

        
    }
}
