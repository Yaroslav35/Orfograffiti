namespace Orfograffiti;

public partial class endgame : ContentPage
{
	int state = 0;
	int reward = 0;
	List<string> guessed = new List<string>();
	public endgame(int state_get, List<string> guessed_get, string mode_get, string difficulty_get, int reward_get)
	{
		InitializeComponent();
		state = state_get;
		guessed = guessed_get;
		reward = reward_get;

		diff.Text = " " + difficulty_get;
		if (state == 1) title.Text = "Поражение";
		else if (state == 2) title.Text = "Победа";

		foreach(var word in guessed)
		{
			HorizontalStackLayout cont = new HorizontalStackLayout()
			{
				HorizontalOptions = LayoutOptions.Center,
				Margin = new Thickness(0,10),
				BindingContext = word
			};
			Label wordl = new Label()
			{
				Text = word,
				FontSize = 36,
				FontAttributes = FontAttributes.Bold
			};
			cont.Children.Add(wordl);
			Image img = new Image()
			{
				Source = "searchico.png",
				HeightRequest = 20,
				Margin = new Thickness(10,0,0,0)
			};
			cont.Children.Add(img);
			container.Children.Add(cont);

			TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
			cont.GestureRecognizers.Add(tapGestureRecognizer);
		}

		if (state == 2)
		{
			int exp = Preferences.Get("exp", 0);
			int bucoins = Preferences.Get("bucoins", 0);

            Preferences.Set("exp", exp + reward);
            Preferences.Set("bucoins", bucoins + (reward / 10));
            rewardexp.Text = $"Награда! {reward} опыта!";
            rewardbucoins.Text = $"Награда! {reward / 10} букоинов!";
		}
		else
		{
			rewardexp.IsVisible = false;
			rewardbucoins.IsVisible = false;
		}
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
    private async void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
		string word = (sender as VisualElement).BindingContext.ToString();
		await Launcher.Default.OpenAsync($"https://www.google.com/search?q=define%3A{word.ToLower()}&oq=def&gs_lcrp=EgZjaHJvbWUqBggBEEUYOzINCAAQRRiDARixAxiABDIGCAEQRRg7MgoIAhAAGLEDGIAEMgoIAxAAGLEDGIAEMgoIBBAAGLEDGIAEMgoIBRAuGLEDGIAEMg0IBhAAGIMBGLEDGIAEMgcIBxAAGIAEMgoICBAAGLEDGIAE0gEINzAwMWowajGoAgCwAgA&sourceid=chrome&ie=UTF-8&sei=FOfDaf_zDqPRwPAPnazY0AY");
    }

    private void back_Clicked(object sender, EventArgs e)
    {
		Shell.Current.Navigation.PopToRootAsync();
    }
}