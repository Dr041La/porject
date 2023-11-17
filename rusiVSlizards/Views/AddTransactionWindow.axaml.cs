using Avalonia.Controls;
using Avalonia.Interactivity;
using HarfBuzzSharp;
using rusiVSlizards.Models;
using rusiVSlizards.ViewModels;
using System;

namespace rusiVSlizards.Views
{
    public partial class AddTransactionWindow : Window
    {
        public AddTransactionWindow()
        {
            InitializeComponent();

            transtypebox.ItemsSource = Enum.GetValues(typeof(UserRole));

            addButton.Click += delegate { AddTr(); };
            cancelButton.Click += delegate { Close(null); };
        }


        private void AddTr()
        {
            Transaction tr = GetData();

            if (tr == null)
            {
                return;
            }

            Db.AddTransaction(tr);
            Close(null);
        }

        private Transaction GetData()
        {
            Transaction tr = new Transaction();

            int amount;

            if (userbox.SelectedIndex == -1 || transtypebox.SelectedIndex == -1 || !Int32.TryParse(amountText.Text, out amount))
            {
                return null;
            }

            tr.user = userbox.SelectedItem as Client;
            tr.amount = amount;
            tr.date = DateTime.Now;
            tr.decs = descText.Text == string.Empty ? " " : descText.Text;
            tr.transactionType = (TransactionType)transtypebox.SelectedItem;
            return tr;
        }
    }
}
