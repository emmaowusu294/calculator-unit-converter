using CalculatorUnitConverter.Core;

namespace CalculatorUnitConverter.Tests;

public class CurrencyConverterTests
{
    private readonly CurrencyConverter _converter = new();

    [Fact]
    public void GhsToUsd_UsesGivenRate()
    {
        // 155 GHS at 15.5 GHS per USD = 10.00 USD
        Assert.Equal(10.00m, _converter.GhsToUsd(155m, 15.5m));
    }

    [Fact]
    public void GhsToUsd_RateZeroOrLess_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.GhsToUsd(100m, 0m));
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.GhsToUsd(100m, -1m));
    }
}
