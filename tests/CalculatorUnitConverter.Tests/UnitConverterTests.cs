using CalculatorUnitConverter.Core;

namespace CalculatorUnitConverter.Tests;

public class UnitConverterTests
{
    private readonly UnitConverter _converter = new();

    [Fact]
    public void KmToMiles_ConvertsCorrectly()
    {
        // 10 km / 1.609344 = 6.2137119224 miles
        Assert.Equal(6.2137119224, _converter.KmToMiles(10), 8);
    }

    [Fact]
    public void CelsiusToFahrenheit_ConvertsCorrectly()
    {
        Assert.Equal(212, _converter.CelsiusToFahrenheit(100), 8);
    }
}
