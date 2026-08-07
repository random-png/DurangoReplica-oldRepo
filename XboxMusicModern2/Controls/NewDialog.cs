using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxMusic.Controls;

public sealed partial class NewDialog : Control
{
    public NewDialog()
    {
        this.DefaultStyleKey = typeof(NewDialog);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        StretchPopupToAppBounds();
    }

    private void StretchPopupToAppBounds()
    {
        if (GetTemplateChild("RootGrid") is Grid rootGrid)
        {
            rootGrid.Width = ApplicationView.GetForCurrentView().VisibleBounds.Width;
            rootGrid.Height = ApplicationView.GetForCurrentView().VisibleBounds.Height;
        }
    }
}
