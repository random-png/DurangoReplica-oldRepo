using System;
using Windows.UI.Xaml.Markup;

namespace XboxMusic.Controls
{
    internal static partial class SwitchHelpers
    {
        internal static Case EvaluateCases(this CaseCollection switchCases, object value, Type targetType)
        {
            if (switchCases == null || switchCases.Count == 0)
            {
                return null;
            }

            Case xdefault = null;
            Case newcase = null;

            foreach (Case xcase in switchCases)
            {
                if (xcase.IsDefault)
                {
                    xdefault = xcase;
                    continue;
                }

                if (CompareValues(value, xcase.Value, targetType))
                {
                    newcase = xcase;
                    break;
                }
            }

            if (newcase == null && xdefault != null)
            {
                newcase = xdefault;
            }

            return newcase;
        }

        internal static bool CompareValues(object compare, object value, Type targetType)
        {
            if (compare == null || value == null)
            {
                return compare == value;
            }

            if (targetType == null ||
                (targetType == compare.GetType() &&
                 targetType == value.GetType()))
            {
                return compare.Equals(value);
            }
            else if (compare.GetType() == targetType)
            {
                var valueBase2 = ConvertValue(targetType, value);
                return compare.Equals(valueBase2);
            }

            var compareBase = ConvertValue(targetType, compare);
            var valueBase = ConvertValue(targetType, value);

            return compareBase.Equals(valueBase);
        }

        internal static object ConvertValue(Type targetType, object value)
        {
            if (targetType.IsInstanceOfType(value))
            {
                return value;
            }
            else if (targetType.IsEnum && value is string str)
            {
                object result;
                if (Enum.TryParse(targetType, str, out result))
                {
                    return result;
                }
                return ThrowExceptionForKeyNotFound();
            }
            else
            {
                return XamlBindingHelper.ConvertValue(targetType, value);
            }
        }

        private static object ThrowExceptionForKeyNotFound()
        {
            throw new InvalidOperationException("The requested enum value was not present in the provided type.");
        }
    }
}