namespace CalculatorUnitConverter.Core;

// Distance and temperature conversions on doubles.
public class UnitConverter
{
    // Exact international definition of a mile in kilometres.
    public const double KmPerMile = 1.609344;

    public const double AbsoluteZeroCelsius = -273.15;
    public const double AbsoluteZeroFahrenheit = -459.67;

    public double KmToMiles(double km)
    {
        EnsureNonNegativeDistance(km);
        return km / KmPerMile;
    }

    public double MilesToKm(double miles)
    {
        EnsureNonNegativeDistance(miles);
        return miles * KmPerMile;
    }

    public double CelsiusToFahrenheit(double celsius)
    {
        if (celsius < AbsoluteZeroCelsius)
        {
            throw new ArgumentOutOfRangeException(nameof(celsius), "Temperature cannot be below absolute zero (-273.15 C).");
        }

        return celsius * 9 / 5 + 32;
    }

    public double FahrenheitToCelsius(double fahrenheit)
    {
        if (fahrenheit < AbsoluteZeroFahrenheit)
        {
            throw new ArgumentOutOfRangeException(nameof(fahrenheit), "Temperature cannot be below absolute zero (-459.67 F).");
        }

        return (fahrenheit - 32) * 5 / 9;
    }

    private static void EnsureNonNegativeDistance(double value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Distance cannot be negative.");
        }
    }
}
