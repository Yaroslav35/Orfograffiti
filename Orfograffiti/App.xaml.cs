namespace Orfograffiti
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var _splashService = new SplashService();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}