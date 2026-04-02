namespace Orfograffiti;

public partial class achievements : ContentPage
{
	public achievements()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        int wins = Preferences.Get("wins",0);
        int loses = Preferences.Get("loses", 0);

        string theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        string lightColor = ThemeApp.ThemeDict[theme][1];
        string darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
        achieve1.Background = Color.FromArgb(lightColor);
        achieve2.Background = Color.FromArgb(lightColor);
        achieve3.Background = Color.FromArgb(lightColor);
        achieve4.Background = Color.FromArgb(lightColor);
        achieve5.Background = Color.FromArgb(lightColor);
        achieve6.Background = Color.FromArgb(lightColor);
        achieve7.Background = Color.FromArgb(lightColor);
        achieve8.Background = Color.FromArgb(lightColor);

        if (wins > 0) pic1.Source = "win1.png";
        if (wins > 9) pic2.Source = "win10.png";
        if (wins > 99) pic3.Source = "Win100.png";

        if (loses > 0) pic4.Source = "lose1.png";
        if (loses > 9) pic5.Source = "lose10.png";
        if (loses > 99) pic6.Source = "lose100.png";

        if (Preferences.Get("lastmoment", false)) pic7.Source = "lastmoment.png";
        if (Preferences.Get("fizhma", false)) pic8.Source = "fizhma.png";
    }
}