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
        <NavigationPage x:Name="Navigator">
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
