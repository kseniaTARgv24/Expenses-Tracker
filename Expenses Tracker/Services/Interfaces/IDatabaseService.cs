using Expenses_Tracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expenses_Tracker.Services.Interfaces
{
    public interface IDatabaseService
    {
        //Categories:
        Task<List<Category>> GetCategoriesAsync(); //select
        Task<Category> GetCategoryByIdAsync(int id);
        Task<int> SaveCategoryAsync(Category category); // insert/update
        Task<int> DeleteCategoryAsync(Category category); //delete

        //Transactions:
        Task<List<Transaction>> GetTransactionsAsync(); //select
        Task<List<Transaction>> GetTransactionsByMounthAsync(int year, int month); //select w/ filter
        Task<int> SaveTransactionAsync(Transaction transaction);
        Task<int> DeleteTransactionAsync(Transaction transaction); 
    }
}
