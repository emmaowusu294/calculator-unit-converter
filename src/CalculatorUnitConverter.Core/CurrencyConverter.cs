namespace CalculatorUnitConverter.Core;

// Converts Ghana cedis to US dollars using a caller-supplied rate.
public class CurrencyConverter
{
    // Rate is GHS per 1 USD (e.g. 15.50 means 1 USD = 15.50 GHS); result is rounded to cents.
    public decimal GhsToUsd(decimal amountGhs, decimal ghsPerUsd)
    {
        if (ghsPerUsd <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ghsPerUsd), "Exchange rate must be greater than zero.");
        }

        if (amountGhs < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amountGhs), "Amount cannot be negative.");
        }

        return Math.Round(amountGhs / ghsPerUsd, 2, MidpointRounding.AwayFromZero);
    }
}
