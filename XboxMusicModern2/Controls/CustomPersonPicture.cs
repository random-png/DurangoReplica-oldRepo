using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxMusic.Controls;

[TemplateVisualState(Name = NormalState, GroupName = IconStates)]
[TemplateVisualState(Name = ModernState, GroupName = IconStates)]
[TemplateVisualState(Name = ClassicState, GroupName = IconStates)]

public sealed partial class CustomPersonPicture : Control
{
    internal const string IconStates = "IconStates";
    internal const string NormalState = "Normal";
    internal const string ModernState = "Modern";
    internal const string ClassicState = "Classic";

    public CustomPersonPicture()
    {
        this.DefaultStyleKey = typeof(CustomPersonPicture);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateVisualState(true);
    }

    // DEPENDENCY PROPERTIES
    // Profile Picture
    public static readonly DependencyProperty ProfilePictureProperty = DependencyProperty.Register(nameof(ProfilePicture), typeof(ImageSource), typeof(CustomPersonPicture), new PropertyMetadata(null));
    public ImageSource ProfilePicture
    {
        get => (ImageSource)GetValue(ProfilePictureProperty);
        set => SetValue(ProfilePictureProperty, value);
    }

    // Icon Style
    public enum IconStyleEnum
    {
        Normal,
        Modern,
        Classic
    }
    public static readonly DependencyProperty IconStyleProperty = DependencyProperty.Register(nameof(IconStyle), typeof(IconStyleEnum), typeof(CustomPersonPicture), new PropertyMetadata(defaultValue: IconStyleEnum.Normal, OnIconStateChanged));
    public IconStyleEnum IconStyle
    {
        get => (IconStyleEnum)GetValue(IconStyleProperty);
        set => SetValue(IconStyleProperty, value);
    }

    // PROPERTY CHANGES
    // Icon Style changed
    private static void OnIconStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is CustomPersonPicture control)
        {
            control.UpdateVisualState(useTransitions: true);
        }
    }

    // MISCELLANEOUS (aka: i didn't know what to categorize these as)
    // Visual State Updater
    private void UpdateVisualState(bool useTransitions)
    {
        switch (IconStyle)
        {
            case IconStyleEnum.Modern:
                VisualStateManager.GoToState(this, ModernState, useTransitions);
                break;
            case IconStyleEnum.Classic:
                VisualStateManager.GoToState(this, ClassicState, useTransitions);
                break;
            default:
                VisualStateManager.GoToState(this, NormalState, useTransitions);
                break;
        }
    }
}
