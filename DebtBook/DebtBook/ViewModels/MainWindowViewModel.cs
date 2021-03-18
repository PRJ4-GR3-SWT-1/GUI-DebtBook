using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using DebtBook.Models;
using Prism.Commands;

namespace DebtBook
{
    public class MainWindowViewModel
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
            debtors[0].AddDebt(new Debt("10/5/2020", 55));
            debtors[0].AddDebt(new Debt("12/5/2020", 255));
            debtors[0].AddDebt(new Debt("15/5/2020", 420));

            // Peter
            debtors[1].AddDebt(new Debt("20/7/2020", 40));
            debtors[1].AddDebt(new Debt("25/7/2020", 999));

            // Caroline
            debtors[2].AddDebt(new Debt("1/9/2020", 40));
            debtors[2].AddDebt(new Debt("2/9/2020", 10));
            debtors[2].AddDebt(new Debt("5/9/2020", 9));
        }

        #endregion

        /// COMMANDS:
        ///
        ///
        /// 


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
                listOfNames[i] = debtor.name;
                i++;
            }

            //Save names of persons:
            XmlSerializer nX = new XmlSerializer(typeof(string[]));
            TextWriter NameWriter = new StreamWriter(@"DebtorNames.xml");
            nX.Serialize(NameWriter, listOfNames);
            NameWriter.Close();
            NameWriter.Flush();
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

            //Pull out names:
            XmlSerializer nameSerializer = new XmlSerializer(typeof(string[]));

            FileStream nfs = new FileStream(@"DebtorNames.xml", FileMode.Open);
            string[] listOfNames = (string[]) nameSerializer.Deserialize(nfs);
            //Read debt:
            debtors.Clear();
            bool fileExists = File.Exists("DebtorSaveFile0.xml");
            int i = 0;
            while (fileExists)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Debt>));
                FileStream fs = new FileStream(@"DebtorSaveFile" + i + ".xml", FileMode.Open);
                debtors.Add(new Debtor(listOfNames[i]));
                debtors[i].debts = (List<Debt>) serializer.Deserialize(fs);
                fs.Dispose();
                i++;
                fileExists = File.Exists("DebtorSaveFile" + i + ".xml");
            }
            nfs.Dispose();
        }
    }


}
