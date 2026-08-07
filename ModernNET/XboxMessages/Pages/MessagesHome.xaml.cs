using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.WindowManagement.Preview;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XboxMessages.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MessagesHome : Page
{
    private AppWindow? DebugWindow;

    public MessagesHome()
    {
        ApplicationView.GetForCurrentView().Title = "Messages";
        this.InitializeComponent();
        Page_Loaded(null, null);
        KeyDown += StandardVideoPlayback_KeyDown;
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        MessageList.Focus(FocusState.Programmatic);
    }

    private async void DebugWindowButton_Click(object sender, RoutedEventArgs e)
    {
        if (DebugWindow == null)
        {
            DebugWindowButton.IsEnabled = !DebugWindowButton.IsEnabled;
            DebugWindow = await AppWindow.TryCreateAsync();
            WindowManagementPreview.SetPreferredMinSize(DebugWindow, new Size(360, 680));
            DebugWindow.RequestSize(new Size(380, 700));
            Frame AppWindowFrame = new Frame();
            ElementCompositionPreview.SetAppWindowContent(DebugWindow, AppWindowFrame);
            AppWindowFrame.Background = (Brush)Application.Current.Resources["SystemControlAcrylicWindowBrush"];
            _ = await DebugWindow.TryShowAsync();
            AppWindowFrame.Navigate(typeof(DevUI.MessagesDebugWindow), null, new CommonNavigationTransitionInfo());
            DebugWindow.Closed += delegate { DebugWindow = null; AppWindowFrame.Content = null; DebugWindowButton.IsEnabled =! DebugWindowButton.IsEnabled; ElementSoundPlayer.Play(ElementSoundKind.GoBack); };
        }
    }

    private void StandardVideoPlayback_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.GamepadY)
        {
            if (SnapViewButton.Visibility == Visibility.Collapsed)
            {
                SnapViewButton.Visibility = Visibility.Visible;
                UnfaithfulButton.Visibility = Visibility.Visible;
                ElementSoundPlayer.Play(ElementSoundKind.Show);
                SnapViewButton.Focus(FocusState.Programmatic);
            }
        }
    }

    private void ItemsWrapGrid_BringIntoViewRequested(UIElement sender, BringIntoViewRequestedEventArgs args)
    {
        if (args.HorizontalAlignmentRatio != 0.067)  // Guard against our own request
        {
            args.Handled = true;
            //Swallow this request and restart it with a request to center the item.We could instead have chosen
            //to adjust the TargetRect's Y and Height values to add a specific amount of padding as it bubbles up, 
            //but if we just want to center it then this is easier.

            //(Optional) Account for sticky headers if they exist
            var headerOffset = 0.0;
            var itemsWrapGrid = sender as ItemsWrapGrid;
            if (MessageList.IsGrouping && itemsWrapGrid.AreStickyGroupHeadersEnabled)
            {
                var header = MessageList.GroupHeaderContainerFromItemContainer(args.TargetElement as GridViewItem);
                if (header != null)
                {
                    headerOffset = ((FrameworkElement)header).ActualHeight;
                }
            }

            //Issue a new request
            args.TargetElement.StartBringIntoView(new BringIntoViewOptions()
            {
                AnimationDesired = true,
                HorizontalAlignmentRatio = 0.067, // a normalized alignment position (0 for the top, 1 for the bottom)
                HorizontalOffset = headerOffset, // applied after meeting the alignment ratio request
            });
        }
    }

    private void SnapViewButton_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(Pages.SnapUI.MessagesSnapHome), null, new SuppressNavigationTransitionInfo());
    }
}
