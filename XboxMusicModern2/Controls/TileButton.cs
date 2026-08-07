using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxMusic.Controls;

[TemplateVisualState(Name = NormalState, GroupName = CommonStates)]
[TemplateVisualState(Name = PressedState, GroupName = CommonStates)]
[TemplateVisualState(Name = UnfocusedState, GroupName = FocusStates)]
[TemplateVisualState(Name = FocusedState, GroupName = FocusStates)]
[TemplateVisualState(Name = ShowOnFocusState, GroupName = TextBackdropStates)]
[TemplateVisualState(Name = HidingState, GroupName = TextBackdropStates)]
[TemplateVisualState(Name = FullTileWidthState, GroupName = TextBackdropStates)]
[TemplateVisualState(Name = NoBackdropState, GroupName = TextBackdropStates)]

public sealed partial class TileButton : ButtonBase
{
    internal const string CommonStates = "CommonStates";
    internal const string NormalState = "Normal";
    internal const string PressedState = "Pressed";
    internal const string FocusStates = "FocusStates";
    internal const string UnfocusedState = "Unfocused";
    internal const string FocusedState = "Focused";
    internal const string TextBackdropStates = "TextBackdropStates";
    internal const string ShowOnFocusState = "ShowOnFocus";
    internal const string HidingState = "Hide";
    internal const string FullTileWidthState = "FullTileWidth";
    internal const string NoBackdropState = "NoBackdrop";

    public TileButton()
    {
        this.DefaultStyleKey = typeof(TileButton);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateTextBackdropState(true);
    }

    // DEPENDENCY PROPERTIES
    // Tile Text
    public static readonly DependencyProperty TileTextProperty = DependencyProperty.Register(nameof(TileText), typeof(string), typeof(TileButton), new PropertyMetadata(string.Empty));
    public string TileText
    {
        get => (string)GetValue(TileTextProperty);
        set => SetValue(TileTextProperty, value);
    }

    // Icon Glyph
    public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(nameof(Glyph), typeof(string), typeof(TileButton), new PropertyMetadata(string.Empty));
    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    // Icon Margin
    public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(TileButton), new PropertyMetadata(new Thickness(0, 0, 0, 0)));
    public Thickness IconMargin
    {
        get => (Thickness)GetValue(IconMarginProperty);
        set => SetValue(IconMarginProperty, value);
    }

    // Tile Text Padding
    public static readonly DependencyProperty TileTextPaddingProperty = DependencyProperty.Register(nameof(TileTextPadding), typeof(Thickness), typeof(TileButton), new PropertyMetadata(new Thickness(16, 10, 16, 12)));
    public Thickness TileTextPadding
    {
        get => (Thickness)GetValue(TileTextPaddingProperty);
        set => SetValue(TileTextPaddingProperty, value);
    }

    public static readonly DependencyProperty TextLineBoundsProperty = DependencyProperty.Register(nameof(TileTextLineBounds), typeof(TextLineBounds), typeof(TileButton), new PropertyMetadata(TextLineBounds.Full));
    public TextLineBounds TileTextLineBounds
    {
        get => (TextLineBounds)GetValue(TextLineBoundsProperty);
        set => SetValue(TextLineBoundsProperty, value);
    }

    // Icon FontSize
    public static readonly DependencyProperty IconFontSizeProperty = DependencyProperty.Register(nameof(IconFontSize), typeof(double), typeof(TileButton), new PropertyMetadata(24.0));
    public double IconFontSize
    {
        get => (double)GetValue(IconFontSizeProperty);
        set => SetValue(IconFontSizeProperty, value);
    }

    // Text Backdrop
    public enum TextBackdropStateEnum
    {
        ShowOnFocus,
        AlwaysShown,
        NoBackdrop,
        FullTileWidth
    }
    public static readonly DependencyProperty TextBackdropStateProperty = DependencyProperty.Register(nameof(TextBackdropState), typeof(TextBackdropStateEnum), typeof(TileButton), new PropertyMetadata(TextBackdropStateEnum.ShowOnFocus, OnTextBackdropChanged));
    public TextBackdropStateEnum TextBackdropState
    {
        get => (TextBackdropStateEnum)GetValue(TextBackdropStateProperty);
        set => SetValue(TextBackdropStateProperty, value);
    }

    // PROPERTY CHANGES
    // Text Backdrop changed
    private static void OnTextBackdropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((TileButton)d).UpdateTextBackdropState(true);
    }

    // CONTROL INTERACTION OVERRIDES
    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);
        VisualStateManager.GoToState(this, FocusedState, true);
        if (TextBackdropState != TextBackdropStateEnum.AlwaysShown)
        {
            VisualStateManager.GoToState(this, ShowOnFocusState, true);
        }
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        VisualStateManager.GoToState(this, UnfocusedState, true);
        VisualStateManager.GoToState(this, NormalState, true);
        if (TextBackdropState != TextBackdropStateEnum.AlwaysShown)
        {
            VisualStateManager.GoToState(this, HidingState, true);
        }
    }

    protected override void OnPreviewKeyDown(KeyRoutedEventArgs e)
    {
        var element = FocusManager.GetFocusedElement(XamlRoot);
        base.OnPreviewKeyDown(e);
        if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Space || e.Key == Windows.System.VirtualKey.GamepadA)
        {
            if (element is TileButton)
            {
                VisualStateManager.GoToState(this, PressedState, true);
            }
        }
    }

    protected override void OnPreviewKeyUp(KeyRoutedEventArgs e)
    {
        base.OnPreviewKeyUp(e);
        if (e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Space || e.Key == Windows.System.VirtualKey.GamepadA)
        {
            VisualStateManager.GoToState(this, NormalState, true);
        }
    }

    // MISCELLANEOUS (aka: i didn't know what to categorize these as)
    // Text Backdrop State Updater
    private void UpdateTextBackdropState(bool useTransitions)
    {
        var currentState = TextBackdropState.ToString();
        if (TextBackdropState != TextBackdropStateEnum.ShowOnFocus)
        {
            VisualStateManager.GoToState(this, currentState, useTransitions);
        }
    }
}
