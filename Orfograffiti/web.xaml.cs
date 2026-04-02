namespace Orfograffiti;

public partial class web : ContentPage
{
	string mode = "";
	public web(string source_get, string mode_get)
	{
		InitializeComponent();
		mode = mode_get;
		webview.Source = source_get;

		if (mode == "secret") Secret();
	}

	private async void Secret()
	{
		await Task.Delay(40000);
        Preferences.Set("allow_splash", true);
        await Navigation.PopModalAsync();
		
	}
}