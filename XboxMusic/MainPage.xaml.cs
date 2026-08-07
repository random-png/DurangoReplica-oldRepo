using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XboxMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int _lastIndex = -1;

        public MainPage()
        {
            this.InitializeComponent();
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
            ListViewItem activeItem = ScreenList.SelectedItem as ListViewItem;
            string activeTag = activeItem.Tag as string;
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
            ListViewItem activeItem = ScreenList.SelectedItem as ListViewItem;
            string activeTag = activeItem.Tag as string;
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
                    testpfp.IconStyle = Controls.IconStyleEnum.Normal;
                    break;
                case "Modern":
                    testpfp.IconStyle = Controls.IconStyleEnum.Modern;
                    break;
                case "Classic":
                    testpfp.IconStyle = Controls.IconStyleEnum.Classic;
                    break;
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            testpfp.ProfilePicture = null;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            string uri = "ms-appx:///Assets/Images/GenericPFP.png";
            testpfp.ProfilePicture = new BitmapImage(new Uri(uri));
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PivotLeft.Begin();
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            PivotRight.Begin();
        }
    }
}