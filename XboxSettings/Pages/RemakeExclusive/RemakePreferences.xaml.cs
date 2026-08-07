using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XboxSettings.Pages.RemakeExclusive
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RemakePreferences : Page
    {
        public RemakePreferences()
        {
            this.InitializeComponent();
        }

        private void UnfaithfulSwitch_Checked(object sender, RoutedEventArgs e)
        {
            UnfaithfulOptionsButton.IsEnabled = true;
        }

        private void UnfaithfulSwitch_Unhecked(object sender, RoutedEventArgs e)
        {
            UnfaithfulOptionsButton.IsEnabled = false;
        }

        private void FadeInThemeAnimation_Completed(object sender, object e)
        {
            Debug.WriteLine("Animator: FadeInThemeAnimation is finished");
        }

        private void FadeOutThemeAnimation_Completed(object sender, object e)
        {
            Debug.WriteLine("Animator: FadeOutThemeAnimation is finished");
        }

        private void UnfaithfulOptionsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UnfaithfulSettings), null, new SuppressNavigationTransitionInfo());
        }
    }
}
