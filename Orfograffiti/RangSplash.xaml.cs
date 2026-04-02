namespace Orfograffiti;

public partial class RangSplash : ContentPage
{
	string rangname = "";
	string startimg = "", endimg = "";
	int exp = 0;
    Dictionary<string, string> turn = new Dictionary<string, string>()
	{
		{"wood", "iron"},
		{"iron", "copper"},
		{"copper", "silver"},
		{"silver", "gold"},
		{"gold", "emerald"},
		{"emerald", "rubin"},
		{"rubin", "neon"}
	};
	Dictionary<string, string> titles = new Dictionary<string, string>()
	{
		{"wood", "Железный ранг!"},
        {"iron", "Бронзовый ранг!"},
        {"copper", "Серебряный ранг!"},
        {"silver", "Золотой ранг!"},
        {"gold", "Изумрудный ранг!"},
        {"emerald", "Рубиновый ранг!"},
        {"rubin", "Неоновый ранг!"}
    };
	public RangSplash(string rangname_get)
	{
		InitializeComponent();

		rangname = rangname_get;

		startimg = $"{rangname}trophy.png";
		endimg = $"{turn[rangname]}trophy.png";
		exp = Preferences.Get("exp", 0);

		title.Text = titles[rangname];
		subtitle2.Text = $"Текущий опыт: {exp}";

		Anim();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        string theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        string lightColor = ThemeApp.ThemeDict[theme][1];
        string darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
    }
    private async void Anim()
	{
		title.Opacity = 0;
		ultratitle.Opacity = 0;
		subtitle2.Opacity = 0;
		img.Source = startimg;
        for (float i = 181; i <= 361; i += 5)
        {
            img.RotationY = i;
            if (Math.Abs(i - 271) < 0.1)
            {
                img.Source = endimg;
            }
            await Task.Delay(10);
        }

		await Task.Delay(500);

		float tr = 0;
		while(tr < 1.0f)
		{
            title.Opacity = tr;
            ultratitle.Opacity = tr;
            subtitle2.Opacity = tr;
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