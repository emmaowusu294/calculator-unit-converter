namespace CalculatorUnitConverter.Core;

// Basic arithmetic on doubles.
public class Calculator
{
    public double Add(double a, double b) => EnsureFinite(a + b);

    public double Subtract(double a, double b) => EnsureFinite(a - b);

    public double Multiply(double a, double b) => EnsureFinite(a * b);

    public double Divide(double a, double b)
    {
        // Doubles return Infinity on /0, so we reject it explicitly.
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }

        return EnsureFinite(a / b);
    }

    // Results beyond double range become Infinity; treat that as an error.
    private static double EnsureFinite(double result)
    {
        if (!double.IsFinite(result))
        {
            throw new OverflowException("Result is too large to represent.");
        }

        return result;
    }
}
