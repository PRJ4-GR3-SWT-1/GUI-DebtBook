using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using DebtBook.Models;
using DebtBook.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace DebtBook
{
    public class MainWindowViewModel : DependencyObject
    {
        public ObservableCollection<Debtor> debtors { get; set; }
        public Debtor CurrentDebtor { get; set; }

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>();
            debtors.Add(new Debtor("Søren"));
            debtors.Add(new Debtor("Peter"));
            debtors.Add(new Debtor("Caroline"));
            CurrentDebtor = debtors[0];

            addDummyData();
        }

        #region DummyData

        void addDummyData()
        {
            // Søren
            debtors[0].AddDebt(new Debt("10-5-2020", 55));
            debtors[0].AddDebt(new Debt("12-5-2020", 255));
            debtors[0].AddDebt(new Debt("15-5-2020", 420));

            // Peter
            debtors[1].AddDebt(new Debt("20-7-2020", 40));
            debtors[1].AddDebt(new Debt("25-7-2020", 999));

            // Caroline
            debtors[2].AddDebt(new Debt("1-9-2020", 40));
            debtors[2].AddDebt(new Debt("2-9-2020", 10));
            debtors[2].AddDebt(new Debt("5-9-2020", 9));
        }

        #endregion

        #region Commands

        /// 🔽🔽🔽COMMANDS🔽🔽🔽:

        private DelegateCommand saveCommand;

        public DelegateCommand SaveCommand
        {
            get { return saveCommand ?? (saveCommand = new DelegateCommand(SaveCommandHandler)); }
        }

        void SaveCommandHandler()
        {
            string[] listOfNames = new string[debtors.Count];
            //Save all the debt:
            int i = 0;
            foreach (var debtor in debtors)
            {
                XmlSerializer x = new XmlSerializer(typeof(List<Debt>));
                TextWriter writer = new StreamWriter(@"DebtorSaveFile" + i + ".xml");
                x.Serialize(writer, debtor.debts);
                writer.Dispose();
                listOfNames[i] = debtor.name;
                i++;
            }

            //Save names of persons:
            XmlSerializer nX = new XmlSerializer(typeof(string[]));
            TextWriter NameWriter = new StreamWriter(@"DebtorNames.xml");
            nX.Serialize(NameWriter, listOfNames);
            NameWriter.Dispose();

            MessageBox.Show("Data is saved in DebtorSaveFileN.xml");
        }

        private DelegateCommand loadCommand;

        public DelegateCommand LoadCommand
        {
            get { return loadCommand ?? (loadCommand = new DelegateCommand(LoadCommandHandler)); }
        }

        void LoadCommandHandler()
        {
            bool fileExists = File.Exists("DebtorSaveFile0.xml");
            if (!fileExists)
            {
                MessageBox.Show("Could not find save-file","Load error");
                return;
            }
            //Pull out names:
            XmlSerializer nameSerializer = new XmlSerializer(typeof(string[]));

            FileStream nfs = new FileStream(@"DebtorNames.xml", FileMode.Open);
            string[] listOfNames = (string[]) nameSerializer.Deserialize(nfs);
            //Read debt:
            debtors.Clear();
            
            int i = 0;
            while (fileExists)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Debt>));
                FileStream fs = new FileStream(@"DebtorSaveFile" + i + ".xml", FileMode.Open);
                if (i >= listOfNames.Length) break;
                debtors.Add(new Debtor(listOfNames[i]));
                debtors[i].debts = (List<Debt>) serializer.Deserialize(fs);
                fs.Dispose();
                i++;
                fileExists = File.Exists("DebtorSaveFile" + i + ".xml");
            }
            nfs.Dispose();
        }

        private DelegateCommand addDebtorCommand;
        public static readonly DependencyProperty DebtorWindowViewModelProperty = DependencyProperty.Register("DebtorWindowViewModel", typeof(object), typeof(MainWindowViewModel), new PropertyMetadata(default(object)));

        public DelegateCommand AddDebtorCommand
        {
            get { return addDebtorCommand ?? (addDebtorCommand = new DelegateCommand(AddDebtorCommandHandler)); }
        }

        public addDebtorWindow window;
        void AddDebtorCommandHandler()
        {
            window = new addDebtorWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.DataContext = App.Current.MainWindow.DataContext;
            
            

            if (window.ShowDialog() == true)//Åben tilføj debtor vindue
            {
                double amount;
                try
                {
                    amount = double.Parse(window.ValueBox.Text);
                }
                catch (Exception FormatException)//Bruger har ikke indtastet initial amount
                {
                    amount = 0;
                }


                debtors.Add(new Debtor(window.NameBox.Text, amount));
            }
        }

        private DelegateCommand saveNewAgentCommand;

        public DelegateCommand SaveNewAgentCommand
        {
            get { return saveNewAgentCommand ?? (saveNewAgentCommand = new DelegateCommand(SaveNewAgentCommandHandler)); }
        }

        void SaveNewAgentCommandHandler()//Selvom knappen er sat til IsDefault=true - lukker vinduet ikke. Derfor kommer denne kode:
        {
            window.DialogResult = true;
        }


        private DelegateCommand cancelNewAgentCommand;

        public DelegateCommand CancelNewAgentCommand
        {
            get { return cancelNewAgentCommand ?? (cancelNewAgentCommand = new DelegateCommand(CancelNewAgentCommandHandler)); }
        }

        void CancelNewAgentCommandHandler()
        {
            window.DialogResult = false;
        }
    
       

        private DelegateCommand _openDebtorWindowCommand;

        public DelegateCommand OpenDebtorWindowCommand
        {
            get
            {
                return _openDebtorWindowCommand ??
                       (_openDebtorWindowCommand = new DelegateCommand(OpenDebtorWindowCommandHandler));
            }
        }

        private debtorWindow debtorWindow;
        void OpenDebtorWindowCommandHandler()
        {
            debtorWindow = new debtorWindow();
            debtorWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            debtorWindow.DataContext = this;

            if (debtorWindow.ShowDialog() == true)
            {
                // All data is added instantly, Therefore there are no OK button. 
                // The code herein will never be run.
            }
            CurrentDebtor.Totaldebt = 0;
        }

        private DelegateCommand _addDebtCommand;

        public DelegateCommand AddDebtCommand
        {
            get
            {
                return _addDebtCommand ??
                       (_addDebtCommand = new DelegateCommand(AddDebtCommandHandler));
            }
        }

        void AddDebtCommandHandler()
        {
            try
            {
                DateTime now = DateTime.Today;
                string[] date = now.ToString().Split(' ');
                CurrentDebtor.AddDebt(new Debt(date[0], int.Parse(debtorWindow.valueBox.Text)));
                debtorWindow.DataGridWithDebts.Items.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Invalid value. Use numbers");
            }
        }




        #endregion
    }


}
