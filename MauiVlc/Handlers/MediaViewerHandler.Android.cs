using System;
using LibVLCSharp.Platforms.Android;
using LibVLCSharp.Shared;
using MauiVlc.Controls;
using Microsoft.Maui.Handlers;

namespace MauiVlc.Handlers
{
	public partial class MediaViewerHandler: ViewHandler<MediaViewer, VideoView>
    {
        VideoView _videoView;
        LibVLC _libVLC;
        LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

        protected override VideoView CreatePlatformView() => new VideoView(Context);

        protected override void ConnectHandler(VideoView nativeView)
        {
            base.ConnectHandler(nativeView);

            PrepareControl(nativeView);
            HandleUrl(VirtualView.VideoUrl);

            base.ConnectHandler(nativeView);
        }

        private void VirtualView_PlayRequested()
        {
            ConnectHandler(null);

            HandleUrl(VirtualView.VideoUrl);
        }

        private void VirtualView_PauseRequested()
        {
            _videoView.Dispose();
            _mediaPlayer.Dispose();
            _libVLC.Dispose();
        }

        protected override void DisconnectHandler(VideoView nativeView)
        {
            VirtualView.PauseRequested -= VirtualView_PauseRequested;
            nativeView.Dispose();
            base.DisconnectHandler(nativeView);
        }

        private void PrepareControl(VideoView nativeView)
        {
            _libVLC = new LibVLC(enableDebugLogs: true);
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true
            };

            _videoView = nativeView ?? new VideoView(Context);
            _videoView.MediaPlayer = _mediaPlayer;

            VirtualView.PauseRequested += VirtualView_PauseRequested;
            VirtualView.PlayRequested += VirtualView_PlayRequested;
        }

        private void HandleUrl(string url)
        {
            try
            {

                if (url.EndsWith("/"))
                {
                    url = url.TrimEnd('/');
                }

                //url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

                if (!string.IsNullOrEmpty(url))
                {
                    var media = new Media(_libVLC, url, FromType.FromLocation);

                    _mediaPlayer.NetworkCaching = 1500;

                    if (_mediaPlayer.Media != null)
                    {
                        _mediaPlayer.Stop();
                        _mediaPlayer.Media.Dispose();
                    }

                    _mediaPlayer.Media = media;
                    _mediaPlayer.Mute = true;
                    _mediaPlayer.Play();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}

