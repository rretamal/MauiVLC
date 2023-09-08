namespace MauiVlc
{
    public partial class App : Application
    {
        public event Action OnSleepEvent;
        public event Action OnResumeEvent;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
            OnSleepEvent?.Invoke();
        }

        protected override void OnResume()
        {
            OnResumeEvent?.Invoke();
        }
    }
}
