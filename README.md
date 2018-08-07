# A Xamarin Binding for [Iris Bambuser](https://irisplatform.io/docs/)

## How to use

### Projects setup

The first thing is update the `HttpClientHandler` to `CFNetworkHandler` or `NSURLSession`. (for more on this read [here](https://docs.microsoft.com/en-us/xamarin/cross-platform/macios/http-stack)).

Then go to the build configuration page and add the mtouch flags: `--gcc_flags="-DNS_BLOCK_ASSERTIONS=1 -stdlib=libc++"`.

### Adding code to Broadcast

Update Build Identifier with yours.
Update your `ViewController` to conform to `IBambuserViewDelegate`.
Change `Info.plist` to require:
1. `NSPhotoLibraryUsageDescription`
2. `NSLocationWhenInUseUsageDescription`
3. `NSCameraUsageDescription`
4. `NSMicrophoneUsageDescription`

Add the field memebers and consts:
```
BambuserView _bambuserView;
UIButton _startButton;
UIButton _startButton;
const string START_TITLE = "Start broadcasting";
const string STOP_TITLE = "Stop broadcasting";
```

In the ctor:
```
_bambuserView = new BambuserView(BambuserConstants.SessionPresetAuto);
_bambuserView.WeakDelegate = this;
_bambuserView.ApplicationId = "YOUr-APP-ID";
_bambuserView.BroadcastTitle = "Testing from Xamarin!";
_bambuserView.Author = "John Doe";
_bambuserView.SetOrientation(InterfaceOrientation, InterfaceOrientation);
_bambuserView.StartCapture();

_startButton = new UIButton(UIButtonType.RoundedRect);
_startButton.SetTitle(START_TITLE, UIControlState.Normal);
_startButton.AddTarget(StartButton_TouchUpInside, UIControlEvent.TouchUpInside);
```

In `LoadView`
```
View.AddSubview(_bambuserView.View);
View.AddSubview(_startButton);
```

In `ViewWillLayoutSubviews`
```
var statusBarOffset = TopLayoutGuide.Length;
_bambuserView.PreviewFrame = new CGRect(0,
                                        0 + statusBarOffset,
                                        View.Bounds.Size.Width,
                                        View.Bounds.Size.Height - statusBarOffset);
_startButton.Frame = new CGRect(0, 50 + statusBarOffset, 100, 50);
```

And the `_startButton` touch handler
```
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
}
```

In `ViewDidUnload`
```
_bambuserView.StopBroadcasting();
```

### Adding code to Reproduce

Update Build Identifier with yours.
Update your `ViewController` to conform to `IBambuserPlayerDelegate`.

Add the field memebers and consts:
```
readonly string _resourceUri;
BambuserPlayer _player;
UIButton _playButton;
```

In the ctor:
```
_player = new BambuserPlayer();
_player.WeakDelegate = this;
_player.ApplicationId = "YOUR-APP-ID";
_resourceUri = "https://cdn.bambuser.net/broadcasts/57e8b843-a6b9-451d-9f1f-621c2dcb9bcc?da_id=cbf70495-232a-c827-e5ff-681104245155&da_timestamp=1606780800000&da_nonce=0.1&da_signature_method=HMAC-SHA256&da_signature=3dacb0b640c405a35236b21a4dbb5e6a5647aa1b228b3f91699020803e000756";

_playButton = new UIButton(UIButtonType.System);
_playButton.SetTitle("Play", UIControlState.Normal);
_playButton.AddTarget((sender, e) =>
    {
        _player.PlayVideo(_resourceUri);
    }, UIControlEvent.TouchUpInside);
```

In `LoadView`
```
View.AddSubview(_player);
View.AddSubview(_playButton);
```

In `ViewWillLayoutSubviews`
```
var statusBarOffset = TopLayoutGuide.Length;
_player.Frame = new CoreGraphics.CGRect(0, 0 + statusBarOffset, View.Bounds.Size.Width, View.Bounds.Size.Height - statusBarOffset);

_playButton.Frame = new CoreGraphics.CGRect(20, 20 + statusBarOffset, 100, 40);
```

Add some handlers to block `PlayButton`
```
public void PlaybackStarted()
{
    _playButton.Enabled = false;
}

public void PlaybackPaused()
{
    _playButton.Enabled = true;
}

public void PlaybackStopped()
{
    _playButton.Enabled = true;
}
```

Complete the other required methods from `IBambuserPlayerDelegate`.

## Sample Broadcast App

It's just a silly sample app to get you started.
Be sure to update the ids as required.

## Sample Player App

It's just a silly sample app to get you started.
Be sure to update the ids as required.