namespace Orfograffiti;

public partial class ranged : ContentPage
{
	int exp = 0;
    List<Label> rangs;
    List<Label> exps;
	public ranged()
	{
		InitializeComponent();
        rangs = new List<Label>() {title, rang1, rang2, rang3, rang4, rang5, rang6, rang7, rang8, rang9};
        exps = new List<Label>() {explbl, rang1exp, rang2exp, rang3exp, rang4exp, rang5exp, rang6exp, rang7exp, rang8exp, rang9exp};
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        string theme = Preferences.Get("theme", "white");
        string fonSource = ThemeApp.ThemeDict[theme][0];
        string lightColor = ThemeApp.ThemeDict[theme][1];
        string darkColor = ThemeApp.ThemeDict[theme][2];

        fon.Source = fonSource;
        mainrang.Background = Color.FromArgb(lightColor);
        brang1.Background = Color.FromArgb(lightColor);
        brang2.Background = Color.FromArgb(lightColor);
        brang3.Background = Color.FromArgb(lightColor);
        brang4.Background = Color.FromArgb(lightColor);
        brang5.Background = Color.FromArgb(lightColor);
        brang6.Background = Color.FromArgb(lightColor);
        brang7.Background = Color.FromArgb(lightColor);
        brang8.Background = Color.FromArgb(lightColor);

        exp = Preferences.Get("exp", 0);

        if (exp < 5000)
        {
            ico.Source = "woodtrophy.png";
            title.Text = "Деревянный ранг";
            explbl.Text = $"Опыт: {exp}/5000";
        }
        else if (exp >= 5000 && exp < 12000)
        {
            ico.Source = "irontrophy.png";
            title.Text = "Железный ранг";
            explbl.Text = $"Опыт: {exp}/12000";
        }
        else if (exp >= 12000 && exp < 25000)
        {
            ico.Source = "coppertrophy.png";
            title.Text = "Бронзовый ранг";
            explbl.Text = $"Опыт: {exp}/25000";
        }
        else if (exp >= 25000 && exp < 40000)
        {
            ico.Source = "silvertrophy.png";
            title.Text = "Серебряный ранг";
            explbl.Text = $"Опыт: {exp}/40000";
        }
        else if (exp >= 40000 && exp < 60000)
        {
            ico.Source = "goldtrophy.png";
            title.Text = "Золотой ранг";
            explbl.Text = $"Опыт: {exp}/60000";
        }
        else if (exp >= 60000 && exp < 90000)
        {
            ico.Source = "emeraldtrophy.png";
            title.Text = "Изумрудный ранг";
            explbl.Text = $"Опыт: {exp}/90000";
        }
        else if (exp >= 90000 && exp < 130000)
        {
            ico.Source = "rubintrophy.png";
            title.Text = "Рубиновый ранг";
            explbl.Text = $"Опыт: {exp}/130000";
        }
        else if (exp >= 130000 && exp < 200000)
        {
            ico.Source = "neontrophy.png";
            title.Text = "Неоновый ранг";
            explbl.Text = $"Опыт: {exp}/200000";
        }
        else
        {
            prestigebox.IsVisible = false;
            int prestige = (int)((exp - 200000) / 100000);
            title.Text = $"Престиж {prestige}";
            ico.Source = "prestigetrophy.png";
            explbl.Text = $"Опыт: {exp}/{200000 + (prestige + 1) * 100000}";
        }
    }
    private void ScrollView_SizeChanged(object sender, EventArgs e)
    {
        if((sender as ScrollView).Width < 500)
        {
            foreach (var el in rangs) el.FontSize = 18;
            foreach (var el in exps) el.FontSize = 18;
        }
        else
        {
            foreach (var el in rangs) el.FontSize = 28;
            foreach (var el in exps) el.FontSize = 20;
        }
    }
}