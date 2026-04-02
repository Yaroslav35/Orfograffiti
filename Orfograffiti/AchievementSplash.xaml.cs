namespace Orfograffiti;

public partial class AchievementSplash : ContentPage
{
    string name = "";
    string pic = "";
    string cond = "";
	public AchievementSplash(string name_get, string pic_get, string cond_get)
	{
        name = name_get;
        pic = pic_get;
        cond = cond_get;
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
        condbor.BackgroundColor = Color.FromArgb(lightColor);

        title.Text = name;
        subtitle2.Text = cond;

        Anim();
    }

    private async void Anim()
    {
        title.Opacity = 0;
        ultratitle.Opacity = 0;
        condbor.Opacity = 0;
        img.Source = "unknown.png";
        for (float i = 181; i <= 361; i += 5)
        {
            container.RotationY = i;
            if (Math.Abs(i - 271) < 0.1)
            {
                img.Source = pic;
            }
            await Task.Delay(10);
        }

        await Task.Delay(500);

        float tr = 0;
        while (tr < 1.0f)
        {
            title.Opacity = tr;
            ultratitle.Opacity = tr;
            condbor.Opacity = tr;
            tr += 0.1f;
            await Task.Delay(20);
        }
        title.SetAppThemeColor(Label.TextColorProperty, Colors.Black, Colors.White);
        ultratitle.SetAppThemeColor(Label.TextColorProperty, Colors.Black, Colors.White);
        subtitle2.SetAppThemeColor(Label.TextColorProperty, Colors.Black, Colors.White);

        await Task.Delay(2000);
        Preferences.Set("allow_splash", true);
        await Navigation.PopModalAsync();
    }
}