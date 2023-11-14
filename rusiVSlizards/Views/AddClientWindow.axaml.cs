using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using rusiVSlizards.Models;

namespace rusiVSlizards.Views;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();
    }
    
    public AddClientWindow(Client client)
    {
        InitializeComponent();
    }
}