using System.Collections.ObjectModel;
using Avalonia.Collections;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using rusiVSlizards.Models;
using SkiaSharp;

namespace rusiVSlizards.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
    public static DataGridCollectionView ClientsView { get; set; } = new DataGridCollectionView(Clients);

    public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
    public static DataGridCollectionView AccountsView { get; set; } = new DataGridCollectionView(Accounts);

    public static ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();
    public static DataGridCollectionView TransactionsView { get; set; } = new DataGridCollectionView(Transactions);
    public ISeries[] Series { get; set; } = new ISeries[]
    {
        new LineSeries<double>
        {
            Values = new double[] {0, 4, 10, 12, 8, 2, -2},
            Fill = new SolidColorPaint(new SKColor(0, 90, 120)),
            Stroke = new SolidColorPaint(new SKColor(120, 152, 203)),
            LineSmoothness = 50
        }
    };
    
    public static void RefreshClients()
    {
        Clients.Clear();
        Clients.AddRange(Db.GetAllClients());
        ClientsView.Refresh();
        RefreshAccounts();
    }

    public static void RefreshAccounts()
    {
        Accounts.Clear();
        Accounts.AddRange(Db.GetAllAccounts());
        AccountsView.Refresh();
    }

    public static void RefreshTransactions()
    {
        Transactions.Clear();
        Transactions.AddRange(Db.GetAllTransactions());
        TransactionsView.Refresh();
    }
}