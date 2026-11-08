# Calculator & Unit Converter

A C# .NET 10 console app that does basic arithmetic and a few common conversions:

- Add, subtract, multiply, divide
- Kilometres <-> miles
- Celsius <-> Fahrenheit
- Ghana cedis (GHS) -> US dollars (USD), using an exchange rate you type in

Bad input never crashes the app. Invalid numbers are re-prompted, invalid menu choices are reported, and errors such as dividing by zero or a rate of 0 or less are shown as messages.

## Requirements

- .NET 10 SDK (`dotnet --version` should print 10.x)

## How to run

From the repository root:

```bash
dotnet run --project src/CalculatorUnitConverter.App
```

Pick an option from the menu, then type the numbers when prompted. Use `.` as the decimal separator (e.g. `3.5`). Enter `0` to exit.

Example session:

```
=== Calculator & Unit Converter ===
1. Add
...
9. GHS -> USD
0. Exit
Choose an option: 9
Amount in GHS: 155
Rate (GHS per 1 USD): 15.5
Result: 155 GHS = 10.00 USD
```

## How to test

```bash
dotnet build
dotnet test
```

The xUnit tests in `tests/CalculatorUnitConverter.Tests` cover every public method in Core. They include normal cases, edge cases (zero, negatives, -40 where C and F meet, absolute zero, round trips), and error cases (divide by zero, overflow, rate <= 0, negative amounts and distances, midpoint rounding).

## Project structure

```
CalculatorUnitConverter.sln
src/
  CalculatorUnitConverter.Core/      # Pure logic, no Console calls
    Calculator.cs                    # Add, Subtract, Multiply, Divide (double)
    UnitConverter.cs                 # km <-> miles, C <-> F (double)
    CurrencyConverter.cs             # GHS -> USD (decimal)
  CalculatorUnitConverter.App/       # Console UI only
    Program.cs                       # Menu, input parsing, error messages
tests/
  CalculatorUnitConverter.Tests/     # xUnit tests for all Core classes
```

### Why it's split this way

- **Core has no Console calls**, so its logic can be tested by calling methods and checking return values. Nothing needs to simulate typing.
- **The App only handles input and output.** It reads text, parses it, calls Core, and prints the result or error. Changing the UI (to a web or GUI app, say) would not change Core.
- **One class per job.** `Calculator`, `UnitConverter` and `CurrencyConverter` each do one thing, so each is small and easy to read.
- **Core reports problems by throwing exceptions** (`DivideByZeroException`, `ArgumentOutOfRangeException`, `OverflowException`). The App decides how to show them to the user.

## Assumptions

- **The exchange rate is GHS per 1 USD.** For example, `15.5` means 1 USD = 15.50 GHS, which is how the rate is usually quoted in Ghana. USD = GHS / rate.
- **USD results are rounded to 2 decimal places** with midpoint-away-from-zero rounding (0.125 -> 0.13), not banker's rounding.
- **`decimal` is used for currency** to avoid binary rounding errors with money. **`double` is used for units and arithmetic**, where tiny floating-point differences don't matter.
- **Numbers are parsed with `CultureInfo.InvariantCulture`**, so `.` is always the decimal separator whatever the machine's region settings are. `1,5` is rejected rather than read as 1.5.
- **Distances can't be negative** and **temperatures can't be below absolute zero** (-273.15 C / -459.67 F). Negative GHS amounts are also rejected.
- The mile is the international mile: exactly 1.609344 km.
- `NaN` and `Infinity` are not accepted as input. Arithmetic results too large for a `double` are reported as an overflow error instead of printing `Infinity`.

## Known limitations

- The exchange rate is not fetched live. It is typed in every time and not saved.
- Only GHS -> USD is supported, not USD -> GHS or other currencies.
- Each calculation takes two numbers. There is no expression parsing (e.g. `2 + 3 * 4`), no chaining from the previous result, and no history.
- Results are shown with up to 10 significant digits, so very precise `double` results are rounded for display. Very large or small values use E-notation.
- Thousands separators are allowed for GHS amounts (`1,000.50`) but not for other numbers.
- The UI is English-only, and the console app itself (Program.cs) has no automated tests. It was checked by hand.
- If input ends (Ctrl+Z / Ctrl+D, or piped input runs out), the app prints a goodbye message and exits.
