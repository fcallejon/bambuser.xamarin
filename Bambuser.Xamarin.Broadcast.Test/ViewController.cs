using System;
using System.Drawing;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Bambuser.Xamarin.Broadcast.Test
{
    public partial class ViewController : UIViewController, IBambuserViewDelegate
    {
        BambuserView _bambuserView;
        NSTimer _startBroadcastTimer;
        UIButton _startButton;
        UITextView _logView;
        HttpClient _httpClient;
        UIButton _settingsButton;

        const string START_TITLE = "Start broadcasting";
        const string STOP_TITLE = "Stop broadcasting";

        protected ViewController(IntPtr handle) : base(handle)
        {
            _bambuserView = new BambuserView(BambuserConstants.SessionPresetAuto);
            _bambuserView.WeakDelegate = this;
            _bambuserView.ApplicationId = "YOUr-APP-ID";
            _bambuserView.BroadcastTitle = "Testing from Xamarin!";
            _bambuserView.Author = "GOD";
            _bambuserView.Talkback = true;
            _bambuserView.SetOrientation(InterfaceOrientation, InterfaceOrientation);
            _bambuserView.EnableArchiveOption();
            _bambuserView.EnablePositionOption();
            _bambuserView.EnableTalkbackOption();
            _bambuserView.EnablePrivateModeOption();
            _bambuserView.EnableSaveLocallyOption();
            _bambuserView.StartCapture();

            _startButton = new UIButton(UIButtonType.RoundedRect);
            _startButton.SetTitle(START_TITLE, UIControlState.Normal);
            _startButton.AddTarget(StartButton_TouchUpInside, UIControlEvent.TouchUpInside);

            _settingsButton = new UIButton(UIButtonType.RoundedRect);
            _settingsButton.SetTitle("Seetings", UIControlState.Normal);
            _settingsButton.AddTarget(SettingsButton_TouchUpInside, UIControlEvent.TouchUpInside);

            _logView = new UITextView(new RectangleF(0, (float)UIScreen.MainScreen.Bounds.Height - 300, (float)UIScreen.MainScreen.Bounds.Width, 300))
            {
                Editable = false
            };
            _httpClient = new HttpClient();
        }

        public override void LoadView()
        {
            base.LoadView();

            View.AddSubview(_bambuserView.View);
            View.AddSubview(_startButton);
            View.AddSubview(_settingsButton);
            View.AddSubview(_logView);
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            var statusBarOffset = TopLayoutGuide.Length;
            _bambuserView.PreviewFrame = new CGRect(0,
                                                    0 + statusBarOffset,
                                                    View.Bounds.Size.Width,
                                                    View.Bounds.Size.Height - statusBarOffset);
            _startButton.Frame = new CGRect(0, 50 + statusBarOffset, 100, 50);
            _settingsButton.Frame = new CGRect(View.Bounds.Size.Width - 100, 50 + statusBarOffset, 100, 50);

            if (_bambuserView.ChatView != null)
            {
                View.AddSubview(_bambuserView.ChatView);
            }

            if (_bambuserView.SettingsView.IsViewLoaded)
            {
                _bambuserView.SettingsView.View.Frame = new CGRect(0, 0 + statusBarOffset, View.Bounds.Size.Width, View.Bounds.Size.Height - statusBarOffset);
            }
        }

        void StartButton_TouchUpInside(object sender, EventArgs e)
        {
            if (_startButton.Title(UIControlState.Normal) == START_TITLE)
            {
                _bambuserView.StartBroadcasting();
                _startButton.SetTitle(STOP_TITLE, UIControlState.Normal);
            }
            else
            {
                _bambuserView.StopBroadcasting();
                _startButton.SetTitle(START_TITLE, UIControlState.Normal);
            }

            Task.Run(async () =>
            {
                var response = await _httpClient.GetAsync("https://ingest.bambuser.io/uploadtest");

                var data = await response.Content.ReadAsStringAsync();
                LogMessage(data.Replace("\n", string.Empty));
            });
        }

        void SettingsButton_TouchUpInside(object sender, EventArgs e)
        {
            View.AddSubview(_bambuserView.SettingsView.View);
        }

        private void LogMessage(string message)
        {
            message = $"{DateTime.Now.ToString("HH:mm:ss")}:: {message}";
            LogExtensions.OutputStringToConsole(message);

            InvokeOnMainThread(() => _logView.Text = $"{message}\r\n{_logView.Text}");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewDidUnload()
        {
            _bambuserView.StopBroadcasting();
            base.ViewDidUnload();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void BroadcastStarted()
        {
            LogMessage("BroadcastStarted");
        }

        public void BroadcastStopped()
        {
            LogMessage("BroadcastStopped");
        }

        public void BambuserError(BambuserError errorCode, string errorMessage)
        {
            LogMessage($"BambuserError {errorCode} {errorMessage}");
        }

        public void UplinkTestComplete(float speedRecomendation, bool shouldBroadcast)
        {
            LogMessage($"UplinkTestComplete {speedRecomendation} {shouldBroadcast}");
        }

        public void HideSettingsView()
        {
            _bambuserView.SettingsView.View.RemoveFromSuperview();
            LogMessage("HideSettingsView");
        }

        public void ChatMessageReceived(string message)
        {
            LogMessage($"ChatMessageReceived: {message}");
        }

        public void RecordingComplete(string filename)
        {
            LogMessage($"RecordingComplete: {filename}");
        }

        public void HealthUpdated(int health)
        {
            LogMessage($"HealthUpdated {health}");
        }

        public void CurrentViewerCountUpdated(int viewers)
        {
            LogMessage($"CurrentViewerCountUpdated: {viewers}");
        }

        public void TotalViewerCountUpdated(int viewers)
        {
            LogMessage($"TotalViewerCountUpdated: {viewers}");
        }

        public void TalkbackRequest(string request, string caller, int talkbackID)
        {
            LogMessage($"TalkbackRequest: {request} - {caller} - {talkbackID}");
            _bambuserView.AcceptTalkbackRequest(talkbackID);
        }

        public void TalkbackStateChanged(TalkbackState state)
        {
            LogMessage($"TalkbackStateChanged: {state}");
        }

        public void BroadcastIdReceived(string broadcastId)
        {
            LogMessage($"BroadcastIdReceived - {broadcastId}");
        }

        public void SnapshotTaken(UIImage image)
        {
            LogMessage($"SnapshotTaken: {image.Size}");
        }
    }
}

