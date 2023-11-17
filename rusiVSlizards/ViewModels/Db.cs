using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using rusiVSlizards.Models;

namespace rusiVSlizards.ViewModels;

public static class Db
{
    private static MySqlConnection connection;

    static Db()
    {
        connection = new MySqlConnection("server = localhost;uid=user;pwd=qwerty228;database=world");
    }

    #region Clients

    public static List<Client> GetAllClients()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        List<Client> clients = new List<Client>();
        MySqlCommand commang = new MySqlCommand("select * from User", connection);
        MySqlDataReader reader = commang.ExecuteReader();
        while (reader.Read())
        {
            clients.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Enum.Parse<UserRole>(reader.GetString(3))));
        }
        connection.Close();

        return clients;
    }

    public static void AddClient(Client client)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("insert into User (Name, Password, Role) value (@n, @ps, @rl)", connection);
        command.Parameters.AddWithValue("@n", client.name);
        command.Parameters.AddWithValue("@ps", client.password);
        command.Parameters.AddWithValue("@rl", client.userRole + 1);
        command.ExecuteNonQuery();

        command = new MySqlCommand("select LAST_INSERT_ID() from User", connection);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        int id = reader.GetInt32(0);
        reader.Close();

        Account ac = new Account();
        ac.user = GetClientAt(id);
        ac.balance = 0;

        AddAccount(ac);

        connection.Close();
    }

    public static Client GetClientAt(int id)
    {
        Client user;
        MySqlCommand command = new MySqlCommand("select * from User where UserID = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        user = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Enum.Parse<UserRole>(reader.GetString(3)));
        reader.Close();
        return user;
    }

    public static void DeleteClient(Client client)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();

        MySqlCommand command = new MySqlCommand("DELETE FROM Accounts WHERE UserID = @id", connection);
        command.Parameters.AddWithValue("@id", client.id);
        command.ExecuteNonQuery();

        command = new MySqlCommand("DELETE FROM User WHERE UserID = @id", connection);
        command.Parameters.AddWithValue("@id", client.id);
        command.ExecuteNonQuery();

        command = new MySqlCommand("DELETE FROM Accounts WHERE UserID = @id", connection);
        command.Parameters.AddWithValue("@id", client.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void ChangeClient(Client client)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("update User set Name = @n, Password = @ps, Role = @rl", connection);
        command.Parameters.AddWithValue("@n", client.name);
        command.Parameters.AddWithValue("@ps", client.password);
        command.Parameters.AddWithValue("@rl", client.userRole + 1);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    #region Accounts

    public static List<Account> GetAllAccounts()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        DataTable dt = new DataTable();
        List<Account> accs = new List<Account>();
        MySqlCommand command = new MySqlCommand("select * from Accounts", connection);
        MySqlDataAdapter reader = new MySqlDataAdapter(command);
        reader.Fill(dt);
        Debug.WriteLine(dt.Rows.Count);
        foreach (DataRow item in dt.Rows)
        {
            Account acc = new Account();
            acc.id = item.Field<int>("UserID");
            acc.balance = item.Field<decimal>("Balance");
            acc.user = GetClientAt(acc.id);
            accs.Add(acc);
        }
        connection.Close();

        return accs;
    }

    public static void AddAccount(Account acc)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("insert into accounts (UserID, Balance) VALUE (@ui, @bl)", connection);
        command.Parameters.AddWithValue("@ui", acc.user.id);
        command.Parameters.AddWithValue("@bl", acc.balance);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void DeleteAccount(Account acc)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();

        MySqlCommand command = new MySqlCommand("DELETE FROM Accounts WHERE AccountID = @id", connection);
        command.Parameters.AddWithValue("@id", acc.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void ChangeAccount(Account acc)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("update User set UserID = @ui, Balance = @bll", connection);
        command.Parameters.AddWithValue("@ui", acc.user.id);
        command.Parameters.AddWithValue("@bl", acc.balance);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    #region Accounts

    public static List<Account> GetAllTransactions()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        DataTable dt = new DataTable();
        List<Account> accs = new List<Account>();
        MySqlCommand command = new MySqlCommand("select * from Accounts", connection);
        MySqlDataAdapter reader = new MySqlDataAdapter(command);
        reader.Fill(dt);
        Debug.WriteLine(dt.Rows.Count);
        foreach (DataRow item in dt.Rows)
        {
            Account acc = new Account();
            acc.id = item.Field<int>("UserID");
            acc.balance = item.Field<decimal>("Balance");
            acc.user = GetClientAt(acc.id);
            accs.Add(acc);
        }
        connection.Close();

        return accs;
    }

    public static void AddAccount(Account acc)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("insert into accounts (UserID, Balance) VALUE (@ui, @bl)", connection);
        command.Parameters.AddWithValue("@ui", acc.user.id);
        command.Parameters.AddWithValue("@bl", acc.balance);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void DeleteAccount(Account acc)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();

        MySqlCommand command = new MySqlCommand("DELETE FROM Accounts WHERE AccountID = @id", connection);
        command.Parameters.AddWithValue("@id", acc.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void ChangeAccount(Account acc)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("update User set UserID = @ui, Balance = @bll", connection);
        command.Parameters.AddWithValue("@ui", acc.user.id);
        command.Parameters.AddWithValue("@bl", acc.balance);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion
}