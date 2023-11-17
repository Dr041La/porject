using System;

namespace rusiVSlizards.Models;

public class Transaction
{
    public int id;
    public Client user;

    public string User
    {
        get
        {
            return user.ToString();
        }
    }

    public TransactionType transactionType;

    public string TransactType
    {
        get
        {
            if (transactionType == TransactionType.income)
            {
                return "Дохов";
            }
            else if (transactionType == TransactionType.expense) 
            {
                return "Расхов";
            }
            return transactionType.ToString();
        }
    }

    public DateTime date { get; set; }

    public decimal amount { get; set; }
    public string decs { get; set; }

    public Transaction(int id, Client user, DateTime date, decimal amount, string decs)
    {
        this.id = id;
        this.user = user;
        this.date = date;
        this.amount = amount;
        this.decs = decs;
    }

    public Transaction() 
    { }
}

public enum TransactionType
{
    income,
    expense
}