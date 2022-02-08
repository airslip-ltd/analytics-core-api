using System;

namespace Airslip.Analytics.Core.Extensions;

public static class CurrencyExtensions
{
    public static double ToPositiveCurrency(this long value)
    {
        if (value < 0) value = value * -1;
        
        return Convert.ToDouble(value) / 100;
    }
    
    public static double ToCurrency(this long value)
    {
        return Convert.ToDouble(value) / 100;
    }
    
    public static double ToCurrency(this long? value)
    {
        return Convert.ToDouble(value) / 100;
    }
}