using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orfograffiti
{
    internal class SplashService : IDisposable
    {
        private readonly System.Timers.Timer _timer;
        private bool _isDisposed;

        public SplashService()
        {
            //Preferences.Set("win1_got", false);
            //Preferences.Set("fizhma_got", false);
            //
            //Preferences.Set("fizhma", false);
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private async void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var currentPage = GetCurrentPage();
            int exp = Preferences.Get("exp", 0);
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (Preferences.Get("allow_splash", false))
                {
                    if (exp >= 5000 && !Preferences.Get("woodSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("woodSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("wood"));
                    }
                    else if (exp >= 12000 && !Preferences.Get("ironSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("ironSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("iron"));
                    }
                    else if (exp >= 25000 && !Preferences.Get("copperSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("copperSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("copper"));
                    }
                    else if (exp >= 40000 && !Preferences.Get("silverSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("silverSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("silver"));
                    }
                    else if (exp >= 60000 && !Preferences.Get("goldSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("goldSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("gold"));
                    }
                    else if (exp >= 90000 && !Preferences.Get("emeraldSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("emeraldSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("emerald"));
                    }
                    else if (exp >= 130000 && !Preferences.Get("rubinSplash", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("rubinSplash", true);
                        await currentPage.Navigation.PushModalAsync(new RangSplash("rubin"));
                    }

                    //ачивки
                    if(Preferences.Get("fizhma", false) && !Preferences.Get("fizhma_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("fizhma_got", true);
                        await currentPage.Navigation.PushModalAsync(new web("secret.html", "secret"));
                    }
                    if (Preferences.Get("lastmoment", false) && !Preferences.Get("lastmoment_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("lastmoment_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("Вовремя!", "lastmoment.png", "Выиграй, когда на таймере менее 5 секунд"));
                    }
                    if (Preferences.Get("wins",0) > 0 && !Preferences.Get("win1_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("win1_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("Новичок","win1.png","Выиграй 1 игру"));
                    }
                    if (Preferences.Get("wins", 0) > 9 && !Preferences.Get("win10_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("win10_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("продвинутый", "win10.png", "Выиграй 10 игр"));
                    }
                    if (Preferences.Get("wins", 0) > 99 && !Preferences.Get("win100_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("win100_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("Мастер слов", "win100.png", "Выиграй 100 игр"));
                    }
                    if (Preferences.Get("loses", 0) > 0 && !Preferences.Get("lose1_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("lose1_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("Поражение", "lose1.png", "Проиграй 1 игру"));
                    }
                    if (Preferences.Get("loses", 0) > 9 && !Preferences.Get("lose10_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("lose10_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("Невежда", "lose10.png", "Проиграй 10 игр"));
                    }
                    if (Preferences.Get("loses", 0) > 99 && !Preferences.Get("lose100_got", false))
                    {
                        Preferences.Set("allow_splash", false);
                        Preferences.Set("lose100_got", true);
                        await currentPage.Navigation.PushModalAsync(new AchievementSplash("Нубяра", "lose100.png", "Проиграй 100 игр"));
                    }

                }
            });
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _timer?.Dispose();
                _isDisposed = true;
            }
        }

        private Page GetCurrentPage()
        {
            if (Application.Current?.MainPage == null)
                return null;

            var currentPage = Application.Current.MainPage;

            if (currentPage is NavigationPage navPage)
                return navPage.CurrentPage;

            if (currentPage is Shell shell)
                return shell.CurrentPage;

            if (currentPage is TabbedPage tabbedPage)
                return tabbedPage.CurrentPage;

            if (currentPage is FlyoutPage flyoutPage)
                return flyoutPage.Detail;

            return currentPage;
        }
    }
}