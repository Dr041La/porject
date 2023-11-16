using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using rusiVSlizards.Models;
using rusiVSlizards.ViewModels;
using System;
using System.Linq;

namespace rusiVSlizards.Views;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();
        rolesBox.ItemsSource = Enum.GetValues(typeof(UserRole));

        addButton.Click += delegate { AddClient(); };
        cancelButton.Click += delegate { Close(null); };
    }

    public AddClientWindow(Client client)
    {
        InitializeComponent();
        rolesBox.ItemsSource = Enum.GetValues(typeof(UserRole));

        addText.Text = "Изменить";
        addButton.Click += delegate { ChangeClient(client.id); };
        cancelButton.Click += delegate { Close(null); };

        nameText.Text = client.name;
        passText.Text = client.password;

        rolesBox.Items.CollectionChanged += delegate
        {
            for (int i = 0; i < rolesBox.Items.Count; i++)
            {
                UserRole c = (UserRole)rolesBox.Items[i];
                if (c.ToString() == client.userRole.ToString())
                {
                    rolesBox.SelectedIndex = i;
                    return;
                }
            }
        };
    }

    private void ChangeClient(int id)
    {
        Client client = GetData();
        client.id = id;
        if (client == null)
        {
            return;
        }

        Db.ChangeClient(client);
        Close(null);
    }

    private void AddClient()
    {
        Client client = GetData();

        if (client == null)
        {
            return;
        }

        Db.AddClient(client);
        Close(null);
    }

    private Client GetData()
    {
        Client client = new Client();

        int phone;

        if (nameText.Text == string.Empty || passText.Text == string.Empty || rolesBox.SelectedIndex == -1)
        {
            return null;
        }

        client.name = nameText.Text;
        client.password = passText.Text;
        client.userRole = (UserRole)rolesBox.SelectedItem;
        return client;
    }
}