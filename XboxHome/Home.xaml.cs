using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace XboxHome;

/// <summary>
/// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
/// </summary>
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        InstallListButton.Focus(Windows.UI.Xaml.FocusState.Programmatic);
    }

    private void InstallList(object sender, Windows.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(typeof(InstallList), null, new SuppressNavigationTransitionInfo());
    }
}
