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
