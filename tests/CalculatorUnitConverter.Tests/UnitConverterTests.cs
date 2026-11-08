using CalculatorUnitConverter.Core;

namespace CalculatorUnitConverter.Tests;

public class UnitConverterTests
{
    private readonly UnitConverter _converter = new();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1.609344, 1)]
    [InlineData(10, 6.2137119224)]
    [InlineData(42.195, 26.2187574565)]
    public void KmToMiles_Converts(double km, double expectedMiles)
    {
        Assert.Equal(expectedMiles, _converter.KmToMiles(km), 8);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1.609344)]
    [InlineData(26.2, 42.1648128)]
    public void MilesToKm_Converts(double miles, double expectedKm)
    {
        Assert.Equal(expectedKm, _converter.MilesToKm(miles), 8);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(123.456)]
    public void KmToMiles_ThenBack_ReturnsOriginal(double km)
    {
        Assert.Equal(km, _converter.MilesToKm(_converter.KmToMiles(km)), 10);
    }

    [Fact]
    public void KmToMiles_Negative_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.KmToMiles(-1));
    }

    [Fact]
    public void MilesToKm_Negative_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.MilesToKm(-0.01));
    }

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(-40, -40)]
    [InlineData(37, 98.6)]
    [InlineData(-273.15, -459.67)]
    public void CelsiusToFahrenheit_Converts(double celsius, double expectedFahrenheit)
    {
        Assert.Equal(expectedFahrenheit, _converter.CelsiusToFahrenheit(celsius), 8);
    }

    [Theory]
    [InlineData(32, 0)]
    [InlineData(212, 100)]
    [InlineData(-40, -40)]
    [InlineData(98.6, 37)]
    [InlineData(-459.67, -273.15)]
    public void FahrenheitToCelsius_Converts(double fahrenheit, double expectedCelsius)
    {
        Assert.Equal(expectedCelsius, _converter.FahrenheitToCelsius(fahrenheit), 8);
    }

    [Fact]
    public void CelsiusToFahrenheit_BelowAbsoluteZero_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.CelsiusToFahrenheit(-273.16));
    }

    [Fact]
    public void FahrenheitToCelsius_BelowAbsoluteZero_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _converter.FahrenheitToCelsius(-460));
    }
}
