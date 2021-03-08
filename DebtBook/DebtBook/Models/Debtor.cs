using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DebtBook.Models
{
    public class Debtor: BindableBase
    {
        public string name;
        public ICollection<Debt> debts;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }


        public ICollection<Debt> Debts
        {
            get { return debts; }
        }

        public void AddDebt(Debt newdebt)
        {
            if(newdebt!=null)
                Debts.Add(newdebt);
        }

        public double CalculateAllDebt()
        {
            double result=0;
            foreach (var debt in Debts)
            {
                result += debt.GetAmount(); 
            }
        }
    }
}
