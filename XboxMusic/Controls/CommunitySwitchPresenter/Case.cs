using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxMusic.Controls
{
    [ContentProperty(Name = nameof(Content))]
    public partial class Case : DependencyObject
    {
        public object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(Case), new PropertyMetadata(null));

        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }

        public static readonly DependencyProperty IsDefaultProperty =
            DependencyProperty.Register(nameof(IsDefault), typeof(bool), typeof(Case), new PropertyMetadata(false));

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(object), typeof(Case), new PropertyMetadata(null));

        public Case()
        {
        }
    }
}
