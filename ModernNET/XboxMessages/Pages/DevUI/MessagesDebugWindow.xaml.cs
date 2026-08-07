using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XboxMessages.Pages.DevUI;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MessagesDebugWindow : Page
{
    public MessagesDebugWindow()
    {
        this.InitializeComponent();
    }

    private void NewMessageFormReset(object sender, RoutedEventArgs e) => ClearOutForm();

    private void SendMessage_Click(object sender, RoutedEventArgs e)
    {
        MessageList.Items.Add(MessageContent.Text);
        if(SendResetCheckbox.IsChecked == true)
        {
            ClearOutForm();
        }
    }

    private void ListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        // TODO: Delete a message when clicked, and remove it from whatever source ends up being used
        MessageList.Items.Remove(e.ClickedItem);
    }

    private void ClearOutForm()
    {
        MessageUsername.Text = 
        MessageContent.Text = string.Empty;
        MessageTimePicker.SelectedTime = null;
        MessageDatePicker.SelectedDate = null;
        ReadStatus.IsOn = false;
    }

    private void DeleteMessagesButton_Click(object sender, RoutedEventArgs e)
    {
        MessageList.Items.Clear();
    }

    private async Task PickCurrentGamerpic_ClickAsync()
    {
        PickCurrentGamerpic.IsEnabled = false;
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
        picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");

        Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            BitmapImage bitmap = new BitmapImage();
            using (Stream stream = await file.OpenStreamForReadAsync())
            {
                await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            }

            ImageBrush brush = new ImageBrush
            {
                ImageSource = bitmap,
                Stretch = Stretch.UniformToFill
            };

            PickCurrentGamerpic.Background = brush;
        }
        PickCurrentGamerpic.IsEnabled = true;
    }

    private void PickCurrentGamerpic_Click(object sender, RoutedEventArgs e) => PickCurrentGamerpic_ClickAsync();
}
