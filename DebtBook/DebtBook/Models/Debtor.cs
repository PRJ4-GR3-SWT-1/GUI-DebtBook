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
        public string printData;
        public double totaldebt;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string total = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(total));
        }

        public double Totaldebt
        {
            get
            {
                return CalculateAllDebt();
            }
            set
            {
                if (this.totaldebt == CalculateAllDebt())
                {
                    return;}

                this.totaldebt = CalculateAllDebt();
                Notify();

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
                debts.Add(new Debt(now.ToString(), InitialValue));
            }
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
