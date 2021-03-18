using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DebtBook.Models
{
    public class Debtor: BindableBase
    {
        public string name;
        public ICollection<Debt> debts;
        public double totaldebt;

        public double Totaldebt
        {
            get
            {
                return CalculateAllDebt();
            }
            set
            {
                SetProperty(ref totaldebt,CalculateAllDebt());
            }
        }

        public Debtor()
        {
            Name = "Ukendt";
            debts = new List<Debt>();
        }
        public Debtor(string NameOnDebtor, double InitialValue=0)
        {
            Name = NameOnDebtor ?? "Ukendt";
            debts = new List<Debt>();
            if (InitialValue != 0)
            {
                DateTime now = DateTime.Today;
                string[] date = now.ToString().Split(' ');
                debts.Add(new Debt(date[0], InitialValue));
            }
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        public ICollection<Debt> Debts
        {
            get => debts;
            set => SetProperty(ref debts, value);
        }

        public void AddDebt(Debt newDebt)
        {
            if(newDebt!=null)
                Debts.Add(newDebt);
        }

        public Debtor Clone()
        {
            return this.MemberwiseClone() as Debtor;
        }


        public double CalculateAllDebt()
        {
            return Debts.Sum(debt => debt.Amount);
        }
    }
}
