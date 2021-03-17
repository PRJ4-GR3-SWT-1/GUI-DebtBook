﻿using System;
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
using DebtBook.Views;

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

        private void debtorWindow_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            debtorWindow window = new debtorWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.DataContext = MWVM;
            //MessageBox.Show(MWVM.CurrentDebtor.Name);
            //window.DataGridWithDebts.ItemsSource = MWVM.CurrentDebtor.debts;

            if (window.ShowDialog() == true)
            {
                //DebtorsLstBx.UpdateLayout();
                /*  List<Debt> windowData = window.DataGridWithDebts.ItemsSource as List<Debt>;
                  if (MWVM.CurrentDebtor.debts.Count != windowData.Count)
                  {
                      for (int i = MWVM.CurrentDebtor.debts.Count; i < windowData.Count; i++)
                      {
                          MWVM.CurrentDebtor.debts.Add(windowData[i-1]);
                      }
                  }*/
            }
        }


        
    }
}
