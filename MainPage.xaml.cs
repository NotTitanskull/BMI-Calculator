using System.Globalization;

namespace MyFirstMauiApp;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        if (!TryReadPositiveNumber(WeightEntry.Text, out double weightInPounds) ||
            !TryReadPositiveNumber(HeightEntry.Text, out double heightInCentimeters))
        {
            ResultLabel.Text = "--";
            CategoryLabel.Text = "Please enter valid positive numbers.";
            CategoryLabel.TextColor = Colors.Red;
            SemanticScreenReader.Announce(CategoryLabel.Text);
            return;
        }

        double heightInInches = heightInCentimeters / 2.54;
        double bmi = weightInPounds * 703 / Math.Pow(heightInInches, 2);

        string category = bmi switch
        {
            < 18.5 => "Underweight",
            < 25 => "Healthy weight",
            < 30 => "Overweight",
            _ => "Obesity"
        };

        ResultLabel.Text = bmi.ToString("0.0", CultureInfo.CurrentCulture);
        CategoryLabel.Text = category;
        CategoryLabel.TextColor = Colors.Green;

        SemanticScreenReader.Announce($"Your BMI is {ResultLabel.Text}. Category: {category}.");
    }

    private void OnClearClicked(object? sender, EventArgs e)
    {
        WeightEntry.Text = string.Empty;
        HeightEntry.Text = string.Empty;
        ResultLabel.Text = "--";
        CategoryLabel.Text = "Enter your details above";
        CategoryLabel.ClearValue(Label.TextColorProperty);

        WeightEntry.Focus();
    }

    private static bool TryReadPositiveNumber(string? input, out double value)
    {
        return double.TryParse(
                   input,
                   NumberStyles.Float,
                   CultureInfo.CurrentCulture,
                   out value)
               && value > 0;
    }
}