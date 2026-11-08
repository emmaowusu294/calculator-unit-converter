using System.Globalization;
using CalculatorUnitConverter.Core;

namespace CalculatorUnitConverter.Tests;

public class CurrencyConverterTests
{
    private readonly CurrencyConverter _converter = new();

    // Parses test data the same way the app does, independent of machine culture.
    private static decimal Dec(string value) => decimal.Parse(value, CultureInfo.InvariantCulture);

    [Theory]
    [InlineData("155", "15.5", "10.00")]
    [InlineData("100", "12", "8.33")]
    [InlineData("0", "15", "0.00")]
    [InlineData("1", "1", "1.00")]
    [InlineData("50", "0.5", "100.00")]
    public void GhsToUsd_Converts(string amount, string rate, string expected)
    {
        // Strings are used because C# attributes cannot hold decimal literals.
        decimal result = _converter.GhsToUsd(Dec(amount), Dec(rate));
        Assert.Equal(Dec(expected), result);
    }

    [Fact]
    public void GhsToUsd_RoundsToTwoDecimalPlaces()
    {
        // 10 / 3 = 3.333... -> 3.33
        Assert.Equal(3.33m, _converter.GhsToUsd(10m, 3m));
    }

    [Fact]
    public void GhsToUsd_MidpointRoundsAwayFromZero()
    {
        // 0.125 / 1 = 0.125 -> 0.13 (banker's rounding would give 0.12)
        Assert.Equal(0.13m, _converter.GhsToUsd(0.125m, 1m));
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("-0.0001")]
    public void GhsToUsd_RateNotPositive_Throws(string rate)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.GhsToUsd(100m, Dec(rate)));
    }

    [Fact]
    public void GhsToUsd_NegativeAmount_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.GhsToUsd(-5m, 15m));
    }

    [Fact]
    public void GhsToUsd_ResultTooLarge_Throws()
    {
        Assert.Throws<OverflowException>(() => _converter.GhsToUsd(decimal.MaxValue, 0.5m));
    }
}
