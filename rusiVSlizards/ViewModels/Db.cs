using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using MySql.Data.MySqlClient;
using rusiVSlizards.Models;

namespace rusiVSlizards.ViewModels;

public static class Db
{
    private static MySqlConnection connection;
    
    

    static Db()
    {
        connection = new MySqlConnection("server=10.10.1.24;uid=user_01;pwd=user01pro;database=pro1_6");
    }
    
    #region Clients
    public static List<Client> GetAllClients()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        List<Client> clients = new List<Client>();
        MySqlCommand commang = new MySqlCommand("select * from Client", connection);
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
        connection.Close();
    }

    public static void DeleteClient(Client client)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Client WHERE id = @id", connection);
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
}