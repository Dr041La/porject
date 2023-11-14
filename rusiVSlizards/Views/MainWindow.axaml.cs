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
            /*
            Client client = (Client)o;
            if (client.firstName.Contains(clientFilterText.Text) || client.surName.Contains(clientFilterText.Text) || client.phone.ToString().Contains(clientFilterText.Text) || client.birthday.ToString().Contains(clientFilterText.Text) || client.lastLanguage.Contains(clientFilterText.Text) || client.languageNeeds.Contains(clientFilterText.Text) || client.languageLevel.Contains(clientFilterText.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
            */
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
    
}