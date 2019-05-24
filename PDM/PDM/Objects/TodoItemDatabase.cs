using SQLite;


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Objects
{
    public class TodoItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TodoItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Items>().Wait();
        }

        public Task<List<Items>> GetItemsAsync()
        {
            return database.Table<Items>().ToListAsync();
        }

        public Task<List<Items>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Items>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<Items> GetItemAsync(int id)
        {
            return database.Table<Items>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Items item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Items item)
        {
            return database.DeleteAsync(item);
        }
    }
}
