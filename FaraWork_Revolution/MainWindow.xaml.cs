using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MahApps.Metro.Controls;
using FaraWork_Revolution.ViewModels;
using MenuItem = FaraWork_Revolution.ViewModels.MenuItem;
using Squirrel;
using FaraWork_Revolution.Services.Notification;
using Notifications.Wpf;

namespace FaraWork_Revolution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.Navigation.Frame = new Frame() { NavigationUIVisibility = NavigationUIVisibility.Hidden };
            Navigation.Navigation.Frame.Navigated += SplitViewFrame_OnNavigated;

            // Navigate to the home page.
            this.Loaded += MainWindow_OnLoaded;
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //NotificationService.NativeNotification("ee", "ee", "WindowArea");
            //NotificationService.SimpleNotification("whatever", "dumb shit", NotificationType.Error);
            Navigation.Navigation.Navigate(new Uri("Views/MainPage.xaml", UriKind.RelativeOrAbsolute));
            try
            {
                using (var updateManager = new UpdateManager(@"C:\Users\Mahdi Khalili\Documents\Visual Studio 2015\Projects\FaraWork_Revolution\FaraWork_Revolution\bin\Debug\Releases"))
                {
                    var CurrentVersion = $"Current version: {updateManager.CurrentlyInstalledVersion()}";
                    var releaseEntry = await updateManager.UpdateApp();
                    var NextVersion = $"Update Version: {releaseEntry?.Version.ToString() ?? "No update"}";
                    MessageBox.Show(CurrentVersion + "\t" + releaseEntry + "\t" + NextVersion);
                }
            }
            catch
            {

            }
        }
        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            this.HamburgerMenuControl.Content = e.Content;
            this.HamburgerMenuControl.SelectedItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetItem(e.Uri);
            this.HamburgerMenuControl.SelectedOptionsItem = e.ExtraData ?? ((ShellViewModel)this.DataContext).GetOptionsItem(e.Uri);
            GoBackButton.Visibility = Navigation.Navigation.Frame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            Navigation.Navigation.GoBack();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            var menuItem = e.InvokedItem as MenuItem;
            if (menuItem != null && menuItem.IsNavigation)
            {
                Navigation.Navigation.Navigate(menuItem.NavigationDestination, menuItem);
            }
        }
    }
}
