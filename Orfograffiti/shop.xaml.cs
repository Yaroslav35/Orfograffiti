namespace Orfograffiti;

public partial class shop : ContentPage
{
	int bucoins = 0;
	public shop()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        check();
    }

	private void check()
	{
        bucoins = Preferences.Get("bucoins", 0);
        bucoinslbl.Text = bucoins.ToString();

        string fontsstr = Preferences.Get("fonts", "classic;");
        string selected = Preferences.Get("selected_font", "classic");
        string themesstr = Preferences.Get("themes", "white;");
        string theme = Preferences.Get("theme", "white");
        List<string> fonts = fontsstr.Split(';').ToList();
        List<string> themes = themesstr.Split(";").ToList();
        fonts.RemoveAt(fonts.Count - 1);
        themes.RemoveAt(themes.Count - 1);

        if (fonts.Contains("classic"))
        {
            if (selected == "classic")
            {
                classicstate.Text = "Выбрано";
            }
            else
            {
                classicstate.Text = "Не выбрано";
            }
        }

        if (fonts.Contains("impact"))
        {
            if (selected == "impact")
            {
                impactstate.Text = "Выбрано";
                impactcoin.IsVisible = false;
            }
            else
            {
                impactstate.Text = "Не выбрано";
                impactcoin.IsVisible = false;
            }
        }
        else
        {
            impactcoin.IsVisible = true;
            impactstate.Text = "1000";
        }

        if (fonts.Contains("timesnewroman"))
        {
            if (selected == "timesnewroman")
            {
                timesnewromanstate.Text = "Выбрано";
                timesnewromancoin.IsVisible = false;
            }
            else
            {
                timesnewromanstate.Text = "Не выбрано";
                timesnewromancoin.IsVisible = false;
            }
        }
        else
        {
            timesnewromancoin.IsVisible = true;
            timesnewromanstate.Text = "1000";
        }

        if (fonts.Contains("comicsans"))
        {
            if (selected == "comicsans")
            {
                comicsansstate.Text = "Выбрано";
                comicsanscoin.IsVisible = false;
            }
            else
            {
                comicsansstate.Text = "Не выбрано";
                comicsanscoin.IsVisible = false;
            }
        }
        else
        {
            comicsanscoin.IsVisible = true;
            comicsansstate.Text = "1000";
        }

        if (fonts.Contains("pixel"))
        {
            if (selected == "pixel")
            {
                pixelstate.Text = "Выбрано";
                pixelcoin.IsVisible = false;
            }
            else
            {
                pixelstate.Text = "Не выбрано";
                pixelcoin.IsVisible = false;
            }
        }
        else
        {
            pixelcoin.IsVisible = true;
            pixelstate.Text = "1000";
        }

        if (themes.Contains("white"))
        {
            if (theme == "white")
            {
                whitestate.Text = "Выбрано";
            }
            else
            {
                whitestate.Text = "Не выбрано";
            }
        }
        else
        {
            whitestate.Text = "1000";
        }

        if (themes.Contains("blue"))
        {
            if (theme == "blue")
            {
                bluestate.Text = "Выбрано";
                bluecoin.IsVisible = false;
            }
            else
            {
                bluestate.Text = "Не выбрано";
                bluecoin.IsVisible = false;
            }
        }
        else
        {
            bluecoin.IsVisible = true;
            bluestate.Text = "1000";
        }

        if (themes.Contains("red"))
        {
            if (theme == "red")
            {
                redstate.Text = "Выбрано";
                redcoin.IsVisible = false;
            }
            else
            {
                redstate.Text = "Не выбрано";
                redcoin.IsVisible = false;
            }
        }
        else
        {
            redcoin.IsVisible = true;
            redstate.Text = "1000";
        }

        if (themes.Contains("yellow"))
        {
            if (theme == "yellow")
            {
                yellowstate.Text = "Выбрано";
                yellowcoin.IsVisible = false;
            }
            else
            {
                yellowstate.Text = "Не выбрано";
                yellowcoin.IsVisible = false;
            }
        }
        else
        {
            yellowcoin.IsVisible = true;
            yellowstate.Text = "1000";
        }

        if (themes.Contains("green"))
        {
            if (theme == "green")
            {
                greenstate.Text = "Выбрано";
                greencoin.IsVisible = false;
            }
            else
            {
                greenstate.Text = "Не выбрано";
                greencoin.IsVisible = false;
            }
        }
        else
        {
            greencoin.IsVisible = true;
            greenstate.Text = "1000";
        }

        theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        string lightColor = ThemeApp.ThemeDict[theme][1];
        string darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
        font1.BackgroundColor = Color.FromArgb(darkColor);
        font2.BackgroundColor = Color.FromArgb(darkColor);
        font3.BackgroundColor = Color.FromArgb(darkColor);
        font4.BackgroundColor = Color.FromArgb(darkColor);
        font5.BackgroundColor = Color.FromArgb(darkColor);
    }

    private void classicTapped(object sender, EventArgs e)
    {
        string selected = Preferences.Get("selected_font", "classic");

        if (selected != "classic")
        {
            Preferences.Set("selected_font", "classic");
        }

        check();
    }
    private void impactTapped(object sender, EventArgs e)
    {
        string fontsstr = Preferences.Get("fonts", "classic;");
        int bucoins = Preferences.Get("bucoins",0);
        string selected = Preferences.Get("selected_font", "classic");
        List<string> fonts = fontsstr.Split(';').ToList();
        fonts.RemoveAt(fonts.Count - 1);

        if(!fonts.Contains("impact"))
        {
            if(bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                fontsstr += "impact;";
                Preferences.Set("fonts", fontsstr);
                Preferences.Set("selected_font", "impact");
            }
        }
        else
        {
            if(selected != "impact")
            {
                Preferences.Set("selected_font", "impact");
            }
        }

        check();
    }
    private void timesnewromanTapped(object sender, EventArgs e)
    {
        string fontsstr = Preferences.Get("fonts", "classic;");
        int bucoins = Preferences.Get("bucoins", 0);
        string selected = Preferences.Get("selected_font", "classic");
        List<string> fonts = fontsstr.Split(';').ToList();
        fonts.RemoveAt(fonts.Count - 1);

        if (!fonts.Contains("timesnewroman"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                fontsstr += "timesnewroman;";
                Preferences.Set("fonts", fontsstr);
                Preferences.Set("selected_font", "timesnewroman");
            }
        }
        else
        {
            if (selected != "timesnewroman")
            {
                Preferences.Set("selected_font", "timesnewroman");
            }
        }

        check();
    }
    private void comicsansTapped(object sender, EventArgs e)
    {
        string fontsstr = Preferences.Get("fonts", "classic;");
        int bucoins = Preferences.Get("bucoins", 0);
        string selected = Preferences.Get("selected_font", "classic");
        List<string> fonts = fontsstr.Split(';').ToList();
        fonts.RemoveAt(fonts.Count - 1);

        if (!fonts.Contains("comicsans"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                fontsstr += "comicsans;";
                Preferences.Set("fonts", fontsstr);
                Preferences.Set("selected_font", "comicsans");
            }
        }
        else
        {
            if (selected != "comicsans")
            {
                Preferences.Set("selected_font", "comicsans");
            }
        }

        check();
    }
    private void pixelTapped(object sender, EventArgs e)
    {
        string fontsstr = Preferences.Get("fonts", "classic;");
        int bucoins = Preferences.Get("bucoins", 0);
        string selected = Preferences.Get("selected_font", "classic");
        List<string> fonts = fontsstr.Split(';').ToList();
        fonts.RemoveAt(fonts.Count - 1);

        if (!fonts.Contains("pixel"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                fontsstr += "pixel;";
                Preferences.Set("fonts", fontsstr);
                Preferences.Set("selected_font", "pixel");
            }
        }
        else
        {
            if (selected != "pixel")
            {
                Preferences.Set("selected_font", "pixel");
            }
        }

        check();
    }

    private void whiteTapped(object sender, EventArgs e)
    {
        string theme = Preferences.Get("theme", "white");

        if (theme != "classic")
        {
            Preferences.Set("theme", "white");
        }

        check();
    }
    private void blueTapped(object sender, EventArgs e)
    {
        string themesstr = Preferences.Get("themes", "white;");
        int bucoins = Preferences.Get("bucoins", 0);
        string theme = Preferences.Get("theme", "white");
        List<string> themes = themesstr.Split(';').ToList();
        themes.RemoveAt(themes.Count - 1);

        if (!themes.Contains("blue"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                themesstr += "blue;";
                Preferences.Set("themes", themesstr);
                Preferences.Set("theme", "blue");
            }
        }
        else
        {
            if (theme != "blue")
            {
                Preferences.Set("theme", "blue");
            }
        }

        check();
    }
    private void redTapped(object sender, EventArgs e)
    {
        string themesstr = Preferences.Get("themes", "white;");
        int bucoins = Preferences.Get("bucoins", 0);
        string theme = Preferences.Get("theme", "white");
        List<string> themes = themesstr.Split(';').ToList();
        themes.RemoveAt(themes.Count - 1);

        if (!themes.Contains("red"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                themesstr += "red;";
                Preferences.Set("themes", themesstr);
                Preferences.Set("theme", "red");
            }
        }
        else
        {
            if (theme != "red")
            {
                Preferences.Set("theme", "red");
            }
        }

        check();
    }
    private void yellowTapped(object sender, EventArgs e)
    {
        string themesstr = Preferences.Get("themes", "white;");
        int bucoins = Preferences.Get("bucoins", 0);
        string theme = Preferences.Get("theme", "white");
        List<string> themes = themesstr.Split(';').ToList();
        themes.RemoveAt(themes.Count - 1);

        if (!themes.Contains("yellow"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                themesstr += "yellow;";
                Preferences.Set("themes", themesstr);
                Preferences.Set("theme", "yellow");
            }
        }
        else
        {
            if (theme != "yellow")
            {
                Preferences.Set("theme", "yellow");
            }
        }

        check();
    }
    private void greenTapped(object sender, EventArgs e)
    {
        string themesstr = Preferences.Get("themes", "white;");
        int bucoins = Preferences.Get("bucoins", 0);
        string theme = Preferences.Get("theme", "white");
        List<string> themes = themesstr.Split(';').ToList();
        themes.RemoveAt(themes.Count - 1);

        if (!themes.Contains("green"))
        {
            if (bucoins >= 1000)
            {
                bucoins -= 1000;
                Preferences.Set("bucoins", bucoins);
                themesstr += "green;";
                Preferences.Set("themes", themesstr);
                Preferences.Set("theme", "green");
            }
        }
        else
        {
            if (theme != "green")
            {
                Preferences.Set("theme", "green");
            }
        }

        check();
    }
}