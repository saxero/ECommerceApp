# Xamarine

1. Create Xamarin Application

2. Go to ECommerce(Portable) project.  
In App.xaml, create a ResourceDictionary inside the <Application.Resources>.  Color schemes can be found at https://color.adobe.com/es/explore
```xml
    <!-- Application resource dictionary -->
    <ResourceDictionary>
      <Color x:Key="MainColor">#1b2b32</Color>
      <Color x:Key="FontColor">#e1e7e8</Color>
      <Color x:Key="MenuColor">#a3abaf</Color>
      <Color x:Key="MenuFontColor">#e1e7e8</Color>
      <Color x:Key="BackgroundColor">#37646b</Color>
      <Color x:Key="AccentColor">#b22e2f</Color>
    </ResourceDictionary>
```

3. Create a Pages folder and add 3 new ContentPages: MasterPage, MenuPage and UserPage
Change _MasterPage_ xaml from COntentpage to MasterDetailPage and set the master-detail sections. It is important to define the namespace of the classes that define the section.  Note that each <pages> must specify the class that implements them.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<MasterDetailPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ECommerceApp.Pages.MasterPage"
             xmlns:pages="clr-namespace:ECommerceApp.Pages;assembly=ECommerceApp" >
    <MasterDetailPage.Master>
        <pages:MenuPage></pages:MenuPage>
    </MasterDetailPage.Master>
    <MasterDetailPage.Detail>
        <NavigationPage x:Name="Navigator" BackgroundColor="{StaticResource MainColor}">
            <x:Arguments>
                <pages:UserPage>           
                </pages:UserPage>
            </x:Arguments>
        </NavigationPage>
    </MasterDetailPage.Detail>
</MasterDetailPage>
```
Also change the codebehind with the following changes:
    - the Masterpage cass must inherit from MasterDetailPage
    - override OnAppearing event: double click on App.Navigator to create this property in the App class
    - add the corresponding "using" in App.xaml.cs
    - change the MainPage property to use our MasterPage class
```c#
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Navigator = Navigator;
        }
    }
```
...
```c#
using ECommerceApp.Pages;
public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public App()
        {
            InitializeComponent();

            //MainPage = new ECommerceApp.MainPage();
            MainPage = new MasterPage();
        }
```
4. Custom launcher icon:  we can use a custom icon for launching the app. This icon is not visible when running the app, but in the device collection of apps.  Find a free nice icon at https://www.iconfinder.com and download it.

Upload the icon at romannurik.github.io/AndroidAssetStudio in order to customize the colors, shape and download the dufferent andoid png versions.  Then, extract each file and place into the Resources folder (Android project):
- drawable-hdpi
- drawable-xhdpi
- drawable-xxhdpi
- put the drawable-hdpi version in drawable folder as well

Once the files are copied, remember to add them to the project (via Solution Explorer).

5. Open MainActivity.cs (Android project) and change the Icon property:
```c#
 [Activity(Label = "ECommerce App", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
```

6. Create the corresponding icons for each element in the menu, place them in the Resources folder (Android project) and load them into the project.

7. ViewModels
Crete a new folder called ViewModels containing the MenuItemViewModel class for each entry of the menu
```c#
public class MenuItemViewModel
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
    }
```
Create the MainViewModel class to implement the menu: 
    - an ObservableCollection as a Collection with the items in the menu
    - a LoadMenu() function that fills the Collection
```c#
public class MainViewModel
    {
        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            LoadMenu();
        }

        #endregion
        
        
        #region Metodos
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                 Icon = "ic_action_product.png",
                 PageName = "ProducsPage",
                Title = "Productos"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_customer.png",
                PageName = "ProducsPage",
                Title = "Clientes"
            });
```

8. Design the menu
The menuPage.xaml will display the menu.  It implements a list of items that will be populated from the viewmodels. 
    - the listview is binded to a Menu element.  
    - a grid is used to place the info in columns (width="Auto" allows to get the necessary spacefor the first element).
MenuPage.xaml:
```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="ECommerceApp.Pages.MenuPage"
             Title="Menu"
             BackgroundColor="{StaticResource MenuColor}"
             BindingContext="{Binding Main, Source={StaticResource Locator}}">
    <ContentPage.Content>
        <StackLayout>
            <ListView ItemsSource="{Binding Menu}" HasUnevenRows="True">
                <ListView.ItemTemplate>
                <DataTemplate>
                    <ViewCell>
                        <Grid>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="Auto"></ColumnDefinition>
                                <ColumnDefinition Width="*"></ColumnDefinition>
                            </Grid.ColumnDefinitions>
                            <Image Source="{Binding Icon}"
                                WidthRequest="50"      
                                HeightRequest="50"/>
                            <Label 
                                Grid.Column="1"
                                VerticalOptions="Center"
                                TextColor="{StaticResource MenuFontColor}"
                                Text="{Binding Title}"
                                ></Label>
                        </Grid>
                    </ViewCell>
                </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
- a BindingContext is pointing to a "Locator" resource defined in App.xaml
App.xaml:
```xml
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:infra="clr-namespace:ECommerceApp.Infrastructure;assembly=ECommerceApp"
             x:Class="ECommerceApp.App">
	<Application.Resources>

		<!-- Application resource dictionary -->
        <ResourceDictionary>
            <Color x:Key="MainColor">#1b2b32</Color>
            <Color x:Key="FontColor">#1b2b32</Color>
            <Color x:Key="MenuColor">#a3abaf</Color>
            <Color x:Key="MenuFontColor">#e1e7e8</Color>
            <Color x:Key="BackgroundColor">#e1e7e8</Color>
            <Color x:Key="AccentColor">#b22e2f</Color>
            
            <!-- Locator-->
            <infra:InstanceLocator x:Key="Locator" />
            
        </ResourceDictionary>
	</Application.Resources>
</Application>
```
9. Bind the menu.
A new class InstanceLocator is in charge of create a new instance of MainViewModel and publish it through a public property (Menu as it is needed in the ListView):
```c#
namespace ECommerceApp.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
```
