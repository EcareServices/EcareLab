# Tabbed page demo

## The project

Open and inspect the project. Make sure the project builds properly.

## Tabs

Expand the TabbedPageDemo (Portable) project.

First we need to add a tabbed page to the project that will contain the pages.

Add a class named `MainPage.cs` to the Views folder and let it inherit from `TabbedPage`.

Now add pages as tabs. Add the following fields to the `MainPage.cs` class:

```cs
public readonly Clienten Clienten;
public readonly Teams Teams;
```
And instantiate them in the constructor.

```cs
Children.Add(Clienten = new Clienten());
Children.Add(Teams = new Teams());
```
We need to set the titles of the pages, these will be used for the tab names.

Add a title to `Clienten.xaml` and `Teams.xaml`. This can be done in xaml by setting the Title attribute of the root element or set the Title property in de class constructor.

For iOS you can also set the Icon property to show icons in the tabs.

Open App.cs and set MainPage as MainPage.

```cs
	public App()
    {
        // The root page of your application
        MainPage = new MainPage();
    }
```

Build and test the project the app should have two tabs with the associated pages.

## Tabs custom navigation

You may want to change tabs using code. You can set the `CurrentPage` property of the `TabbedPage`.

Add a new item to the Views folder, select (Visual C#/Cross Platform) "Forms Xaml Page" and name it Dashboard.cs.

Set the title to `Dashboard` and add the following snippet in the ContentPage element. 

```xml
<StackLayout Orientation="Vertical" HorizontalOptions="FillAndExpand" Padding="20" Spacing="10">
	<Button x:Name="GotoClienten" Text="Naar Cliënten"></Button>
	<Button x:Name="GotoTeams" Text="Naar Teams"></Button>
</StackLayout>
```
In the code behind pass the `MainPage mainPage` as parameter in the constructor and store it as a private field.

Handle the buttons click events and set the `CurrentPage` to the appropriate page.

```cs
    public partial class Dashboard : ContentPage {
        private readonly MainPage _mainPage;

        public Dashboard(MainPage mainPage) {
            InitializeComponent();

            _mainPage = mainPage;

            GotoClienten.Clicked += ClientenClicked;
            GotoTeams.Clicked += TeamsClicked;
        }

        public void ClientenClicked(object sender, EventArgs e) {
            _mainPage.CurrentPage = _mainPage.Clienten;
        }

        public void TeamsClicked(object sender, EventArgs e)
        {
            _mainPage.CurrentPage = _mainPage.Teams;
        }
    }
```

Open `MainPage.cs` and add a DashBoard field, then add the dashboard page as **first** item in the Children collection.

```cs
    public readonly Dashboard Dashboard;
    public readonly Clienten Clienten;
    public readonly Teams Teams;

    public MainPage() {
        Children.Add(Dashboard = new Dashboard(this));
        Children.Add(Clienten = new Clienten());
        Children.Add(Teams = new Teams());
    }
```
Run the project. You should have three tabs. The dashboard tab buttons should also navigate to the configured tabs.


If you have any problems, check the completed version 