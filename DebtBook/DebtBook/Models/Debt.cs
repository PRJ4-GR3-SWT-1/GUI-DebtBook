using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace DebtBook.Models
{
   public class Debt : BindableBase
    {
        public string Date { get; set; }
        public double Amount { get; set; }
    }
}
