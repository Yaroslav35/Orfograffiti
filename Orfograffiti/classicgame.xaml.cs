using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Orfograffiti;

public partial class classicgame : ContentPage
{
	Random rand = new Random();
	string difficulty = "";

    string lightColor = "", darkColor = "";

    int size = 80;
    int fontsize = 0;
    int focused = 0;
    int wordscount = 5;
    int loses = 0, wins = 0;
    int totaltime = 0;

	Dictionary<string, string> titles = new Dictionary<string, string>() {{"easy","Лёгкий режим" }, {"normal","Нормальный режим"}, {"hard","Сложный режим"}};
	Dictionary<string, Color> colors = new Dictionary<string, Color>() {{"easy",Colors.Black}, {"normal",Colors.Black}, {"hard",Colors.Black}};
	Dictionary<string, int> max_len = new Dictionary<string, int>() { {"easy",5}, {"normal",7}, {"hard",8}};
    Dictionary<string, int> open_lets = new Dictionary<string, int>() { {"easy", 4},{"normal", 3}, {"hard", 2} };

    List<char> rus_lets = new List<char>()
	{
		'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й',
		'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф',
		'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
	};

    List<string> words = new List<string>();
    List<string> easywords = new List<string>();
	List<string> can_used = new List<string>();

	//Параметры игры
	List<char> opened = new List<char>();
	List<string> guessed = new List<string>();
	List<char> letters = new List<char>();
    List<char> used_lets = new List<char>();

    List<List<Entry>> wordentryes = new List<List<Entry>>();
    List<HorizontalStackLayout> hors = new List<HorizontalStackLayout>();
	//List<Entry> wordentry1 = new List<Entry>();
    //List<Entry> wordentry2 = new List<Entry>();
    //List<Entry> wordentry3 = new List<Entry>();
    //List<Entry> wordentry4 = new List<Entry>();
    //List<Entry> wordentry5 = new List<Entry>();
    List<Entry> allentrys = new List<Entry>();
    int reward = 0;
    string modifier = "";

    int tries = 0;
	public classicgame(string difficulty_get, int wordscount_get, int atts_get, int reward_get, string modifier_get)
	{
		InitializeComponent();

        difficulty = difficulty_get;
        modifier = modifier_get;
        reward = reward_get;
        wordscount = wordscount_get;
		title.Text = titles[difficulty];
		title.TextColor = colors[difficulty];
        tries = atts_get;
        remaining.Text = $"Осталось попыток: {tries}";

        wins = Preferences.Get("wins",0);
        loses = Preferences.Get("loses", 0);

        string theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        lightColor = ThemeApp.ThemeDict[theme][1];
        darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
        check.BackgroundColor = Color.FromArgb(darkColor);

        using var stream = FileSystem.OpenAppPackageFileAsync("words.txt").Result;
        using var reader = new StreamReader(stream);

        var lines = new System.Collections.Generic.List<string>();
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            words.Add(line);
        }

        using var stream1 = FileSystem.OpenAppPackageFileAsync($"{difficulty}words.txt").Result;
        using var reader1 = new StreamReader(stream1);

        var lines1 = new System.Collections.Generic.List<string>();
        string line1;

        while ((line = reader1.ReadLine()) != null)
        {
            easywords.Add(line);
        }

        foreach (var word in easywords) if (word.Length <= max_len[difficulty]) can_used.Add(word);

		while(guessed.Count < wordscount)
		{
			int ind = rand.Next(0, can_used.Count);
			if (!guessed.Contains(can_used[ind])) guessed.Add(can_used[ind]);
		}

        foreach(string word in guessed)
        {
            foreach(char ch in word)
            {
                if(!used_lets.Contains(ch)) used_lets.Add(ch);
            }
        }
        while (letters.Count < open_lets[difficulty])
        {
            int ind = rand.Next(0, rus_lets.Count);
            if (!letters.Contains(rus_lets[ind]))
            {
                if(used_lets.Contains(rus_lets[ind])) letters.Add(rus_lets[ind]);
            }
        }
        string selected = Preferences.Get("selected_font", "classic");
        foreach (char ch in rus_lets)
        {
            Label cha = new Label()
            {
                Text = ch.ToString(),
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                Margin = new Thickness(5),
                TextColor = Colors.Black,
                FontFamily = selected
            };
            Border bor = new Border()
            {
                Stroke = Colors.Black,
                Content = cha,
                Margin = new Thickness(2)
            };

            if (letters.Contains(ch)) bor.BackgroundColor = Colors.LightGreen;
            else bor.BackgroundColor = Color.FromArgb("dddddd");

            lettersbox.Children.Add(bor);
        }

