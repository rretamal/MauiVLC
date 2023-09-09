namespace MauiVlc
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public string VideoUrl { get; set; } = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = this;

            ((App)Application.Current).OnSleepEvent += MainPage_OnSleepEvent;
            ((App)Application.Current).OnResumeEvent += MainPage_OnResumeEvent;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void MainPage_OnResumeEvent()
        {
            videoViewer.Play();
        }

        private void MainPage_OnSleepEvent()
        {
            videoViewer.Pause();
        }
    }

}
