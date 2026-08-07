using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Gaming.Input;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxSettings
{
    public sealed class SplitOpenControl : Control
    {
        private Popup _contentPopup;
        public SplitOpenControl()
        {
            this.DefaultStyleKey = typeof(SplitOpenControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentPopup = GetTemplateChild("contentPopup") as Popup;
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            this.CapturePointer(e.Pointer);
            _contentPopup.IsOpen = true;
            VisualStateManager.GoToState(this, "Open", true);
            ElementSoundPlayer.Play(ElementSoundKind.Show);
        }

        protected override void OnPointerReleased(PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Closed", true);
            this.ReleasePointerCapture(e.Pointer);
            ElementSoundPlayer.Play(ElementSoundKind.Hide);
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            ElementSoundPlayer.Play(ElementSoundKind.Focus);
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            ElementSoundPlayer.Play(ElementSoundKind.Focus);
        }
    }
}
