using System;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using XboxMusic.Helpers;

namespace XboxMusic;
/// <summary>
/// Provides application-specific behavior to supplement the default <see cref="Application"/> class.
/// </summary>
public sealed partial class App : Application
{
    private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        ElementSoundPlayer.State = ElementSoundPlayerState.On;

        InitializeComponent();
        Suspending += OnSuspending;
    }

    /// <inheritdoc/>
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        var view = ApplicationView.GetForCurrentView();
        view.FullScreenSystemOverlayMode = FullScreenSystemOverlayMode.Minimal;
        view.SetPreferredMinSize(new Windows.Foundation.Size(480, 500));
        ThemeHelper.Initialize();

        // Do not repeat app initialization when the Window already has content,
        // just ensure that the window is active.
        if (Window.Current.Content is not Frame rootFrame)
        {
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Colors.Green;
            titleBar.InactiveBackgroundColor = Colors.Green;
            titleBar.InactiveForegroundColor = Color.FromArgb(255, 153, 208, 153);
            titleBar.ButtonBackgroundColor = Colors.Green;
            titleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 25, 149, 25);
            titleBar.ButtonPressedBackgroundColor = Colors.Green;
            titleBar.ButtonInactiveBackgroundColor = Colors.Green;
            titleBar.ButtonInactiveForegroundColor = Color.FromArgb(255, 153, 208, 153);

            // Create a Frame to act as the navigation context and navigate to the first page
            rootFrame = new Frame();
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += CoreDispatcher_AcceleratorKeyActivated;
            SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
            rootFrame.NavigationFailed += OnNavigationFailed;
            rootFrame.Navigated += RootFrame_Navigated;

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                // TODO: Load state from previously suspended application
            }

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
        }

        if (e.PrelaunchActivated == false)
        {
            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page, configuring
                // the new page by passing required information as a navigation parameter.
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }
    }

    private void RootFrame_Navigated(object sender, NavigationEventArgs e)
    {
        Frame? rootFrame = Window.Current.Content as Frame;
        UpdateBackButton(rootFrame);
    }

    /// <summary>
    /// Invoked when Navigation to a certain page fails.
    /// </summary>
    /// <param name="sender">The Frame which failed navigation.</param>
    /// <param name="e">Details about the navigation failure.</param>
    private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new Exception($"Failed to load page '{e.SourcePageType.FullName}'.");
    }

    /// <summary>
    /// Invoked when application execution is being suspended. Application state is saved
    /// without knowing whether the application will be terminated or resumed with the contents
    /// of memory still intact.
    /// </summary>
    /// <param name="sender">The source of the suspend request.</param>
    /// <param name="e">Details about the suspend request.</param>
    private void OnSuspending(object sender, SuspendingEventArgs e)
    {
        SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

        // TODO: Save application state and stop any background activity
        deferral.Complete();
    }

    public static TEnum GetEnum<TEnum>(string text) where TEnum : struct
    {
        if (!typeof(TEnum).GetTypeInfo().IsEnum)
        {
            throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
        }
        return (TEnum)Enum.Parse(typeof(TEnum), text);
    }

    public static bool TryGoBack()
    {
        Frame rootFrame = Window.Current.Content as Frame;
        if (rootFrame.CanGoBack)
        {
            rootFrame.GoBack(new SuppressNavigationTransitionInfo());
            return true;
        }
        return false;
    }

    private bool TryGoForward()
    {
        Frame rootFrame = Window.Current.Content as Frame;
        if (rootFrame.CanGoForward)
        {
            rootFrame.GoForward();
            return true;
        }
        return false;
    }

    private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
    {
        // When Alt+Left are pressed navigate back.
        // When Alt+Right are pressed navigate forward.
        if (e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown
            && (e.VirtualKey == VirtualKey.Left || e.VirtualKey == VirtualKey.Right)
            && e.KeyStatus.IsMenuKeyDown == true
            && !e.Handled)
        {
            if (e.VirtualKey == VirtualKey.Left)
            {
                e.Handled = TryGoBack();
            }
            else if (e.VirtualKey == VirtualKey.Right)
            {
                e.Handled = TryGoForward();
            }
        }
    }

    private void System_BackRequested(object sender, BackRequestedEventArgs e)
    {
        if (!e.Handled)
        {
            e.Handled = TryGoBack();
        }
    }

    private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
    {
        // For this event, e.Handled arrives as 'true'.
        if (e.CurrentPoint.Properties.IsXButton1Pressed)
        {
            e.Handled = !TryGoBack();
        }
        else if (e.CurrentPoint.Properties.IsXButton2Pressed)
        {
            e.Handled = !TryGoForward();
        }
    }

    public void UpdateBackButton(Frame frame)
    {
        bool canGoBack = (frame?.CanGoBack ?? false);

        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = canGoBack
            ? AppViewBackButtonVisibility.Visible
            : AppViewBackButtonVisibility.Collapsed;
    }
}
