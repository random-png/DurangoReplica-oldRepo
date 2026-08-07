using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace XboxMessages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        var titleBar = ApplicationView.GetForCurrentView().TitleBar;
        ApplicationView.GetForCurrentView().Title = "We couldn't recreate this app";
        titleBar.BackgroundColor = Colors.Black;
        titleBar.ButtonBackgroundColor = Colors.Black;
        titleBar.ButtonHoverBackgroundColor = Colors.Black;
        titleBar.ButtonPressedBackgroundColor = Colors.Black;
        coreTitleBar.ExtendViewIntoTitleBar = true;
        Window.Current.SetTitleBar(TitleBar);

        this.InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Exit();
    }

    private void Preview(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Pages.MessagesHome), null, new SuppressNavigationTransitionInfo());
    }
}
