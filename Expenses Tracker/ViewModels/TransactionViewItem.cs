// TransactionViewItem.cs
using Expenses_Tracker.Models;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace Expenses_Tracker.ViewModels
{
    public class TransactionViewItem
    {
        public Transaction Transaction { get; }
        public Category Category { get; }
        public ObservableCollection<Transaction> Transactions { get; } = new ObservableCollection<Transaction>(); //or just new(), { get; } --> can only read!
                                                                                                                  //https://www.youtube.com/watch?v=qqJUg-HXV3c

        public TransactionViewItem(Transaction transaction, Category category)
        {
            Transaction = transaction;
            Category = category;
        }

        public string AmountText =>
            Transaction.Type == TransactionType.Expense ? "-" + Transaction.Amount.ToString("F2") : Transaction.Amount.ToString("F2");

        public Color AmountColor =>
            Transaction.Type == TransactionType.Expense ? Colors.Red : Colors.Green;

        public string CategoryDisplay =>
            Category != null ? (Category.Icon ?? Category.Name) : "—";
        

        public DateTime Date => Transaction.Date;
        public string Note => Transaction.Note;
        public TransactionType Type => Transaction.Type;
        
    }
}
