using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace XboxMusic.Controls
{
    [ContentProperty(Name = nameof(SwitchCases))]
    public sealed partial class SwitchConverter : DependencyObject, IValueConverter
    {
        public CaseCollection SwitchCases
        {
            get { return (CaseCollection)GetValue(SwitchCasesProperty); }
            set { SetValue(SwitchCasesProperty, value); }
        }

        public static readonly DependencyProperty SwitchCasesProperty =
            DependencyProperty.Register(nameof(SwitchCases), typeof(CaseCollection), typeof(SwitchConverter), new PropertyMetadata(null));

        public Type TargetType
        {
            get { return (Type)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.Register(nameof(TargetType), typeof(Type), typeof(SwitchConverter), new PropertyMetadata(null));

        public SwitchConverter()
        {
            SwitchCases = new CaseCollection();
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = SwitchCases.EvaluateCases(value, TargetType ?? targetType);

            return result?.Content;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
