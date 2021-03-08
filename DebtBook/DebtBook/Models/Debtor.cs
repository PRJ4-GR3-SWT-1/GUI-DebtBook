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
        public string printData;

        public Debtor(string NameOnDebtor)
        {
            Name = NameOnDebtor;
            debts = new List<Debt>();
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        public ICollection<Debt> Debts
        {
            get { return debts; }
        }

        public string PrintData
        {
            get
            {
                return Name +" "+ CalculateAllDebt() + " kr.";
            }
            set { }
        }

        public void AddDebt(Debt newDebt)
        {
            if(newDebt!=null)
                Debts.Add(newDebt);
        }

        public double CalculateAllDebt()
        {
            return Debts.Sum(debt => debt.Amount);
        }
    }
}