        int screenWidth = 0;
#if WINDOWS
        var window = Application.Current.Windows[0];
        if (window.Handler != null)
        {
            var mauiWindow = window.Handler.PlatformView as Microsoft.UI.Xaml.Window;
            if (mauiWindow != null)
            {
                screenWidth = (int)mauiWindow.Bounds.Width;
            }
        }
#else
        screenWidth = (int)DeviceDisplay.Current.MainDisplayInfo.Width;
#endif
        size = Math.Min((screenWidth / max_len[difficulty]) - 150, 80);
#if WINDOWS
        fontsize = (int)(((int)Math.Floor(size / DeviceDisplay.Current.MainDisplayInfo.Density)) / 1.7);
#elif ANDROID || IOS
        fontsize = (int)(((int)Math.Floor(size / DeviceDisplay.Current.MainDisplayInfo.Density)) / 1.0);
#endif

        title.FontFamily = selected;
        remaining.FontFamily = selected;
        check.FontFamily = selected;
        container.Children.Remove(check);
        for (int i = 0; i < wordscount; i++)
        {
            HorizontalStackLayout wordbox = new HorizontalStackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0,10)
            };
            container.Children.Add(wordbox);
            hors.Add(wordbox);
            wordentryes.Add(new List<Entry>());
        }
        container.Children.Add(check);

        for (int i = 0; i < wordscount; i++)
		{
			foreach(char let in guessed[i])
			{
				Entry ent = new Entry()
				{
					FontSize = fontsize,
                    FontFamily = selected,
					Keyboard = Keyboard.Plain,
					WidthRequest = size,
					HeightRequest = size,
					FontAttributes = FontAttributes.Bold,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center,
					BindingContext = let.ToString(),
					IsReadOnly = false,
					Margin = new Thickness(5,0),
					BackgroundColor = Color.FromArgb(lightColor)
				};
                allentrys.Add(ent);
                ent.TextChanged += Ent_TextChanged;
                ent.Focused += Ent_Focused;

				if(letters.Contains(let))
				{
					ent.Text = let.ToString();
					ent.IsReadOnly = true;
					ent.BackgroundColor = Color.FromArgb(darkColor);
				}

                hors[i].Children.Add(ent);
                wordentryes[i].Add(ent);
			}
		}
        Console.WriteLine();

        if (modifier == "timer")
        {
            timecontainer.IsVisible = true;
            if (difficulty == "easy") ControlTimer(480);
            else if (difficulty == "normal") ControlTimer(420);
            else if (difficulty == "hard") ControlTimer(390);
        }
    }
	private bool CanAllow(List<Entry> entries)
	{
		string word = "";
		foreach (Entry ent in entries)
		{
			if(!string.IsNullOrEmpty(ent.Text)) word += ent.Text;
		}

        if (word.Length == entries.Count)
        {
            if (word == "фижма") Preferences.Set("fizhma", true);

            if (words.Contains(word)) return true;
            else
            {
                Toast.Make("Данного слова не существует!");
                return false;
            }
        }
        else
        {
            Toast.Make("Неполное слово!");
            return false;
        }
	}
    private void Ent_Focused(object? sender, FocusEventArgs e)
    {
        var entry = (Entry)sender;
        entry.CursorPosition = 0;
        entry.SelectionLength = entry.Text?.Length ?? 0;

        for(int i = 0; i < allentrys.Count; i++)
        {
            if (entry == allentrys[i]) focused = i;
        }
    }

    private void Ent_TextChanged(object? sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
		string last = "";
		try
		{
			last = entry.Text[^1].ToString();
		}
		catch { }
        if (entry.Text.Length > 1) entry.Text = last;

        int i = 1;
        while (allentrys[(focused + i) % allentrys.Count].IsReadOnly) i++;
        allentrys[(focused + i)% allentrys.Count].Focus();
    }

    private void check_Clicked(object sender, EventArgs e)
    {
		List<string> append_lets = new List<string>();
        int[] triesbools = new int[wordscount];
        for(int i = 0; i < wordscount;i++) triesbools[i] = 0;

        for(int i = 0; i < wordscount; i++)
        {
            if (CanAllow(wordentryes[i]))
            {
                foreach (Entry entry in wordentryes[i])
                {
                    if (!entry.IsReadOnly && !string.IsNullOrEmpty(entry.Text))
                    {
                        triesbools[i] = 1;
                        if (!append_lets.Contains(entry.Text))
                        {
                            append_lets.Add(entry.Text);
                        }
                    }
                }
            }
        }

        if (append_lets.Count > 0)
		{
            foreach (string el in append_lets) if (!letters.Contains(el.ToCharArray()[0])) letters.Add(el.ToCharArray()[0]);
        }
		tries -= triesbools.Count(x => x == 1);

        for(int i = 0; i < wordscount; i++)
        {
            foreach (Entry entry in wordentryes[i])
            {
                if (letters.Contains(entry.BindingContext.ToString().ToCharArray()[0]))
                {
                    entry.Text = entry.BindingContext.ToString();
                    entry.IsReadOnly = true;
                    entry.BackgroundColor = Color.FromArgb(darkColor);
                }
                else entry.Text = "";
            }
        }
        remaining.Text = $"Осталось попыток: {tries}";

        foreach(string ch in append_lets)
        {
            foreach (Border el in lettersbox.Children)
            {
                var lbl = el.Content as Label;
                if(lbl.Text.ToLower() == ch.ToLower())
                {
                    if (used_lets.Contains(lbl.Text[0])) el.BackgroundColor = Colors.LightGreen;
                    else el.BackgroundColor = Color.FromArgb("ff7373");
                }
            }
        }

        //Проверка состояния игры
        //0 - игра продолжается
        //1 - поражение
        //2 - победа
        if (CheckGameOver() == 1)
        {
            loses++;
            Preferences.Set("loses",loses);
            check.IsEnabled = false;

            if (modifier == "timer") if (totaltime <= 5) Preferences.Set("lastmoment", true);

            if(modifier == "timer") Navigation.PushModalAsync(new endgame(1, guessed, "Гонка", titles[difficulty], 0));
            else if(modifier == "hyper") Navigation.PushModalAsync(new endgame(1, guessed, "ГиперИгра", titles[difficulty], 0));
            else Navigation.PushModalAsync(new endgame(1, guessed, "Классика", titles[difficulty], 0));
        }
        else if (CheckGameOver() == 2)
        {
            wins++;
            Preferences.Set("wins",wins);
            check.IsEnabled = false;
            if(modifier == "timer") Navigation.PushModalAsync(new endgame(2, guessed, "Гонка", titles[difficulty], reward));
            else if(modifier == "hyper") Navigation.PushModalAsync(new endgame(2, guessed, "ГиперИгра", titles[difficulty], reward));
            else Navigation.PushModalAsync(new endgame(2, guessed, "Классика", titles[difficulty], reward));
        }
    }
    private int CheckGameOver()
    {
        int alllets = 0;
        for(int i = 0; i < wordscount; i++)
        {
            alllets += wordentryes[i].Count;
        }
        int lets = 0;
        for(int i = 0; i < wordscount; i++)
        {
            foreach(Entry entry in wordentryes[i]) if (entry.IsReadOnly) lets++;
        }

        if (lets == alllets) return 2;
        else if (tries <= 0) return 1;
        else return 0;
    }
    private async void ControlTimer(int total_time)
    {
        while(total_time >= 0)
        {
            totaltime = total_time;
            if (total_time == 120)
            {
#if ANDROID || IOS
                Vibration.Vibrate();
#endif
                Toast.Make("Осталась 2 минуты!", ToastDuration.Short);
            }
            else if (total_time == 30)
            {
#if ANDROID || IOS
                if(Preferences.Get("vibro", false)) Vibration.Vibrate();
#endif
                Toast.Make("Осталась 30 секунд!", ToastDuration.Short);
            }

            if (total_time % 60 < 10) timer.Text = $"{total_time / 60}:0{total_time % 60}";
            else timer.Text = $"{total_time / 60}:{total_time % 60}";
            await Task.Delay(1000);
            total_time -= 1;
        }
        check.IsEnabled = false;
        loses++;
        Preferences.Set("loses", loses);
        await Navigation.PushModalAsync(new endgame(1, guessed, "Гонка", titles[difficulty], 0));
    }
}