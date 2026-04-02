namespace Orfograffiti
{
    public partial class MainPage : ContentPage
    {
        int size = 0, fontsize = 0;
        Random rand = new Random();
        List<Border> borderList = new List<Border>();
        List<Label> labelList = new List<Label>();
        public MainPage()
        {
            InitializeComponent();
            //Preferences.Set("bucoins", 1000);
            Preferences.Set("allow_splash", true);

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
            size = Math.Min((screenWidth / 12) - 10, 30);

#if WINDOWS
            fontsize = (int)(((int)Math.Floor(size / DeviceDisplay.Current.MainDisplayInfo.Density)) / 1.7);
#elif ANDROID || IOS
            fontsize = (int)(((int)Math.Floor(size / DeviceDisplay.Current.MainDisplayInfo.Density)) / 1.0);
#endif
            foreach (char let in "ОрфоГраффити")
            {
                TapGestureRecognizer tap = new TapGestureRecognizer();
                Label chr = new Label()
                {
                    Text = let.ToString(),
                    FontSize = fontsize,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    TextColor = Colors.Black
                };
                Border br = new Border()
                {
                    WidthRequest = size,
                    HeightRequest = size,
                    Stroke = Colors.Black,
                    Margin = new Thickness(5),
                    Rotation = rand.Next(-5, 6),
                    Content = chr
                };
                br.GestureRecognizers.Add(tap);
                borderList.Add(br);
                labelList.Add(chr);
                titleContainer.Children.Add(br);

                tap.Tapped += Tap_Tapped;
            }
        }

        private void Tap_Tapped(object? sender, TappedEventArgs e)
        {
            Border border = sender as Border;
            //int newAngle = rand.Next(-5,6);
            int newAngle = 0;
            if(border.Rotation >= 0) newAngle = -5;
            else newAngle = 5;
            Anim(border, (int)border.Rotation - newAngle);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            string theme = Preferences.Get("theme", "white");
            string fonSource = ThemeApp.ThemeDict[theme][0];
            string lightColor = ThemeApp.ThemeDict[theme][1];
            string darkColor = ThemeApp.ThemeDict[theme][2];

            fon.Source = fonSource;
            mode1.BackgroundColor = Color.FromArgb(lightColor);
            mode2.BackgroundColor = Color.FromArgb(lightColor);
            mode3.BackgroundColor = Color.FromArgb(lightColor);

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
            size = Math.Min((screenWidth / 12) - 10, 30);

#if WINDOWS
            fontsize = (int)(((int)Math.Floor(size / DeviceDisplay.Current.MainDisplayInfo.Density)) / 1.7);
#elif ANDROID || IOS
            fontsize = (int)(((int)Math.Floor(size / DeviceDisplay.Current.MainDisplayInfo.Density)) / 1.0);
#endif

            foreach (Border br in borderList)
            {
                br.Background = Color.FromArgb(lightColor);
                br.WidthRequest = size;
                br.HeightRequest = size;
            }
            foreach (Label label in labelList) label.FontSize = fontsize;
        }

        private async void Anim(Border br, int angle)
        {
            if(angle < 0)
            {
                for(int i = 0; i < Math.Abs(angle); i++)
                {
                    br.Rotation--;
                    await Task.Delay(100);
                }
            }
            else
            {
                for (int i = 0; i < Math.Abs(angle); i++)
                {
                    br.Rotation++;
                    await Task.Delay(100);
                }
            }
        }

        private void classiceasy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("easy",5, 12, 200, ""));
        }

        private void classicnormal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("normal",5, 9, 500, ""));
        }

        private void classichard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("hard",5, 7, 1000, ""));
        }

        private void hypereasy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("easy", 10, 22, 400, "hyper"));
        }

        private void hypernormal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("normal", 10, 17, 1000, "hyper"));
        }

        private void hyperhard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("hard", 10, 14, 2000, "hyper"));
        }

        private void clockeasy_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("easy", 5, 12, 400, "timer"));
        }

        private void clocknormal_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("normal", 5, 9, 1000, "timer"));
        }

        private void clockhard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new classicgame("hard", 5, 7, 2000, "timer"));
        }
    }
}
