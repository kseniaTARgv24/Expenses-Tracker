using SQLite;

namespace Expenses_Tracker.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement] //using SQLite;
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public bool IsDefault { get; set; }
        
    }
}
