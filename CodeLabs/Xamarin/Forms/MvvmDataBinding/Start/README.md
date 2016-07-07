# MVVM Data binding ListView en infinite scroll

## The project

Open and inspect the project. Make sure the project builds properly.

MvvmDataBinding (Portable) is the only project we will be working in.

## MVVM (Model View ViewModel) and Data binding

### the Model

As a model we will use the entity `/Model/Person.cs`

Inspect `/Service/PersonService.cs`, this will be used as the datasource for the project. It randomly generates n persons in the Get method.

### Create the ViewModel

Add the folder `/ViewModel`

Add the file `/ViewModel/MainPageViewModel.cs`

Implement the class:

```cs
    public class MainPageViewModel {
        public ObservableCollection<Person> Persons { get; }
        
        public MainPageViewModel() {
            Persons = new ObservableCollection<Person>();

            LoadPersons();
        }

        private void LoadPersons()
        {
            var persons = PersonService.Get(10);
            foreach (var character in persons)
            {
                Persons.Add(character);
            }
        }
    }
```

### Create the View

Add the folder `/View`

Add a new item `Forms Xaml Page` to the folder and name it `MainPage.xaml.cs`

Open `/App.cs`

In the constructor set the MainPage as start page:

```cs
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }
```

Open the code behind of `/View/MainPage.xaml.cs`.

Add the ViewModel as property and instantiate it in the constructor.

```cs
        private MainPageViewModel _viewModel;

        public MainPage() {
            
            _viewModel = new MainPageViewModel();
         
            InitializeComponent();
        }
```

Now set the ViewModel as the BindingContext of the Xaml page. The constructor should look like this:

```cs
        public MainPage() {
            
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;

            InitializeComponent();
        }
```

By setting the BindingContext we can now reference it in the xaml markup like this `{Binding ...}`

Let's bind the ViewModel's Person list to a ListView.

Open the `MainPage.xaml` markup. And add the following snippet:

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MvvmDataBinding.Pages.MainPage">
  <StackLayout Padding="30">
    <Label Text="Phonebook" Font="32"></Label>
    <ListView x:Name="PersonList" 
              ItemsSource=""
              RowHeight="100">
      <ListView.ItemTemplate>
        <DataTemplate> 
          <ViewCell>
            <StackLayout Padding="10">
              <Label Text=""></Label>
              <Label Text="" Font="10"></Label>
              <Label Text=""></Label>
            </StackLayout>
          </ViewCell>  
        </DataTemplate>
      </ListView.ItemTemplate>
    </ListView>
  </StackLayout>
</ContentPage>
```

In the ListView tag set the `ItemsSource` property to the MainPageViewModel.Persons. Because MainPageViewModel is the BindingContext we can use `{Binding Persons}`

```xml
	ItemsSource="{Binding Persons}"
```

Now for each person set the labels to the properties

```xml
            <StackLayout Padding="10">
              <Label Text="{Binding FullName}"></Label>
              <Label Text="{Binding Email}" Font="10"></Label>
              <Label Text="{Binding Phone}"></Label>
            </StackLayout>
```

Finally add a ItemTapped event handler to the ListView under ItemDataSource.

```xml
	ItemTapped="PersonList_OnItemTapped"
```

In the code behind implement the method to display the tapped item

```cs
		private void PersonList_OnItemTapped(object sender, ItemTappedEventArgs e) {
            var person = (Person) e.Item;
            DisplayAlert("Selection made", $"You tapped {person.FullName}", "Ok");
        }
```

Run the project. The ListView should contain persons. 

## Infinite scroll

Inspect the file `Service/InfiniteListView.cs` it is an overload of the ListView class that exposes a `LoadMoreCommand` command. When a list item is visible on the screen the method InfiniteListView_ItemAppearing will be called, if this item is the last item in the list then it will try to execute the `LoadMoreCommand`.

Open `MainPage.xaml`

Change the tag `ListView` in `InfiniteListView` resharper will suggest to add the control namespace in the root node. The tagname now containts the namespace, somthing like `service:InfiniteListView`. Now change the tag `ListView.ItemTemplate` in `service:InfiniteListView.ItemTemplate`

Open `ViewModel/MainPageViewModel.cs`

Add the property 

```cs
	public ICommand LoadPersonsCommand { get; set; }
```

And in the constructor bind the command to the `LoadPersons()` method.

```cs
	LoadPersonsCommand = new Command(LoadPersons);
```

Now wire up the LoadMore binding of the InfiniteListView `MainPage.xaml`. After ItemTapped add the LoadMore.

```xml
	LoadMoreCommand="{Binding LoadCharactersCommand}"
```

Run the project. The list should scroll infinite. 

If you have any problems, check out the [Completed version](https://github.com/EcareServices/EcareLab/tree/master/CodeLabs/Xamarin/Forms/MvvmDataBinding/Completed). 