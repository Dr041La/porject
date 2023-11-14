using System;

namespace rusiVSlizards.ViewModels;

public class Tax
{
    public int id { get; set; }
    public int userId { get; set; }
    public string taxType { get; set; }
    public int taxAmount { get; set; }
    public DateTime date { get; set; }

    public Tax(int id, int userId, string taxType, int taxAmount, DateTime date)
    {
        this.id = id;
        this.userId = userId;
        this.taxType = taxType;
        this.taxAmount = taxAmount;
        this.date = date;
    }
}