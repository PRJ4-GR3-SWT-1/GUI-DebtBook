using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebtBook.Models;

namespace DebtBook
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Debtor> debtors;

        public MainWindowViewModel()
        {
            debtors = new ObservableCollection<Debtor>();
            debtors.Add(new Debtor("Søren"));
            debtors.Add(new Debtor("Peter"));
            debtors.Add(new Debtor("Caroline"));
            debtors.Add(new Debtor("August"));
            debtors.Add(new Debtor("Marie"));

            // Søren
            debtors[0].AddDebt(new Debt("10/5/2020",55));
            debtors[0].AddDebt(new Debt("12/5/2020", 255));
            debtors[0].AddDebt(new Debt("15/5/2020", 420));

            // Peter
            debtors[1].AddDebt(new Debt("20/7/2020", 40));
            debtors[1].AddDebt(new Debt("22/7/2020", 510));
            debtors[1].AddDebt(new Debt("25/7/2020", 999));

        }

    }
}
