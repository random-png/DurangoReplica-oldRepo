using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XboxMusic.Pages.RemakeExclusive
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnfaithfulSettings : Page
    {
        public UnfaithfulSettings()
        {
            this.InitializeComponent();
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

            switch (selectedTag)
            {
                case "XboxGreen":
                    var greenColor = Color.FromArgb(255, 0, 128, 0);
                    Application.Current.Resources["SystemAccentColor"] = greenColor;
                    break;
                case "GrooveBlue":
                    var blueColor = Color.FromArgb(255, 0, 120, 212);
                    Application.Current.Resources["SystemAccentColor"] = blueColor;
                    break;
                case "SystemColor":
                    Application.Current.Resources["SystemAccentColor"] = trueSystemAccent;
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
                    }
                    break;
            }
        }
    }
}
