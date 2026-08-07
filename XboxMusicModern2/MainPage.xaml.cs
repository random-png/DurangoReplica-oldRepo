using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Xaml.Input;

namespace XboxMusic;
/// <summary>
/// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
/// </summary>
public sealed partial class MainPage : Page
{
    private int _lastIndex = -1;
    private bool rightTriggerHeld;
    private bool leftTriggerHeld;
    private bool viewButtonHeld;
    private bool menuButtonHeld;
    private bool xButtonHeld;

    public MainPage()
    {
        InitializeComponent();
        KeyDown += MainPage_KeyDown;
        KeyUp += MainPage_KeyUp;
    }

    private void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        var wereAllHeld = leftTriggerHeld && rightTriggerHeld && menuButtonHeld && viewButtonHeld && xButtonHeld;

        if (e.Key == VirtualKey.GamepadLeftTrigger)
            leftTriggerHeld = true;

        if (e.Key == VirtualKey.GamepadRightTrigger)
            rightTriggerHeld = true;

        if (e.Key == VirtualKey.GamepadMenu)
            menuButtonHeld = true;

        if (e.Key == VirtualKey.GamepadView)
            viewButtonHeld = true;

        if (e.Key == VirtualKey.GamepadX)
            xButtonHeld = true;

        if (!wereAllHeld && leftTriggerHeld && rightTriggerHeld && menuButtonHeld && viewButtonHeld && xButtonHeld)
        {
            UnfaithfulDisplayer();
        }
    }

    private void MainPage_KeyUp(object sender, KeyRoutedEventArgs e)
    {
        switch (e.Key)
        {
            case VirtualKey.GamepadLeftTrigger:
                leftTriggerHeld = false;
                break;

            case VirtualKey.GamepadRightTrigger:
                rightTriggerHeld = false;
                break;

            case VirtualKey.GamepadMenu:
                menuButtonHeld = false;
                break;

            case VirtualKey.GamepadView:
                viewButtonHeld = false;
                break;

            case VirtualKey.GamepadX:
                xButtonHeld = false;
                break;
        }
    }

    private void UnfaithfulDisplayer()
    {
        if (UnfaithfulButton.Visibility == Visibility.Collapsed)
        {
            UnfaithfulButton.Visibility = Visibility.Visible;
            ElementSoundPlayer.Play(ElementSoundKind.Invoke);
        }
        else if (UnfaithfulButton.Visibility == Visibility.Visible)
        {
            UnfaithfulButton.Visibility = Visibility.Collapsed;
            ElementSoundPlayer.Play(ElementSoundKind.GoBack);
        }
        //if (ScreenList.Items.Count == 4)
        //{
        //    var unfaithfulItem = new ListViewItem() { Content = "Unfaithful mode", Tag = "unfaithful" };

        //    ScreenList.Items.Add(unfaithfulItem);
        //}
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        AppName.Text = (string)Application.Current.Resources["AppBrandingNameText"];
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        if (ScreenList.SelectedIndex == 4)
        {
            ScreenList.SelectedIndex--;
        }
    }

    private async void ScreenList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var activeItem = ScreenList.SelectedItem as ListViewItem;
        var activeTag = activeItem.Tag as string;
        // TODO: Play MoveNext or MovePrevious sounds from ElementSoundKind.Player based on if the SelectedIndex increased or decreased.
        if (ScreenList.SelectedIndex < 0)
            return;

        var newIndex = ScreenList.SelectedIndex;

        if (_lastIndex >= 0)
        {
            if (newIndex > _lastIndex)
            {
                ElementSoundPlayer.Play(ElementSoundKind.MovePrevious);
                PivotRight.Begin();
                // Task.Delay does introduce a chance of desyncing between what RootPresenter.Value shows and the actual ScreenList.SelectedIndex,
                // but it mainly happens when you're switching between items quickly, which most wouldn't. SelectedResync is a band-aid for that scenario.
                await Task.Delay(190);
                RootPresenter.Value = activeTag;
            }
            else if (newIndex < _lastIndex)
            {
                ElementSoundPlayer.Play(ElementSoundKind.MoveNext);
                PivotLeft.Begin();
                await Task.Delay(190);
                RootPresenter.Value = activeTag;
            }
        }

        _lastIndex = newIndex;
    }

    private void SelectedResync(object sender, object e)
    {
        var activeItem = ScreenList.SelectedItem as ListViewItem;
        var activeTag = activeItem.Tag as string;
        RootPresenter.Value = activeTag;

        if (activeTag == "unfaithful")
        {
            Frame.Navigate(typeof(Pages.RemakeExclusive.UnfaithfulSettings), null, new SuppressNavigationTransitionInfo());
        }
    }

    // TEST PAGE CODE
    // CustomPersonPicture testing
    private void GPSizeBox_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        testpfp.Width = GPSizeBox.Value;
        testpfp.Height = GPSizeBox.Value;
    }

    private void IconToggle_Toggled(object sender, SelectionChangedEventArgs e)
    {
        var selectedValue = IconToggle.SelectedItem as string;

        switch (selectedValue)
        {
            case "Normal":
                testpfp.IconStyle = Controls.CustomPersonPicture.IconStyleEnum.Normal;
                break;
            case "Modern":
                testpfp.IconStyle = Controls.CustomPersonPicture.IconStyleEnum.Modern;
                break;
            case "Classic":
                testpfp.IconStyle = Controls.CustomPersonPicture.IconStyleEnum.Classic;
                break;
        }
    }

    private void ProfilePictureToggleClick(object sender, RoutedEventArgs e)
    {
        if (testpfp.ProfilePicture is not null)
        {
            testpfp.ProfilePicture = null;
        } else {
            var uri = "ms-appx:///Assets/Images/GenericPFP.png";
            testpfp.ProfilePicture = new BitmapImage(new Uri(uri));
        }
    }

    private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
    {
        PivotLeft.Begin();
    }

    private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
    {
        PivotRight.Begin();
    }

    private void UnfaithfulButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Pages.RemakeExclusive.UnfaithfulSettings), null, new SuppressNavigationTransitionInfo());
    }
}
