using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Media.Audio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxMusic.Controls
{
    [TemplateVisualState(Name = UnfocusedState, GroupName = FocusStates)]
    [TemplateVisualState(Name = FocusedState, GroupName = FocusStates)]
    [TemplateVisualState(Name = FocusedPressedState, GroupName = FocusStates)]

    public sealed class TileButton : ButtonBase
    {
        internal const string FocusStates = "FocusStates";
        internal const string UnfocusedState = "Unfocused";
        internal const string FocusedState = "Focused";
        internal const string FocusedPressedState = "FocusedPressed";

        public static readonly DependencyProperty TileTextProperty = DependencyProperty.Register(nameof(TileText), typeof(string), typeof(TileButton), new PropertyMetadata(string.Empty));
        public string TileText
        {
            get => (string)GetValue(TileTextProperty);
            set => SetValue(TileTextProperty, value);
        }

        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(TileButton), new PropertyMetadata(new Thickness(0, 0, 0, 0)));
        public Thickness IconMargin
        {
            get => (Thickness)GetValue(IconMarginProperty);
            set => SetValue(IconMarginProperty, value);
        }

        // Crashes if the double isn't set
        public static readonly DependencyProperty IconFontSizeProperty = DependencyProperty.Register(nameof(IconFontSize), typeof(double), typeof(TileButton), new PropertyMetadata(24.0));
        public double IconFontSize
        {
            get => (double)GetValue(IconFontSizeProperty);
            set => SetValue(IconFontSizeProperty, value);
        }

        public TileButton()
        {
            this.DefaultStyleKey = typeof(TileButton);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            VisualStateManager.GoToState(this, FocusedState, true);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            VisualStateManager.GoToState(this, UnfocusedState, true);
        }

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            base.OnKeyDown(e);
            VisualStateManager.GoToState(this, FocusedPressedState, true);
        }

        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {
            base.OnKeyUp(e);
            VisualStateManager.GoToState(this, FocusedState, true);
        }
    }
}
