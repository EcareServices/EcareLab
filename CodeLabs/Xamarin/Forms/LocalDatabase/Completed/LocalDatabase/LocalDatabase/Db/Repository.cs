using System.Collections.Generic;
using System.Linq;
using LocalDatabase.Model;
using SQLite;
using Xamarin.Forms;

namespace LocalDatabase.Db
{
    public class Repository {
        private static readonly object Locker = new object();
        private readonly SQLiteConnection _database;

        public Repository() {
            _database = DependencyService.Get<ISqLite>().GetConnection();
            
            _database.CreateTable<TodoItem>();
        }

        public IEnumerable<TodoItem> GetItems()
        {
            lock (Locker)
            {
                return _database.Table<TodoItem>().ToList();
            }
        }

        public IEnumerable<TodoItem> GetItemsNotDone()
        {
            lock (Locker)
            {
                return _database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
            }
        }

        public TodoItem GetItem(int id)
        {
            lock (Locker)
            {
                return _database.Table<TodoItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem(TodoItem item)
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
                    _database.Update(item);
                    return item.Id;
                }

                return _database.Insert(item);
            }
        }

        public int DeleteItem(int id)
        {
            lock (Locker)
            {
                return _database.Delete<TodoItem>(id);
            }
        }
    }
}
