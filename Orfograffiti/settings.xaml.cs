namespace Orfograffiti;

public partial class settings : ContentPage
{
    bool initialized = false;
	public settings()
	{
		InitializeComponent();
        vibroCheck.IsChecked = Preferences.Get("vibro", false);

#if WINDOWS
        platform.Text += " Windows";
#elif ANDROID
        platform.Text += " Android";
#elif IOS
        platform.Text += " IOS";
#elif MACCATALYST
        platform.Text += " MacOs";
#endif
        initialized = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        string lightColor = ThemeApp.ThemeDict[theme][1];
        string darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
        bor1.BackgroundColor = Color.FromArgb(lightColor);
        bor2.BackgroundColor = Color.FromArgb(lightColor);
    }

    private void vibroCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (initialized) Preferences.Set("vibro", vibroCheck.IsChecked);
#if ANDROID || IOS
        if(Preferences.Get("vibro", false)) Vibration.Vibrate();
#endif
    }
}