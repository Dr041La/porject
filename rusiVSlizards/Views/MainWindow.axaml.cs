using System;
using System.Diagnostics;
using Avalonia.Collections;
using Avalonia.Controls;
using MsBox.Avalonia;
using rusiVSlizards.Models;
using rusiVSlizards.ViewModels;


namespace rusiVSlizards.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        SetClientsGrid();
        SetAccountsGrid();
        SetTransactionGrid();
    }
    
    #region Clients

    private Client selectedClient;

    public void SetClientsGrid()
    {
        addClientButton.Click += delegate { ShowAddClientWindow(); };
        redactClientButton.Click += delegate { ShowRedactClientWindow(); };
        deleteClientButton.Click += delegate { DeleteClient(); };
        clearClientsFilterButton.Click += delegate { clientFilterText.Clear(); };

        clientsDataGrid.SelectionChanged += ClientsDataGrid_OnSelectionChanged;
        clientsDataGrid.AutoGeneratingColumn += SetClientsGridCollumnName;

        MainWindowViewModel.RefreshClients();

        clientFilterText.TextChanged += delegate { OnClientFilterChanged(); };

        MainWindowViewModel.ClientsView = new DataGridCollectionView(MainWindowViewModel.Clients);
        MainWindowViewModel.ClientsView.Filter = ClientsFilter;
        MainWindowViewModel.ClientsView.Refresh();
    }

    public void ShowAddClientWindow()
    {
        AddClientWindow adw = new AddClientWindow();
        adw.Closed += delegate { RefreshClient(); };
        adw.ShowDialog(this);
    }

    public void ShowRedactClientWindow()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            AddClientWindow adw = new AddClientWindow(selectedClient);
            Console.WriteLine("ID = " + selectedClient.id);
            adw.Closed += delegate { RefreshClient(); };
            adw.ShowDialog(this);
        }
    }

    public void RefreshClient()
    {
        MainWindowViewModel.RefreshClients();
    }

  
    public async void DeleteClient()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteClient(selectedClient);
                RefreshClient();
            }
            //Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnClientFilterChanged()
    {
        MainWindowViewModel.ClientsView.Refresh();
    }

    public void SetClientsGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "name":
                e.Column.Header = "Имя";
                break;
            case "password":
                e.Column.Header = "Пароль";
                break;
            case "userRole":
                e.Column.Header = "Роль";
                break;
        }
    }

    public bool ClientsFilter(object o)
    {
        if (clientFilterText.Text != null && clientFilterText.Text != string.Empty)
        {           
            Client client = (Client)o;
            if (client.name.Contains(clientFilterText.Text) || client.password.Contains(clientFilterText.Text) || client.userRole.ToString().Contains(clientFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }          
        }
        return true;
    }

    private void ClientsDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedClient = e.AddedItems[0] as Client;
        }
    }

    #endregion

    #region Accounts

    private Account selectedAccount;

    public void SetAccountsGrid()
    {
        clearAccFilterButton.Click += delegate { accFilterText.Clear(); };

        accDataGrid.SelectionChanged += AccDataGrid_OnSelectionChanged;
        accDataGrid.AutoGeneratingColumn += SetAccGridCollumnName;

        MainWindowViewModel.RefreshAccounts();

        accFilterText.TextChanged += delegate { OnAccFilterChanged(); };

        MainWindowViewModel.AccountsView = new DataGridCollectionView(MainWindowViewModel.Accounts);
        MainWindowViewModel.AccountsView.Filter = AccFilter;
        MainWindowViewModel.AccountsView.Refresh();
    }


    public void RefreshAcc()
    {
        MainWindowViewModel.RefreshAccounts();
    }


    private void OnAccFilterChanged()
    {
        MainWindowViewModel.AccountsView.Refresh();
    }

    public void SetAccGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "User":
                e.Column.Header = "Пользователь";
                break;
            case "balance":
                e.Column.Header = "Баланс";
                break;
        }
    }

    public bool AccFilter(object o)
    {
        if (accFilterText.Text != null && accFilterText.Text != string.Empty)
        {
            Account acc = (Account)o;
            if (acc.User.Contains(accFilterText.Text) || acc.balance.ToString().Contains(accFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void AccDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedAccount = e.AddedItems[0] as Account;
        }
    }
    #endregion

    #region Transactions

    private Transaction selectedTransaction;

    public void SetTransactionGrid()
    {
        addFinButton.Click += delegate { ShowAddTransactiontWindow(); };
        deleteFinButton.Click += delegate { DeleteTransaction(); };
        clearFinFilterButton.Click += delegate { finFilterText.Clear(); };

        finDataGrid.SelectionChanged += TransactionDataGrid_OnSelectionChanged;
        finDataGrid.AutoGeneratingColumn += SetTransactionGridCollumnName;

        MainWindowViewModel.RefreshTransactions();

        finFilterText.TextChanged += delegate { OnTransactionFilterChanged(); };

        MainWindowViewModel.TransactionsView = new DataGridCollectionView(MainWindowViewModel.Clients);
        MainWindowViewModel.TransactionsView.Filter = TransactionFilter;
        MainWindowViewModel.TransactionsView.Refresh();
    }

    public void ShowAddTransactiontWindow()
    {
        AddTransactionWindow adw = new AddTransactionWindow();
        adw.DataContext = this;
        adw.Closed += delegate { RefreshClient(); };
        adw.ShowDialog(this);
    }

    public void RefreshTransaction()
    {
        MainWindowViewModel.RefreshTransactions();
    }


    public async void DeleteTransaction()
    {
        int id = clientsDataGrid.SelectedIndex;
        if (id != -1)
        {
            var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await mBox.ShowAsPopupAsync(this);

            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                Db.DeleteTransaction(selectedTransaction);
                RefreshTransaction();
            }
            //Debug.WriteLine(clientsDataGrid.Columns.Count);
        }
    }

    private void OnTransactionFilterChanged()
    {
        MainWindowViewModel.TransactionsView.Refresh();
    }

    public void SetTransactionGridCollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "User":
                e.Column.Header = "Клиент";
                break;
            case "date":
                e.Column.Header = "Дата операции";
                break;
            case "TransactType":
                e.Column.Header = "Тип операции";
                break;
            case "Desc":
                e.Column.Header = "Описание";
                break;
            case "amount":
                e.Column.Header = "Сумма";
                break;
        }
    }

    public bool TransactionFilter(object o)
    {
        if (finFilterText.Text != null && finFilterText.Text != string.Empty)
        {
            Transaction tr = (Transaction)o;
            if (tr.User.Contains(finFilterText.Text) || tr.date.ToString().Contains(finFilterText.Text) || tr.transactionType.ToString().Contains(finFilterText.Text) || tr.decs.Contains(finFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void TransactionDataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedTransaction = e.AddedItems[0] as Transaction;
        }
    }

    #endregion
}