using System;
using SQLite;

namespace Expenses_Tracker.Models
{
    public enum TransactionType { Expense = 0, Income = 1 }
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]  //using SQLite;
        public int Id { get; set; }
        public double Amount { get; set; }
        public int CategoryId { get; set; } //foreign key
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Note { get; set; }
        public TransactionType Type { get; set; }

    }
}
