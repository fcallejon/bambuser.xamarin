using System;
using System.Drawing;
using Iris.Xamarin.Broadcast;
using UIKit;

namespace Iris.Xamarin.Player
{
    public partial class ViewController : UIViewController, IBambuserPlayerDelegate
    {
        BambuserPlayer _player;
        UIButton _rewindButton;
        readonly string _resourceUri;
        UIButton _playButton;
        UIButton _pauseButton;
        UITextView _logView;

        protected ViewController(IntPtr handle) : base(handle)
        {
            _logView = new UITextView(new RectangleF(0, (float)UIScreen.MainScreen.Bounds.Height - 300, (float)UIScreen.MainScreen.Bounds.Width, 300))
            {
                Editable = false
            };
            _player = new BambuserPlayer();
            _player.WeakDelegate = this;
            _player.ApplicationId = "YOUT-API-ID";
            _resourceUri = "https://cdn.bambuser.net/broadcasts/57e8b843-a6b9-451d-9f1f-621c2dcb9bcc?da_id=cbf70495-232a-c827-e5ff-681104245155&da_timestamp=1606780800000&da_nonce=0.1&da_signature_method=HMAC-SHA256&da_signature=3dacb0b640c405a35236b21a4dbb5e6a5647aa1b228b3f91699020803e000756";


            _playButton = new UIButton(UIButtonType.System);
            _playButton.SetTitle("Play", UIControlState.Normal);
            _playButton.AddTarget((sender, e) =>
            {
                _player.PlayVideo(_resourceUri);
            }, UIControlEvent.TouchUpInside);

            _pauseButton = new UIButton(UIButtonType.System);
            _pauseButton.SetTitle("Pause", UIControlState.Normal);
            _pauseButton.AddTarget((sender, e) => { _player.PauseVideo(); }, UIControlEvent.TouchUpInside);

            _rewindButton = new UIButton(UIButtonType.System);
            _rewindButton.SetTitle("Rewind", UIControlState.Normal);
            _rewindButton.AddTarget((sender, e) => { _player.SeekTo(0.0); }, UIControlEvent.TouchUpInside);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.AddSubview(_player);
            View.AddSubview(_rewindButton);
            View.AddSubview(_playButton);
            View.AddSubview(_pauseButton);
            View.AddSubview(_logView);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            var statusBarOffset = TopLayoutGuide.Length;
            _player.Frame = new CoreGraphics.CGRect(0, 0 + statusBarOffset, View.Bounds.Size.Width, View.Bounds.Size.Height - statusBarOffset);

            _playButton.Frame = new CoreGraphics.CGRect(20, 20 + statusBarOffset, 100, 40);
            _pauseButton.Frame = new CoreGraphics.CGRect(20, 80 + statusBarOffset, 100, 40);
            _rewindButton.Frame = new CoreGraphics.CGRect(20, 140 + statusBarOffset, 100, 40);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void LogMessage(string message)
        {
            message = $"{DateTime.Now.ToString("HH:mm:ss")}:: {message}";
            LogExtensions.OutputStringToConsole(message);

            InvokeOnMainThread(() => _logView.Text = $"{message}\r\n{_logView.Text}");
        }

        public void VideoLoadFail()
        {
            LogMessage($"VideoLoadFail '{_player.ResourceUri}'");
        }

        public void PlaybackStarted()
        {
            _playButton.Enabled = false;
            _pauseButton.Enabled = true;
            LogMessage("PlaybackStarted");
        }

        public void PlaybackPaused()
        {
            _playButton.Enabled = true;
            _pauseButton.Enabled = false;
            LogMessage("PlaybackPaused");
        }

        public void PlaybackStopped()
        {
            _playButton.Enabled = true;
            _pauseButton.Enabled = false;
            LogMessage("PlaybackStopped");
        }

        public void PlaybackCompleted()
        {
            LogMessage("PlaybackCompleted");
        }

        public void DurationKnown(double duration)
        {
            LogMessage($"DurationKnown {duration}");
        }

        public void CurrentViewerCountUpdated(int viewers)
        {
            LogMessage($"CurrentViewerCountUpdated {viewers}");
        }

        public void TotalViewerCountUpdated(int viewers)
        {
            LogMessage($"TotalViewerCountUpdated {viewers}");
        }
    }
}
