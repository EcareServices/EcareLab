# Local Database demo (SqlLite.net) todo list app

## The project

Open and inspect the project. Make sure the project builds properly.

Install the the nuget package sqlite-net-pcl in the following projects:

`install-package sqlite-net-pcl`

- PCL
- Droid
- iOS
- Windows
- WinPhone

Add reference > Windows 8.1 > `Microsoft Visual C++ 2013 Runtime Package for Windows` to the projects:
- Windows
- WinPhone
You need to set the Platform target on anything else then "Any CPU"


## Sql connection Dependency resolver

Create interface for platform specific Connections using the Dependency resolver

Create the interface in the PCL project in the Db Folder

```cs
    public interface ISqLite
    {
        SQLiteConnection GetConnection();
    }
```

Create the following class in the projects: Droid, iOS, Windows, WinPhone
and mark it with the attribute for de Dependency resolver

```cs
	[assembly: Xamarin.Forms.Dependency(typeof(SqlConnection))]
	
	//namespace ...
	
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection() {
            throw new NotImplementedException();
        }
    }
```

Now in the repository we can resolve the interface. Open [PCL project]\Db\Repository.cs. Add the following code in the constructor:

```cs
		private readonly SQLiteConnection _database;

        public Repository() {
            _database = DependencyService.Get<ISqLite>().GetConnection();
            
            _database.CreateTable<TodoItem>();
        }
```

Complete the SqlConnection.cs classes in the OS specific projects

iOS:

```cs
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
```
	
Android:

```cs
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection() {
            var sqliteFilename = "TodoSQLite.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
```
	
Windows and WinPhone:

```cs
    public class SqlConnection : ISqLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
```

## Implement the repository

Open `[PCL project]\Db\Repository.cs`

Add the repository methods

```cs
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
```

Open `[PCL project]\Model\TodoItem.cs`

Add the following attribute to the Id property

```cs

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
		
```

Create access to the repository in the App.cs class by adding this code in the class

```cs
        private static Repository _database;
        public static Repository Database => _database ?? (_database = new Repository());

```

Now we can access the repository anywhere in the application.


Open `Views\TodoItemList.xaml.cs`

Override the OnAppearing method and bind the result to the listview.

```cs
        protected override void OnAppearing()
        {
            base.OnAppearing();

            ListViewItems.ItemsSource = App.Database.GetItems();
        }
```

Open `Views\TodoItemPage.xaml.cs`

Wire the save and delete click events to the repository

```cs
        void SaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            App.Database.SaveItem(todoItem);
            Navigation.PopAsync();
        }

        void DeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            App.Database.DeleteItem(todoItem.Id);
            Navigation.PopAsync();
        }
```
