using MauiVlc.Controls;
using Microsoft.Maui.Handlers;
using System;
using LibVLCSharp.Platforms.iOS;
using LibVLCSharp.Shared;

namespace MauiVlc.Handlers
{
    public partial class MediaViewerHandler : ViewHandler<MediaViewer, VideoView>
    {
        VideoView _videoView;
        LibVLC _libVLC;
        LibVLCSharp.Shared.MediaPlayer _mediaPlayer;

        protected override VideoView CreatePlatformView()
        {
            return new VideoView();
        }

        protected override void ConnectHandler(VideoView nativeView)
        {
            base.ConnectHandler(nativeView);

            Core.Initialize();

            _libVLC = new LibVLC(enableDebugLogs: true);
            _mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true
            };

            _videoView = nativeView ?? new VideoView();
            _videoView.MediaPlayer = _mediaPlayer;

            VirtualView.PauseRequested += VirtualView_PauseRequested;
            VirtualView.PlayRequested += VirtualView_PlayRequested;

            HandleUrl(VirtualView.VideoUrl);
        }

        protected override void DisconnectHandler(VideoView nativeView)
        {
            VirtualView.PauseRequested -= VirtualView_PauseRequested;
            VirtualView.PlayRequested -= VirtualView_PlayRequested;

            nativeView.Dispose();
            base.DisconnectHandler(nativeView);
        }

        private void VirtualView_PlayRequested()
        {
            _videoView.MediaPlayer.Play();
        }

        private void VirtualView_PauseRequested()
        {
            _videoView.MediaPlayer.Pause();
        }

        public void HandleUrl(string url)
        {
            try
            {
                if (url.EndsWith("/"))
                {
                    url = url.TrimEnd('/');
                }

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

                    _videoView.MediaPlayer.Play();
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }
}

