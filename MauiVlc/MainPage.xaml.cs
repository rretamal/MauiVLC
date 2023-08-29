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
        }
    }

}
