namespace Orfograffiti;

public partial class rules : ContentPage
{
	public rules()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        string lightColor = ThemeApp.ThemeDict[theme][1];
        string darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
        rule1.BackgroundColor = Color.FromArgb(lightColor);
        rule2.BackgroundColor = Color.FromArgb(lightColor);
        rule3.BackgroundColor = Color.FromArgb(lightColor);
    }
}