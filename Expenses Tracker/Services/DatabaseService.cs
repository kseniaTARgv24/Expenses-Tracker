using Expenses_Tracker.Models;
using Expenses_Tracker.Services.Interfaces;
using Microsoft.Maui.Storage;
using SQLite;
using System.Linq.Expressions;

namespace Expenses_Tracker.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _db;   //using SQLite;
        public DatabaseService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Category>().Wait();   // waits for tables to be created w/o moving futher
            _db.CreateTableAsync<Transaction>().Wait();
        }



        ///////////// Describing the methods ////////////////
        

        public Task<int> DeleteCategoryAsync(Category category) => _db.DeleteAsync(category);
        public Task<List<Category>> GetCategoriesAsync() => _db.Table<Category>().ToListAsync();
        public Task<Category> GetCategoryByIdAsync(int id) =>  _db.Table<Category>().FirstOrDefaultAsync(c => c.Id == id);
        public Task<int> SaveCategoryAsync(Category category) => _db.InsertAsync(category);




        public Task<int> DeleteTransactionAsync(Transaction transaction)=> _db.DeleteAsync(transaction);
        public Task<int> SaveTransactionAsync(Transaction transaction) => _db.InsertAsync(transaction);
        public Task<List<Transaction>> GetTransactionsAsync()=> _db.Table<Transaction>().OrderByDescending(t => t.Date).ToListAsync(); //.OrderBy...
        public Task<List<Transaction>> GetTransactionsByMounthAsync(int year, int month)
        {
            var start = new DateTime(year, month, 1); //DateTime(int year, int month, int day)
            var end = start.AddMonths(1);

            return _db.Table<Transaction>().Where(t => t.Date >= start && t.Date < end).OrderByDescending(t => t.Date).ToListAsync();

        }

        // From official annotation:
        //     Filters the query based on a predicate.
        //public AsyncTableQuery<T> Where(Expression<Func<T, bool>> predExpr)

    }
}
