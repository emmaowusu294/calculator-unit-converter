using System.Globalization;
using CalculatorUnitConverter.Core;

var calculator = new Calculator();
var units = new UnitConverter();
var currency = new CurrencyConverter();

while (true)
{
    PrintMenu();
    string choice = ReadLineOrExit("Choose an option: ").Trim();

    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
        break;
    }

    try
    {
        switch (choice)
        {
            case "1": RunArithmetic("+", calculator.Add); break;
            case "2": RunArithmetic("-", calculator.Subtract); break;
            case "3": RunArithmetic("*", calculator.Multiply); break;
            case "4": RunArithmetic("/", calculator.Divide); break;
            case "5": RunUnit("km", "miles", units.KmToMiles); break;
            case "6": RunUnit("miles", "km", units.MilesToKm); break;
            case "7": RunUnit("C", "F", units.CelsiusToFahrenheit); break;
            case "8": RunUnit("F", "C", units.FahrenheitToCelsius); break;
            case "9": RunCurrency(); break;
            default: Console.WriteLine("Invalid choice. Please enter a number from 0 to 9."); break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {CleanMessage(ex)}");
    }
    catch (ArithmeticException ex)
    {
        // Covers DivideByZeroException and OverflowException.
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine();
}

void PrintMenu()
{
    Console.WriteLine("=== Calculator & Unit Converter ===");
    Console.WriteLine("1. Add");
    Console.WriteLine("2. Subtract");
    Console.WriteLine("3. Multiply");
    Console.WriteLine("4. Divide");
    Console.WriteLine("5. Km -> Miles");
    Console.WriteLine("6. Miles -> Km");
    Console.WriteLine("7. Celsius -> Fahrenheit");
    Console.WriteLine("8. Fahrenheit -> Celsius");
    Console.WriteLine("9. GHS -> USD");
    Console.WriteLine("0. Exit");
}

void RunArithmetic(string symbol, Func<double, double, double> operation)
{
    double a = ReadDouble("First number: ");
    double b = ReadDouble("Second number: ");
    Console.WriteLine($"Result: {Format(a)} {symbol} {Format(b)} = {Format(operation(a, b))}");
}

void RunUnit(string fromUnit, string toUnit, Func<double, double> convert)
{
    double value = ReadDouble($"Value in {fromUnit}: ");
    Console.WriteLine($"Result: {Format(value)} {fromUnit} = {Format(convert(value))} {toUnit}");
}

void RunCurrency()
{
    decimal amount = ReadDecimal("Amount in GHS: ");
    decimal rate = ReadDecimal("Rate (GHS per 1 USD): ");
    decimal usd = currency.GhsToUsd(amount, rate);
    Console.WriteLine($"Result: {amount.ToString(CultureInfo.InvariantCulture)} GHS = {usd.ToString("F2", CultureInfo.InvariantCulture)} USD");
}

// Re-prompts until the user types a valid finite number.
double ReadDouble(string prompt)
{
    while (true)
    {
        string input = ReadLineOrExit(prompt);
        if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double value) && double.IsFinite(value))
        {
            return value;
        }

        Console.WriteLine("Invalid number. Use digits with '.' as the decimal separator (e.g. 3.5).");
    }
}

// Re-prompts until the user types a valid decimal.
decimal ReadDecimal(string prompt)
{
    while (true)
    {
        string input = ReadLineOrExit(prompt);
        if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal value))
        {
            return value;
        }

        Console.WriteLine("Invalid amount. Use digits with '.' as the decimal separator (e.g. 100.50).");
    }
}

// Exits cleanly if input ends (e.g. Ctrl+Z / Ctrl+D or piped input runs out).
string ReadLineOrExit(string prompt)
{
    Console.Write(prompt);
    string? line = Console.ReadLine();
    if (line is null)
    {
        Console.WriteLine();
        Console.WriteLine("Input closed. Goodbye!");
        Environment.Exit(0);
    }

    return line;
}

// G10 trims float noise (0.1 + 0.2 shows 0.3) and uses E-notation for huge values.
string Format(double value) => value.ToString("G10", CultureInfo.InvariantCulture);

// Drops the " (Parameter 'x')" suffix .NET adds to argument exception messages.
string CleanMessage(ArgumentException ex) =>
    ex.ParamName is null ? ex.Message : ex.Message.Replace($" (Parameter '{ex.ParamName}')", "");
