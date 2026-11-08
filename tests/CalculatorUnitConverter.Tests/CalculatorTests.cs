using CalculatorUnitConverter.Core;

namespace CalculatorUnitConverter.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-2, -3, -5)]
    [InlineData(-2, 3, 1)]
    [InlineData(0, 0, 0)]
    [InlineData(1.5, 2.25, 3.75)]
    public void Add_ReturnsSum(double a, double b, double expected)
    {
        Assert.Equal(expected, _calculator.Add(a, b), 10);
    }

    [Fact]
    public void Add_DecimalFractions_IsCloseToExpected()
    {
        // 0.1 + 0.2 is not exactly 0.3 in binary floating point.
        Assert.Equal(0.3, _calculator.Add(0.1, 0.2), 10);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(3, 5, -2)]
    [InlineData(-5, -3, -2)]
    [InlineData(0, 0, 0)]
    public void Subtract_ReturnsDifference(double a, double b, double expected)
    {
        Assert.Equal(expected, _calculator.Subtract(a, b), 10);
    }

    [Theory]
    [InlineData(4, 3, 12)]
    [InlineData(-4, 3, -12)]
    [InlineData(-4, -3, 12)]
    [InlineData(123.456, 0, 0)]
    [InlineData(0.5, 0.5, 0.25)]
    public void Multiply_ReturnsProduct(double a, double b, double expected)
    {
        Assert.Equal(expected, _calculator.Multiply(a, b), 10);
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(-9, 3, -3)]
    [InlineData(1, 4, 0.25)]
    [InlineData(0, 5, 0)]
    public void Divide_ReturnsQuotient(double a, double b, double expected)
    {
        Assert.Equal(expected, _calculator.Divide(a, b), 10);
    }

    [Fact]
    public void Divide_RepeatingDecimal_IsCloseToExpected()
    {
        Assert.Equal(1.0 / 3.0, _calculator.Divide(1, 3), 10);
    }

    [Theory]
    [InlineData(5, 0)]
    [InlineData(0, 0)]
    [InlineData(-5, -0.0)]
    public void Divide_ByZero_Throws(double a, double b)
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(a, b));
    }

    [Fact]
    public void Add_Overflow_Throws()
    {
        Assert.Throws<OverflowException>(() => _calculator.Add(double.MaxValue, double.MaxValue));
    }

    [Fact]
    public void Subtract_Overflow_Throws()
    {
        Assert.Throws<OverflowException>(() => _calculator.Subtract(-double.MaxValue, double.MaxValue));
    }

    [Fact]
    public void Multiply_Overflow_Throws()
    {
        Assert.Throws<OverflowException>(() => _calculator.Multiply(double.MaxValue, 2));
    }

    [Fact]
    public void Divide_Overflow_Throws()
    {
        Assert.Throws<OverflowException>(() => _calculator.Divide(double.MaxValue, 0.5));
    }
}
