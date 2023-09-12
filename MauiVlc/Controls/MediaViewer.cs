using System;

namespace MauiVlc.Controls
{
	public class MediaViewer : View
	{
        public event Action PauseRequested;
        public event Action PlayRequested;

        public static BindableProperty VideoUrlProperty = BindableProperty.Create(nameof(VideoUrl)
           , typeof(string)
           , typeof(MediaViewer)
           , ""
           , defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Disables or enables scanning
        /// </summary>
        public string VideoUrl
        {
            get => (string)GetValue(VideoUrlProperty);
            set => SetValue(VideoUrlProperty, value);
        }

        public MediaViewer()
		{
		}

        public void Pause()
        {
            PauseRequested?.Invoke();
        }

        public void Play()
        {
            PlayRequested?.Invoke();
        }
    }
}

