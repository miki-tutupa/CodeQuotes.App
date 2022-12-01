namespace CodeQuotes;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadQuotes().ConfigureAwait(false);
    }

    Random random = new();

    private void BtnGenerateQuote_Clicked(object sender, EventArgs e)
    {
        var startColor = System.Drawing.Color.FromArgb(
            random.Next(0, 256),
            random.Next(0, 256),
            random.Next(0, 256));

        var endColor = System.Drawing.Color.FromArgb(
            random.Next(0, 256),
            random.Next(0, 256),
            random.Next(0, 256));

        var colors = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 6);

        float stopOffset = .0f;
        var stops = new GradientStopCollection();
        foreach (var color in colors)
        {
            stops.Add(new GradientStop(Color.FromArgb(color.Name), stopOffset));
            stopOffset += .2f;
        }

        var gradient = new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 1));

        Background.Background = gradient;

        int index = random.Next(0, quotes.Count);
        lblQuote.Text = quotes[index];
    }

    List<string> quotes = new();

    async Task LoadQuotes()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
        using var reader = new StreamReader(stream);

        while (reader.Peek() >= 0)
        {
            quotes.Add(await reader.ReadLineAsync());
        }
    }

}

