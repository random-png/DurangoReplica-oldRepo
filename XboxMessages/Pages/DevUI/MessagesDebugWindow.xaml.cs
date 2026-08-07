using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XboxMessages.Pages.DevUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessagesDebugWindow : Page
    {
        public MessagesDebugWindow()
        {
            this.InitializeComponent();
        }

        private void NewMessageFormReset(object sender, RoutedEventArgs e)
        {
            MessageUsername.Text = string.Empty;
            MessageContent.Text = string.Empty;
            MessageTimePicker.SelectedTime = null;
            MessageDatePicker.SelectedDate = null;
            ReadStatus.IsOn = false;
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if(SendResetCheckbox.IsChecked == true)
            {
                NewMessageFormReset(null, null);
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
