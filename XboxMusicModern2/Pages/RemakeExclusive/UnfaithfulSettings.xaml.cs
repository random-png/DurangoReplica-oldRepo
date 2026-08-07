using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.Gaming.Input;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XboxMusic.Controls;
using XboxMusic.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XboxMusic.Pages.RemakeExclusive
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnfaithfulSettings : Page
    {
        public string CurrentAppVersion
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return string.Format("App version:\n{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        public static string CurrentAppVersionStringV
        {
            get
            {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return string.Format("App version:\n{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        public UnfaithfulSettings()
        {
            this.InitializeComponent();
            SystemThemeDropdown.SelectionChanged += SystemThemeDropdown_SelectionChanged;
        }

        private void AccentBrandingUnlinkCheckbox_Toggled(object sender, RoutedEventArgs e)
        {
            // TODO: Actually fix AccentDropdown_SelectionChanged instead of using a bandaid
            AccentDropdown.SelectionChanged -= AccentDropdown_SelectionChanged;
            if (AccentBrandingUnlinkCheckbox.IsChecked == true)
            {
                AccentDropdown.IsEnabled = true;
            } else {
                AccentDropdown.IsEnabled = false;
                AccentDropdown.SelectedIndex = BrandingDropdown.SelectedIndex;
            }
            AccentDropdown.SelectionChanged += AccentDropdown_SelectionChanged;
        }

        private void BrandingDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccentBrandingUnlinkCheckbox.IsChecked == false)
            {
                AccentDropdown.SelectedIndex = BrandingDropdown.SelectedIndex;
            }

            ComboBoxItem selectedItem = BrandingDropdown.SelectedItem as ComboBoxItem;
            string selectedTag = selectedItem.Tag as string;

            switch (selectedTag)
            {
                case "XboxMusic":
                    Application.Current.Resources["AppBrandingNameText"] = "Xbox Music";
                    break;
                case "GrooveMusic":
                    Application.Current.Resources["AppBrandingNameText"] = "Groove";
                    break;
            }
        }

        private void ReturnInfoScrollViewer_FocusDisengaged(Control sender, FocusDisengagedEventArgs args)
        {
            ReturnInfoScrollViewer.ChangeView(0, 0, null, false);
        }

        private async void AccentDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = AccentDropdown.SelectedItem as ComboBoxItem;
            string selectedTag = selectedItem.Tag as string;
            var uiSettings = new Windows.UI.ViewManagement.UISettings();
            var trueSystemAccent = uiSettings.GetColorValue(Windows.UI.ViewManagement.UIColorType.Accent);
            var window = ApplicationView.GetForCurrentView();

            switch (selectedTag)
            {
                case "XboxGreen":
                    var greenColor = Color.FromArgb(255, 0, 128, 0);
                    Application.Current.Resources["SystemAccentColor"] = greenColor;
                    window.TitleBar.BackgroundColor = greenColor;
                    window.TitleBar.InactiveBackgroundColor = greenColor;
                    window.TitleBar.ButtonBackgroundColor = greenColor;
                    window.TitleBar.ButtonInactiveBackgroundColor = greenColor;
                    break;
                case "GrooveBlue":
                    var blueColor = Color.FromArgb(255, 0, 120, 212);
                    Application.Current.Resources["SystemAccentColor"] = blueColor;
                    window.TitleBar.BackgroundColor = blueColor;
                    window.TitleBar.InactiveBackgroundColor = blueColor;
                    window.TitleBar.ButtonBackgroundColor = blueColor;
                    window.TitleBar.ButtonInactiveBackgroundColor = blueColor;
                    break;
                case "SystemColor":
                    Application.Current.Resources["SystemAccentColor"] = trueSystemAccent;
                    window.TitleBar.BackgroundColor = trueSystemAccent;
                    window.TitleBar.InactiveBackgroundColor = trueSystemAccent;
                    window.TitleBar.ButtonBackgroundColor = trueSystemAccent;
                    window.TitleBar.ButtonInactiveBackgroundColor = trueSystemAccent;
                    break;
                case "CustomColor":
                    ContentDialog customColorDialog = new ContentDialog();
                    ColorPicker customColorPicker = new ColorPicker() { IsColorChannelTextInputVisible = false, IsHexInputVisible = false };
                    customColorDialog.Content = customColorPicker;
                    customColorDialog.PrimaryButtonText = "Use this color";
                    customColorDialog.CloseButtonText = "Cancel";
                    customColorDialog.DefaultButton = ContentDialogButton.Primary;
                    var result = await customColorDialog.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        Application.Current.Resources["SystemAccentColor"] = customColorPicker.Color;
                        window.TitleBar.BackgroundColor = customColorPicker.Color;
                        window.TitleBar.InactiveBackgroundColor = customColorPicker.Color;
                        window.TitleBar.ButtonInactiveBackgroundColor = customColorPicker.Color;
                        window.TitleBar.ButtonBackgroundColor = customColorPicker.Color;
                    }
                    break;
            }
        }

        private void SystemThemeDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem? comboBoxItem = SystemThemeDropdown.SelectedItem as ComboBoxItem;
            if (comboBoxItem.Tag is string selectedTheme)
            {
                ThemeHelper.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
            }
        }

        private void TileButton_Click(object sender, RoutedEventArgs e)
        {
            var dial = new NewDialog();
            var newGrid = new Grid();
            newGrid.Children.Add(dial);
        }
    }
}
